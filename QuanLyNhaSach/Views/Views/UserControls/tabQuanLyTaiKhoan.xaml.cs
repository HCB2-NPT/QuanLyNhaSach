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

        private void Button_Click_ShowDetails(object sender, RoutedEventArgs e)
        {

        }
    }
}
