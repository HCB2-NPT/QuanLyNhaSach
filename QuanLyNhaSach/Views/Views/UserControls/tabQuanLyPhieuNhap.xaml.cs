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
            DataContext = this;
        }
        #endregion
    }
}