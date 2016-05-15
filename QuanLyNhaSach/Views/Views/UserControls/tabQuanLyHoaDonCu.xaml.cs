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
            ListBill = Adapters.BillAdapter.GetOldBills();
            lv_DSHoaDon.ItemsSource = ListBill;
            lv_ChiTietHoaDonCu.ItemsSource = SelectedBill.BillItems;


        }

        private void lv_DSHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_DSHoaDon.SelectedItem!=null)
                if (e.AddedItems.Count > 0)
                {
                    SelectedBill = e.AddedItems[0] as Bill;

                    tblock_TotalMoney.DataContext = SelectedBill;
                    tblock_ReturnMoney.DataContext = SelectedBill;
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
            if (string.IsNullOrEmpty(tb_PayMoney.Text))
            {
                tb_PayMoney.Text = "0";
                return;
            }
            SelectedBill.PayMoney = int.Parse(tb_PayMoney.Text);
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBill!=null)
            {
                Bus.UpdateData.UpdateOldBill(SelectedBill);
            }
        }
    }
}
