﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager.Data
{
    public class AccessLevel : INotifyPropertyChanged
    {
        //==================================================
        /*
         * Khởi tạo các quyền truy cập ban có sẵn!
         */
        private static ObservableCollection<AccessLevel> _accessLevelList = null;
        public static ObservableCollection<AccessLevel> AccessLevelList
        {
            get
            {
                if (_accessLevelList == null)
                    _accessLevelList = Adapter.AccessLevelAdapter.GetList();
                return _accessLevelList;
            }
        }
        //==================================================

        private int _id;
        private string _name = "<Tên phân quyền>";

        public AccessLevel()
        {
            _id = 0;
        }

        public AccessLevel(int id)
        {
            _id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ID { get { return _id; } private set { _id = value; NotifyPropertyChanged("ID"); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); } }
    }
}
