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
    /// Interaction logic for tabThemHoaDonMoi.xaml
    /// </summary>
    public partial class tabThemHoaDonMoi : UserControl,INotifyPropertyChanged
    {
        public tabThemHoaDonMoi()
        {
            InitializeComponent();
            tb_SDTKH.ItemsSource = Adapters.CustomerAdapter.GetAll();
            tb_SDTKH.FilterMode = AutoCompleteFilterMode.Contains;
            tb_SDTKH.IsTextCompletionEnabled = true;

            tb_NameBook.ItemsSource = Adapters.BookAdapter.GetAll();
            tb_NameBook.FilterMode = AutoCompleteFilterMode.Contains;
            tb_NameBook.IsTextCompletionEnabled = true;

            lv_ChiTietHoaDon.ItemsSource = GioHang;
            //tb_SDTKH.ItemsSource = Adapters.CustomerAdapter.ListCustomer;
        }
        private Customer c;
        public Customer SelectedCustomer { get { return c; } set { c = value; NotifyPropertyChanged("SelectedCustomer"); } }

        private Book b;
        public Book SelectedBook { get { return b; } set { b = value; NotifyPropertyChanged("SelectedBook"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        #region XuLyHoaDon
        private ObservableCollection<BuyingBook> _listbook = new ObservableCollection<BuyingBook>();
        public ObservableCollection<BuyingBook> GioHang
        {
            get
            {
                return _listbook;
            }
            set
            {
                _listbook = value;
                NotifyPropertyChanged("GioHang");
            }
        }
        private void btn_ThemSachVaoHoaDon_Click(object sender, RoutedEventArgs e)
        {
            var item = GioHang.FirstOrDefault(x=>x.Book.ID == SelectedBook.ID);
            if(item!=null)
            {
                item.Number += (int)num_SLSach.Value;
                lv_ChiTietHoaDon.SelectedItem = item;
            }
            else
            {
                BuyingBook buybook = new BuyingBook(SelectedBook, (int)num_SLSach.Value);
                GioHang.Add(buybook);
            }
        }

        #endregion
    }
}
