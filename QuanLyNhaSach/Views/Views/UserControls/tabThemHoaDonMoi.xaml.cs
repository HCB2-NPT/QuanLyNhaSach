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
    public partial class tabThemHoaDonMoi : UserControl,INotifyPropertyChanged
    {
        #region Properties
        private BookCart _bookCart = new BookCart();

        public BookCart BookCart
        {
            get { return _bookCart; }
            set
            {
                _bookCart = value;

            }
        }
        #endregion

        #region Binding Controlers
        private Customer c;
        public Customer SelectedCustomer { get { return c; } set { c = value; NotifyPropertyChanged("SelectedCustomer"); } }

        private Book b;
        public Book SelectedBook { get { return b; } set { b = value; NotifyPropertyChanged("SelectedBook"); } }
        #endregion

        #region Constructor
        public tabThemHoaDonMoi()
        {
            InitializeComponent();

            var customers = Adapters.CustomerAdapter.GetAll();
            tb_SDTKH.ItemsSource = customers;
            tb_SDTKH.FilterMode = AutoCompleteFilterMode.Contains;
            tb_SDTKH.IsTextCompletionEnabled = true;
            tb_SDTKH.Text = customers.FirstOrDefault(x => x.ID == 13).CustomerInfo;

            tb_NameBook.ItemsSource = Adapters.BookAdapter.GetAll();
            tb_NameBook.FilterMode = AutoCompleteFilterMode.Contains;
            tb_NameBook.IsTextCompletionEnabled = true;
            tblock_TienHoaDon.DataContext = BookCart;

            lv_ChiTietHoaDon.ItemsSource = BookCart.Cart;
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

        private void tb_TienKhachTra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void tb_SDTKH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                SelectedCustomer = e.AddedItems[0] as Customer;
        }

        private void tb_NameBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedBook = e.AddedItems[0] as Book;
                tb_TacGia.Text = SelectedBook.AuthorsFormat;
                tb_TheLoai.Text = SelectedBook.GenresFormat;
                tb_SoLuongTon.DataContext = SelectedBook;
            }
        }

        private void btn_ThemSachVaoHoaDon_Click(object sender, RoutedEventArgs e)
        {
            var item = BookCart.Cart.FirstOrDefault(x=>x.Book.ID == SelectedBook.ID);
            if(item!=null)
            {
                item.Number += (int)num_SLSach.Value;
                lv_ChiTietHoaDon.SelectedItem = item;
            }
            else
            {
                BuyingBook buybook = new BuyingBook(SelectedBook, (int)num_SLSach.Value);
                BookCart.Cart.Add(buybook);
            }
        }

        private void tb_TienKhachTra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_TienKhachTra.Text))
            {
                tb_TienKhachTra.Text = "0";
                return;
            }
                
            BookCart.PayMoney = int.Parse(tb_TienKhachTra.Text);
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_SDTKH.SelectedItem = (tb_SDTKH.ItemsSource as ObservableCollection<Customer>).First();
        }

        private void btn_InHoaDon_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer == null)
            {
                ErrorManager.Current.UnknowCustomer.Call("Vui lòng điền thông tin khách hàng!");
                return;
            }
            if (BookCart.ReturnMoney < 0 && SelectedCustomer.ID == 13)
            {
                ErrorManager.Current.UnknowCustomer.Call(SelectedCustomer.Name + " không được nợ tiền cửa hàng. Vui lòng thanh toán đầy đủ!");
                return;
            }
            if (Math.Abs(BookCart.ReturnMoney + SelectedCustomer.Debt) < Managers.RuleManager.Current.Rule.MaxDebt && SelectedCustomer.ID != 13)
            {
                ErrorManager.Current.LimitMaxDebtMoney.Call("Khách hàng " + SelectedCustomer.Name + " sau khi mua sẽ nợ nhiều hơn " + RuleManager.Current.Rule.MaxDebt + " nên không thể hoàn tất thanh toán!");
                return;
            }
            lv_ChiTietHoaDon.SelectedItems.Clear();
            foreach (var item in BookCart.Cart)
            {
                if (item.Book.Number - item.Number < Managers.RuleManager.Current.Rule.MinNumberInStore)
                {
                    ErrorManager.Current.MinNumberLimitBookInStorage.Call(item.Book.Name + " sau khi bán có số lượng tồn trong kho nhỏ hơn " + RuleManager.Current.Rule.MinNumberInStore);
                    lv_ChiTietHoaDon.SelectedItems.Add(item);
                }
            }
            if (lv_ChiTietHoaDon.SelectedItems.Count > 0)
                return;
            /*
             * insert
             */
        }

        private void removeItem(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Tag == null)
                return;
            var item = button.Tag as BuyingBook;
            if (item != null)
                BookCart.Cart.Remove(item);
        }
    }
}
