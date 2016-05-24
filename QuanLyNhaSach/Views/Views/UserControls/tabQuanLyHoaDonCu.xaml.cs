using QuanLyNhaSach.Managers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for tabQuanLyHoaDonCu.xaml
    /// </summary>
    public partial class tabQuanLyHoaDonCu : UserControl,INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Bill> _listBill = new ObservableCollection<Bill>();
        public ObservableCollection<Bill> ListBill
        {
            get { return _listBill; }
            set { _listBill = value; NotifyPropertyChanged("ListBill"); }
        }

        private Bill _selectedBill = new Bill();

        public Bill SelectedBill
        {
            get { return _selectedBill; }
            set { _selectedBill = value; NotifyPropertyChanged("SelectedBill"); }
        }

        public string ReturnMoney_AbsAndFormat
        {
            get
            {
                if (SelectedBill == null)
                    return null;
                return string.Format("{0:#,##0} vnđ", Math.Abs(SelectedBill.ReturnMoney));
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

        public tabQuanLyHoaDonCu()
        {
            InitializeComponent();
            DataContext = this;
            ListBill = Bus.FillData.GetOldBill();
        }

        private void lv_DSHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_DSHoaDon.SelectedItem!=null)
                if (e.AddedItems.Count > 0)
                {
                    SelectedBill = e.AddedItems[0] as Bill;
                    SelectedBill.Tag = Math.Min(SelectedBill.PayMoney - SelectedBill.TotalMoney, 0);

                    tblock_TotalMoney.DataContext = SelectedBill;
                    tblock_ReturnMoney.DataContext = this;
                    tb_PayMoney.DataContext = SelectedBill;

                    lv_ChiTietHoaDonCu.ItemsSource = SelectedBill.BillItems;
                }
        }

        private void removeItem(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Tag == null)
                return;
            var item = button.Tag as BillItem;
            if (item != null)
                SelectedBill.BillItems.Remove(item);
        }

        private void tb_PayMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void tb_PayMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBill != null)
            {
                if (string.IsNullOrEmpty(tb_PayMoney.Text))
                    SelectedBill.PayMoney = 0;

                if (SelectedBill.ReturnMoney < 0)
                {
                    text_HoanTien.Text = "Nợ :";
                    text_HoanTien.Foreground = Brushes.Red;
                    tblock_ReturnMoney.Foreground = Brushes.Red;
                }
                else
                {
                    text_HoanTien.Text = "Hoàn tiền :";
                    text_HoanTien.Foreground = Brushes.Blue;
                    tblock_ReturnMoney.Foreground = Brushes.Blue;
                }

                NotifyPropertyChanged("ReturnMoney_AbsAndFormat");
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBill!=null)
            {
                if (SelectedBill.ReturnMoney<0 && SelectedBill.Customer.ID==13)
                {
                    ErrorManager.Current.PopularCustomer.Call("Vui lòng thanh toán đầy đủ!");
                    return;
                }
                if (SelectedBill.ReturnMoney<0)
                {
                    if (Math.Abs(SelectedBill.ReturnMoney)+SelectedBill.Customer.Debt > RuleManager.Current.Rule.MaxDebt)
                    {
                         ErrorManager.Current.LimitMaxDebtMoney.Call("Khách hàng " + SelectedBill.Customer.Name + " sau khi mua sẽ nợ nhiều hơn " + RuleManager.Current.Rule.MaxDebt + " nên không thể hoàn tất thanh toán!");
                        return;
                    }
                }
                else
                    SelectedBill.PayMoney = SelectedBill.TotalMoney;
                Bus.UpdateData.UpdateOldBill(SelectedBill);

                ListBill = Bus.FillData.GetOldBill();
                SelectedBill = null;
                lv_ChiTietHoaDonCu.ItemsSource = null;
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            Bus.DeleteData.DeleteOldBill(SelectedBill);
            ListBill.Remove(SelectedBill);
            SelectedBill.BillItems.Clear();
            lv_DSHoaDon.SelectedItem = null;
            ListBill = Bus.FillData.GetOldBill();
            
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            ListBill = Bus.FillData.GetOldBill();
            SelectedBill = null;
            SelectedBill = new Bill();
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Search.Text))
            {
                var item = ListBill.FirstOrDefault(x => x.ID.ToString() == tb_Search.Text);
                tb_Search.Text = "";
                if (item==null)
                {
                    ErrorManager.Current.InfoIsNull.Call("Không tìm thấy hóa đơn tương ứng.");
                    return;
                }
                else
                {
                    lv_DSHoaDon.SelectedItem = item;
                    lv_DSHoaDon.ScrollIntoView(item);
                }
            }
        }
    }
}
