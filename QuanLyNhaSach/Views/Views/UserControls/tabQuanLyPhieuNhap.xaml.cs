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
using System.Linq;
using QuanLyNhaSach.Managers;

namespace QuanLyNhaSach.Views.Views.UserControls
{
	/// <summary>
	/// Interaction logic for tabQuanLyPhieuNhap.xaml
	/// </summary>
	public partial class tabQuanLyPhieuNhap : UserControl,INotifyPropertyChanged
    {
        #region Properties

        private DateTime BackupDate = new DateTime();

        ObservableCollection<ManagerListAddedBook> _listMLAB = Bus.FillData.GetAllManagerListAddedBook();
        public ObservableCollection<ManagerListAddedBook> ListMLAB
        {
            get { return _listMLAB; }
            set { _listMLAB = value; NotifyPropertyChanged("ListMLAB"); }
        }

        private int MinNumberWhenImport
        {
            get
            {
                return Managers.RuleManager.Current.Rule.MinNumberToImport;
            }
        }

        private ManagerListAddedBook _selectedMLAB = new ManagerListAddedBook();
        public ManagerListAddedBook SelectedMLAB 
        {
            get
            {
                if (lv_ListPN.SelectedItem != null)
                    return lv_ListPN.SelectedItem as ManagerListAddedBook;
                return _selectedMLAB;
            }
            set
            {
                _selectedMLAB = value;
                NotifyPropertyChanged("SelectedMLAB");
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

        #region Constructor
        public tabQuanLyPhieuNhap()
		{
			this.InitializeComponent();
            num_UpDown.DataContext = Managers.RuleManager.Current;
            DataContext = this;
        }
        #endregion

        private void lv_ListPN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_ListPN.SelectedItem != null)
            {
                SelectedMLAB = lv_ListPN.SelectedItem as ManagerListAddedBook;
                SelectedMLAB.ListAddedBook = new ObservableCollection<AddedBook>();
                SelectedMLAB.ListAddedBook = Bus.FillData.GetAllListAddedBook(SelectedMLAB.ID);
                //NotifyPropertyChanged("SelectedMLAB");
            }
            
        }

        private void btn_DeletePN(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag!=null)
            {
                ManagerListAddedBook mlab = btn.Tag as ManagerListAddedBook;
                ListMLAB.Remove(mlab);
                NotifyPropertyChanged("ListMLAB");
                Bus.DeleteData.DeleteManagerListAddedBook(mlab);
            }
        }

        private void dt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker dp = sender as DatePicker;
            if (dp.SelectedDate < (dp.Tag as ManagerListAddedBook).DateCreated)
            {
                ErrorManager.Current.WrongDateTime.Call("Ngày nhập kho phải tính từ ngày lập phiếu trở đi!");
                (sender as DatePicker).SelectedDate = BackupDate;
            }
            
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            BackupDate = (sender as DatePicker).SelectedDate.Value;
        }

        private void btn_DeleteBook(object sender, RoutedEventArgs e)
        {
            ((sender as Button).Tag as AddedBook).IsDeletedItem = true;
            NotifyPropertyChanged("SelectedMLAB");
        }

        private void btn_SaveChangeItem(object sender, RoutedEventArgs e)
        {
            if (SelectedMLAB!=null)
            {
                Bus.SaveChanges.SaveChangesListAddedBook(SelectedMLAB);
                SelectedMLAB.ListAddedBook = Bus.FillData.GetAllListAddedBook(SelectedMLAB.ID);
            }
        }

        private void btn_Restore_Click(object sender, RoutedEventArgs e)
        {
            ((sender as Button).Tag as AddedBook).IsDeletedItem = false;
        }

        private void btn_SaveDate(object sender, RoutedEventArgs e)
        {

            Bus.SaveChanges.SaveChangeManagerListAddedBook((sender as Button).Tag as ManagerListAddedBook);
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Search.Text!="")
            {
                ManagerListAddedBook mlab = ListMLAB.FirstOrDefault(x => x.ID.ToString() == tb_Search.Text.ToString());
                if (mlab == null)
                {
                    ErrorManager.Current.InfoIsNull.Call("Không tìm được phiếu có mã cần tìm!");
                    tb_Search.Text = "";
                }
                else
                {
                    lv_ListPN.SelectedItem = mlab;
                    lv_ListPN.ScrollIntoView(mlab);
                }
            }
        }

       


    }
}