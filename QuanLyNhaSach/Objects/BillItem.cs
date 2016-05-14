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
                NotifyPropertyChanged("Number");
                NotifyPropertyChanged("Total");
            }
        }

        private int _number;

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
                NotifyPropertyChanged("Total");
                Container.WhenChildreUpdate();
            }
        }

        public int Total
        {
            get { return Book.Price*Number; }
        }



        public BillItem(Book b, int _num) : base()
        {
            _book = b;
            _number = _num;
        }

        public BillItem() : base(true)
        {
            _book = new Book();
            _number = 0;
        }
    }
}
