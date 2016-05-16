﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Genre : Editable
    {
        private int _id;
        private string _name = null;

        #region Constructor
        public Genre() : base(true)
        {
            _id =0;
        }

        public Genre(int id) : base()
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
