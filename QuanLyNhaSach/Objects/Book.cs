using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Book : INotifyPropertyChanged
    {
        private string _author = "";

        public string Author
        {
            get { return _author; }
            set { _author = value; NotifyPropertyChanged("Author"); }
        }
        private string _type = "";

        public string Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged("Type"); }
        }
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
            set { _id = value; NotifyPropertyChanged("ID"); }
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
