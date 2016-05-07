using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class ToolBox
    {
        #region Window Events
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

        #region Button Events
        private void _ca_Click(object sender, RoutedEventArgs e)
        {
            Bus.AppHandler.ProcessStart("OUTLOOK", "outlook", "/select outlook:calendar");
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void _cal_Click(object sender, RoutedEventArgs e)
        {
            Bus.AppHandler.ProcessStart("Calculator", "calc.exe");
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void _mes_Click(object sender, RoutedEventArgs e)
        {
            Bus.AppHandler.ProcessStart("OUTLOOK", "outlook");
            Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion
    }
}
