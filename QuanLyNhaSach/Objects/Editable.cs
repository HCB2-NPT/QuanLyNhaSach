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
        #region Constructor
        public Editable(bool isNew = false)
        {
            _isCreatedItem = isNew;
            _isEditedItem = false;
            _isDeletedItem = false;
        }
        #endregion

        #region Implements
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (!_pThrough.Any(x => x == propertyName))
            {
                if (!IsInitializingItem && !IsEditedItem)
                    IsEditedItem = true;
            }
        }
        #endregion

        #region Properties control the editting
        private bool _isCreatedItem;
        private bool _isEditedItem;
        private bool _isDeletedItem;

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
        #endregion

        #region Properties can through
        private string[] _pThrough = { "IsCreatedItem", 
                                     "IsEditedItem", 
                                     "IsDeletedItem", 
                                     "IsNotDeletedItem", 
                                     "Tag", 
                                     "Switch", 
                                     "NotSwitch" };

        public string[] PThrough { get { return _pThrough; } }
        #endregion

        #region Custom Properties
        private object _tag = null;
        private bool _switch = false;

        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                NotifyPropertyChanged("Tag");
            }
        }

        public bool Switch
        {
            get
            {
                return _switch;
            }
            set
            {
                _switch = value;
                NotifyPropertyChanged("Switch");
                NotifyPropertyChanged("NotSwitch");
            }
        }

        public bool NotSwitch
        {
            get
            {
                return !_switch;
            }
            set
            {
                _switch = !value;
                NotifyPropertyChanged("Switch");
                NotifyPropertyChanged("NotSwitch");
            }
        }
        #endregion
    }
}
