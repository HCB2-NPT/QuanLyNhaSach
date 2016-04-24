using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyNhaSach.Windows
{
    public partial class MainWindow : Window
    {
        /*
         * Sự kiện đăng xuất
         */
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Hidden;
            Manager.Manager.Current.User.Logout();
            Owner.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
