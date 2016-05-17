﻿using QuanLyNhaSach.Objects;
using QuanLyNhaSach.Views.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WPF.MDI;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class MainWindow
    {
        #region Methods
        /*
         * Show/Hide a function
         */
        private void ShowFunction(FrameworkElement controlFull, FrameworkElement controlMini, bool isShow, object tag = null)
        {
            if (isShow)
            {
                controlFull.Height = 40;
                controlFull.Visibility = System.Windows.Visibility.Visible;
                controlMini.Height = 40;
                controlMini.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                controlFull.Height = 0;
                controlFull.Visibility = System.Windows.Visibility.Hidden;
                controlMini.Height = 0;
                controlMini.Visibility = System.Windows.Visibility.Hidden;
            }
            controlFull.Tag = tag;
            controlMini.Tag = tag;
        }

        /*
         * Xử lý sau khi login
         */
        private void HandleAfterLogin()
        {
            if (Managers.UserManager.Current.Info.AccessLevel.ID == 3)
            {
                ShowFunction(btnSachFull, btnSachMini, false);
                ShowFunction(btnHoaDonFull, btnHoaDonMini, false);
                ShowFunction(btnKhachHangFull, btnKhachHangMini, false);
                ShowFunction(btnTaiKhoanFull, btnTaiKhoanMini, true);
                ShowFunction(btnBaoCaoFull, btnBaoCaoMini, false);
                ShowFunction(btnQuyDinhFull, btnQuyDinhMini, false);
            }
            else if (Managers.UserManager.Current.Info.AccessLevel.ID == 2)
            {
                var _book = new Binding();
                {
                    _book.Children.Add(new Binding("Thư viện") { Tag = typeof(tabQuanLySach) });
                    _book.Children.Add(new Binding("Thể loại") { Tag = typeof(tabTheLoai) });
                    _book.Children.Add(new Binding("Tác Giả") { Tag = typeof(tabTacGia) });
                    _book.Children.Add(new Binding("Nhập Sách") { Tag = typeof(tabPhieuNhapSach) });

                }
                var _bill = new Binding();
                {
                    _bill.Children.Add(new Binding("Lập Hóa Đơn") { Tag = typeof(tabThemHoaDonMoi), Key = true });
                    _bill.Children.Add(new Binding("Hóa Đơn Cũ") { Tag = typeof(tabQuanLyHoaDonCu) });
                }
                var _customer = new Binding();
                {
                    _customer.Children.Add(new Binding("Khách hàng") { Tag = typeof(tabKhachHang) });
                    _customer.Children.Add(new Binding("Đòi nợ") { Tag = typeof(tabPhieuThuTien) });
                }
                var _report = new Binding();
                {
                    _report.Children.Add(new Binding("Tồn Kho") { Tag = "" });
                    _report.Children.Add(new Binding("Công nợ") { Tag = "" });
                }
                var _account = new Binding();
                {
                    _account.Children.Add(new Binding("Tra cứu") { Tag = "" });
                }
                ShowFunction(btnSachFull, btnSachMini, true, _book);
                ShowFunction(btnHoaDonFull, btnHoaDonMini, true, _bill);
                ShowFunction(btnKhachHangFull, btnKhachHangMini, true, _customer);
                ShowFunction(btnTaiKhoanFull, btnTaiKhoanMini, true, _account);
                ShowFunction(btnBaoCaoFull, btnBaoCaoMini, true, _report);
                ShowFunction(btnQuyDinhFull, btnQuyDinhMini, true);
            }
            else if (Managers.UserManager.Current.Info.AccessLevel.ID == 1)
            {
                var _book = new Binding();
                {
                    _book.Children.Add(new Binding("Tra cứu") { Tag = "<Tab mà nó giữ>" });
                }
                var _bill = new Binding();
                {
                    _bill.Children.Add(new Binding("Lập Hóa Đơn") { Tag = typeof(tabThemHoaDonMoi), Key = true });
                    _bill.Children.Add(new Binding("Hóa Đơn Cũ") { Tag = typeof(tabQuanLyHoaDonCu) });
                }
                var _customer = new Binding();
                {
                    _customer.Children.Add(new Binding("Tra cứu") { Tag = "" });
                    _customer.Children.Add(new Binding("Đòi nợ") { Tag = "" });
                }
                ShowFunction(btnSachFull, btnSachMini, true, _book);
                ShowFunction(btnHoaDonFull, btnHoaDonMini, true, _bill);
                ShowFunction(btnKhachHangFull, btnKhachHangMini, true, _customer);
                ShowFunction(btnTaiKhoanFull, btnTaiKhoanMini, false);
                ShowFunction(btnBaoCaoFull, btnBaoCaoMini, false);
                ShowFunction(btnQuyDinhFull, btnQuyDinhMini, false);
            }
        }

        /*
         * Sử lý minimize & maximize window
         */
        private void MinimizeAndMaximize()
        {
            if (WindowState == System.Windows.WindowState.Normal)
                WindowState = System.Windows.WindowState.Maximized;
            else
                WindowState = System.Windows.WindowState.Normal;
            NotifyPropertyChanged("ShowMaximized");
            NotifyPropertyChanged("ShowRestore");
            NotifyPropertyChanged("ShowResizer");
        }

        /*
         * Đóng (ảo) MainWindow khi đăng xuất
         */
        private void VirtualWindowClose()
        {
            //thu nhỏ window lại
            if (WindowState != System.Windows.WindowState.Normal)
                MinimizeAndMaximize();
            //đóng ds chức năng nếu có mở
            if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
            //mở thanh menu rộng ra như ban đầu
            (this.FindResource("MenuMiniToFull") as Storyboard).Begin();
            //đóng các window con
            if (TitleMainRightClick != null)
                TitleMainRightClick.Visibility = System.Windows.Visibility.Hidden;
            if (ToolBox != null)
                ToolBox.Visibility = System.Windows.Visibility.Hidden;
            if (About != null)
                About.Visibility = System.Windows.Visibility.Hidden;
            //xóa các chức năng đã mở
            mdiContainer.Children.Clear();
        }
        #endregion

        #region TitleMain Events
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
                TitleMainRightClick = new TitleMainRightClick(this);
                TitleMainRightClick.Show();
            }
            TitleMainRightClick._Show();
            var p = Assets.Scripts.Cursor.GetCursorPosition();
            TitleMainRightClick.Left = p.X;
            TitleMainRightClick.Top = p.Y;
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
            Bus.AppHandler.Shutdown();
        }
        #endregion

        #region Theme Menu Events
        private void Luna_Click(object sender, RoutedEventArgs e)
        {
            Luna.IsChecked = true;
            Aero.IsChecked = false;
            Metro.IsChecked = false;

            mdiContainer.Theme = ThemeType.Luna;
        }

        private void Aero_Click(object sender, RoutedEventArgs e)
        {
            Luna.IsChecked = false;
            Aero.IsChecked = true;
            Metro.IsChecked = false;

            mdiContainer.Theme = ThemeType.Aero;
        }

        private void Metro_Click(object sender, RoutedEventArgs e)
        {
            Luna.IsChecked = false;
            Aero.IsChecked = false;
            Metro.IsChecked = true;

            mdiContainer.Theme = ThemeType.Metro;
        }

        private void mdiPlus_closeThis_Click(object sender, RoutedEventArgs e)
        {
            mdiContainer.Children.Remove(mdiContainer.GetTopChild());
        }

        private void mdiPlus_closeAllButThis_Click(object sender, RoutedEventArgs e)
        {
            var top = mdiContainer.GetTopChild();
            foreach (var item in mdiContainer.Children.ToArray())
            {
                if (item != top)
                {
                    mdiContainer.Children.Remove(item);
                }
            }
        }

        private void mdiPlus_closeAll_Click(object sender, RoutedEventArgs e)
        {
            mdiContainer.Children.Clear();
        }
        #endregion

        #region Login/Logout Events
        /*
         * Sự kiện đăng xuất
         */
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            VirtualWindowClose();
            Bus.AppHandler.VirtualWindowClose(this);
            Managers.Manager.Current.User.Logout();
            Bus.AppHandler.VirtualWindowOpen(Owner);
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
        #endregion

        #region WindowExtensions Events
        /*
         * Sự kiện hiển thị thanh công cụ
         */
        private void showToolBox(object sender, RoutedEventArgs e)
        {
            if (ToolBox == null)
            {
                ToolBox = new ToolBox();
                ToolBox.Show();
            }
            Bus.AppHandler.VirtualWindowOpen(ToolBox);
            var p = Assets.Scripts.Cursor.GetCursorPosition();
            ToolBox.Left = p.X;
            ToolBox.Top = p.Y;
            ToolBox.Focus();
        }
        
        /*
         * Sự kiện hiển thị About
         */
        private void showAbout(object sender, RoutedEventArgs e)
        {
            if (About == null)
            {
                About = new About();
                About.Owner = this;
                About.Show();
            }
            Bus.AppHandler.VirtualWindowOpen(About);
            About.Focus();
        }

        /*
         * Sự kiện hiển thị Setting
         */
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Managers.ErrorManager.Current.AppCanNotUseNow.Call();
        }
        #endregion

        #region HandleAnimation Events
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
            var tag = btn.Tag as Binding;
            if (tag != null)
            {
                listboxDSChucNang.ItemsSource = tag.Children as ObservableCollection<Binding>;
                (this.FindResource("ShowFunctions") as Storyboard).Begin();
            }
            else if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
        }
        #endregion

        #region Mdi Events
        private void mdiResizeButton_Click(object sender, RoutedEventArgs e)
        {
            var first = mdiContainer.Children.FirstOrDefault();
            if (first != null)
            {
                if (first.ActualWidth == mdiContainer.InnerWidth && first.ActualHeight == mdiContainer.InnerHeight)
                {
                    mdiContainer.MdiLayout = MdiLayout.TileVertical;
                }
                else
                {
                    foreach (var item in mdiContainer.Children)
                    {
                        item.Position = new Point(0, 0);
                        item.Width = mdiContainer.InnerWidth;
                        item.Height = mdiContainer.InnerHeight;
                    }
                }
            }
        }

        /*
         * Đóng danh sách chức năng khi chức năng được chọn và show tab chức năng được chọn
         */
        private void listboxDSChucNang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
            var item = listboxDSChucNang.SelectedItem as Binding;
            listboxDSChucNang.SelectedItem = null;
            if (item != null)
                Bus.AppHandler.OpenTab(mdiContainer, item.Tag as Type, item.Data as string, item.Key);
        }

        /*
         * Sự kiện khi thêm xóa một child vào mdi container
         */
        void mdiContainer_ChildrenChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                MdiChild mdiChild = mdiContainer.Children[e.NewStartingIndex];
                mdiChild.Loaded += (s, a) =>
                {
                    mdiChild.Position = new Point(0, 0);
                    mdiChild.Width = mdiContainer.InnerWidth;
                    mdiChild.Height = mdiContainer.InnerHeight;
                    mdiChild.MaximizeBox = false;
                };
            }

            NotifyPropertyChanged("ShowMdiContainer");

            WindowsMenu.Items.Clear();
            MenuItem mi;
            for (int i = 0; i < mdiContainer.Children.Count; i++)
            {
                MdiChild child = mdiContainer.Children[i];
                mi = new MenuItem { Header = child.Title };
                mi.Click += (o, ev) => child.Focus();
                WindowsMenu.Items.Insert(i, mi);
            }

            WindowsMenu.Items.Add(new Separator());
            WindowsMenu.Items.Add(mi = new MenuItem { Header = "Cascade" });
            mi.Click += (o, ev) => mdiContainer.MdiLayout = MdiLayout.Cascade;
            WindowsMenu.Items.Add(mi = new MenuItem { Header = "Vertically" });
            mi.Click += (o, ev) => mdiContainer.MdiLayout = MdiLayout.TileVertical;
        }
        #endregion

        #region Button Events
        private void openQuyDinh(object sender, RoutedEventArgs e)
        {
            Bus.AppHandler.OpenTab(mdiContainer, typeof(tabQuyDinh) as Type, "Quy định cửa hàng", false);
        }
        #endregion
    }
}
