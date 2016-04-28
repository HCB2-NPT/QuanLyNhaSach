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
            listboxDSChucNang.SelectedItem = null;
            if (item != null)
            {
                var type = item.Tag as Type;
                var key = item.Key;
                if (type != null)
                {
                    if (!key)
                    {
                        var type_string = type.ToString();
                        var first = mdiContainer.Children.FirstOrDefault(o => o.Content.GetType().ToString() == type_string);
                        if (first != null)
                        {
                            first.Focus();
                            return;
                        }
                    }
                    var mdiChild = new WPF.MDI.MdiChild() { Content = (UIElement)Activator.CreateInstance(type), Title = item.Data as string };
                    mdiChild.WindowState = System.Windows.WindowState.Maximized;
                    mdiContainer.Children.Add(mdiChild);
                }
            }
        }

        /*
         * Sự kiện khi thêm xóa một child vào mdi container
         */
        void mdiContainer_ChildrenChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("ShowMdiContainer");
        }

        /*
         * Hiện bảng Setting
         */
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Manager.ErrorManager.Current.AppCanNotUseNow.Call();
        }

        /*
         * chạy ngay khi đăng nhập vào
         */
        private void window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;
            if (!oldValue && newValue)
                HandleAfterLogin();
        }
    }
}
