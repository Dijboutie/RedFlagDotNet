using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace RedFlag.SourceCode
{
    /// <summary>
    /// Class that gets the source code - if there is none try Reflector
    /// </summary>
    public class SourceHound
    {
        private string m_Reflectorpath = String.Empty;
        public bool ReflectorInstalled = false;
        public SourceHound()
        {
            // get the path to Reflector
            try
            {
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"Applications\Reflector.exe\shell\open\command");
                if (key != null)
                {
                    m_Reflectorpath = (string)key.GetValue("");
                    m_Reflectorpath = m_Reflectorpath.Substring(1);
                    m_Reflectorpath = m_Reflectorpath.Substring(0, m_Reflectorpath.IndexOf('\"'));
                    ReflectorInstalled = true;
                }
            }
            catch { }

        }
        public void OpenSourceFile(Method TargetMethod, List<Module> LoadedModules)
        {
            if (ReflectorInstalled && (
                String.IsNullOrEmpty(TargetMethod.SourceFile) ||
                !File.Exists(TargetMethod.SourceFile)
                ))
            {
                // Find the module that the source should be in
                string moduleFile = GetModuleContainingMethod(TargetMethod.Name, LoadedModules);
                if (String.IsNullOrEmpty(moduleFile)) throw new ArgumentNullException("Target module could not be found.\r\nEither the module is not on disk or has an incompatible runtime version");
                try
                {
                    string methodNameSpace = TargetMethod.Name.Substring(0, TargetMethod.Name.LastIndexOf('.'));
                    StringBuilder argBuilder=new StringBuilder(50);
                    argBuilder.Append("(");
                    for (int i=0;i<TargetMethod.Arguments.Count;i++)
                    {
                        string arg=TargetMethod.Arguments[i].Type;
                        //have to remove "System" from base types
                        if (arg.Contains("."))
                            argBuilder.Append(arg.Substring(arg.LastIndexOf('.')+1));
                        else argBuilder.Append(arg);
                        if (i < TargetMethod.Arguments.Count - 1) argBuilder.Append(", ");
                    }
                    argBuilder.Append(")");
                    string methodName = TargetMethod.Name.Substring(methodNameSpace.Length+1)+argBuilder.ToString();
                    OpenInReflector(moduleFile, methodNameSpace,methodName);
                }
                catch (System.Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(TargetMethod.SourceFile)
                    && File.Exists(TargetMethod.SourceFile))
                OpenInNotepad(TargetMethod.SourceFile);
            }
        }
        public void OpenInNotepad(string SourceFile)
        {
            string notePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.System),
                "notepad.exe");
            ExecuteNonShell(notePath, SourceFile);
        }
        public void OpenInReflector(string ModuleFileName, string MethodNameSpace, string MethodName)
        {
            string reflectorArgs = String.Format(
           "/share /select:\"code://locatedAssembly:{0}/{1}/{2}\"",
            ModuleFileName, MethodNameSpace,MethodName);
            ExecuteNonShell(m_Reflectorpath, reflectorArgs);
        }
        private void ExecuteNonShell(string Command, string Arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo(Command, Arguments);
            psi.UseShellExecute = false;
            Process.Start(psi);
        }
        private string GetModuleContainingMethod(string MethodName, List<Module> ModuleList)
        {
            String moduleFileName = String.Empty;
            System.AppDomain tempDomain=System.AppDomain.CreateDomain("ReflectionOnly");
            string methodTypeName=MethodName.Substring(0,MethodName.LastIndexOf('.'));
            string methodMethodName=MethodName.Substring(MethodName.LastIndexOf("."));
            foreach (Module mod in ModuleList)
            {
                if (File.Exists(mod.FileName))
                {
                    // load module and reflect for method
                    try
                    {
                        System.Runtime.Remoting.ObjectHandle typHandle = tempDomain.CreateInstanceFrom(mod.FileName, methodTypeName);
                        if (typHandle != null)
                        {
                            moduleFileName = mod.FileName;
                            // If the type contains the method, break here -- otherwise keep going
                            // We'll take the type name because sometimes the type can't be serialized
                            // and we won't know if the method name exists or not.
                            try
                            {
                                Type typ = typHandle.Unwrap().GetType();
                                if (typ.GetMember(methodMethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null)
                                break;
                            }
                            catch (SerializationException)
                            {

                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        //this assembly was not on disk
                    }
                    catch (TypeLoadException)
                    {
                        // assembly was loaded, but type was not inside
                    }
                    catch (MissingMethodException)
                    {
                        // Parameterless constructor required on type
                        moduleFileName = mod.FileName;
                    }
                    catch (MethodAccessException)
                    {
                        // Insufficient permissions
                    }
                    catch (BadImageFormatException)
                    {
                        // module needs to be compat with runtime version
                    }
                    catch (FileLoadException)
                    {//vcruntime dll
                    }
                }
            }
            System.AppDomain.Unload(tempDomain);
            return moduleFileName;
        }
    }
}
