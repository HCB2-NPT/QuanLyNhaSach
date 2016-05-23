using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class ManagerListAddedBook : Editable
    {
        private ObservableCollection<AddedBook> _listAddedBook = new ObservableCollection<AddedBook>();
        public ObservableCollection<AddedBook> ListAddedBook
        {
            get { return _listAddedBook; }
            set { _listAddedBook = value; NotifyPropertyChanged("ListAddedBook"); }
        }

        private DateTime _dateCreate = new DateTime();

        public DateTime DateCreated
        {
            get { return _dateCreate; }
            set { _dateCreate = value; NotifyPropertyChanged("DateCreate"); }
        }

        private DateTime _dateAddIntoStorage = new DateTime();
        public DateTime DateAddIntoStorage
        {
            get { return _dateAddIntoStorage; }
            set { _dateAddIntoStorage = value; NotifyPropertyChanged("DateAddIntoStorage"); }
        }

        private int _iDManager;
        public int IDManager
        {
            get { return _iDManager; }
            set { _iDManager = value; }
        }

        private int _iD;
        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        public ManagerListAddedBook():base(true)
        {
            _iD = 0;
        }
        public ManagerListAddedBook(int id): base()
        {
            _iD = id;
        }
    }
}
