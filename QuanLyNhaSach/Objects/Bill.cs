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
            set { _dateCreate = value; }
        }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        private int _iDBill;
        public int IDBill
        {
            get { return _iDBill; }
            set { _iDBill = value; }
        }
        private int _payMoney = 0;
        public int PayMoney
        {
            get { return _payMoney; }
            set
            {
                _payMoney = value;
                NotifyPropertyChanged("ReturnMoney");
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
            set
            {
                _billItems = value;
                NotifyPropertyChanged("Cart");
                NotifyPropertyChanged("TotalMoney");
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
            NotifyPropertyChanged("ReturnMoney");
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
            NotifyPropertyChanged("ReturnMoney");
        }
    }
}
