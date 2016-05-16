using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Author : Editable
    {
        private int _id;
        private string _name = null;

        #region Constructor
        public Author() : base(true)
        {
            _id = 0;
        }

        public Author(int id) : base()
        {
            _id = id;
        }
        #endregion

        #region Properties
        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        #endregion
    }
}
