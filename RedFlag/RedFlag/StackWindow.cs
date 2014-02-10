using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RedFlag
{
    public partial class StackWindow : Form
    {
        private string m_StackDump = String.Empty;
        private List<Module> m_LoadedModules;
        public StackWindow(string StackDump, List<Module> LoadedModules)
        {
            m_StackDump = StackDump;
            m_LoadedModules = LoadedModules;
            InitializeComponent();
        }

        private void StackWindow_Load(object sender, EventArgs e)
        {
            // tbStacks.Text = m_StackDump;
            List<TreeNode> stackNodes = new List<TreeNode>();
            //update the thingmajig
            string[] stacks = m_StackDump.Split('\n');
            TreeNode currentNode = null;
            for (int i = 0; i < stacks.Length; i++)
            {
                if (!stacks[i].StartsWith(" ")) //this is a new thread
                {
                    string threadIdentifier = stacks[i].TrimEnd('\r');
                    if (!String.IsNullOrEmpty(threadIdentifier) && 
                        !threadIdentifier.StartsWith("Callstack for")
                        && !threadIdentifier.StartsWith("Connected"))
                    {
                        TreeNode tn = new TreeNode(threadIdentifier);
                        tn.Name = threadIdentifier;
                        treeView1.Nodes.Add(tn);
                        currentNode = tn;
                    }
                }
                else
                {
                    string cookedMethod = stacks[i].Trim(new char[] { '\r', ' ' });
                    if (currentNode != null && !String.IsNullOrEmpty(cookedMethod)) currentNode.Nodes.Add(new TreeNode(cookedMethod));
                }
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            // Check that this is a method (treeview level >0)
            if (treeView1.SelectedNode.Level > 0)
            {
                // Construct a Method object from a string describing a method
                int sigLength = treeView1.SelectedNode.Text.IndexOf(")");
                Method m = new Method(treeView1.SelectedNode.Text.Substring(0,sigLength));
                if (treeView1.SelectedNode.Text.Length > sigLength)
                {
                    //we have src!!
                    int lastColon = treeView1.SelectedNode.Text.LastIndexOf(":");
                    m.SourceFile = treeView1.SelectedNode.Text.Substring(sigLength + 1,(lastColon-sigLength-1));
                    // TODO! m.SourceLine = Int32.TryParse(treeView1.SelectedNode.Text.Substring(lastColon + 1));
                }

                // Open this in Notepad, or reflector
                SourceCode.SourceHound sh = new SourceCode.SourceHound();
                try
                {
                    sh.OpenSourceFile(m, m_LoadedModules);
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
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                Clipboard.SetText(m_StackDump);
        }
    }
}
