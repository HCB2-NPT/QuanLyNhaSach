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
        #region Constants
        private const int min_length__AdressShortFormat = 30;
        #endregion

        private int _id;
        private string _name = null;
        private string _adress = null;
        private string _phone = null;
        private int _debt = 0;
        private string _email = null;
        private bool _isDeleted = false;

        #region Constructor
        public Customer() : base(true)
        {
            _id = 0;
        }

        public Customer(int id) : base()
        {
            _id = id;
        }
        #endregion

        #region Properties
        public int ID { get { return _id; } }
        public string Email { get { return _email; } set { _email = value; NotifyPropertyChanged("Email"); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); } }
        public bool IsDeleted { get { return _isDeleted; } set { _isDeleted = value; NotifyPropertyChanged("IsDeleted"); } }
        public string Adress { get { return _adress; } set { _adress = value; NotifyPropertyChanged("Adress"); } }
        public string Phone { get { return _phone; } set { _phone = value; NotifyPropertyChanged("Phone"); } }
        public int Debt { get { return _debt; } set { _debt = value; NotifyPropertyChanged("Debt"); NotifyPropertyChanged("DebtFormat"); } }
        #endregion

        #region PropertiesFormat
        public int PhoneByInteger
        {
            get
            {
                if (string.IsNullOrEmpty(Phone))
                    return 0;
                int result;
                if (int.TryParse(Phone, out result))
                    return result;
                return 0;
            }
        }

        public string PhoneFormat
        {
            get
            {
                return string.Format("(+84) {0:0000 0000 000}", PhoneByInteger);
            }
        }

        public string CustomerInfo
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                    return null;
                return string.Format("{0:0000 0000 0000} - {1}", PhoneByInteger, Name);
            }
        }

        public string AdressShortFormat
        {
            get
            {
                var format = Adress;
                if (string.IsNullOrEmpty(format))
                    return "<Không có thông tin địa chỉ>";
                if (format.Length > min_length__AdressShortFormat)
                {
                    format = format.Remove(min_length__AdressShortFormat);
                    format += "...";
                }
                return format;
            }
        }

        public string DebtFormat
        {
            get
            {
                return Debt.ToString("#,##0 vnđ");
            }
        }
        #endregion
    }
}
