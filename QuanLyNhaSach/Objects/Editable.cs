using System;
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
            _isCreatedItem = isNew;
            _isEditedItem = false;
            _isDeletedItem = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (!names.Any(x => x == propertyName))
            {
                if (!IsInitializingItem && !IsEditedItem && !IsCreatedItem)
                    IsEditedItem = true;
            }
        }

        private bool _isCreatedItem;
        private bool _isEditedItem;
        private bool _isDeletedItem;

        private string[] names = { "IsCreatedItem", "IsEditedItem", "IsDeletedItem", "IsNotDeletedItem" };

        public bool IsCreatedItem { get { return _isCreatedItem; } set { _isCreatedItem = value; NotifyPropertyChanged("IsCreatedItem"); } }

        public bool IsEditedItem { get { return _isEditedItem; } set { _isEditedItem = value; NotifyPropertyChanged("IsEditedItem"); } }

        public bool IsDeletedItem { get { return _isDeletedItem; } set { _isDeletedItem = value; NotifyPropertyChanged("IsDeletedItem"); NotifyPropertyChanged("IsNotDeletedItem"); } }

        public bool IsNotDeletedItem { get { return !_isDeletedItem; } }

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
