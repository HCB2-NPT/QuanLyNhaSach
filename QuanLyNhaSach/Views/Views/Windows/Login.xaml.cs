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
        public string Username { get { return tbEmail.Text; } set { tbEmail.Text = value; } }

        public string Password { get { return pbMK.Password; } set { pbMK.Password = value; } }

        public MainWindow MainWindow { get; private set; }

        private Assets.Scripts.WindowsDragger Dragger { get; set; }

        private int _loginWarningTimes = 0;
        public int LoginWarningTimes { get { return _loginWarningTimes; } set { _loginWarningTimes = value; NotifyPropertyChanged("LoginWarningTimes"); NotifyPropertyChanged("LoginWarningColor"); } }

        private Assets.Scripts.WindowsDragger EventWindowDrag { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public Login()
        {
            InitializeComponent();
            Dragger = new Assets.Scripts.WindowsDragger(this, menu);
            DataContext = Managers.Manager.Current;
        }

        private void HandleLoginData(Error error, bool checkFail = true)
        {
            if (error == null)
            {
                LoginWarningTimes = 0;
                Visibility = System.Windows.Visibility.Hidden;
                Username = "";
                Password = "";
                if (MainWindow == null)
                {
                    MainWindow = new MainWindow();
                    MainWindow.Owner = this;
                    MainWindow.Show();
                }
                else
                    MainWindow.Visibility = System.Windows.Visibility.Visible;
            }
            else if (checkFail)
            {
                LoginWarningTimes++;
                if (LoginWarningTimes > Managers.ConfigManager.Current.MaxTimesLoginFail)
                    Managers.ErrorManager.Current.LoginWrongManyTimes.Call();
                else
                    tbEmail.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Managers.ConfigManager.Current.TestMode_PassLogin)
            {
                Managers.Manager.Current.Error.Ignore = true;
                HandleLoginData(Managers.Manager.Current.User.Login(Managers.ConfigManager.Current.TestMode_PassLogin_Username, Managers.ConfigManager.Current.TestMode_PassLogin_Password, LoginWarningTimes), false);
                Managers.Manager.Current.Error.Ignore = false;
            }
        }

        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                HandleLoginData(Managers.Manager.Current.User.Login(Username, Password, LoginWarningTimes));
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void pbMK_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
        	if (string.IsNullOrEmpty(Password))
				pbMK.ToolTip = "B********p";
			else
				pbMK.ToolTip = null;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HandleLoginData(Managers.Manager.Current.User.Login(Username, Password, LoginWarningTimes));
        }
    }
}
