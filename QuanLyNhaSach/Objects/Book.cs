using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Book : Editable
    {
        private ObservableCollection<Author> _authors;

        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value; 
                NotifyPropertyChanged("Authors");
                NotifyPropertyChanged("AuthorsFormat");
            }
        }
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                NotifyPropertyChanged("Genres");
                NotifyPropertyChanged("GenresFormat");
            }
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
            set
            {
                _image = value;
                NotifyPropertyChanged("Image");
                NotifyPropertyChanged("ImageFormat");
            }
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
        public Book() : base(true)
        {
            _id = 0;
        }

        public Book(int id) : base()
        {
            _id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region FormatProperty

        public string AuthorsFormat
        {
            get
            {
                string format = "";
                int max_length = 40;
                foreach (Author item in Authors)
                {
                    format += item.Name + ", ";
                    if (format.Length > max_length)
                        break;
                }
                format = format.TrimEnd(' ', ',');
                if (format.Length > max_length)
                {
                    format = format.Remove(40);
                    format += "...";
                }
                if (string.IsNullOrEmpty(format))
                    format = "<Không có tác giả rõ ràng>";
                return format;
            }
        }

        public string GenresFormat
        {
            get
            {
                int max_length = 40;
                string format = "";
                foreach (Genre item in Genres)
                {
                    format += item.Name + ", ";
                    if (format.Length > max_length)
                        break;
                }
                format = format.TrimEnd(' ', ',');
                if (format.Length > max_length)
                {
                    format = format.Remove(40);
                    format += "...";
                }
                if (string.IsNullOrEmpty(format))
                    format = "<Không có thể loại rõ ràng>";
                return format;
            }
        }
        public string ImageFormat
        {
            get
            {
                if (Image == null)
                    return Directory.GetCurrentDirectory() + "\\Data\\Images\\no_image.png";
                if (File.Exists(Image))
                    return Image;
                else
                    return Directory.GetCurrentDirectory() + "\\Data\\Images\\" + Image;
            }
        }

        #endregion
    }
}
