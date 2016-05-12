using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class BuyingBook : INotifyPropertyChanged
    {
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

        private int _number;

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }

        private int _total = 0;

        public int Total
        {
            get { return Book.Price*Number; }
            set { _total = Book.Price * Number; NotifyPropertyChanged("Total"); }
        }



        public BuyingBook(Book b, int _num)
        {
            _book = b;
            _number = _num;
        }

        public BuyingBook()
        {
            _book = new Book();
            _number = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
