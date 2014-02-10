using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedFlag
{
    public class LoadedSessionInfo
    {
        private string m_OSVersion = String.Empty;
        private string m_DotNetVersions = String.Empty;
        private int m_Bitness = 0;
        private string m_CommandAndArgs = String.Empty;
        public string OSVersion {
            get
            {
                return m_OSVersion;
            }
            set
            {
                m_OSVersion = value;
            }
        }
        public int Bitness {
            get
            {
                return m_Bitness;
            }
            set
            {
                m_Bitness = value;
            }
        }
        public string DotNetVersions
        {
            get
            {
                return m_DotNetVersions;
            }
            set
            {
                m_DotNetVersions = value;
            }
        }
        public string CommandAndArgs
        {
            get
            {
                return m_CommandAndArgs;
            }
            set
            {
                m_CommandAndArgs = value;
            }
        }
        public bool IsClean
        {
            get
            {
                if (String.IsNullOrEmpty(m_CommandAndArgs) &&
                    String.IsNullOrEmpty(m_DotNetVersions) &&
                    String.IsNullOrEmpty(m_OSVersion) &&
                    m_Bitness == 0) return true;
                else return false;
            }
        }
        public void Clear()
        {
            m_CommandAndArgs = String.Empty;
            m_DotNetVersions = String.Empty;
            m_OSVersion = String.Empty;
            m_Bitness = 0;
        }
    }
}
