using QuanLyNhaSach.Errors;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public class UserManager : INotifyPropertyChanged
    {
        private static UserManager _current = null;
        public static UserManager Current
        {
            get
            {
                if (_current == null)
                    _current = new UserManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private Account _user = null;
        public Account Info { get { return _user; } set { _user = value; NotifyPropertyChanged("Info"); } }

        public Error Login(string username, string password, int timesfail)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (_user != null)
                    return ErrorManager.Current.LoginDuplicate.Call();
                Account user;
                if (!Bus.CheckData.IsAccountExist(username, password, out user))
                    return ErrorManager.Current.NotExistsAccount.Call();
                Info = user;
                return null;
            }
            return ErrorManager.Current.WrongUsernameOrPassword.Call();
        }

        public Error Logout()
        {
            if (_user == null)
                return ErrorManager.Current.NoOneLogin.Call();
            _user = null;
            return null;
        }
    }
}
