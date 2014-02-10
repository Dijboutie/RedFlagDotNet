using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedFlag.ExceptionInfo;

namespace RedFlag
{
    public partial class HeapStatsWindow : Form
    {
        private HeapStatistics m_HeapStats;
        public HeapStatsWindow(HeapStatistics HeapStats)
        {
            InitializeComponent();
            listView1.ListViewItemSorter = new ListViewColumnSorter();
            m_HeapStats = HeapStats;
        }

        private void HeapStatsWindow_Load(object sender, EventArgs e)
        {
            // put stuff in things
            foreach (HeapStatistic kvp in m_HeapStats)
            {
            ListViewItem lvi=new ListViewItem(new string[]{kvp.TypeName,kvp.TypeCount.ToString(),kvp.TypeSize.ToString()});
            listView1.Items.Add(lvi);
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
