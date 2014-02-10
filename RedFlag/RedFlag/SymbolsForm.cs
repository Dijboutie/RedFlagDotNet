using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.SymbolStore;
using Microsoft.Samples.Debugging.CorSymbolStore;
using RedFlag.Symbols;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RedFlag
{
    [Flags]
    public enum PdbFailureCode : uint
    {
    E_PDB_OK=0x806d0001,
    E_PDB_USAGE=0x806d0002,
    E_PDB_OUT_OF_MEMORY=0x806d0003,
    E_PDB_FILE_SYSTEM=0x806d0004,
    E_PDB_NOT_FOUND=0x806d0005,
    E_PDB_INVALID_SIG=0x806d0006,
    E_PDB_INVALID_AGE=0x806d0007,
    E_PDB_PRECOMP_REQUIRED=0x806d0008,
    E_PDB_OUT_OF_TI=0x806d0009,
    E_PDB_NOT_IMPLEMENTED=0x806d0010,
    E_PDB_V1_PDB=0x806d0011,
    E_PDB_FORMAT=0x806d0012,
    E_PDB_LIMIT=0x806d0013,
    E_PDB_CORRUPT=0x806d0014,
    E_PDB_TI16=0x806d0015,
    E_PDB_ACCESS_DENIED=0x806d0016,
    E_PDB_ILLEGAL_TYPE_EDIT=0x806d0017,
    E_PDB_INVALID_EXECUTABLE=0x806d0018,
    E_PDB_DBG_NOT_FOUND=0x806d0019,
    E_PDB_NO_DEBUG_INFO=0x806d0020,
    E_PDB_INVALID_EXE_TIMESTAMP=0x806d0021,
    E_PDB_RESERVED=0x806d0022,
    E_PDB_DEBUG_INFO_NOT_IN_PDB=0x806d0023,
    E_PDB_SYMSRV_BAD_CACHE_PATH=0x806d0024,
    E_PDB_SYMSRV_CACHE_FULL=0x806d0025,
    E_PDB_MAX = 0x806d0026
    }
    public partial class SymbolsForm : Form
    {
        private List<RedFlag.Module> m_Modules;
        public SymbolsForm(List<RedFlag.Module> modules)
        {
            InitializeComponent();
            m_Modules = modules;
            lvSymbolDocs.ListViewItemSorter = new ListViewColumnSorter();
            // For each PDB found, get a list of methods and their associated source code
            foreach (RedFlag.Module module in m_Modules)
            {
                if (module.SymbolFile != null)
                {
                    ISymbolReader reader = null;
                    try
                    {
                        reader = SymUtil.GetSymbolReaderForFile(module.FileName, null);
                    }
                    catch (COMException) {

                    }
                    // if PDB is messed up, reader returns null
                    if (reader == null)
                    {
                        PdbFailureCode fail = (PdbFailureCode)SymUtil.GetLastReaderFailureCode(module.FileName);
                        //MessageBox.Show(String.Format(
                        //  "Failed to load symbols for {0}: {1} ({2})\r\nPlease note that you will always get \"Not Found\" under UAC if you are not running as Administrator.",
                        //  module.FileName,
                        //  fail,
                        //  fail.ToString("X")));
                        //return;
                        string[] docInfo = new string[2]; // PDB then source code
                        docInfo[0] = module.FileName.Substring(0,module.FileName.LastIndexOf('.'))+".pdb";
                        docInfo[1] = "Symbol load failure: "+fail.ToString("G");
                        ListViewItem LVI = new ListViewItem(docInfo);
                        lvSymbolDocs.Items.Add(LVI);
                    }
                    else
                    {
                        ISymbolDocument[] docs = reader.GetDocuments();
                        foreach (ISymbolDocument doc in docs)
                        {
                            string[] docInfo = new string[2]; // PDB then source code
                            docInfo[0] = module.SymbolFile;
                            docInfo[1] = doc.URL;
                            ListViewItem LVI = new ListViewItem(docInfo);
                            lvSymbolDocs.Items.Add(LVI);
                        }
                    }
                }
            }
        }

        private void f1Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                //ListView
                if (sender.GetType().Equals(typeof(ListView))) Program.CopyListViewToClipboard((ListView)sender);
            }
        }
        private void lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;
            ListViewColumnSorter sorter = (ListViewColumnSorter) myListView.ListViewItemSorter;
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
    }
}
