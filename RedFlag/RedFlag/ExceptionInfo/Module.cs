using System;
using System.Collections.Generic;
using System.Text;

namespace RedFlag
{
    [Serializable]
    public class Module
    {
        private string m_ModuleName = String.Empty;
        private string m_FileName=String.Empty;
        private string m_SymbolPath = String.Empty;
        public Module() { }
        public Module(string name,string path,string symbols)
        {
            m_ModuleName = name;
            m_FileName = path;
            m_SymbolPath = symbols;
        }
        public string Name
        {
            get
            {
                return m_ModuleName;
            }
            set
            {
                m_ModuleName = value;
            }
        }
        public string FileName
        {
            get
            {
                return m_FileName;
            }
            set
            {
                m_FileName = value;
            }
        }
        public string SymbolFile
        {
            get
            {
                return m_SymbolPath;
            }
            set
            {
                m_SymbolPath = value;
            }
        }
        public override string ToString()
        {
            return m_ModuleName;
        }
    }
}
