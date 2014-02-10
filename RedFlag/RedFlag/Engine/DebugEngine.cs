using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Samples.Debugging.MdbgEngine;
using System.Runtime.InteropServices;
using RedFlag.Engine;
using System.Threading;

namespace RedFlag.Engine
{
    public enum EngineMode
    {
        None=0,
        Attach,
        Launch
    }
    public enum EngineStatus
    {
        Idle = 0,
        Attaching,
        Debugging,
        Detaching,
        Dumping,
        DumpCreated,
        HeapDumped,
        AssemblyLoaded,
        Aborting
    }
    public delegate void NewExceptionHandler(object o, EventArgs e);
    public delegate void ProcessStatusHandler(object o, ProcessEventArgs e);
    public delegate void DebugMessageHandler(object o, MessageEventArgs e);
    class DebugEngine
    {
        public RedFlag.ExceptionInfo.HeapStatistics HeapStats = new RedFlag.ExceptionInfo.HeapStatistics();
        /// <summary>
        /// If we are launching unmanaged, what version of net do we want?
        /// </summary>
        public string DefaultNetVersion = System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion;
        /// <summary>
        /// This debugger is running in the WOW
        /// </summary>
        private bool ThisProcessIs32Bit = false;
        /// <summary>
        /// The process we are debugging is running in the WOW
        /// </summary>
        private bool DebuggedProcessIs32Bit=false;
        /// <summary>
        /// The maximum number of methods of the stack trace (saves processing time!)
        /// </summary>
        public int MaxStackDepth = 6; 
        /// <summary>
        /// The amount of recursion into dependent objects
        /// </summary>
        public int MaxObjectDepth = 4;
        /// <summary>
        /// Fires when a new exception is detected
        /// </summary>
        public event NewExceptionHandler NewException;
        /// <summary>
        /// Fires when a trace message is produced
        /// </summary>
        public event DebugMessageHandler NewMessage;
        /// <summary>
        /// Fires when the status of the debugger changes
        /// </summary>
        public event ProcessStatusHandler ProcessStatus;
        /// <summary>
        /// The arguments to the program to be launched
        /// </summary>
        public string ProcessArgs = String.Empty;
        /// <summary>
        /// The program to be launched for debugging
        /// </summary>
        public string ProcessName = String.Empty;
        /// <summary>
        /// Get local array values (affects performance badly!)
        /// </summary>
        public bool GetArrays = false;
        public AppDomains AppDomains = new AppDomains();
        /// <summary>
        /// The ID of the process to attach to (overrides launch)
        /// </summary> 
        public int ProcessId=-1;
        /// <summary>
        /// The last stack dump requested by the user
        /// </summary>
        public string StackDump = String.Empty;
        /// <summary>
        /// The list of modules loaded into this process;
        /// </summary>
        public List<Module> Modules = new List<Module>();
        /// <summary>
        /// A list of exception types that we won't collect any information about
        /// </summary>
        public List<string> ExceptionsToIgnore
        {
            set
            {
                m_ExceptionsToIgnore = value;
            }
        }
        /// <summary>
        /// A sync event handle to use (currently only for releasing the debugger from pause)
        /// </summary>
        private AutoResetEvent m_DebuggerPause = new AutoResetEvent(false);
        private string m_BreakSourceFile=String.Empty;
        private int m_BreakSourceLine=0;
        private ManualResetEvent m_SignalDebugger = new ManualResetEvent(false);
        /// <summary>
        /// Set a breakpoint format=SourceFileName.cs(line number)
        /// If debugging, interrupt the debugger's thread for the changes to take effect.
        /// </summary>
        public string SetBreakpoint 
        {
            get
            {
                return String.Format("{0}({1})",m_BreakSourceFile,m_BreakSourceLine);
            }
            [SmartAssembly.ReportUsage.ReportUsage("Set breakpoint")]
            set
            {
                try
                {
                    m_BreakSourceFile = value.Substring(0, value.IndexOf('('));
                    m_BreakSourceLine = Convert.ToInt32(value.Substring(value.IndexOf('(') + 1, value.Length - value.IndexOf('(') - 2));
                    if (m_Debugger.Processes.Count > 0) m_InterruptFlags = m_InterruptFlags | ThreadInterruptFlags.NewBreakpoint;
                }
                catch { m_BreakSourceFile = String.Empty; m_BreakSourceLine = 0; }
            }
        }
        private MDbgEngine m_Debugger = new MDbgEngine();
        private bool m_cancel = false; // flag to stop the debugger on thread interrupt
        private DateTime m_StartTime = DateTime.Now;
        private EngineStatus m_Status = EngineStatus.Idle;
        private List<string> m_ExceptionsToIgnore;
        private enum ThreadInterruptFlags
        {
            None = 0,
            StackDump,
            HeapStats,
            NewBreakpoint,
            StopDebugger,
            PauseDebugger
        }
        private ThreadInterruptFlags m_InterruptFlags = ThreadInterruptFlags.None;
        public void DumpStacks(System.Threading.Thread worker)
        {
            // I'm not proud of this, but interrput the debugger thread
            // and set m_StackDump to true, then the debugger thread will dump the stacks and resume
            m_InterruptFlags = m_InterruptFlags | ThreadInterruptFlags.StackDump;
            m_SignalDebugger.Set();
        }
        public void GetHeapStats()
        {
            m_InterruptFlags = m_InterruptFlags | ThreadInterruptFlags.HeapStats;
            m_SignalDebugger.Set();
        }
        /// <summary>
        /// Method that will stop the running process so we can inspect something manually
        /// </summary>
        /// <param name="worker"></param>
        public void PauseDebugger(System.Threading.Thread worker)
        {
            m_InterruptFlags = m_InterruptFlags | ThreadInterruptFlags.PauseDebugger;
            m_DebuggerPause.Reset();
            //worker.Interrupt();
            m_SignalDebugger.Set();
        }
        /// <summary>
        /// If paused, set the debugger running again
        /// </summary>
        /// <param name="worker"></param>
        public void RestartDebugger(System.Threading.Thread worker)
        {
             m_DebuggerPause.Set();
        }
        /// <summary>
        /// Interrupt the debugger when we want to change the settings
        /// </summary>
        /// <param name="worker"></param>
        public void ChangeDebuggerSettings(System.Threading.Thread worker)
        {
            m_InterruptFlags = m_InterruptFlags | ThreadInterruptFlags.NewBreakpoint;
           // worker.Interrupt();
            m_SignalDebugger.Set();
        }
        /// <summary>
        /// Start the specified process name and gather exceptions, stack traces, and stack objects
        /// </summary>
        public void RunProcess()
        {
            #region Set up debugging engine
            m_Debugger.Options.CreateProcessWithNewConsole = true;
            m_Debugger.Options.StopOnException = true;
            m_Debugger.Options.StopOnAssemblyLoad = true;
            m_Debugger.Options.StopOnLogMessage = true;
            MDbgProcess proc = null;
            if (ProcessId < 0)
            {
                proc = m_Debugger.CreateProcess(ProcessName, ProcessArgs, DebugModeFlag.Default, DebugEngineUtils.GetAssemblyRuntimeVersion(ProcessName,DefaultNetVersion));
                ProcessId = proc.CorProcess.Id;
                onStatusChange("Launching " + ProcessName, EngineStatus.Attaching,ProcessId);
            }
            SysWowInfo.IsWow64Process(System.Diagnostics.Process.GetCurrentProcess().Handle, out ThisProcessIs32Bit);
            SysWowInfo.IsWow64Process(System.Diagnostics.Process.GetProcessById(ProcessId).Handle, out DebuggedProcessIs32Bit);
            if (!ThisProcessIs32Bit && DebuggedProcessIs32Bit)
            {
                onStatusChange("Can't debug " + ProcessName + "\r\nbecause it is a 32-bit application. Please use RedFlag(x86).exe", EngineStatus.Aborting, ProcessId);
                m_cancel = true;
            }
            if (ThisProcessIs32Bit && !DebuggedProcessIs32Bit)
            {
                onStatusChange("Can't debug " + ProcessName + "\r\nbecause it is a 64-bit application. Please use RedFlag.exe", EngineStatus.Aborting, ProcessId);
                m_cancel = true;
            }
            if (ProcessId > -1 && !m_cancel && m_Status != EngineStatus.Attaching) proc = m_Debugger.Attach(ProcessId);
            else
            {//can't disable JIT while attaching only
                if (proc!=null) proc.CorProcess.OnCreateProcess += new Microsoft.Samples.Debugging.CorDebug.CorProcessEventHandler(CorProcess_OnCreateProcess);
                if (proc!=null) proc.CorProcess.OnModuleLoad += new Microsoft.Samples.Debugging.CorDebug.CorModuleEventHandler(CorProcess_OnModuleLoad);
            }
            // Once process is running, we can set a breakpoint
            if (!String.IsNullOrEmpty(m_BreakSourceFile) && m_BreakSourceLine>0) m_Debugger.Processes.Active.Breakpoints.CreateBreakpoint(
                  m_BreakSourceFile,m_BreakSourceLine
             );

            if (ProcessStatus != null && !m_cancel) onStatusChange("Debugging " + ProcessName, EngineStatus.Debugging, ProcessId);
            if (proc != null)
            {
                proc.CorProcess.OnCreateAppDomain += new Microsoft.Samples.Debugging.CorDebug.CorAppDomainEventHandler(CorProcess_OnCreateAppDomain);
                proc.CorProcess.OnAppDomainExit += new Microsoft.Samples.Debugging.CorDebug.CorAppDomainEventHandler(CorProcess_OnAppDomainExit);
            }
            if (proc != null) proc.EnableUserEntryBreakpoint = false;
            #endregion
            while (!m_cancel && proc.IsAlive)
            {
                // Let the debuggee run and wait until it hits a debug event or m_SignalDebugger is set.
                try
                {
                    m_SignalDebugger.SafeWaitHandle = proc.Go().SafeWaitHandle;
                    m_SignalDebugger.WaitOne();
                }
                catch (InvalidOperationException)
                {
                    //Increment stop count to prevent this again.
                    proc.AsyncStop();
                }
                #region Check if user requested stop
                // If the handle was not set by MDbgEngine, handle the reason for the set.
                    
                    switch (m_InterruptFlags)
                    {
                        case ThreadInterruptFlags.HeapStats:
                            proc.AsyncStop().WaitOne();
                            HeapStats = DebugEngineUtils.HeapStats(proc,this.MaxObjectDepth,this.MaxObjectDepth,this.GetArrays);
                            onStatusChange("Heap dump created", EngineStatus.HeapDumped, ProcessId);
                            m_InterruptFlags = m_InterruptFlags ^ ThreadInterruptFlags.HeapStats;
                        break;
                        case ThreadInterruptFlags.StackDump:
                            Dictionary<int, int[]> threads = DebugEngineUtils.GetThreadStatus(ProcessId);
                            proc.AsyncStop().WaitOne();
                            StackDump = DebugEngineUtils.DumpStacks(proc, ProcessId, threads);
                            onStatusChange("Stack dump created", EngineStatus.DumpCreated, ProcessId);
                            m_InterruptFlags = m_InterruptFlags ^ ThreadInterruptFlags.StackDump;
                            break;
                        case ThreadInterruptFlags.NewBreakpoint:
                            proc.AsyncStop().WaitOne();
                            m_Debugger.Processes.Active.Breakpoints.DeleteAll();
                            if (!String.IsNullOrEmpty(m_BreakSourceFile) && m_BreakSourceLine>0)
                            m_Debugger.Processes.Active.Breakpoints.CreateBreakpoint(m_BreakSourceFile, m_BreakSourceLine);
                            m_InterruptFlags = m_InterruptFlags ^ ThreadInterruptFlags.NewBreakpoint;
                            break;
                        case ThreadInterruptFlags.StopDebugger:
                            proc.AsyncStop().WaitOne();
                            m_Debugger.Processes.Active.Breakpoints.DeleteAll();
                            m_InterruptFlags = m_InterruptFlags ^ ThreadInterruptFlags.StopDebugger;
                            break;
                        case ThreadInterruptFlags.PauseDebugger:
                            proc.AsyncStop().WaitOne();
                            // wait for set event
                            m_DebuggerPause.WaitOne();
                            m_InterruptFlags = m_InterruptFlags ^ ThreadInterruptFlags.PauseDebugger;
                            break;
                        default:
                            //m_cancel = true;
                            break;
                    }
                #endregion
                    m_SignalDebugger.Reset(); // we should be done now.
                if (m_cancel) break; // exit the loop if the main thread wants to stop debugging
                object o = proc.StopReason;
                // Process is now stopped. proc.StopReason tells us why we stopped.
                // The process is also safe for inspection.
                LogMessageStopReason msg = o as LogMessageStopReason;
                if (msg != null && NewMessage!=null)
                {
                    TraceMessage traceMessage = new TraceMessage(msg.Message, msg.Name, msg.SwitchName);
                    MessageEventArgs e=new MessageEventArgs(traceMessage);
                    NewMessage(this, e);
                }
                #region Stop because new assembly loaded
                AssemblyLoadedStopReason l = o as AssemblyLoadedStopReason;
                if (l != null) //a new assembly has been loaded
                {
                    //Modules.Clear();
                    lock (Modules)
                    {
                        foreach (MDbgModule module in proc.Modules)
                        {
                            
                              string assemblyFilespec=module.CorModule.Name;
                              if (Modules.Find(delegate(Module mod)
                              {
                                  return mod.FileName == assemblyFilespec;
                              }) == null &&
                                  Modules.Find(delegate(Module mod)
                              {
                                  return mod.FileName == "0x"+module.CorModule.BaseAddress.ToString("X");
                              }) == null)
                              {

                                  if (System.IO.File.Exists(assemblyFilespec))
                                  {
                                      string assemblyFullName = System.IO.Path.GetFileNameWithoutExtension(assemblyFilespec);
                                      try
                                      {
                                          System.Reflection.AssemblyName an = System.Reflection.AssemblyName.GetAssemblyName(assemblyFilespec);
                                          assemblyFullName = an.FullName;
                                      }
                                      catch
                                      {
                                          // at least we should get the file version if Cecil fails
                                          System.Diagnostics.FileVersionInfo fi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyFilespec);
                                          assemblyFullName += String.Format(", Version={0}", fi.FileVersion);
                                      }
                                      Modules.Add(new Module(assemblyFullName, assemblyFilespec, module.SymbolFilename));
                                  }
                                  else // module has been loaded into the appdomain from memory
                                  {
                                      Modules.Add(new Module(module.CorModule.Name, "0x" + module.CorModule.BaseAddress.ToString("X"), module.SymbolFilename));
                                  }
                              }
                        }
                    }
                }
                #endregion
                ExceptionThrownStopReason m = o as ExceptionThrownStopReason;
                BreakpointHitStopReason b = o as BreakpointHitStopReason;
                 // This is an exception or breakpoint
                 if (m != null|| b!=null)
                {

                        MDbgThread t = proc.Threads.Active;
                        MDbgValue ex = t.CurrentException;
                        MDbgFrame f = null;   
                     try
                        {
                            f = t.CurrentFrame;
                        }
                        catch (MDbgNoCurrentFrameException) {  }

                        RedFlag.Exception rfException = new RedFlag.Exception();
                        if (ex != null)
                        {
                            if (m_ExceptionsToIgnore.Find(delegate(string exType) { return exType == ex.TypeName; }) != null) continue;
                            try
                            {
                                // Store the Href before anything else -- will allow locked file detection
                                rfException.Message = ex.GetField("_message").GetStringValue(false);
                                rfException.HResult = System.Convert.ToInt32(ex.GetField("_HResult").GetStringValue(false));

                            }
                            catch (MDbgValueException) { /* don't let the exception escape if there is no message! */}

                            rfException.Name = ex.TypeName;
                        }
                        if (b!=null)
                        {
                            rfException.Name = "RedFlag.BreakpointReached";
                            if (b.Breakpoint.GetType().Name=="UserEntryBreakpoint")
                                rfException.Message = "User entry breakpoint";
                            else
                             rfException.Message = "A breakpoint has been hit at " + SetBreakpoint;
                        }
                        rfException.Time = DateTime.Now.Subtract(m_StartTime);
                        // can walk stack?
                        for (int i = 0; i < MaxStackDepth; i++)
                        {
                            if (f != null)
                            {
                                RedFlag.Method rfMethod = new RedFlag.Method(); 
                                // add the args
                                List<MethodArgument> rfArgs = new List<MethodArgument>();
                                // add private members
                                List<StackObject> sobjs = new List<StackObject>();
                                try
                                {
                                rfMethod.Name = f.Function.FullName;
                               
                                    if (f.SourcePosition != null)
                                    {
                                        rfMethod.SourceFile = f.SourcePosition.Path;
                                        rfMethod.SourceLine = f.SourcePosition.Line;
                                    }
                                }
                                catch (COMException) { continue; }
                                try{
                                    if (f.CorFrame.GetArgumentCount() > -1)
                                    {
                                        foreach (MDbgValue val in f.Function.GetArguments(f))
                                        {
                                            MethodArgument arg = new MethodArgument();
                                            arg.Name = val.Name;
                                            arg.Type = val.TypeName;
                                            arg.Value = val.GetStringValue(false);
                                            rfArgs.Add(arg);
                                            //Cheeky! Add the function args to appear in the local vars list.
                                            DebugEngineUtils.GetAllMembers(val, 2, 2, false, ref sobjs);
                                        }
                                    }
                                }
                                catch (COMException) { continue; }
                                rfMethod.Arguments = rfArgs;
    
                                foreach (MDbgValue v in f.Function.GetActiveLocalVars(f))
                                {
                                    //Console.WriteLine(v.Name);
                                   // Console.WriteLine(v.Value);
                                    try
                                    {
                                        DebugEngineUtils.GetAllMembers(v, MaxObjectDepth, MaxObjectDepth, GetArrays, ref sobjs);
                                    }
                                    catch (COMException) { }
                                }
                                rfMethod.PrivateMembers = sobjs;
                                rfException.Methods.Add(rfMethod);
                            }
                            if (f!=null) f = f.NextUp;
                        }
                        onNewException(rfException);
                }
            }
             if (m_cancel && proc!=null)
             {
                 // If we have requested a detach, stop the process
                 proc.CorProcess.Stop(20000);
                 proc.CorProcess.Detach();
             }
            if (ProcessStatus != null) onStatusChange("Idle...",EngineStatus.Idle,ProcessId);
        }

        void CorProcess_OnAppDomainExit(object sender, Microsoft.Samples.Debugging.CorDebug.CorAppDomainEventArgs e)
        {
            this.AppDomains.Remove(new AppDomain(e.AppDomain.Name));
        }

        void CorProcess_OnCreateAppDomain(object sender, Microsoft.Samples.Debugging.CorDebug.CorAppDomainEventArgs e)
        {
            this.AppDomains.Add(new AppDomain(e.AppDomain.Name));
        }

        void CorProcess_OnModuleLoad(object sender, Microsoft.Samples.Debugging.CorDebug.CorModuleEventArgs e)
        {
            e.Module.JITCompilerFlags = Microsoft.Samples.Debugging.CorDebug.CorDebugJITCompilerFlags.CORDEBUG_JIT_DISABLE_OPTIMIZATION;
        }

        void CorProcess_OnCreateProcess(object sender, Microsoft.Samples.Debugging.CorDebug.CorProcessEventArgs e)
        {
            //try to disable optimization
            ((Microsoft.Samples.Debugging.CorDebug.CorProcess)sender).DesiredNGENCompilerFlags = Microsoft.Samples.Debugging.CorDebug.CorDebugJITCompilerFlags.CORDEBUG_JIT_DISABLE_OPTIMIZATION;
        }
        /// <summary>
        /// Signal the debugging loop to exit and detach the debugger
        /// </summary>
        /// <param name="worker">The thread that is running this debugging session</param>
        public void StopDebugging(System.Threading.Thread worker)
        {
            if (ProcessStatus != null) onStatusChange("Detaching from " + ProcessName, EngineStatus.Detaching,ProcessId);
            m_InterruptFlags = m_InterruptFlags | ThreadInterruptFlags.StopDebugger;
            m_cancel = true; // Setting this will break the debugging loop after setting the WaitHandle
            //worker.Interrupt();
            m_SignalDebugger.Set();
            DateTime tmr = DateTime.Now;
            while (m_Status != EngineStatus.Idle)
            {
                Thread.Sleep(1000);
                if (DateTime.Now.Subtract(tmr) > TimeSpan.FromSeconds(10)) break;
            }
        }
        /// <summary>
        /// Pass the detected exception to the main thread
        /// </summary>
        /// <param name="exc"></param>
        protected void onNewException(RedFlag.Exception exc)
        {
            EventArgs args=new EventArgs();
            NewException(exc, args);
        }
        /// <summary>
        /// Notify the main thread about what the debugger is doing
        /// </summary>
        /// <param name="message">Message to pass along</param>
        /// <param name="stat">The current value for the EngineStatus</param>
        protected void onStatusChange(string message,EngineStatus stat,int pid)
        {
            this.m_Status = stat;
            ProcessEventArgs arg = new ProcessEventArgs();
            arg.Message = message;
            arg.Status = stat;
            arg.ProcessId = pid;
            ProcessStatus(this, arg);
        }
        // Skip past fake attach events. 
        static void DrainAttach(MDbgEngine debugger, MDbgProcess proc)
        {
            bool fOldStatus = debugger.Options.StopOnNewThread;
            debugger.Options.StopOnNewThread = false; // skip while waiting for AttachComplete

            proc.Go().WaitOne();

            debugger.Options.StopOnNewThread = true; // needed for attach= true; // needed for attach

            // Drain the rest of the thread create events.
            while (proc.CorProcess.HasQueuedCallbacks(null))
            {
                proc.Go().WaitOne();
            }

            debugger.Options.StopOnNewThread = fOldStatus;
        }


    }
    
}
