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

namespace QuanLyNhaSach.Views.Views.UserControls
{
	/// <summary>
	/// Interaction logic for tabQuanLyPhieuNhap.xaml
	/// </summary>
	public partial class tabQuanLyPhieuNhap : UserControl,INotifyPropertyChanged
    {
        #region Properties

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
                NotifyPropertyChanged("SelectedMLAB");
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
            }
        }

        private void dt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("1");
            MessageBox.Show((sender as DatePicker).SelectedDate.Value.ToString());
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("2");
        }


    }
}