using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RedFlag
{
    public delegate void DebuggerSettingsEventHandler(object o,ChangeSettingsEventArgs e);
    public partial class DebuggerSettings : Form
    {
        private string m_BreakSource=String.Empty;
        private int m_BreakLine=0;
        private int m_StackLength = 0;
        private int m_ObjectDepth = 0;
        private bool m_ProcessArrays = false;
        private string m_DefaultNetVersion = "0";
        private List<string> m_ExceptionsToIgnore = new List<string>();
        public DebuggerSettings(int StackLength, int StackDepth, bool ProcessArrays, string SetBreakpoint, List<string> ExceptionsToIgnore, string DefaultNetVersion)
        {
            if (SetBreakpoint != String.Empty)
            {
                try
                {
                    m_BreakSource = SetBreakpoint.Substring(0, SetBreakpoint.IndexOf('('));
                    m_BreakLine = Convert.ToInt32(SetBreakpoint.Substring(SetBreakpoint.IndexOf('(') + 1, SetBreakpoint.Length - SetBreakpoint.IndexOf('(') - 2));
                    
                }
                catch { }
            }
            m_StackLength = StackLength;
            m_ObjectDepth = StackDepth;
            m_ProcessArrays = ProcessArrays;
            m_ExceptionsToIgnore = ExceptionsToIgnore;
            m_DefaultNetVersion = DefaultNetVersion;
            InitializeComponent();
        }
        public event DebuggerSettingsEventHandler DebuggerSettingsChosen;
        public DebuggerSettings()
        {
            InitializeComponent();
        }
        protected void onSettingsChosen()
        {
            ChangeSettingsEventArgs settings = new ChangeSettingsEventArgs();
            settings.StackDepth = Convert.ToInt32(num_ObjectDepth.Value);
            settings.StackLength = Convert.ToInt32(num_StackLength.Value);
            settings.ProcessArrays = cb_ArrayItems.Checked;
            string sourceFile = String.Empty;
            if (tb_SourceFile.Text != null) sourceFile = tb_SourceFile.Text;
            settings.BreakSource = sourceFile;
            settings.BreakLine = Convert.ToInt32(num_LineNumber.Value);
            if (DebuggerSettingsChosen != null) DebuggerSettingsChosen(this, settings);
            string[] exceptionsToIgnore=lbIgnore.Text.Split(',');
            List<string> lstIgnore = new List<string>();
            foreach (string s in exceptionsToIgnore)
            {
                lstIgnore.Add(s);
            }
            settings.IgnoreExceptions = lstIgnore;
            if (cbNetVersion.SelectedItem!=null) settings.DefaultDotNetVersion = cbNetVersion.SelectedItem.ToString();
            if (DebuggerSettingsChosen != null) DebuggerSettingsChosen(this, settings);
        }

        private void but_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void but_OK_Click(object sender, EventArgs e)
        {
            onSettingsChosen();
            this.Close();
        }

        private void but_ChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "C# files|*.cs;VB files|*.vb;All files|*.*";
            ofd.ShowDialog();
            tb_SourceFile.Text = ofd.FileName;
        }

        private void DebuggerSettings_Load(object sender, EventArgs e)
        {
            tb_SourceFile.Text = m_BreakSource;
            num_LineNumber.Value = m_BreakLine;
            num_StackLength.Value = m_StackLength;
            num_ObjectDepth.Value = m_ObjectDepth;
            cb_ArrayItems.Checked = m_ProcessArrays;
            foreach (string s in m_ExceptionsToIgnore)
            {
                lbIgnore.Text += s + ",";
            }
            lbIgnore.Text = lbIgnore.Text.TrimEnd(',');
            cbNetVersion.Text = m_DefaultNetVersion.ToString();
        }
    }
    public class ChangeSettingsEventArgs : EventArgs
    {
        public int StackDepth { get; set; }
        public int StackLength { get; set; }
        public string BreakSource { get; set; }
        public int BreakLine { get; set; }
        public bool ProcessArrays { get; set; }
        public string DefaultDotNetVersion { get; set; }
        public List<string> IgnoreExceptions { get; set; }
    }
}
