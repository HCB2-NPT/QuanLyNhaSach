using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.Windows
{
    /// <summary>
    /// Interaction logic for WarningBox.xaml
    /// </summary>
    public partial class WarningBox : Window, IDisposable, INotifyPropertyChanged
    {
        #region Properties
        public string ErrorTitle { get { return tb_title.Text; } set { tb_title.Text = value; } }

        public string ErrorName { get { return tb_name.Text; } set { tb_name.Text = value; } }

        public string ErrorContent { get { return tb_content.Text; } set { tb_content.Text = value; } }

        public string ErrorException { get { return tb_exception.Text; } set { tb_exception.Text = value; NotifyPropertyChanged("ErrorExceptionHeight"); } }

        public bool IsCrash { get; set; }
        #endregion

        #region Binding Controlers
        public double ErrorExceptionHeight { get { return !string.IsNullOrEmpty(ErrorException) ? double.NaN : 0; } }

        private Assets.Scripts.WindowsDragger EventWindowDrag { get; set; }
        #endregion

        #region Implements
        /*
         * INotifyPropertyChanged
         */
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /*
         * IDisposable
         */
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Constructor
        public WarningBox(string title, string name, string content, bool isCrash = false, string exception = null)
        {
            InitializeComponent();
            EventWindowDrag = new Assets.Scripts.WindowsDragger(this, menu);
            DataContext = Managers.Manager.Current;
			
            ErrorTitle = title;
            ErrorName = name;
            ErrorContent = content;
            IsCrash = isCrash;
        }
        #endregion
    }
}
