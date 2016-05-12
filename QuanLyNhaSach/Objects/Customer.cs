using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Customer : Editable
    {
        private int _id;
        private string _name = null;
        private string _adress = null;
        private string _phone = null;
        private int _debt = 0;
        private string _email = null;
        private bool _isDeleted = false;

        public Customer() : base(true)
        {
            _id = 0;
        }

        public Customer(int id) : base()
        {
            _id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ID { get { return _id; } }
        public string Email { get { return _email; } set { _email = value; NotifyPropertyChanged("Email"); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); } }
        public bool IsDeleted { get { return _isDeleted; } set { _isDeleted = value; NotifyPropertyChanged("IsDeleted"); } }
        public string Adress { get { return _adress; } set { _adress = value; NotifyPropertyChanged("Adress"); } }
        public string Phone { get { return _phone; } set { _phone = value; NotifyPropertyChanged("Phone"); } }
        public int Debt { get { return _debt; } set { _debt = value; NotifyPropertyChanged("Debt"); } }

        public int PhoneByInteger
        {
            get
            {
                int result;
                if (int.TryParse(Phone, out result))
                    return result;
                return 0;
            }
        }

        public string CustomerInfo
        {
            get
            {
                return string.Format("{0:0000 0000 0000} - {1}", PhoneByInteger, Name);
            }
        }
    }
}
