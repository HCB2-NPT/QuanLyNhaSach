using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF.MDI;

namespace QuanLyNhaSach.Bus
{
    public class AppHandler
    {
        #region Application
        public static void Shutdown(int exitcode = 0)
        {
            Application.Current.Shutdown(exitcode);
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

        public static void AppCantOpenMoreTimes()
        {
            var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length > 1)
                Managers.ErrorManager.Current.CantOpenAppMoreTimes.Call();
        }

        public static void SetupAutoUpdaters()
        {
            //update: bills
            new Assets.Scripts.AutoUpdate(new TimeSpan(0, 10, 0), u =>
            {
                Managers.ErrorManager.Current.Ignore = true;
                Adapters.BillAdapter.FixedBillsOverTime();
                Managers.ErrorManager.Current.Ignore = false;
            });

            //update: addedbooks
            new Assets.Scripts.AutoUpdate(new TimeSpan(0, 10, 0), u =>
            {
                Managers.ErrorManager.Current.Ignore = true;
                var adds = Adapters.ManagerListAddedBookAdapter.GetWaitForImport();
                var now = DateTime.Now;
                if (adds != null)
                {
                    foreach (var c in adds)
                    {
                        if (DateTime.Compare(now, c.DateAddIntoStorage) >= 0)
                        {
                            foreach (var b in Adapters.AddedBookAdapter.GetAllListAddedBook(c.ID))
                            {
                                Adapters.BookAdapter.UpdateNumber(b.Book.ID, Adapters.BookAdapter.GetNumber(b.Book.ID) + b.Number);
                            }
                            Adapters.ManagerListAddedBookAdapter.UpdateIsImported(c);
                        }
                    }
                }
                Managers.ErrorManager.Current.Ignore = false;
            });
        }
        #endregion

        #region Window
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

        public static void OpenWindowExt<T>(Window owner, ref T windowVar) where T : Window
        {
            if (windowVar == null)
            {
                windowVar = (T)Activator.CreateInstance(typeof(T));
                windowVar.Owner = owner;
                windowVar.Show();
            }
            Bus.AppHandler.VirtualWindowOpen(windowVar);
        }
        #endregion

        #region Tab (Mdi Child)
        private static int _id = 0;
        public static UserControl OpenTab(MdiContainer container, Type type, string title, bool canDuplicate)
        {
            if (type != null)
            {
                if (!canDuplicate)
                {
                    var type_string = type.ToString();
                    var first = container.Children.FirstOrDefault(o => o.Content.GetType().ToString() == type_string);
                    if (first != null)
                    {
                        first.Focus();
                        first.WindowState = WindowState.Normal;
                        return null;
                    }
                }
                else
                {
                    title += string.Format(" - {0}", ++_id);
                }
                var child = new WPF.MDI.MdiChild() { Title = title, Resizable = false };
                var content = (UserControl)Activator.CreateInstance(type);
                content.Tag = child;
                child.Tag = container;
                child.Content = content;
                container.Children.Add(child);
                return content;
            }
            return null;
        }

        public static void SelectTab(MdiContainer container, string title)
        {
            var child = container.Children.FirstOrDefault(x => x.Title == title);
            if (child != null)
            {
                child.WindowState = WindowState.Normal;
            }
        }
        #endregion
    }
}
