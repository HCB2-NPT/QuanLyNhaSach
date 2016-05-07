using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class WarningBox
    {
        #region Methods
        public static void Show(string title, string name, string content, bool isCrash = false, string exception = null)
        {
            var b = new WarningBox(title, name, content, isCrash, exception);
            b.ShowDialog();
        }
        #endregion

        #region Window Events
        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsCrash)
                Bus.AppHandler.Shutdown();
            else
                Dispose();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region HotKey Events
        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Close();
        }
        #endregion
    }
}
