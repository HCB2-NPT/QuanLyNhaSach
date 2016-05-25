using QuanLyNhaSach.Objects;
using QuanLyNhaSach.Views.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for lvi_DSKhachHang.xaml
    /// </summary>
    public partial class lvi_DSKhachHang : UserControl
    {
        public lvi_DSKhachHang()
        {
            InitializeComponent();
        }

        private void QuickPayDebtMoney(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Customer;
            if (tag != null)
            {
                if (tag.Debt > 0)
                {
                    var uc = (tabPhieuThuTien)Bus.AppHandler.OpenTab((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer, typeof(tabPhieuThuTien), "Đòi nợ", false);
                    uc.CustomerTransfer = tag.ID;
                }
                else
                {
                    WarningBox.Show("Thông báo", "Không tồn tại nợ", "Khách hàng <" + tag.Name + "> không thiếu nợ!");
                }
            }
        }
    }
}
