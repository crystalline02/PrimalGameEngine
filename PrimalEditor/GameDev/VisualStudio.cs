using EnvDTE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using PrimalEditor.Utilities;
using System.Windows;
using System.Runtime.CompilerServices;

namespace PrimalEditor.GameDev
{
    static internal class VisualStudio
    {
        private static EnvDTE80.DTE2? _vsInstance = null;

        private static string _vsProgramId = "VisualStudio.DTE.17.0";

        [DllImport("Ole32.dll")]
        static extern uint GetRunningObjectTable(uint reserved, out IRunningObjectTable rot);

        [DllImport("Ole32.dll")]
        static extern uint CreateBindCtx(uint reserved, out IBindCtx ctx);
        public static void OpenVisualStudio(string solutionPath)
        {
            if (_vsInstance == null)
            {
                IRunningObjectTable? rot = null;
                IEnumMoniker? eMoniker = null;
                IMoniker[] currentMoniker = new IMoniker[1];
                IBindCtx? ctx = null;
                uint hResult = 0;
                try
                {
                    hResult = GetRunningObjectTable(0, out rot);  // rot可以通过GetRunningObjectTable拿到，也可以通过ctx.GetRunningObjectTable拿到
                    if (hResult != 0 || rot== null)
                        throw new COMException("Failed to get running object table");
                    rot.EnumRunning(out eMoniker);
                    if(eMoniker == null)
                        throw new COMException("Failed to get a IEnumMoniker");

                    while(eMoniker.Next(1, currentMoniker, IntPtr.Zero) == 0)
                    {
                        if(CreateBindCtx(0, out ctx) != 0)
                            throw new COMException("Failed to get a Binding context");

                        if(ctx == null)
                            throw new COMException("Failed to get a Binding context");
                        currentMoniker[0].GetDisplayName(ctx, null, out string displayName);
                        Debug.WriteLine(displayName);
                        if(displayName.Contains(_vsProgramId))
                        {
                            rot.GetObject(currentMoniker[0], out object tmp);
                            EnvDTE80.DTE2? dte = tmp as EnvDTE80.DTE2;
                            if (dte == null)
                                throw new COMException("Failed to get the acutal running object as EnvDTE80.DTE2");
                            //Debug.WriteLine(dte.Solution.FullName);
                            if (dte.Solution.FullName == solutionPath)
                            {
                                _vsInstance = dte;  // Found!

                                CleanNotExitsFiles();
                                break;
                            }
                        }
                    }

                    if (_vsInstance == null)
                    {
                        Type? t = Type.GetTypeFromProgID(_vsProgramId);
                        Debug.Assert(t != null);
                        _vsInstance = Activator.CreateInstance(t) as EnvDTE80.DTE2;            
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Faild to open visual studio.\n" + ex.Message);
                }
                finally
                {
                    Marshal.ReleaseComObject(rot);
                    Marshal.ReleaseComObject(eMoniker);
                    Marshal.ReleaseComObject(ctx);
                }
            }

            _vsInstance.MainWindow.Activate();  // When Activate is invoked, it is as if the user clicked the item, but no click events occur.
            _vsInstance.MainWindow.Visible = true;  // The Find/Replace utility does not search windows that are not visible in open documents, even if the document associated with the window is still open.
            if (!_vsInstance.Solution.IsOpen)
                _vsInstance.Solution.Open(solutionPath);
        }

        public static void CleanNotExitsFiles()
        {
            // Still have no ideal how to do this
        }


        public static void CloseVisualStudio()
        {
            if(_vsInstance != null)
            {
                if (_vsInstance.Solution != null)
                {
                    _vsInstance.ExecuteCommand("File.SaveAll");
                    _vsInstance.Solution.Close(true);
                }
                _vsInstance.Quit();
            }
        }

        public static bool AddScriptToSolution(string cppPath, string headerPath, string solutionPath)
        {
            OpenVisualStudio(solutionPath);
            if (_vsInstance == null)
                return false;
            try
            {
                // VisualStudio solution needs to be vaild, so open VisualStudio and it's solution first.
                string projectName = System.IO.Path.GetFileNameWithoutExtension(solutionPath);
                if (!_vsInstance.Solution.IsOpen)
                    _vsInstance.Solution.Open(solutionPath);

                _vsInstance.ExecuteCommand("File.SaveAll");

                foreach(EnvDTE.Project p in _vsInstance.Solution.Projects)
                {
                    if(p.UniqueName.Contains(projectName))
                    {
                        p.ProjectItems.AddFromFile(cppPath);
                        p.ProjectItems.AddFromFile(headerPath);
                        break;
                    }
                }
                _vsInstance.ItemOperations.OpenFile(cppPath, EnvDTE.Constants.vsViewKindTextView);
                return true;
            }
            catch(Exception ex)
            {
                Logger.Log("An eror happend during adding new script:\n" + ex.Message, MessageType.Error);
                return false;
            }
        }
    }
}
