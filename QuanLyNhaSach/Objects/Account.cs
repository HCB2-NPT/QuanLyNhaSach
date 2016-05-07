using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Account : INotifyPropertyChanged
    {
        private int _id;
        private string _email = null;
        private string _password = null;
        private string _name = null;
        private AccessLevel _accessLevel = null;
        private bool _isDeleted = false;
        
        public Account()
        {
            _id = 0;
        }

        public Account(int id)
        {
            _id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ID { get { return _id; } private set { _id = value; NotifyPropertyChanged("ID"); } }
        public string Email { get { return _email; } set { _email = value; NotifyPropertyChanged("Email"); } }
        public string Password { get { return _password; } set { _password = value; NotifyPropertyChanged("Password"); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); } }
        public AccessLevel AccessLevel { get { return _accessLevel; } set { _accessLevel = value; NotifyPropertyChanged("AccessLevel"); } }
        public bool IsDeleted { get { return _isDeleted; } set { _isDeleted = value; NotifyPropertyChanged("IsDeleted"); } }
    }
}
