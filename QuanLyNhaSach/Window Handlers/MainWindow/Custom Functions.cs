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
         * Xử lý sau khi login
         */
        public void HandleAfterLogin()
        {
            if (Manager.UserManager.Current.Info.AccessLevel.ID == 3)
            {
                ShowFunction(btnSachFull, btnSachMini, false);
                ShowFunction(btnHoaDonFull, btnHoaDonMini, false);
                ShowFunction(btnKhachHangFull, btnKhachHangMini, false);
                ShowFunction(btnTaiKhoanFull, btnTaiKhoanMini, true);
                ShowFunction(btnBaoCaoFull, btnBaoCaoMini, false);
                ShowFunction(btnQuyDinhFull, btnQuyDinhMini, false);
            }
            else if (Manager.UserManager.Current.Info.AccessLevel.ID == 2)
            {
                var _book = new Manager.Data.Binding();
                {
                    _book.Children.Add(new Manager.Data.Binding("Thư viện") { Tag = "<Tab mà nó giữ>" });
                    _book.Children.Add(new Manager.Data.Binding("Thể loại") { Tag = "<Tab mà nó giữ>" });
                    _book.Children.Add(new Manager.Data.Binding("Tác Giả") { Tag = "<Tab mà nó giữ>" });
                }
                var _bill = new Manager.Data.Binding();
                {
                    _bill.Children.Add(new Manager.Data.Binding("Lập Hóa Đơn") { Tag = "" });
                    _bill.Children.Add(new Manager.Data.Binding("Hóa Đơn Cũ") { Tag = "" });
                }
                var _customer = new Manager.Data.Binding();
                {
                    _customer.Children.Add(new Manager.Data.Binding("Thông tin khách hàng") { Tag = "" });
                    _customer.Children.Add(new Manager.Data.Binding("Thu Công Nợ") { Tag = "" });
                }
                var _report = new Manager.Data.Binding();
                {
                    _report.Children.Add(new Manager.Data.Binding("Tồn Kho") { Tag = "" });
                    _report.Children.Add(new Manager.Data.Binding("Công Nợ") { Tag = "" });
                }
				var _account = new Manager.Data.Binding();
                {
                    _account.Children.Add(new Manager.Data.Binding("Tra cứu") { Tag = "" });
                }
                ShowFunction(btnSachFull, btnSachMini, true, _book);
                ShowFunction(btnHoaDonFull, btnHoaDonMini, true, _bill);
                ShowFunction(btnKhachHangFull, btnKhachHangMini, true, _customer);
                ShowFunction(btnTaiKhoanFull, btnTaiKhoanMini, true, _account);
                ShowFunction(btnBaoCaoFull, btnBaoCaoMini, true, _report);
                ShowFunction(btnQuyDinhFull, btnQuyDinhMini, true);
            }
            else if (Manager.UserManager.Current.Info.AccessLevel.ID == 1)
            {
                var _book = new Manager.Data.Binding();
                {
                    _book.Children.Add(new Manager.Data.Binding("Tra cứu") { Tag = "<Tab mà nó giữ>" });
                }
                var _bill = new Manager.Data.Binding();
                {
                    _bill.Children.Add(new Manager.Data.Binding("Lập Hóa Đơn") { Tag = "" });
                    _bill.Children.Add(new Manager.Data.Binding("Hóa Đơn Cũ") { Tag = "" });
                }
                var _customer = new Manager.Data.Binding();
                {
                    _customer.Children.Add(new Manager.Data.Binding("Thông tin khách hàng") { Tag = "" });
                    _customer.Children.Add(new Manager.Data.Binding("Thu Công Nợ") { Tag = "" });
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
                this.WindowState = System.Windows.WindowState.Maximized;
            else
                WindowState = System.Windows.WindowState.Normal;
            NotifyPropertyChanged("ShowMaximized");
            NotifyPropertyChanged("ShowRestore");
        }

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
    }
}
