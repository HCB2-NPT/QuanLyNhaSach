using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for tabTaoTaiKhoanMoi.xaml
    /// </summary>
    public partial class tabTaoTaiKhoanMoi : UserControl
    {
        #region Constructor
        
        public tabTaoTaiKhoanMoi()
        {
            InitializeComponent();
            DataContext = this;
            listRoles.ItemsSource = Bus.FillData.GetAllRoles().Where(role => role.ID != 3);
        }

        #endregion

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account.Name = txtFullname.Text;
            account.Email = txtEmail.Text;
            account.AccessLevel = (AccessLevel)listRoles.SelectedItem;

            bool flag = false;
            bool isEmail = Regex.IsMatch(account.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail == false)
            {
                MessageBox.Show("Email không hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (account.Email != "" && account.Name != "" && isEmail)
            {
                flag = Bus.InsertData.InsertNewAccount(account);
            }

            if (flag)
                MessageBox.Show("Tạo tài khoản mới thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Tạo tài khoản mới thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            ((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer).Children.Remove(Tag as WPF.MDI.MdiChild);
            Bus.AppHandler.SelectTab((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer, "Quản lý tài khoản");
        }
    }
}
