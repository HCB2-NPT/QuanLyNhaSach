using QuanLyNhaSach.Objects;
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
                controlFull.Visibility = System.Windows.Visibility.Visible;
                controlMini.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                controlFull.Visibility = System.Windows.Visibility.Collapsed;
                controlMini.Visibility = System.Windows.Visibility.Collapsed;
            }
            controlFull.Tag = tag;
            controlMini.Tag = tag;
        }

        /*
         * Xử lý sau khi login
         */
        public void HandleAfterLogin()
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
                var _book = new Function();
                {
                    _book.Children.Add(new Function("Thư viện") { Data = typeof(tabQuanLySach) });
                    _book.Children.Add(new Function("Thể loại") { Data = typeof(tabTheLoai) });
                    _book.Children.Add(new Function("Tác giả") { Data = typeof(tabTacGia) });
                    _book.Children.Add(new Function("Phiếu nhập") { Data = typeof(tabQuanLyPhieuNhap), Tag = 0 });
                    _book.Children.Add(new Function("Nhập sách") { Data = typeof(tabPhieuNhapSach), Tag = 0 });

                }
                var _bill = new Function();
                {
                    _bill.Children.Add(new Function("Lập hóa đơn") { Data = typeof(tabThemHoaDonMoi), Tag = 0, CanDuplicate = true });
                    _bill.Children.Add(new Function("Hóa đơn cũ") { Data = typeof(tabQuanLyHoaDonCu), Tag = 0 });
                }
                var _customer = new Function();
                {
                    _customer.Children.Add(new Function("Khách hàng") { Data = typeof(tabKhachHang) });
                    _customer.Children.Add(new Function("Đòi nợ") { Data = typeof(tabPhieuThuTien), Tag = 0 });
                }
                var _report = new Function();
                {
                    _report.Children.Add(new Function("Tồn kho") { Data = typeof(tabBaoCaoTonKho), Tag = 0 });
                    _report.Children.Add(new Function("Công nợ") { Data = typeof(tabBaoCaoCongNo), Tag = 0 });
                }
                var _account = new Function();
                {
                    _account.Children.Add(new Function("Tra cứu tài khoản") { Data = typeof(tabTraCuuTaiKhoan), Tag = 0 });
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
                var _book = new Function();
                {
                    _book.Children.Add(new Function("Tra cứu sách") { Data = typeof(tabTraCuuSach), Tag = 0 });
                }
                var _bill = new Function();
                {
                    _bill.Children.Add(new Function("Lập hóa đơn") { Data = typeof(tabThemHoaDonMoi), Tag = 0, CanDuplicate = true });
                    _bill.Children.Add(new Function("Hóa đơn cũ") { Data = typeof(tabQuanLyHoaDonCu), Tag = 0 });
                }
                var _customer = new Function();
                {
                    _customer.Children.Add(new Function("Tra cứu khách hàng") { Data = typeof(tabTraCuuKhachHang), Tag = 0 });
                    _customer.Children.Add(new Function("Đòi nợ") { Data = typeof(tabPhieuThuTien), Tag = 0 });
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
            Bus.AppHandler.OpenWindowExt<TitleMainRightClick>(this, ref TitleMainRightClick);
            var p = Assets.Scripts.Cursor.GetCursorPosition();
            TitleMainRightClick.Left = p.X;
            TitleMainRightClick.Top = p.Y;
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
            if (!_logout)
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
        private bool _logout = false;
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Managers.Manager.Current.User.Logout();
            Bus.AppHandler.VirtualWindowOpen(Owner);
            _logout = true;
            Close();
        }
        #endregion

        #region WindowExtensions Events
        /*
         * Sự kiện hiển thị thanh công cụ
         */
        private void showToolBox(object sender, RoutedEventArgs e)
        {
            Bus.AppHandler.OpenWindowExt<ToolBox>(this, ref ToolBox);
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
            Bus.AppHandler.OpenWindowExt<About>(this, ref About);
            var workingArea = Assets.Scripts.WpfScreen.GetScreenFrom(this).WorkingArea;
            About.Top = workingArea.Height - About.ActualHeight;
            About.Left = workingArea.Width - About.ActualWidth;
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
        #endregion

        #region Menu Functions Events
        /*
         * Sự kiện hiện danh sách chức năng khi chọn các chức năng chính bên cột menu phải
         */
        private void showFunctionList(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Function;
            if (tag != null)
            {
                listboxDSChucNang.Items.Clear();
                listboxDSChucNang2.Items.Clear();
                foreach (var item in tag.Children as ObservableCollection<Function>)
                {
                    if (item.Tag == null)
                        listboxDSChucNang.Items.Add(item);
                    else
                        listboxDSChucNang2.Items.Add(item);
                }
                (this.FindResource("ShowFunctions") as Storyboard).Begin();
            }
            else if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();
        }
        #endregion

        #region Mdi Events
        /*
         * Đóng danh sách chức năng khi chức năng được chọn và show tab chức năng được chọn
         */
        private void selectFunction(object sender, SelectionChangedEventArgs e)
        {
            if (DockChucNang.Width > 0)
                (this.FindResource("CloseFunctions") as Storyboard).Begin();

            var control = sender as ListBox;
            if (control != null)
            {
                var item = control.SelectedItem as Function;
                control.SelectedItem = null;
                if (item != null)
                    Bus.AppHandler.OpenTab(mdiContainer, item.Data as Type, item.Title, item.CanDuplicate);
            }
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
                    mdiChild.Height = mdiContainer.InnerHeight - mdiContainer.MinimizedAreaHeight;
                    mdiChild.MaximizeBox = false;
                };
                mdiChild.WindowStateChanged += (s, a) =>
                {
                    if (mdiChild.WindowState != System.Windows.WindowState.Minimized)
                    {
                        foreach (var item in mdiContainer.Children)
                        {
                            if (item != mdiChild)
                                item.WindowState = System.Windows.WindowState.Minimized;
                        }

                        mdiChild.Position = new Point(0, 0);
                        mdiChild.Width = mdiContainer.InnerWidth;
                        mdiChild.Height = mdiContainer.InnerHeight - mdiContainer.MinimizedAreaHeight;
                    }
                };
                foreach (var item in mdiContainer.Children)
                {
                    if (item != mdiChild)
                        item.WindowState = System.Windows.WindowState.Minimized;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var top = mdiContainer.GetTopChild();
                if (top != null)
                {
                    top.WindowState = System.Windows.WindowState.Normal;
                    top.Position = new Point(0, 0);
                    top.Width = mdiContainer.InnerWidth;
                    top.Height = mdiContainer.InnerHeight - mdiContainer.MinimizedAreaHeight;
                }
            }

            NotifyPropertyChanged("ShowMdiContainer");

            WindowsMenu.Items.Clear();
            MenuItem mi;
            for (int i = 0; i < mdiContainer.Children.Count; i++)
            {
                MdiChild child = mdiContainer.Children[i];
                mi = new MenuItem { Header = child.Title };
                mi.Click += (o, ev) =>
                {
                    child.WindowState = System.Windows.WindowState.Normal;
                    child.Position = new Point(0, 0);
                    child.Width = mdiContainer.InnerWidth;
                    child.Height = mdiContainer.InnerHeight - mdiContainer.MinimizedAreaHeight;
                    child.Focus();
                };
                WindowsMenu.Items.Insert(i, mi);
            }

            //WindowsMenu.Items.Add(new Separator());
            //WindowsMenu.Items.Add(mi = new MenuItem { Header = "Cascade" });
            //mi.Click += (o, ev) => mdiContainer.MdiLayout = MdiLayout.Cascade;
            //WindowsMenu.Items.Add(mi = new MenuItem { Header = "Vertically" });
            //mi.Click += (o, ev) => mdiContainer.MdiLayout = MdiLayout.TileVertical;
            //WindowsMenu.Items.Add(mi = new MenuItem { Header = "Horizontally" });
            //mi.Click += (o, ev) => mdiContainer.MdiLayout = MdiLayout.TileHorizontal;
        }

        private void mdiContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var top = mdiContainer.GetTopChild();
            if (top != null)
            {
                if (top.WindowState == System.Windows.WindowState.Normal)
                {
                    top.Position = new Point(0, 0);
                    top.Width = mdiContainer.InnerWidth;
                    top.Height = mdiContainer.InnerHeight - mdiContainer.MinimizedAreaHeight;
                }
            }
        }
        #endregion

        #region Button Events
        private void openQuyDinh(object sender, RoutedEventArgs e)
        {
            if (Managers.UserManager.Current.Info.AccessLevel.ID == 2)
                Bus.AppHandler.OpenTab(mdiContainer, typeof(tabQuyDinh) as Type, "Quy định cửa hàng", false);
        }

        private void openTaiKhoan(object sender, RoutedEventArgs e)
        {
            if (Managers.UserManager.Current.Info.AccessLevel.ID == 3)
                Bus.AppHandler.OpenTab(mdiContainer, typeof(tabQuanLyTaiKhoan) as Type, "Quản lý tài khoản", false);
            else
                showFunctionList(sender, e);
        }
        #endregion
    }
}
