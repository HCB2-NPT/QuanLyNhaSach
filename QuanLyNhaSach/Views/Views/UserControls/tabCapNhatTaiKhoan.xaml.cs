using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for tabCapNhatTaiKhoan.xaml
    /// </summary>
    public partial class tabCapNhatTaiKhoan : UserControl, INotifyPropertyChanged
    {
        #region Properties

        private Account _account;

        public Account Account
        {
            get { return _account; }
            set { 
                _account = value;
                NotifyPropertyChanged("Account");
            }
        }

        #endregion

        #region Implements

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Contrustor

        public tabCapNhatTaiKhoan()
        {
            InitializeComponent();
            DataContext = this;
            listRoles.ItemsSource = Bus.FillData.GetAllRoles().Where(role => role.ID != 3);
        }

        #endregion

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            ((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer).Children.Remove(Tag as WPF.MDI.MdiChild);
            Bus.AppHandler.SelectTab((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer, "Quản lý tài khoản");
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            bool flag = true;

            if (IsResetPassword.IsChecked == true)
            {
                flag = Bus.UpdateData.ResetPassword(_account);
            }

            flag = Bus.UpdateData.UpdateAccount(_account);

            if (flag)
                MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Cập nhật tài khoản thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
