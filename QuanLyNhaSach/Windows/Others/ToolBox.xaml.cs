using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyNhaSach.Windows.Others
{
    /// <summary>
    /// Interaction logic for ToolBox.xaml
    /// </summary>
    public partial class ToolBox : Window
    {
        public ToolBox()
        {
            InitializeComponent();
        }

        private void _ca_Click(object sender, RoutedEventArgs e)
        {
            var procList = Process.GetProcessesByName("OUTLOOK");
            if (procList.Count().Equals(0))
                try
                {
                    Process.Start("outlook", "/select outlook:calendar");
                }
                catch
                {
                    Manager.ErrorManager.Current.OutLookError.Call();
                }
            else
            {
                Assets.Scripts.User32DLL.SetForegroundWindow(procList.First().MainWindowHandle);
            }
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void _cal_Click(object sender, RoutedEventArgs e)
        {
            if (Process.GetProcessesByName("Calculator").Count().Equals(0))
                try
                {
                    Process.Start("calc.exe");
                }
                catch
                {
                    Manager.ErrorManager.Current.OutLookError.Call();
                }
            else
            {
                Assets.Scripts.User32DLL.SetForegroundWindow(Assets.Scripts.User32DLL.FindWindow(null, "Calculator"));
            }
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void _mes_Click(object sender, RoutedEventArgs e)
        {
            var procList = Process.GetProcessesByName("OUTLOOK");
            if (procList.Count().Equals(0))
                try
                {
                    Process.Start("outlook");
                }
                catch
                {
                    Manager.ErrorManager.Current.OutLookError.Call();
                }
            else
            {
                Assets.Scripts.User32DLL.SetForegroundWindow(procList.First().MainWindowHandle);
            }
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
