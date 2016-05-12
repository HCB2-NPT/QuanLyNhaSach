﻿using QuanLyNhaSach.Objects;
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
        public tabThemHoaDonMoi()
        {
            InitializeComponent();
            tb_SDTKH.ItemsSource = Adapters.CustomerAdapter.GetAll();
            tb_SDTKH.FilterMode = AutoCompleteFilterMode.Contains;
            tb_SDTKH.IsTextCompletionEnabled = true;

            tb_NameBook.ItemsSource = Adapters.BookAdapter.GetAll();
            tb_NameBook.FilterMode = AutoCompleteFilterMode.Contains;
            tb_NameBook.IsTextCompletionEnabled = true;
            tblock_TienHoaDon.DataContext = BookCart;

            lv_ChiTietHoaDon.ItemsSource = BookCart.Cart;
            //tb_SDTKH.ItemsSource = Adapters.CustomerAdapter.ListCustomer;
        }
        private Customer c;
        public Customer SelectedCustomer { get { return c; } set { c = value; NotifyPropertyChanged("SelectedCustomer"); } }

        private Book b;
        public Book SelectedBook { get { return b; } set { b = value; NotifyPropertyChanged("SelectedBook"); } }

        

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
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private BookCart _bookCart = new BookCart();

        public BookCart BookCart
        {
            get { return _bookCart; }
            set
            {
                _bookCart = value;

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
                BuyingBook buybook = new BuyingBook(BookCart,SelectedBook, (int)num_SLSach.Value);
                BookCart.Cart.Add(buybook);
            }
        }
        #endregion

        private void tb_TienKhachTra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void tb_TienKhachTra_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookCart.PayMoney = int.Parse(tb_TienKhachTra.Text);
            BookCart.UpdateReturnMoney();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_SDTKH.SelectedItem = (tb_SDTKH.ItemsSource as ObservableCollection<Customer>).First();
        }
    }
}
