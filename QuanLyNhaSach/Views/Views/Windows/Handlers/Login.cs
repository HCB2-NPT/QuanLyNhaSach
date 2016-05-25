using QuanLyNhaSach.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class Login
    {
        #region Methods
        /*
         * Hàm xử lý Username/Password được nhập
         */
        private void HandleLoginData(Error error)
        {
            if (error == null)
            {
                Bus.AppHandler.VirtualWindowClose(this);
                LoginWarningTimes = 0;
                Username = "";
                Password = "";
                var MainWindow = new MainWindow();
                MainWindow.Owner = this;
                MainWindow.Show();
                MainWindow.HandleAfterLogin();
            }
            else if (!Managers.Manager.Current.Error.Ignore)
            {
                LoginWarningTimes++;
                if (LoginWarningTimes > Managers.ConfigManager.Current.MaxTimesLoginFail)
                    Managers.ErrorManager.Current.LoginWrongManyTimes.Call();
                else
                    tbEmail.Focus();
            }
        }
        #endregion

        #region Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Managers.ConfigManager.Current.TestMode_PassLogin)
            {
                Managers.Manager.Current.Error.Ignore = true;
                HandleLoginData(Managers.Manager.Current.User.Login(Managers.ConfigManager.Current.TestMode_PassLogin_Username, Managers.ConfigManager.Current.TestMode_PassLogin_Password, LoginWarningTimes));
                Managers.Manager.Current.Error.Ignore = false;
            }
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void window_Closed(object sender, EventArgs e)
        {
            Bus.AppHandler.Shutdown();
        }
        #endregion

        #region Button Events
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HandleLoginData(Managers.Manager.Current.User.Login(Username, Password, LoginWarningTimes));
        }
        #endregion

        #region HotKey Events
        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                HandleLoginData(Managers.Manager.Current.User.Login(Username, Password, LoginWarningTimes));
        }
        #endregion

        #region Animation Controlers
        /*
         * Password box không animation trên xaml dc nên phải trích ra animation dưới cs
         */
        private void pbMK_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Password))
                pbMK.ToolTip = "Mật Khẩu";
            else
                pbMK.ToolTip = null;
        }
        #endregion
    }
}
