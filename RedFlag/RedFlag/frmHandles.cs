using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedFlag.FileHandles;
using System.Diagnostics;

namespace RedFlag
{
    public partial class frmHandles : Form
    {
        private int m_ProcessId;
        public frmHandles(int ProcessId)
        {
            InitializeComponent();
            listView1.ListViewItemSorter = new ListViewColumnSorter();
            m_ProcessId = ProcessId;
        }

        private void frmHandles_Load(object sender, EventArgs e)
        {
            // get list o handles
            Process p = Process.GetProcessById(m_ProcessId);
            List<Win32API.SYSTEM_HANDLE_INFORMATION> handles = CustomAPI.GetHandles(p);
            for (int nDex = 0; nDex < handles.Count; nDex++)
            {
                string strName = String.Empty;
                FileDetails details = OpenHandles.GetFileDetails(handles[nDex]);
                strName = details.Name;
                if (!String.IsNullOrEmpty(strName))
                {
                    ListViewItem lvi=new ListViewItem(new string[] {strName,details.ObjectTypeName});
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
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
    }
}
