using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RedFlag
{
    public delegate void ChooseProgramEventHandler(object o, ChooseProgramEventArgs e);
    public partial class ChooseProgram : Form
    {
        public event ChooseProgramEventHandler ProgramChosen;
        private string m_ProgramName=String.Empty;
        private string m_ProgramArguments=String.Empty;
        public ChooseProgram()
        {
            InitializeComponent();
        }

        private void butChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Programs|*.exe";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                m_ProgramName = ofd.FileName;
                tbExecutable.Text = m_ProgramName;
            }
        }
        protected void onProgramChosen()
        {
            ChooseProgramEventArgs args=new ChooseProgramEventArgs();
            if (rb_Desktop.Checked == true)
            {
                args.ProgramName = tbExecutable.Text;
                args.ProgramArguments = tbArgs.Text;
            }
            if (rb_Website.Checked == true)
            {
                try
                {
                    args.ProgramName = IIS.IISConfig.GetWorkerProcessExe();
                    Uri newUrl = new Uri(tbExecutable.Text);
                    int newPort = 0;
                    if (!Int32.TryParse(tbArgs.Text,out newPort)) throw new ArgumentException("Invalid TCP port");
                    args.ProgramArguments = IIS.IISConfig.GetW3wpArgs(newUrl, newPort);
                }
                catch (NotSupportedException nse)
                {
                    MessageBox.Show(nse.Message);
                    return;
                }
                catch (UriFormatException)
                {
                    MessageBox.Show(String.Format("Invalid URL: {0}", tbExecutable.Text));
                    return;
                }
                catch (ArgumentException ae)
                {
                    MessageBox.Show(ae.Message);
                    return;
                }
                
                
            }
            if (ProgramChosen != null) ProgramChosen(this, args);
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            onProgramChosen();
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rb_Website_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "URL";
            label2.Text = "TCP Port";
            butChoose.Enabled = false;
        }

        private void rb_Desktop_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Program Executable";
            label2.Text = "Arguments";
            butChoose.Enabled = true;
        }
    }
    public class ChooseProgramEventArgs : EventArgs
    {
        private string m_ProgramName;
        private string m_ProgramArguments;
        public string ProgramName
        {
            get
            {
                return m_ProgramName;
            }
            set
            {
                m_ProgramName = value;
            }
        }
        public string ProgramArguments
        {
            get
            {
                return m_ProgramArguments;
            }
            set
            {
                m_ProgramArguments = value;
            }

        }

    }
}
