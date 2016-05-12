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
        private BookCart _bookCart;
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
                NotifyPropertyChanged("Total");
                _bookCart.UpdateItemNumber();
            }
        }

        private int _total = 0;

        public int Total
        {
            get { return Book.Price*Number; }
        }



        public BuyingBook(BookCart bc,Book b, int _num)
        {
            _bookCart = bc;
            _book = b;
            _number = _num;
        }

        public BuyingBook(BookCart bc)
        {
            _bookCart = bc;
            _book = new Book();
            _number = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
