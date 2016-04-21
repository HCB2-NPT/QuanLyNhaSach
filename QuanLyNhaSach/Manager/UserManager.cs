using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
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

        private Data.Account _user = null;
        public Data.Account Info { get { return _user; } set { _user = value; NotifyPropertyChanged("Info"); } }

        public Data.Error Login(string username, string password, int timesfail)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (_user != null)
                    return ErrorManager.Current.LoginDuplicate.Call();
                Data.Account user;
                if (!Adapter.AccoutAdapter.IsExists(username, password, out user))
                    return ErrorManager.Current.NotExistsAccount.Call();
                Info = user;
                return null;
            }
            return ErrorManager.Current.WrongUsernameOrPassword.Call();
        }

        public Data.Error Logout()
        {
            if (_user == null)
                return ErrorManager.Current.NoOneLogin.Call();
            _user = null;
            return null;
        }
    }
}
