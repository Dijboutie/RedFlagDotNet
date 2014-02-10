using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using RedFlag.Engine;

namespace RedFlag
{
    public partial class Form1 : Form
    {
        private List<RedFlag.Exception> m_Exceptions = new List<RedFlag.Exception>();
        private List<RedFlag.TraceMessage> m_TraceMessages = new List<TraceMessage>();
        private RedFlag.Exception m_CurrentException = null;
        private string m_ProgramName=null; // Name of the executable we're debugging
        private string m_ProgramToStart = null; // Auto-launch an executable on startup
        private string m_ProgramArgs=String.Empty;
        private int m_ProcessId = -1;
        private DebugEngine engine = null;
        private EngineStatus m_DebugEngineStatus = EngineStatus.Idle;
        private EngineMode m_EngineMode = EngineMode.None;
        private System.Threading.Thread workerThread=null;
        private const string m_DebuggerThreadName = "RedFlag_Debugger";
        private bool m_doArrays=false;
        private int m_StackLength = 5;
        private int m_ObjectDepth = 4;
        private string m_SetBreakpoint=String.Empty;
        private List<string> m_ExceptionsToIgnore = new List<string>();
        private string m_DefaultNetVersion = System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion;
        private LoadedSessionInfo m_LoadedSessionInfo = new LoadedSessionInfo();
        public Form1(string launchProgram, string programArgs, bool DoArrays, int StackLength, int ObjectDepth, string setBreakpoint)
        {
            m_doArrays = DoArrays;
            m_ObjectDepth = ObjectDepth;
            m_StackLength = StackLength;
            m_ProgramToStart = launchProgram;
            m_ProgramArgs = programArgs;
            m_SetBreakpoint = setBreakpoint;
            InitializeComponent();
            SetupListViewSorting();
            launchProcessToolStripMenuItem_Click(this, new EventArgs());

        }
        public Form1(bool DoArrays, int StackLength, int ObjectDepth, string SetBreakpoint)
        {
            m_doArrays = DoArrays;
            m_ObjectDepth = ObjectDepth;
            m_StackLength = StackLength;
            m_SetBreakpoint = SetBreakpoint;
            InitializeComponent();
            SetupListViewSorting();
        }
        private void SetupListViewSorting()
        {
            lvLocals.ListViewItemSorter = new ListViewColumnSorter();
            lvExceptions.ListViewItemSorter = new ListViewColumnSorter();
            lv_Assemblies.ListViewItemSorter = new ListViewColumnSorter();
            lvTraceMessages.ListViewItemSorter = new ListViewColumnSorter();
        }
        public delegate void DoThreadedListFormat(object o, ListControlConvertEventArgs e);
        
        [SmartAssembly.ReportUsage.ReportUsage("Launch process")]
        private void launchProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_EngineMode = EngineMode.Launch;
            if (m_DebugEngineStatus == EngineStatus.Debugging)
               // MessageBox.Show("Please exit " + m_ProgramName + " before launching a new debugger process");
                engine.StopDebugging(workerThread);
             InitGui();      
            if (m_ProgramToStart == null)
            {
                ChooseProgram choose = new ChooseProgram();
                choose.ProgramChosen += new ChooseProgramEventHandler(choose_ProgramChosen);
                choose.ShowDialog();
                
            }
            else m_ProgramName = m_ProgramToStart;
            m_ProgramToStart = null;       
            if (m_ProgramName != null)
            {
                engine = new DebugEngine();
                engine.NewException += new NewExceptionHandler(engine_NewException);
                engine.ProcessStatus += new ProcessStatusHandler(engine_ProcessStatus);
                engine.NewMessage += new DebugMessageHandler(engine_NewMessage);
                engine.MaxStackDepth = m_StackLength;
                engine.MaxObjectDepth = m_ObjectDepth;
                engine.GetArrays = m_doArrays;
                engine.ProcessName = m_ProgramName;
                engine.ProcessArgs = " "+m_ProgramArgs;
                engine.SetBreakpoint = m_SetBreakpoint;
                engine.ExceptionsToIgnore = m_ExceptionsToIgnore;
                engine.DefaultNetVersion = m_DefaultNetVersion;
                try
                {
                    System.Threading.ThreadStart ts = new System.Threading.ThreadStart(engine.RunProcess);
                    workerThread = new System.Threading.Thread(ts);
                    workerThread.IsBackground = true; // mainly for SA so it terms
                    workerThread.Name = m_DebuggerThreadName;
                    workerThread.Start();
                    
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("Exception: " + exc.Message);
                }
        }
            
        }
        public delegate void DoThreadedNewMessage(object o, Engine.MessageEventArgs e);
        void engine_NewMessage(object o, Engine.MessageEventArgs e)
        {
            if (this.lvExceptions.InvokeRequired)
            {
                this.BeginInvoke(new DoThreadedNewMessage(engine_NewMessage), o, e);
                return;
            }
            else
            {
                ListViewItem lvi = new ListViewItem(new string[] { e.TraceMessage.Message, e.TraceMessage.Name, e.TraceMessage.SwitchName, lvTraceMessages.Items.Count.ToString() });
                lvTraceMessages.Items.Add(lvi);
            }
        }
        void choose_ProcessChosen(object o, RunningProcessEventArgs e)
        {
            m_ProgramName = e.ProcessName;
            m_ProcessId = e.ProcessId;
        }
        void choose_ProgramChosen(object o, ChooseProgramEventArgs e)
        {
            m_ProgramName = e.ProgramName;
            m_ProgramArgs = e.ProgramArguments;
        }
        public delegate void DoThreadedProcessStatus(object o, ProcessEventArgs e);
        void engine_ProcessStatus(object o, ProcessEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DoThreadedProcessStatus(engine_ProcessStatus), o, e);
                return;
            }
            switch (e.Status) {
                case EngineStatus.DumpCreated:
                StackWindow w = new StackWindow(engine.StackDump, engine.Modules);
                w.ShowDialog();
                break;
                case EngineStatus.HeapDumped:
                HeapStatsWindow hsw = new HeapStatsWindow(engine.HeapStats);
                hsw.ShowDialog();
                break;
                case EngineStatus.AssemblyLoaded:
                lv_Assemblies_VisibleChanged(this,new EventArgs());
                break;
                case EngineStatus.Aborting:
                MessageBox.Show(e.Message, "Cannot debug");
                //SmartAssembly.ReportException.ExceptionReporting.Report(new System.Exception(e.Message));
                break;
                default:
                m_DebugEngineStatus = e.Status;
                m_ProcessId = e.ProcessId;
                toolStripStatusLabel1.Text = e.Message;
                break;
            }
        }
        public delegate void DoThreadedGoodManualType(object o,EventArgs e);
        void engine_NewException(object o, EventArgs e)
        {
            if (this.lvExceptions.InvokeRequired)
            {
                this.BeginInvoke(new DoThreadedGoodManualType(engine_NewException), o, e);
                return;
            }
            else
            {
                RedFlag.Exception rfException = (RedFlag.Exception)o;
                if (m_ExceptionsToIgnore.Find(delegate(string name) // we can specify a list of exceptions to ignore
                {
                    return rfException.Name == name;
                }) == null)
                {
                    m_Exceptions.Add(rfException);
                    //lbExceptions.Items.Add(new KeyValuePair<Guid, string>(exception.GUID, exception.Name));
                    string[] row = new string[3];
                    row[0] = rfException.Name;
                    row[1] = rfException.Time.ToString();
                    row[2] = rfException.Message;
                    ListViewItem lvItem = new ListViewItem(row);
                    lvItem.Tag = rfException.GUID;
                    lvExceptions.Items.Add(lvItem);
                    m_CurrentException = rfException;

                    lbStackTrace.DataSource = rfException.StackTrace;
                }
            }
        }

        private void lvExceptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvExceptions.InvokeRequired)
            {
                this.BeginInvoke(new DoThreadedGoodManualType(lvExceptions_SelectedIndexChanged), sender, e);
                return;
            }
            else
            {
                try
                {
                    RedFlag.Exception exc = m_Exceptions.Find(delegate(RedFlag.Exception excpt) { return excpt.GUID == (System.Guid)lvExceptions.SelectedItems[0].Tag; });
                    m_CurrentException = exc;
                    lbStackTrace.DataSource = exc.StackTrace;
                }
                catch (System.Exception) { }
            }
        }

        private void lbStackTrace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbStackTrace.InvokeRequired)
            {
                this.BeginInvoke(new DoThreadedGoodManualType(lbStackTrace_SelectedIndexChanged), sender, e);
                return;
            }
            else
            {
                try
                {
                   
                        RedFlag.Method method = m_CurrentException.Methods.Find(delegate(RedFlag.Method meth) { return meth.Signature == (string)lbStackTrace.SelectedItem; });
                       // List<string> memObjects = new List<string>();
                        /*List<StackObject> sobjs = null;
                         if (checkStrings.Checked == true) sobjs = method.PrivateMembers.FindAll(delegate(StackObject obj) { return obj.Type == "System.String"; });
                        else */
                        List<StackObject> sobjs = method.PrivateMembers;
                        lvLocals.Items.Clear();
                        int i = 0;
                        foreach (StackObject sobj in sobjs)
                        {
                            //memObjects.Add(sobj.ToString());
                            string[] row = new string[4];
                            row[0] = sobj.Name;
                            row[1] = sobj.Value;
                            row[2] = sobj.Type;
                            row[3] = i.ToString();
                            lvLocals.Items.Add(new ListViewItem(row));
                            i++;
                        }
                        //lbStrings.DataSource = memObjects;
                        List<string> argList = new List<string>();
                        foreach (MethodArgument arg in method.Arguments)
                        {
                            argList.Add(arg.ToString());
                        }
                        lbArguments.DataSource = argList;
                        butNext.Enabled = true;
                        if (lbStackTrace.SelectedIndex==0) butPrevious.Enabled = false;
                        groupBox2.Text = method.Name;
                  
                }
                catch (System.Exception) { }
            }
        }

       [SmartAssembly.ReportUsage.ReportUsage("Attach to process")]
        private void attachProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_EngineMode = EngineMode.Attach;
            if (m_DebugEngineStatus==EngineStatus.Debugging)
               // MessageBox.Show("Please exit " + m_ProgramName + " before attaching a new debugger process");
               engine.StopDebugging(workerThread);

            InitGui();
            ChooseProcess choose = new ChooseProcess();
            choose.ProcessChosen += new ChooseProcessEventHandler(choose_ProcessChosen);
            choose.ShowDialog();
            if (m_ProcessId>-1)
            {
                engine = new DebugEngine();
                engine.NewException += new NewExceptionHandler(engine_NewException);
                engine.ProcessStatus += new ProcessStatusHandler(engine_ProcessStatus);
                engine.NewMessage+=new DebugMessageHandler(engine_NewMessage);
                engine.MaxObjectDepth = m_ObjectDepth;
                engine.MaxStackDepth = m_StackLength;
                engine.GetArrays = m_doArrays;
                engine.ProcessName = m_ProgramName;
                engine.ProcessId = m_ProcessId;
                engine.SetBreakpoint = m_SetBreakpoint;
                engine.ExceptionsToIgnore = m_ExceptionsToIgnore;
                engine.DefaultNetVersion = m_DefaultNetVersion;
                try
                {
                    System.Threading.ThreadStart ts = new System.Threading.ThreadStart(engine.RunProcess);
                    workerThread = new System.Threading.Thread(ts);
                    workerThread.Name = m_DebuggerThreadName;
                    workerThread.IsBackground = true;
                    workerThread.Start();
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("Exception: " + exc.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_DebugEngineStatus==EngineStatus.Debugging)
               // MessageBox.Show("Please exit " + m_ProgramName + " and then click OK.");
                engine.StopDebugging(workerThread);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = "RedFlagResults.xml";
            if (!String.IsNullOrEmpty(m_ProgramName)) fileName = System.IO.Path.GetFileName(m_ProgramName) + "." + fileName;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML files|*.xml";
            sfd.FileName = fileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = "Verifying \"" + sfd.FileName + "\"";
                fileName = sfd.FileName;
                XmlTextWriter xw = new XmlTextWriter(fileName,Encoding.UTF8);
                RedFlagResults r = new RedFlagResults();
                lock (m_Exceptions)
                {
                    r.Exceptions = m_Exceptions;
                }
                if (engine!=null) lock (engine.Modules)
                {
                    r.Modules = engine.Modules;
                }
                lock (m_TraceMessages)
                {
                    r.Messages = m_TraceMessages;
                }
                if (engine != null) lock (engine.AppDomains)
                    {
                        r.AppDomainList = engine.AppDomains;
                    }
                // Because some stack values can't be serialized - meh!
                r.Verify();
                if (!String.IsNullOrEmpty(m_ProgramName)) r.ProgramName = m_ProgramName + " "+m_ProgramArgs;
                toolStripStatusLabel1.Text = "Saving \"" + sfd.FileName + "\"";
                XmlSerializer xs=new XmlSerializer(typeof(RedFlagResults));
                xs.Serialize(xw,r);
                xw.Close();
                toolStripStatusLabel1.Text = "Idle...";
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_DebugEngineStatus==EngineStatus.Debugging)
            {
                MessageBox.Show("You cannot load results while debugging");
                return;
            }
             InitGui(true);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files|*.xml";
            List<RedFlag.Exception> exceptions=null;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = "Loading \"" + ofd.FileName + "\"";
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                XmlTextReader xr = new XmlTextReader(fs);
                XmlSerializer xs = new XmlSerializer(typeof(List<RedFlag.Exception>));
                RedFlagResults results=null;
                try
                {
                    exceptions = (List<RedFlag.Exception>)xs.Deserialize(xr); //a 1.2 RedFlag file
                }
                catch (InvalidOperationException)
                {
                    try // try it the new way
                    {
                        xs = new XmlSerializer(typeof(RedFlagResults));
                        results = (RedFlagResults) xs.Deserialize(xr);
                        if (engine == null) engine = new DebugEngine();
                        engine.Modules = results.Modules;
                        engine.AppDomains = results.AppDomainList;
                        exceptions = results.Exceptions;
                        m_TraceMessages=results.Messages;
                    }
                    catch (InvalidOperationException ioe2)
                    {
                        MessageBox.Show("Error opening " + ofd.FileName + " - this may not be a valid results set.\r\n" + ioe2.Message);
                        xr.Close();
                        return;
                    }
                }
                xr.Close();

                lock (m_Exceptions)
                {
                    foreach (RedFlag.Exception exception in exceptions)
                    {

                        engine_NewException(exception, new EventArgs());
                    }
                }
                lock (m_TraceMessages)
                {
                    foreach (RedFlag.TraceMessage message in m_TraceMessages)
                    {
                        engine_NewMessage(this,new Engine.MessageEventArgs(message));
                    }
                }
                m_LoadedSessionInfo.Bitness = results.Bitenss;
                m_LoadedSessionInfo.CommandAndArgs = results.ProgramName;
                m_LoadedSessionInfo.DotNetVersions = results.DotNetVetsion;
                m_LoadedSessionInfo.OSVersion = results.OSVersion;
            }
            toolStripStatusLabel1.Text = "Idle...";
        }
        private void InitGui()
        {
            InitGui(false);
        }
        private void InitGui(bool OnlyClearGui)
        {
            if (!OnlyClearGui)
            {
                m_ProgramName = null;
                m_ProcessId = -1;
            }
            lvLocals.Items.Clear();
            lbStackTrace.DataSource = null;
            if (engine!=null) engine.Modules.Clear();
            m_Exceptions = new List<RedFlag.Exception>();
            lvExceptions.Items.Clear();
            m_TraceMessages = new List<TraceMessage>();
            lvTraceMessages.Items.Clear();
            lvAppDomains.Items.Clear();
            m_LoadedSessionInfo.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.ShowDialog();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;
            ListViewColumnSorter sorter = (ListViewColumnSorter)myListView.ListViewItemSorter;
            sorter.SortTimeSpan = false;
            sorter.SortNumeric = false;
            // Default sort is now text - we store info about the column type in the Tag
            // so if the Tag is "rgNumericSort" or "rgTimeSpanSort", change the sort algorithm
            if ((string)myListView.Columns[e.Column].Tag == "rgNumericSort") sorter.SortNumeric = true;
            if ((string)myListView.Columns[e.Column].Tag == "rgTimeSpanSort") sorter.SortTimeSpan = true;
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                   sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (lbStackTrace.SelectedIndex>0) lbStackTrace.SelectedIndex--;
            if (lbStackTrace.SelectedIndex == 0) butPrevious.Enabled = false;
            butNext.Enabled = true;
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            if (lbStackTrace.SelectedIndex < (lbStackTrace.Items.Count-1)) lbStackTrace.SelectedIndex++;
            if (lbStackTrace.SelectedIndex == (lbStackTrace.Items.Count - 1)) butNext.Enabled = false;
            butPrevious.Enabled = true;
        }

        private void lvLocals_DoubleClick(object sender, EventArgs e)
        {
            RedFlag.Method method = m_CurrentException.Methods.Find(delegate(RedFlag.Method meth) { return meth.Signature == (string)lbStackTrace.SelectedItem; });
            ObjectHierarchy h = new ObjectHierarchy(method, Convert.ToInt32(lvLocals.SelectedItems[0].SubItems[3].Text));
            h.ShowDialog();
        }

        private void f1Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                //ListBox
                if (sender.GetType().Equals(typeof(ListBox))) Program.CopyListBoxToClipboard((ListBox)sender);
                //ListView
                if (sender.GetType().Equals(typeof(ListView))) Program.CopyListViewToClipboard((ListView)sender);
            }
        }

        private void detachProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (workerThread != null && engine!=null)
            {
                engine.StopDebugging(workerThread);
            }
        }

        private void dumpStacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Are we debugging something?
            if (m_DebugEngineStatus != EngineStatus.Debugging)
            {
                MessageBox.Show("You must attach to or launch a process before dumping stacks.");
                return;
            }
            // DumpStacks will stop the process, create a stack dump, and
            // notify this thread in the engine_ProcessStatus event handler
            engine.DumpStacks(workerThread);
        }

        private void lv_Assemblies_VisibleChanged(object sender, EventArgs e)
        {
           /* StringBuilder sb=new StringBuilder();*/
            if (engine != null && engine.Modules != null)
            {
                lock (engine.Modules)
                {
                    lv_Assemblies.Items.Clear();
                    foreach (Module assembly in engine.Modules)
                    {
                        /*sb.Append(assembly);
                        sb.Append("\r\n");*/
                        string[] lvItem = new string[3];
                        lvItem[0] = assembly.Name;
                        lvItem[1] = assembly.FileName;
                        lvItem[2] = assembly.SymbolFile;
                        lv_Assemblies.Items.Add(new ListViewItem(lvItem));
                    }
                }
                /*tbAssemblies.Text = sb.ToString();*/
            }
        }

        private void debuggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebuggerSettings settingsForm = new DebuggerSettings(m_StackLength, m_ObjectDepth, m_doArrays, m_SetBreakpoint,m_ExceptionsToIgnore,m_DefaultNetVersion);
            settingsForm.DebuggerSettingsChosen += new DebuggerSettingsEventHandler(settingsForm_DebuggerSettingsChosen);
            settingsForm.ShowDialog();
        }

        void settingsForm_DebuggerSettingsChosen(object o, ChangeSettingsEventArgs e)
        {
            m_doArrays = e.ProcessArrays;
            if (e.BreakSource!=String.Empty)
            m_SetBreakpoint = String.Format("{0}({1})", e.BreakSource, e.BreakLine);
            else m_SetBreakpoint=String.Empty;
            m_StackLength = e.StackLength;
            m_ObjectDepth = e.StackDepth;
            m_ExceptionsToIgnore = e.IgnoreExceptions;
            m_DefaultNetVersion = e.DefaultDotNetVersion;
            if (m_DebugEngineStatus == EngineStatus.Debugging)
            {
                
                engine.GetArrays = m_doArrays;
                engine.MaxObjectDepth = m_ObjectDepth;
                engine.MaxStackDepth = m_StackLength;
                engine.SetBreakpoint = m_SetBreakpoint;
                engine.ExceptionsToIgnore = m_ExceptionsToIgnore;
                engine.ChangeDebuggerSettings(workerThread);
            }
                
            
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (m_Exceptions)
            {
                InitGui(true);
            }
        }

        private void stopProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_DebugEngineStatus == EngineStatus.Debugging)
            {
                if (stopProcessToolStripMenuItem.Text == "&Stop Process")
                {
                    engine.PauseDebugger(workerThread);
                    stopProcessToolStripMenuItem.Text = "&Start Process";
                }
                else
                {
                    engine.RestartDebugger(workerThread);
                    stopProcessToolStripMenuItem.Text = "&Stop Process";
                }
            }
            
        }

        private void readSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (engine!=null && engine.Modules != null)
            {
                SymbolsForm sf = new SymbolsForm(engine.Modules);
                sf.ShowDialog();
            }
        }

        private void lv_Assemblies_DoubleClick(object sender, EventArgs e)
        {
            // take us to the list of source files for this assembly
            List<RedFlag.Module> modules = new List<RedFlag.Module>();
            RedFlag.Module module = new Module();
            ListViewItem lvi=((ListView)sender).SelectedItems[0];
            module.Name = lvi.SubItems[0].Text;
            module.FileName = lvi.SubItems[1].Text;
            module.SymbolFile = lvi.SubItems[2].Text;
            modules.Add(module);
            if (module.SymbolFile != null)
            {
                SymbolsForm sf = new SymbolsForm(modules);
                sf.ShowDialog();
            }
        }

        private void lvAppDomains_VisibleChanged(object sender, EventArgs e)
        {
            // populate the list of AppDomains
            lvAppDomains.Items.Clear();
            if (engine != null)
            {
                AppDomains ads = engine.AppDomains;
                foreach (AppDomain ad in ads)
                {
                    ListViewItem lvi = new ListViewItem(new string[] { ad.Name });
                    lvAppDomains.Items.Add(lvi);
                }
            }
        }

        private void lbStackTrace_DoubleClick(object sender, EventArgs e)
        {
            // try to display source for the selected method
            RedFlag.Method method = m_CurrentException.Methods.Find(delegate(RedFlag.Method meth) { return meth.Signature == (string)lbStackTrace.SelectedItem; });
            SourceCode.SourceHound sh = new SourceCode.SourceHound();
            try
            {
                sh.OpenSourceFile(method, engine.Modules);
            }
            catch (ArgumentNullException ne) // could not open in reflector
            {
                MessageBox.Show(ne.Message);
            }
            catch (InvalidOperationException ioe) // could not open in reflector
            {
                MessageBox.Show(ioe.Message);
            }
        }

        private void dumpHeapStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Stop the running process
            if (engine!=null) {
                if (m_DebugEngineStatus != EngineStatus.Debugging)
                {
                    MessageBox.Show("You must attach to or launch a process before dumping stacks.");
                    return;
                }
                engine.GetHeapStats();               
            }
        }

        private void findVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindVariable fv = new FindVariable(m_Exceptions);
            fv.MethodChosen += new MethodChosenEventHandler(fv_MethodChosen);
            fv.Show();
        }

        void fv_MethodChosen(object o, MethodChosenEventArgs e)
        {
            //navigate to the method chosen in the find window
            foreach (ListViewItem item in lvExceptions.Items)
            {
                if ((Guid) item.Tag == e.ExceptionDetails.GUID)
                {
                    item.Selected = true;
                    lvExceptions.Select();
                    break;
                }
            }
            for (int i=0;i<lbStackTrace.Items.Count;i++)
            {
                if ((string)lbStackTrace.Items[i]==e.ExceptionDetails.Methods[0].Signature)
                {
                    lbStackTrace.SetSelected(i, true);
                    lbStackTrace.Select();
                        break;
                }
            }
        }

        private void showHandlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHandles handlesForm = new frmHandles(m_ProcessId);
            handlesForm.ShowDialog();
        }

        private void processInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_LoadedSessionInfo.IsClean) // live session, not loaded results
            {
                RedFlagResults results=new RedFlagResults();
                if (engine != null) results.Modules = engine.Modules; // need for the net versions
                if (!String.IsNullOrEmpty(m_ProgramName)) results.ProgramName = m_ProgramName + " " + m_ProgramArgs;
                m_LoadedSessionInfo.CommandAndArgs = results.ProgramName;
                m_LoadedSessionInfo.DotNetVersions = results.DotNetVetsion;
                m_LoadedSessionInfo.Bitness = results.Bitenss;
                m_LoadedSessionInfo.OSVersion = results.OSVersion;
            }
            frmDebuggedProcess procForm = new frmDebuggedProcess(m_LoadedSessionInfo);
            procForm.ShowDialog();
        }
    }
}
