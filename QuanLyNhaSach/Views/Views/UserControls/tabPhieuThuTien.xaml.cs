using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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
	/// Interaction logic for tabPhieuThuTien.xaml
	/// </summary>
	public partial class tabPhieuThuTien : UserControl,INotifyPropertyChanged
    {
        #region Properties
        //Dang lam
        private ObservableCollection<Customer> _listDebtor = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> ListDebtor
        {
            get { return _listDebtor; }
            set { _listDebtor = value; NotifyPropertyChanged("ListDebtor"); }
        }


        private Customer _selectedCustomer = new Customer();

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; NotifyPropertyChanged("SelectedCustomer"); }
        }

        private PayDebtMoney _debtMoney = new PayDebtMoney();

        internal PayDebtMoney DebtMoney
        {
            get { return _debtMoney; }
            set { _debtMoney = value; NotifyPropertyChanged("DebtMoney"); }
        }

        private int _returnMoney = 0;

        public int ReturnMoney
        {
            get { return _returnMoney; }
            set { _returnMoney = value; NotifyPropertyChanged("ReturnMoney"); }
        }

        private int _payMoney = 0;

        public int PayMoney
        {
            get { return _payMoney; }
            set { _payMoney = value; NotifyPropertyChanged("PayMoney"); }
        }

        #endregion

        #region Emplements

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public tabPhieuThuTien()
		{
			this.InitializeComponent();
            DataContext = this;
            ListDebtor = Adapters.CustomerAdapter.GetAllDebtor();
		}
	}
}