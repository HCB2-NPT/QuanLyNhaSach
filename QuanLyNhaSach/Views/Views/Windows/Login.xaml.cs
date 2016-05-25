using QuanLyNhaSach.Errors;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {
        #region Properties
        public string Username { get { return tbEmail.Text; } set { tbEmail.Text = value; } }

        public string Password { get { return pbMK.Password; } set { pbMK.Password = value; } }
        #endregion

        #region Binding Controlers
        private Assets.Scripts.WindowsDragger Dragger { get; set; }

        private Assets.Scripts.WindowsDragger EventWindowDrag { get; set; }

        private int _loginWarningTimes = 0;
        public int LoginWarningTimes { get { return _loginWarningTimes; } set { _loginWarningTimes = value; NotifyPropertyChanged("LoginWarningTimes"); NotifyPropertyChanged("LoginWarningColor"); } }

        public string LoginWarningColor
        {
            get
            {
                if (_loginWarningTimes == 0)
                    return "#00000000";
                int p = Convert.ToInt32(Math.Min(((float)_loginWarningTimes / (float)Managers.ConfigManager.Current.MaxTimesLoginFail) * 64f + 160f, 224f));
                return "#FF" + p.ToString("x2") + "0000";

            }
        }
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
        #endregion

        #region Constructor
        public Login()
        {
            InitializeComponent();
            Dragger = new Assets.Scripts.WindowsDragger(this, menu);
            DataContext = Managers.Manager.Current;
        }
        #endregion
    }
}
