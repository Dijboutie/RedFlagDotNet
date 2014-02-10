using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using RedFlag.Engine;

namespace RedFlag
{
    [Serializable]
    public class RedFlagResults
    {
        private string m_ProgramNameAndArgs = String.Empty;
        private string m_DotNetVersion = String.Empty;
        private string m_OSVersion = String.Empty;
        private int m_Bitness = 0;
        public List<RedFlag.Exception> Exceptions { get; set; }
        public List<RedFlag.Module> Modules { get; set; }
        public List<RedFlag.TraceMessage> Messages { get; set; }
        public AppDomains AppDomainList { get; set; }
        /// <summary>
        /// The command-line of the app being debugged
        /// </summary>
        [XmlAttribute]
        public string ProgramName
        {
            get
            {
                return m_ProgramNameAndArgs;
            }
            set {
                m_ProgramNameAndArgs = value;
            }
        }
        /// <summary>
        /// Version string of the operating system
        /// </summary>
        [XmlAttribute]
        public string OSVersion
        {
            get
            {
                if (!String.IsNullOrEmpty(m_OSVersion)) return m_OSVersion;
                else return DebugEngineUtils.GetWindowsVersion();
            }
            set 
            {
                m_OSVersion = value;
            }
        }
        /// <summary>
        /// The version(s) of .NET Framework in the process being debugged
        /// </summary>
        [XmlAttribute]
        public string DotNetVetsion
        {
            get
            {
                if (!String.IsNullOrEmpty(m_DotNetVersion)) return m_DotNetVersion;
                else{
                string modList = String.Empty;
                if (Modules != null) // IF WE HAVE A MODULE LIST, RETURN ALL THE MSCORLIBS WE CAN FIND
                {
                    List<Module> modules = Modules.FindAll(delegate(Module module)
                    {
                        return module.Name.StartsWith("mscorlib");
                    });
                    foreach (Module mod in modules)
                    {
                       /* Assembly a = Assembly.Load(mod.Name); */
                        try
                        {
                            string version = DebugEngineUtils.GetAssemblyRuntimeVersion(mod.FileName, System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion);
                            modList += version + ",";
                        }
                        catch
                        {
                            modList += "Unknown";
                        }
                    }
                }
                return modList.TrimEnd(',');
                }
            }
            set
            {
                m_DotNetVersion=value;
            }
        }
        /// <summary>
        /// Tells us if our process is 64 or 32-bit. The debugged process should also match this.
        /// </summary>
        [XmlAttribute]
        public int Bitenss
        {
            get
            {
                if (m_Bitness > 0) return m_Bitness;
                else return DebugEngineUtils.IAm64Bit() ? 64 : 32;
            }
            set
            {
                m_Bitness = value;
            }
        }
        /*#region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Exceptions", Exceptions);
            info.AddValue("Modules", Modules);
        }

        #endregion*/
        /// <summary>
        /// Some stack values can't be serialized, so replace those.
        /// </summary>
        /// <param name="exceptions"></param>
        public void Verify()
        {
            foreach (RedFlag.Exception ex in Exceptions)
            {
                foreach (RedFlag.Method m in ex.Methods)
                {
                    foreach (StackObject so in m.PrivateMembers)
                    {
                        if (!CanSerialize(so.Value))
                            so.Value = "Cannot serialize";
                    }
                }
            }
        }
        private bool CanSerialize(object Value)
        {
            try
            {
                // To work around SmartAssembly string obfuscation / serialization
                // I rate this code as "sucky".
                System.IO.MemoryStream stm = new System.IO.MemoryStream();
                System.Xml.XmlTextWriter w = new System.Xml.XmlTextWriter(stm, System.Text.Encoding.UTF8);
                w.WriteString(Value.ToString());
                w.Close();
                stm.Dispose();
            }
            catch { return false; }
            return true;
        }
    }
}
