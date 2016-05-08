using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Book : INotifyPropertyChanged
    {
        private ObservableCollection<Author> _listAuthor;

        public ObservableCollection<Author> ListAuthor
        {
            get { return _listAuthor; }
            set { _listAuthor = value; NotifyPropertyChanged("ListAuthor"); }
        }
        private ObservableCollection<Genre> _listGenre;

        public ObservableCollection<Genre> ListGenre
        {
            get { return _listGenre; }
            set { _listGenre = value; NotifyPropertyChanged("ListGenre"); }
        }
        private string _author = "";

      
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; NotifyPropertyChanged("Image"); }
        }
        private int _number;

        public int Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged("Number"); }
        }
        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged("Price"); }
        }
        private bool _isdelete;

        public bool IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; NotifyPropertyChanged("IsDelete"); }
        }
        private int _id;

        public int ID
        {
            get { return _id; }
        }
        public Book()
        {
            _id = 0;
        }

        public Book(int id)
        {
            _id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    }

   
}
