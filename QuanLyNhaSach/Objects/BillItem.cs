using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class BillItem : Editable
    {
        public Bill Container { get; set; }

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set
            {
                _book = value;
                NotifyPropertyChanged("Book");
            }
        }

        private int _number = 0;

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
                NotifyPropertyChanged("Total");
                NotifyPropertyChanged("TotalFormat");
                Container.WhenChildreUpdate();
            }
        }

        public int Total
        {
            get { return Price*Number; }
        }

        private int _price = 0;
        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
                NotifyPropertyChanged("PriceFormat");
                NotifyPropertyChanged("Total");
                NotifyPropertyChanged("TotalFormat");
                Container.WhenChildreUpdate();
            }
        }

        public string TotalFormat
        {
            get
            {
                return Total.ToString("#,##0 vnđ");
            }
        }

        public string PriceFormat
        {
            get
            {
                return Price.ToString("#,##0 vnđ");
            }
        }

        public BillItem(Book b, int num, int price) : base()
        {
            _book = b;
            _number = num;
            _price = price;
        }

        public BillItem() : base(true)
        {
            _book = new Book();
        }
    }
}
