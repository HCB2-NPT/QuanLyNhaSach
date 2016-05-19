using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class TitleMainRightClick
    {
        #region Button Events
        private void minimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Owner.WindowState = System.Windows.WindowState.Minimized;
            Bus.AppHandler.VirtualWindowClose(this);
        }

        private void maximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Owner.WindowState = System.Windows.WindowState.Maximized;
            Bus.AppHandler.VirtualWindowClose(this);
        }
        #endregion

        #region Window Events
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Bus.AppHandler.VirtualWindowClose(this);
        }
        
        private void close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Bus.AppHandler.Shutdown();
        }
        #endregion
    }
}
