using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
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

        ManagerListAddedBook _mLAB = new ManagerListAddedBook();

        public ManagerListAddedBook MLAB
        {
            get { return _mLAB; }
            set { _mLAB = value; NotifyPropertyChanged("MLAB"); }
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
            MLAB.ListAddedBook = Bus.FillData.GetAllAddedBook();
        }
        #endregion
    }
}