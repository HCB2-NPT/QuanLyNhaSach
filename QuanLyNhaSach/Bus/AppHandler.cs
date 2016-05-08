using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyNhaSach.Bus
{
    public class AppHandler
    {
        public static void Shutdown(int exitcode = 0)
        {
            Application.Current.Shutdown(exitcode);
        }

        public static void VirtualWindowClose(Window window)
        {
            if (window.Visibility != Visibility.Hidden)
                window.Visibility = Visibility.Hidden;
        }

        public static void VirtualWindowOpen(Window window)
        {
            if (window.Visibility != Visibility.Visible)
                window.Visibility = Visibility.Visible;
        }

        public static void ProcessStart(params string[] processStartParameters)
        {
            if (processStartParameters.Length <= 1)
                return;
            var procList = Process.GetProcessesByName(processStartParameters[0]);
            if (procList.Count().Equals(0))
            {
                try
                {
                    if (processStartParameters.Length == 2)
                        Process.Start(processStartParameters[1]);
                    else if (processStartParameters.Length == 3)
                        Process.Start(processStartParameters[1], processStartParameters[2]);
                }
                catch
                {
                    Managers.ErrorManager.Current.OutLookError.Call();
                }
            }
            else
            {
                Assets.Scripts.User32DLL.SwitchToThisWindow(procList.First().MainWindowHandle, true);
            }
        }
    }
}
