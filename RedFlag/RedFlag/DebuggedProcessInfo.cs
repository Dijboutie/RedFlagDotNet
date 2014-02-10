using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RedFlag
{
    public partial class frmDebuggedProcess : Form
    {
        private LoadedSessionInfo m_RedFlagResults = null;
        private StringBuilder m_sb=new StringBuilder(250);
        public frmDebuggedProcess(LoadedSessionInfo results)
        {
            InitializeComponent();
            m_RedFlagResults = results;
        }

        private void frmDebuggedProcess_Load(object sender, EventArgs e)
        {
            string processInfo = String.Empty;
            m_sb.Append("Command: ");
            m_sb.Append(m_RedFlagResults.CommandAndArgs);
            AppendRN();
            m_sb.Append("OS Version: ");
            m_sb.Append(m_RedFlagResults.OSVersion);
            AppendRN();
            m_sb.Append(".NET Runtime: ");
            m_sb.Append(m_RedFlagResults.DotNetVersions);
            AppendRN();
            m_sb.Append("Bitness: ");
            m_sb.Append(m_RedFlagResults.Bitness.ToString());
            m_sb.Append(" bits");
            tbThisProcess.Text = m_sb.ToString();
            tbThisProcess.SelectionStart = 0;
            tbThisProcess.DeselectAll();
        }
        private void AppendRN()
        {
            m_sb.Append("\r\n");
        }
    }
}
