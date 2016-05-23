using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Bill : Editable
    {
        private int _id;
        private DateTime _dateCreate;
        private Customer _customer = null;
        private int _payMoney = 0;
        private int _totalMoney = 0;
        private ObservableCollection<BillItem> _billItems = new ObservableCollection<BillItem>();

        private void _listbook_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (BillItem item in e.NewItems)
                {
                    item.Container = this;
                }
            }

            NotifyPropertyChanged("TotalMoney");
            NotifyPropertyChanged("TotalMoneyFormat");
            NotifyPropertyChanged("ReturnMoney");
            NotifyPropertyChanged("ReturnMoneyFormat");
        }
        
        private void Init()
        {
            _billItems.CollectionChanged += _listbook_CollectionChanged;
        }

        public void WhenChildreUpdate()
        {
            NotifyPropertyChanged("TotalMoney");
            NotifyPropertyChanged("TotalMoneyFormat");
            NotifyPropertyChanged("ReturnMoney");
            NotifyPropertyChanged("ReturnMoneyFormat");
        }

        #region Constructor
        public Bill() : base(true)
        {
            _id = 0;
            _dateCreate = DateTime.Now;
            Init();
        }

        public Bill(int id, DateTime dateCreated) : base()
        {
            _id = id;
            _dateCreate = dateCreated;
            Init();
        }
        #endregion

        #region Properties
        public DateTime DateCreated
        {
            get { return _dateCreate; }
            set { _dateCreate = value; NotifyPropertyChanged("DateCreate"); }
        }

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; NotifyPropertyChanged("Customer"); }
        }

        public int ID
        {
            get { return _id; }
        }

        public int PayMoney
        {
            get { return _payMoney; }
            set
            {
                _payMoney = value;
                NotifyPropertyChanged("PayMoney");
                NotifyPropertyChanged("PayMoneyFormat");
                NotifyPropertyChanged("TotalMoney");
                NotifyPropertyChanged("TotalMoneyFormat");
                NotifyPropertyChanged("ReturnMoney");
                NotifyPropertyChanged("ReturnMoneyFormat");
            }
        }

        public int ReturnMoney
        {
            get
            {
                return PayMoney - TotalMoney;
            }
        }

        public int TotalMoney
        {
            get
            {
                _totalMoney = 0;
                foreach (var item in BillItems)
                {
                    _totalMoney += item.Total;
                }
                return _totalMoney;
            }
        }

        public ObservableCollection<BillItem> BillItems
        {
            get
            {
                return _billItems;
            }
        }
        #endregion

        #region PropertiesFormat
        public string TotalMoneyFormat
        {
            get
            {
                return TotalMoney.ToString("#,##0 vnđ");
            }
        }

        public string ReturnMoneyFormat
        {
            get
            {
                return ReturnMoney.ToString("#,##0 vnđ");
            }
        }

        public string PayMoneyFormat
        {
            get
            {
                return PayMoney.ToString("#,##0 vnđ");
            }
        }
        #endregion
    }
}
