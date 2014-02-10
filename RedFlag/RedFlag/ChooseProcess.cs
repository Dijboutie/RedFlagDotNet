using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Management;

namespace RedFlag
{
    public delegate void ChooseProcessEventHandler(object o, RunningProcessEventArgs e);
    public delegate void DetectProcessEventHandler(object o, RunningProcessEventArgs e);
    public partial class ChooseProcess : Form
    {
        public event ChooseProcessEventHandler ProcessChosen;
       public event DetectProcessEventHandler ProcessDetected;
        public ChooseProcess()
        {
            InitializeComponent();

        }
        
        protected void onProcessChosen()
        {
            RunningProcessEventArgs args = new RunningProcessEventArgs();
            args.ProcessName = (string)dgvProcesses.SelectedRows[0].Cells[1].Value;
            args.ProcessId = (int)dgvProcesses.SelectedRows[0].Cells[2].Value;
            if (ProcessChosen != null) ProcessChosen(this, args);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            onProcessChosen();
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PopulateRunningProcesses()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                RunningProcessEventArgs args = new RunningProcessEventArgs();
                args.ProcessName = process.ProcessName;
                args.ProcessId = process.Id;
                
                    try
                    {
                        if (!process.HasExited)
                        {
                            args.ImagePath = process.MainModule.FileName;
                            /* BD- this bit will add the application pool name to w3wp processes */
                            if (args.ImagePath.ToUpper().Contains("W3WP"))
                            {
                                string wmiQuery = string.Format("select CommandLine from Win32_Process where ProcessId='{0}'", process.Id);
                                ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
                                ManagementObjectCollection collection = searcher.Get();
                                foreach (ManagementObject instance in collection)
                                {
                                    string appPoolName = "unknown app pool";
                                    try
                                    {
                                        string[] argString = ((string)instance["CommandLine"]).Split(' ');
                                        for (int i = 0; i < argString.Length; i++)
                                        {
                                            if (argString[i].ToLower() == "-ap")
                                            {
                                                appPoolName = String.Empty;
                                                while (!argString[i+1].StartsWith("-"))
                                                {
                                                    appPoolName += argString[i + 1].Trim('\"');
                                                    i++;
                                                }
                                                break;
                                            }
                                        }
                                        args.ProcessName += " [" + appPoolName + "]";
                                    }
                                    catch {/*don't care */ }
                                }
                            } /* END - W3WP stuff*/
                            onProcessDetected(args);
                        }
                    }
                    catch (Win32Exception)
                    {
                        args.ImagePath = "";
                        onProcessDetected(args);
                    }
            }
        }
        protected void onProcessDetected(RunningProcessEventArgs evArgs)
        {
            if (this.ProcessDetected != null)
            {
                ProcessDetected(this, evArgs);
            }
        }

        private void ChooseProcess_Load(object sender, EventArgs e)
        {
            this.ProcessDetected += new DetectProcessEventHandler(ChooseProcess_ProcessDetected);
            ThreadStart start = new ThreadStart(PopulateRunningProcesses);
            Thread t = new Thread(start);
            t.Name = "RedFlag_Processes";
            t.Start();
        }
        public delegate void ThreadSafeProcessDetected(object o, RunningProcessEventArgs e);
        void ChooseProcess_ProcessDetected(object o, RunningProcessEventArgs e)
        {
            if (this.dgvProcesses.InvokeRequired)
            {
                this.BeginInvoke(new ThreadSafeProcessDetected(ChooseProcess_ProcessDetected), o, e);
                return;
            }
            Icon i=this.Icon;
            try
            {
                i = ShellIcon.GetIcon(e.ImagePath, false);
            }
            catch (System.Exception) { }
            dgvProcesses.Rows.Add(i, e.ProcessName, e.ProcessId);
        }

        private void dgvProcesses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            onProcessChosen();
            this.Close();
        }
    }
    public class RunningProcessEventArgs
    {
        private int m_ProcessId;
        private string m_ProcessName;
        private string m_ImagePath;
        public string ImagePath
        {
            get
            {
                return m_ImagePath;
            }
            set
            {
                m_ImagePath = value;
            }
        }
        public string ProcessName 
        {
            get
            {
                return m_ProcessName;
            }
             set

            {
                 m_ProcessName=value;
            }
        }
        public int ProcessId
        {
            get
            {
                return m_ProcessId;
            }
            set
            {
                m_ProcessId = value;
            }
        }
    }
}
