using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for tabQuanLyTaiKhoan.xaml
    /// </summary>
    public partial class tabQuanLyTaiKhoan : UserControl, INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Account> _accounts = Bus.FillData.GetAllAccount();

        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; NotifyPropertyChanged("Accounts"); }
        }
        #endregion

        #region Constructor

        public tabQuanLyTaiKhoan()
        {
            InitializeComponent();
            dgAccounts.ItemsSource = Accounts;
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

        #region Handlers

        void searchItem(string key)
        {
            dgAccounts.ItemsSource = Bus.SearchData.SearchAccount(Accounts, key);
        }

        #endregion

        #region Events

        private void Button_Click_ShowDetails(object sender, RoutedEventArgs e)
        {
            object accountID = ((Button)sender).CommandParameter;
            Account account = (Account)_accounts.Where(a => a.ID == (int)accountID).FirstOrDefault();

            if (account.AccessLevel.ID == 3)
            {
                MessageBox.Show("Tải khoản Quản Trị mặc định không được cập nhật", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            var uc = (tabCapNhatTaiKhoan)Bus.AppHandler.OpenTab((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer, typeof(tabCapNhatTaiKhoan), "Cập nhật tài khoản", false);
            uc.Account = account;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            object accountID = ((Button)sender).CommandParameter;

            var uc = (tabTaoTaiKhoanMoi)Bus.AppHandler.OpenTab((Tag as WPF.MDI.MdiChild).Tag as WPF.MDI.MdiContainer, typeof(tabTaoTaiKhoanMoi), "Tạo tài khoản mới", false);
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            searchItem(txtSearch.Text.ToLower());
        }

        private void txt_autocomplete(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                searchItem(txtSearch.Text.ToLower());
        }

        #endregion
    }
}
