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
        }

        private void lv_DSHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_DSHoaDon.SelectedItem!=null)
                SelectedBill = lv_DSHoaDon.SelectedItem as Bill;
        }
    }
}
