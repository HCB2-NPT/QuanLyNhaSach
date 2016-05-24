using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for tabThemHoaDonMoi.xaml
    /// </summary>
    public partial class tabThemHoaDonMoi : UserControl, INotifyPropertyChanged
    {
        #region Properties
        private Bill _bookCart = new Bill();
        public Bill BookCart
        {
            get { return _bookCart; }
        }

        private ObservableCollection<Customer> _customers = Bus.FillData.GetAllCustomerNotDeleted();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; NotifyPropertyChanged("Customers"); }
        }

        private ObservableCollection<Book> _books = Bus.FillData.GetAllBookNotDeleted();
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; NotifyPropertyChanged("Books"); }
        }
        #endregion

        #region Constructor
        public tabThemHoaDonMoi()
        {
            InitializeComponent();
            DataContext = this;

            var c = Customers.FirstOrDefault(x => x.ID == Managers.DataManager.Current.PopularCustomerID);
            tb_SDTKH.SelectedItem = c;
            tb_SDTKH.Text = c.CustomerInfo;
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

        #region Custom Functions
        void UpdateData()
        {
            Customers = Bus.FillData.GetAllCustomerNotDeleted();
            Books = Bus.FillData.GetAllBookNotDeleted();
        }

        void Clear()
        {
            BookCart.BillItems.Clear();
            BookCart.PayMoney = 0;

            UpdateData();

            var c = Customers.FirstOrDefault(x => x.ID == Managers.DataManager.Current.PopularCustomerID);
            tb_SDTKH.SelectedItem = c;
            tb_SDTKH.Text = c.CustomerInfo;

            tb_NameBook.SelectedItem = null;
            tb_NameBook.Text = "";
        }
        #endregion

        #region Events
        private void tb_TienKhachTra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void tb_TienKhachTra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_TienKhachTra.Text))
                BookCart.PayMoney = 0;
        }

        private void tb_SDTKH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                BookCart.Customer = e.AddedItems[0] as Customer;
        }

        private void btn_ThemSachVaoHoaDon_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = tb_NameBook.SelectedItem as Book;
            if (selectedBook != null)
            {
                var item = BookCart.BillItems.FirstOrDefault(x => x.Book.ID == selectedBook.ID);
                if (item != null)
                {
                    item.Number += (int)num_SLSach.Value;
                    lv_ChiTietHoaDon.SelectedItem = item;
                    lv_ChiTietHoaDon.ScrollIntoView(lv_ChiTietHoaDon.SelectedItem);
                }
                else
                {
                        var buybook = new BillItem(selectedBook, (int)num_SLSach.Value, selectedBook.Price);
                        BookCart.BillItems.Add(buybook);
                }
                num_SLSach.Value = 1;
            }
        }

        private void btn_InHoaDon_Click(object sender, RoutedEventArgs e)
        {
            if (BookCart.Customer == null)
            {
                ErrorManager.Current.UnknowCustomer.Call("Vui lòng điền thông tin khách hàng!\nNếu khách hàng không có tài khoản thì hãy thanh toán như một Khách Hàng Thông Thường!");
                return;
            }
            if (BookCart.BillItems.Count == 0)
            {
                ErrorManager.Current.BillEmpty.Call("Tồn tại ít nhất 1 mặt hàng để thanh toán!");
                return;
            }
            if (BookCart.ReturnMoney < 0 && BookCart.Customer.ID == Managers.DataManager.Current.PopularCustomerID)
            {
                ErrorManager.Current.PopularCustomer.Call("Vui lòng thanh toán đầy đủ!");
                return;
            }
            if (BookCart.ReturnMoney < 0 && BookCart.Customer.ID != Managers.DataManager.Current.PopularCustomerID)
            {
                if (Math.Abs(BookCart.ReturnMoney) + BookCart.Customer.Debt > Managers.RuleManager.Current.Rule.MaxDebt)
                {
                    ErrorManager.Current.LimitMaxDebtMoney.Call("Khách hàng " + BookCart.Customer.Name + " sau khi mua sẽ nợ nhiều hơn " + RuleManager.Current.Rule.MaxDebt + " nên không thể hoàn tất thanh toán!");
                    return;
                }
            }
            lv_ChiTietHoaDon.SelectedItems.Clear();
            foreach (var item in BookCart.BillItems)
            {
                if (item.Book.Number - item.Number < Managers.RuleManager.Current.Rule.MinNumberInStore)
                {
                    ErrorManager.Current.MinNumberLimitBookInStorage.Call(item.Book.Name + " sau khi bán có số lượng tồn trong kho nhỏ hơn " + RuleManager.Current.Rule.MinNumberInStore);
                    lv_ChiTietHoaDon.SelectedItems.Add(item);
                }
            }
            if (lv_ChiTietHoaDon.SelectedItems.Count > 0)
                return;
            Bus.InsertData.InsertNewBill(BookCart);
            Clear();
        }

        private void removeItem(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Tag == null)
                return;
            var item = button.Tag as BillItem;
            if (item != null)
                BookCart.BillItems.Remove(item);
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        #endregion
    }
}
