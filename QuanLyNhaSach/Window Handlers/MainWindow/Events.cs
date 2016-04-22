using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace QuanLyNhaSach.Windows
{
    public partial class MainWindow : Window
    {
        /*
         * Sự kiện đóng danh sách chức năng khi click ra ngoài
         */
        private void PanelFunctionClickOut(QuanLyNhaSach.Assets.Scripts.EventClickOut sender)
        {
            if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
        }

        /*
         * Sự kiện hiện danh sách chức năng khi chọn các chức năng chính bên cột menu phải
         */
        private void showFunctionList(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Manager.Data.Binding;
            if (tag != null)
            {
                listboxDSChucNang.ItemsSource = tag.Children as ObservableCollection<Manager.Data.Binding>;
                (this.FindResource("ShowFunctions") as Storyboard).Begin();
            }
            else if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
        }

        /*
         * Sự kiện hiển thị thanh công cụ
         */
        private void showToolBox(object sender, RoutedEventArgs e)
        {
            if (ToolBox == null)
            {
                ToolBox = new QuanLyNhaSach.Windows.Others.ToolBox();
                ToolBox.Show();
            }
            var p = Assets.Scripts.Cursor.GetCursorPosition();
            ToolBox.Left = p.X;
            ToolBox.Top = p.Y;
            if (!ToolBox.IsVisible)
                ToolBox.Visibility = System.Windows.Visibility.Visible;
            ToolBox.Focus();
        }

        /*
         * Sự kiện hiển thị About
         */
        private void showAbout(object sender, RoutedEventArgs e)
        {
            if (About == null)
            {
                About = new QuanLyNhaSach.Windows.Others.About();
                About.Show();
            }
            if (!About.IsVisible)
                About.Visibility = System.Windows.Visibility.Visible;
            About.Focus();
        }

        /*
         * Đóng danh sách chức năng khi chức năng được chọn
         */
        private void listboxDSChucNang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
            var item = listboxDSChucNang.SelectedItem as Manager.Data.Binding;
            if (item != null)
            {
                var tab = item.Tag as TabItem;
                if (tab != null)
                {
                    tab.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}
