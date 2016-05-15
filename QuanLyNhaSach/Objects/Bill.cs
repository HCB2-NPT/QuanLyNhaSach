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
        private DateTime _dateCreate;

        public DateTime DateCreate
        {
            get { return _dateCreate; }
            set { _dateCreate = value; NotifyPropertyChanged("DateCreate"); }
        }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; NotifyPropertyChanged("Customer"); }
        }

        private int _iDBill;
        public int IDBill
        {
            get { return _iDBill; }
        }
        private int _payMoney = 0;
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

        private int _totalMoney = 0;
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

        private ObservableCollection<BillItem> _billItems = new ObservableCollection<BillItem>();
        public ObservableCollection<BillItem> BillItems
        {
            get
            {
                return _billItems;
            }
        }

        void _listbook_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

        #region Constructor
        public Bill() : base(true)
        {
            _iDBill = 0;
            Init();
        }

        public Bill(int id) : base()
        {
            _iDBill = id;
            Init();
        }

        private void Init()
        {
            _billItems.CollectionChanged += _listbook_CollectionChanged;
        }
        #endregion

        public void WhenChildreUpdate()
        {
            NotifyPropertyChanged("TotalMoney");
            NotifyPropertyChanged("TotalMoneyFormat");
            NotifyPropertyChanged("ReturnMoney");
            NotifyPropertyChanged("ReturnMoneyFormat");
        }

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
    }
}
