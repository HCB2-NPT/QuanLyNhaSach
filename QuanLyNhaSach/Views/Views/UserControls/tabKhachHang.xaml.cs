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
    /// Interaction logic for tabKhachHang.xaml
    /// </summary>
    public partial class tabKhachHang : UserControl,INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<Customer> _listCustomer = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> ListCustomer
        {
            get { return _listCustomer; }
            set { _listCustomer = value; NotifyPropertyChanged("ListCustomer"); }
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
        public tabKhachHang()
        {
            InitializeComponent();
            DataContext = this;
            ListCustomer = Bus.FillData.GetAllCustomer();
        }
        private void tb_SearchNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = ListCustomer.FirstOrDefault(x =>
            (x.Phone != null && x.Phone.Contains(tb_Search.Text)) ||
            (x.Email != null && x.Email.Contains(tb_Search.Text)) ||
            (x.Name != null && x.Name.Contains(tb_Search.Text)) ||
            (x.Adress != null && x.Adress.Contains(tb_Search.Text)));
            lv_DSKhachHang.SelectedItem = customer;
            tb_Search.Text = "";
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer()
            {
                Adress = tb_Adress.Text,
                Debt=0,
                Email=tb_Email.Text,
                Name = tb_Name.Text,
                Phone = tb_Phone.Text
            };
            ListCustomer.Add(c);
        }

        private void btn_Restore_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag!=null)
            {
                Customer c = btn.Tag as Customer;
                c.IsDeletedItem = false;
            }
        }


        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Customer item in lv_DSKhachHang.SelectedItems)
            {
                item.IsDeletedItem = true;
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            ListCustomer = Bus.FillData.GetAllCustomer();
        }

        private void btn_SaveChange_Click(object sender, RoutedEventArgs e)
        {
            Bus.SaveChanges.SaveChangesCustomers(ListCustomer);
        }

        private void tb_Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }
    }
}
