using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyNhaSach.Windows
{
    public partial class MainWindow : Window
    {
        /*
         * Sự kiện double click vào title
         */
        private void TitleMainDoubleClick(Assets.Scripts.EventDoubleClick sender)
        {
            MinimizeAndMaximize();
        }

        /*
         * Sự kiện click phải chuột vào title
         */
        private void TitleMain_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TitleMainRightClick == null)
            {
                TitleMainRightClick = new QuanLyNhaSach.Windows.Others.TitleMainRightClick(this);
                TitleMainRightClick.Show();
            }
            var p = Assets.Scripts.Cursor.GetCursorPosition();
            TitleMainRightClick.Left = p.X;
            TitleMainRightClick.Top = p.Y;
            if (!TitleMainRightClick.IsVisible)
                TitleMainRightClick._Show();
            TitleMainRightClick.Focus();
        }

        /*
         * Sự kiện minimize
         */
        private void Minisize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        /*
         * Sự kiện maximize/restore
         */
        private void btnMaxMin_Click(object sender, RoutedEventArgs e)
        {
            MinimizeAndMaximize();
        }

        /*
         * Sự kiện close
         */
        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
