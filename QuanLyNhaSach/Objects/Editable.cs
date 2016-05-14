﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Editable : INotifyPropertyChanged
    {
        public Editable(bool isNew = false)
        {
            IsCreatedItem = false;
            IsEditedItem = false;
            IsDeletedItem = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (!IsInitializingItem)
                IsEditedItem = true;
        }

        public bool IsCreatedItem { get; set; }

        public bool IsEditedItem { get; set; }

        public bool IsDeletedItem { get; set; }

        public bool IsInitializingItem { get; set; }

        public void BeginInit()
        {
            IsInitializingItem = true;
        }

        public void EndInit()
        {
            IsInitializingItem = false;
        }
    }
}