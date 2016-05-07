using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class TitleMainRightClick
    {
        #region Methods
        public void _Show()
        {
            minimize.IsEnabled = !(Host.WindowState == System.Windows.WindowState.Minimized);
            maximize.IsEnabled = !(Host.WindowState == System.Windows.WindowState.Maximized);
            Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        #region Button Events
        private void minimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Host.WindowState = System.Windows.WindowState.Minimized;
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void maximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Host.WindowState = System.Windows.WindowState.Maximized;
            Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

        #region Window Events
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Visibility = System.Windows.Visibility.Hidden;
        }
        
        private void close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Bus.AppHandler.Shutdown();
        }
        #endregion
    }
}
