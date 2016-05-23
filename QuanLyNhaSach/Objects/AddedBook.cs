using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class AddedBook : Editable
    {
        private int _iD;

        public int ID
        {
            get { return _iD; }
        }

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { _book = value; NotifyPropertyChanged("Book"); }
        }

        private int _number;

        public int Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged("Number"); }
        }

        #region Constructors

        public AddedBook():base(true)
        {
            _iD = 0;
        }

        public AddedBook(int id):base()
        {
            _iD = id;
        }
        #endregion
    }
}
