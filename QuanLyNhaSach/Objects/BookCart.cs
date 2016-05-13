using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class BookCart :INotifyPropertyChanged
    {
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

        public int TotalMoney
        {
            get
            {
                int _totalMoney = 0;
                foreach (var item in Cart)
                {
                    _totalMoney += item.Total;
                }
                return _totalMoney;
            }
        }

        private ObservableCollection<BuyingBook> _listbook = new ObservableCollection<BuyingBook>();
        public ObservableCollection<BuyingBook> Cart
        {
            get
            {
                return _listbook;
            }
            set
            {
                _listbook = value;
                NotifyPropertyChanged("Cart");
                NotifyPropertyChanged("TotalMoney");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public BookCart()
        {
            _listbook.CollectionChanged += _listbook_CollectionChanged;
        }

        void _listbook_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("TotalMoney");
            NotifyPropertyChanged("ReturnMoney");
        }
    }
}
