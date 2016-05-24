using QuanLyNhaSach.Objects;
using System;
using System.Linq;
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
        private ObservableCollection<Customer> _listDebtor = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> ListDebtor
        {
            get { return _listDebtor; }
            set { _listDebtor = value; NotifyPropertyChanged("ListDebtor"); }
        }

        private string _editText;
        public string EditText
        {
            get { return _editText; }
            set { _editText = value; NotifyPropertyChanged("EditText"); }
        }

        private PayDebtMoney _debtMoney = new PayDebtMoney();

        public PayDebtMoney DebtMoney
        {
            get { return _debtMoney; }
            set { _debtMoney = value; NotifyPropertyChanged("DebtMoney"); }
        }

        public int ReturnMoney
        {
            get
            {
                if (lv_ListDebtor.SelectedItem!=null)
                {
                    int rm = PayMoney - ((Customer)lv_ListDebtor.SelectedItem).Debt;
                    if (rm >= 0)
                    {
                        tb_NameHoanTien.Foreground = Brushes.Green;
                        tb_DebtMoney.Foreground = Brushes.Green;
                        EditText = "Tiền thối: ";
                        return rm;
                    }
                    tb_NameHoanTien.Foreground = Brushes.Red;
                    tb_DebtMoney.Foreground = Brushes.Red;
                    EditText = "Còn nợ: ";
                    return Math.Abs(rm);
                }
                EditText = "Còn nợ: ";
                tb_NameHoanTien.Foreground = Brushes.Black;
                tb_DebtMoney.Foreground = Brushes.Black;
                return 0;
            }
        }

        public string ReturnMoneyFormat
        {
            get
            {
                return ReturnMoney.ToString("#,##0 vnđ");
            }
        }

        private int _payMoney = 0;

        public int PayMoney
        {
            get { return _payMoney; }
            set { _payMoney = value; NotifyPropertyChanged("PayMoney"); NotifyPropertyChanged("ReturnMoney"); NotifyPropertyChanged("ReturnMoneyFormat"); }
        }

        public object CustomerTransfer
        {
            set 
            {
                var val = ListDebtor.FirstOrDefault(x => x.ID == (int)value);
                lv_ListDebtor.SelectedItem = val;
                if (val != null)
                {
                    lv_ListDebtor.ScrollIntoView(val);
                }
            }
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
            ListDebtor = Bus.FillData.GetAllDebtor();
		}

        private void tb_PayMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void tb_Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = tb_Search.SelectedItem as Customer;
            if (item != null)
            {
                lv_ListDebtor.SelectedItem = item;
                lv_ListDebtor.ScrollIntoView(item);
            }
        }

        private void btn_Inphieuthu_Click(object sender, RoutedEventArgs e)
        {
            if (lv_ListDebtor.SelectedItem!=null && PayMoney>0)
            {
                int rm = PayMoney - ((Customer)lv_ListDebtor.SelectedItem).Debt;

                PayDebtMoney pdm = new PayDebtMoney();
                pdm.Customer = lv_ListDebtor.SelectedItem as Customer;
                pdm.DateCreated = DateTime.Now;
                pdm.IDManager = Managers.Manager.Current.User.Info.ID;
                pdm.PayMoney = PayMoney;
                if (rm >= 0)
                {
                    pdm.PayMoney = pdm.Customer.Debt;
                    pdm.Customer.Debt = 0;
                }
                else
                {
                    pdm.PayMoney = PayMoney;
                    pdm.Customer.Debt = Math.Abs(rm);
                }
                Bus.InsertData.NewPayDebtMoney(pdm);
            }

            ListDebtor = Bus.FillData.GetAllDebtor();
            NotifyPropertyChanged("ReturnMoney");
            tb_PayMoney.Text = "0";
        }

        private void tb_PayMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_PayMoney.Text))
            {
                tb_PayMoney.Text = "0";
                return;
            }
        }

        private void lv_ListDebtor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_ListDebtor.SelectedItem !=null)
            {
                NotifyPropertyChanged("PayMoney");
                NotifyPropertyChanged("ReturnMoney");
                NotifyPropertyChanged("ReturnMoneyFormat");
            }

        }
	}
}