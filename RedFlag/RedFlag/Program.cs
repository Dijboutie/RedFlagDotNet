using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using Microsoft.Win32;

namespace RedFlag
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string launchProg=null;
            string launchArgs = String.Empty;
            bool ProcessArrays=false;
            int StackLength=5;
            int ObjectDepth=4;
            string setBreakpoint = String.Empty;
            if (args.Length > 0 && !args[0].StartsWith("/"))
            {
                //a special case so we can attach to w3wp (for ANTS Profiler)
                launchProg = args[0];
                for (int iCount = 1; iCount < args.Length; iCount++)
                {
                    if (args[iCount].Contains(" ")) args[iCount]="\""+args[iCount]+"\"";
                    if (!args[iCount].StartsWith("/") && launchArgs == String.Empty) launchArgs = args[iCount];
                    else launchArgs += " "+args[iCount];
                }
                // Remove the IFEO key
                string keyName = "Software\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\" + System.IO.Path.GetFileName(launchProg);
                RegistryKey keyVal = Registry.LocalMachine.OpenSubKey(keyName,true);
                keyVal.DeleteValue("Debugger",false);
            }
                foreach (string arg in args)
                {
                    if (arg.ToUpper().StartsWith("/LAUNCH:")) launchProg = arg.Substring(8).Trim('\"');
                    if (arg.ToUpper().StartsWith("/ARGUMENTS:")) launchArgs = arg.Substring(11).Trim('\"');
                    if (arg.ToUpper().StartsWith("/OBJECTDEPTH:")) ObjectDepth = Convert.ToInt32(arg.Substring(13).Trim('\"'));
                    if (arg.ToUpper().StartsWith("/STACKLENGTH:")) StackLength = Convert.ToInt32(arg.Substring(13).Trim('\"'));
                    if (arg.ToUpper().StartsWith("/BREAKPOINT:")) setBreakpoint = arg.Substring(12).Trim('\'');
                    if (arg.ToUpper() == "/PROCESSARRAYS") ProcessArrays = true;
                }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (launchProg==null) Application.Run(new Form1(ProcessArrays,StackLength,ObjectDepth,setBreakpoint));
            else Application.Run(new Form1(launchProg, launchArgs, ProcessArrays, StackLength, ObjectDepth,setBreakpoint));
        }
        /// <summary>
        /// Copy the whole list in this case as text
        /// </summary>
        /// <param name="lb"></param>
        public static void CopyListBoxToClipboard(ListBox lb)
        {
            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < lb.Items.Count; i++)
            {
                buffer.Append(lb.Items[i].ToString());
                buffer.Append("\r\n");
            }

            Clipboard.SetText(buffer.ToString());
        }
        /// <summary>
        /// The LV is mighty large--copy only the selected item
        /// </summary>
        /// <param name="lb"></param>
        public static void CopyListViewToClipboard(ListView lv)
        {
            StringBuilder buffer = new StringBuilder();
            ListView.SelectedListViewItemCollection coll = lv.SelectedItems;
            foreach (ListViewItem item in coll)
            {
                foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    buffer.Append(subItem.Text+"\t");
                }
                buffer.Append("\r\n");

            }
            Clipboard.SetText(buffer.ToString());
        }  

    }
}
