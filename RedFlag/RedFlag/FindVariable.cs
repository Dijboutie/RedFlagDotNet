using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;

namespace RedFlag
{
    public delegate void VariableFoundEvent(object o, ExceptionEventArgs exc);
    public delegate void ProgressBarEvent(object o, ProgressBarArgs e);
    public partial class FindVariable : Form
    {
        public event VariableFoundEvent VariableFound;
        public event ProgressBarEvent UpdateProgress;
        private enum VarSearchType : long
        {
            None=0,
            ByName=1,
            ByValue=2,
            ByNameAndValue=4
        }
        private List<RedFlag.Exception> m_ExceptionList;
        private bool IamSearching = false;
        private List<RedFlag.Exception> m_ResultList;
        public event MethodChosenEventHandler MethodChosen;
        private VarSearchType m_VarSearchType;
        private string m_tbValText = String.Empty;
        private string m_tbNameText = String.Empty;
        private Thread m_WorkerThread;
        public FindVariable(List<RedFlag.Exception> ListOfExceptions)
        {
            InitializeComponent();
            lvFindResult.ListViewItemSorter = new ListViewColumnSorter();
            m_ExceptionList = ListOfExceptions;
        }
        /// <summary>
        /// Find the Exception ID and method containing the variable with the specified value
        /// </summary>
        /// <param name="RegularExpression"></param>
        /// <returns>A list of cloned exceptions <ExceptionID, method></returns>
        private void FindVariableByValue()
        {
            //List<RedFlag.Exception> exceptionList = new List<RedFlag.Exception>();
            //Dictionary<RedFlag.Exception, RedFlag.Method> methodList= new Dictionary<RedFlag.Exception, RedFlag.Method>();
            Regex valueExpression = new Regex(m_tbValText,RegexOptions.IgnoreCase);
            Regex nameExpression = new Regex(m_tbNameText, RegexOptions.IgnoreCase);
            RedFlag.Method m = new Method();
            //pbSearchProgress.Maximum = m_ExceptionList.Count;
            int searchProgress=0;
            foreach (RedFlag.Exception exception in m_ExceptionList)
            {
                foreach (RedFlag.Method method in exception.Methods)
                {
                    foreach (StackObject sObject in method.PrivateMembers)
                    {
                        bool valueMatches=valueExpression.IsMatch(sObject.Value);
                        bool nameMatches = nameExpression.IsMatch(sObject.Name);
                        if (
                            ((m_VarSearchType & VarSearchType.ByValue) == VarSearchType.ByValue && valueMatches)
                            || ((m_VarSearchType & VarSearchType.ByName) == VarSearchType.ByName && nameMatches)
                            || m_VarSearchType==VarSearchType.ByNameAndValue && nameMatches && valueMatches
                            )
                        {
                            RedFlag.Exception clone = new RedFlag.Exception();
                            clone.Name = exception.Name;
                            clone.GUID = exception.GUID;
                            //clone.Methods.Add(method);
                            if (VariableFound != null) VariableFound(this, new ExceptionEventArgs(clone, sObject,method.Signature));
                        }
                    }
                    if (!IamSearching) break;
                }
                //pbSearchProgress.Value++;
                searchProgress++;
                if (UpdateProgress != null) UpdateProgress(this, new ProgressBarArgs(searchProgress));
                if (!IamSearching) break;
            }
            if (UpdateProgress != null) UpdateProgress(this, new ProgressBarArgs(0));
           // return exceptionList;
        }
        private void ConditionalSearchForVariables()
        {
            lvFindResult.Items.Clear();
            switch ((string)cbLogicalOp.Text)
            {
                case "And":
                    bool queryName=!String.IsNullOrEmpty(tbNameRegex.Text);
                    bool queryValue=!String.IsNullOrEmpty(tbValueRegexp.Text);
                    if (queryName && queryValue) SearchForVariables(VarSearchType.ByNameAndValue);
                    else
                    {
                        if (queryName) SearchForVariables(VarSearchType.ByName);
                        if (queryValue) SearchForVariables(VarSearchType.ByValue);
                    }
                    break;
                case "Or":
                    VarSearchType searchType = VarSearchType.None;
                    if (!String.IsNullOrEmpty(tbNameRegex.Text)) searchType = VarSearchType.ByName;
                    if (!String.IsNullOrEmpty(tbValueRegexp.Text)) searchType = searchType | VarSearchType.ByValue;
                    SearchForVariables(searchType);
                    break;
                default:
                    break;
            }
        }
        private void SearchForVariables(VarSearchType SearchType)
        {
            pbSearchProgress.Value = 0;
            //lvFindResult.Items.Clear();
            IamSearching = true;
            this.VariableFound += new VariableFoundEvent(FindVariable_VariableFound);
            this.UpdateProgress += new ProgressBarEvent(FindVariable_UpdateProgress);
            m_VarSearchType = SearchType;
            m_tbNameText = tbNameRegex.Text;
            m_tbValText = tbValueRegexp.Text;
            pbSearchProgress.Maximum = m_ExceptionList.Count;
            try
            {
                m_WorkerThread = new Thread(new ThreadStart(FindVariableByValue));
                m_WorkerThread.IsBackground = true;
                m_WorkerThread.Start();
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show("Error: "+aex.Message, "Problem with regular expression");
                return;
            }
        }
        public delegate void ThreadSafeProgressUpdate(object o, ProgressBarArgs e);
        void FindVariable_UpdateProgress(object o, ProgressBarArgs e)
        {
            if (statusStrip1.InvokeRequired)
            {
                this.Invoke(new ThreadSafeProgressUpdate(FindVariable_UpdateProgress), statusStrip1, e);
                return;
            }
            pbSearchProgress.Value = e.CurrentProgress;
        }
        public delegate void ThreadSafeVariableFound(object o, ExceptionEventArgs e);
        void FindVariable_VariableFound(object o, ExceptionEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ThreadSafeVariableFound(FindVariable_VariableFound), this, e);
                return;
            }
            RedFlag.Exception exc = e.RFException;
            StackObject so = e.Variable;
            string methodName = e.MethodName;
                    string variableValue = so.Value;
                    ListViewItem lvi = new ListViewItem(new string[] { so.Name, variableValue, exc.Name, methodName });
                    lvi.Tag = exc.GUID;
                    // do not add if all three things already exist
                    if (!lvFindResult.Items.Contains(lvi))
                    lvFindResult.Items.Add(lvi);
                    
                
            
        }

        private void FindVariable_FormClosing(object sender, FormClosingEventArgs e)
        {
            IamSearching = false;
            //wait for ze threadsz
            if (m_WorkerThread!=null) m_WorkerThread.Abort();
        }

        private void lvFindResult_DoubleClick(object sender, EventArgs e)
        {
            // a method has been chosen - get the Tag, which is a guid, and return that
            if (MethodChosen != null)
            {
                Guid chosenGuid=(Guid)lvFindResult.SelectedItems[0].Tag;
                string chosenMethod=lvFindResult.SelectedItems[0].SubItems[3].Text;
                pbSearchProgress.Value = 0;
                foreach (RedFlag.Exception except in m_ExceptionList)
                {
                    if (except.GUID == chosenGuid && except.Methods[0].Signature==chosenMethod)
                    {
                        MethodChosenEventArgs args = new MethodChosenEventArgs(except);
                        MethodChosen(this, args);
                        break;
                    }
                    pbSearchProgress.Value++;
                }
                pbSearchProgress.Value = 0;
            }
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

        private void tbNameRegex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ConditionalSearchForVariables();
            e.Handled = true;
        }

        private void tbValueRegexp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ConditionalSearchForVariables();
            e.Handled = true;
        }
   }
    public class ExceptionEventArgs : EventArgs
    {
        private RedFlag.Exception m_Exception;
        private StackObject m_StackObject;
        private string m_MethodName = String.Empty;
        public ExceptionEventArgs(RedFlag.Exception RFException, StackObject RFStackObject, string MethodName)
        {
            m_Exception = RFException;
            m_StackObject = RFStackObject;
            m_MethodName = MethodName;
        }
        public StackObject Variable
        {
            get
            {
                return m_StackObject;
            }
            set
            {
                m_StackObject = value;
            }
        }
        public RedFlag.Exception RFException
        {
            get
            {
                return m_Exception;
            }
            set
            {
                m_Exception = value;
            }
        }
        public string MethodName
        {
            get
            {
                return m_MethodName;
            }
            set
            {
                m_MethodName = value;
            }
        }
    }
    public delegate void MethodChosenEventHandler(object o,MethodChosenEventArgs e);
    public class MethodChosenEventArgs : EventArgs
    {
        private RedFlag.Exception m_ExceptionDetails;
        public MethodChosenEventArgs(RedFlag.Exception exc)
        {
            m_ExceptionDetails = exc;
        }
        public RedFlag.Exception ExceptionDetails
        {
            get
            {
                return m_ExceptionDetails;
            }
            set
            {
                m_ExceptionDetails = value;
            }
        }
    }
    public class ProgressBarArgs : EventArgs
    {
        private int m_CurrentProgress = 0;
        public ProgressBarArgs(int CurrentProgress)
        {
            m_CurrentProgress = CurrentProgress;
        }
        public int CurrentProgress
        {
            get
            {
                return m_CurrentProgress;
            }
            set
            {
                m_CurrentProgress = value;
            }
        }
    }
 }
