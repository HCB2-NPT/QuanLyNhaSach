using QuanLyNhaSach.Managers;
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
        #region Constant
        private const int max_length__AuthorsShortFormat = 30;
        private const int max_length__GenresShortFormat = 40;
        #endregion

        #region Properties
        private ObservableCollection<Author> _authors;

        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                NotifyPropertyChanged("Authors");
                UpdateAuthorsFormat();
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
                UpdateGenresFormat();
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
            set { _price = value; NotifyPropertyChanged("Price"); NotifyPropertyChanged("PriceFormat"); }
        }
        private bool _isdeleted;

        public bool IsDeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; NotifyPropertyChanged("IsDelete"); }
        }
        private int _id;

        public int ID
        {
            get { return _id; }
        }
        #endregion

        public Book() : base(true)
        {
            _id = 0;
        }

        public Book(int id) : base()
        {
            _id = id;
        }

        #region PropertiesFormat
        private void UpdateAuthorsFormat()
        {
            string format = "";
            foreach (var item in Authors)
            {
                format += item.Name + ", ";
            }
            if (format.Length > 2)
                format = format.Remove(format.Length - 2);
            AuthorsFormat = format;
        }

        private string _authorsFormat = null;
        public string AuthorsFormat
        {
            get
            {
                if (string.IsNullOrEmpty(_authorsFormat))
                    return "<Không có tác giả rõ ràng>";
                return _authorsFormat;
            }
            set
            {
                _authorsFormat = value;
                NotifyPropertyChanged("AuthorsFormat");
                NotifyPropertyChanged("AuthorsShortFormat");
            }
        }

        public string AuthorsShortFormat
        {
            get
            {
                var format = AuthorsFormat;
                if (string.IsNullOrEmpty(format))
                    return "<Không có tác giả rõ ràng>";
                if (format.Length > max_length__AuthorsShortFormat)
                {
                    format = format.Remove(max_length__AuthorsShortFormat);
                    format += "...";
                }
                return format;
            }
        }

        private void UpdateGenresFormat()
        {
            string format = "";
            foreach (var item in Genres)
            {
                format += item.Name + ", ";
            }
            if (format.Length > 2)
                format = format.Remove(format.Length - 2);
            GenresFormat = format;
        }

        private string _genresFormat = null;
        public string GenresFormat
        {
            get
            {
                if (string.IsNullOrEmpty(_genresFormat))
                    return "<Không có thể loại rõ ràng>";
                return _genresFormat;
            }
            set
            {
                _genresFormat = value;
                NotifyPropertyChanged("GenresFormat");
                NotifyPropertyChanged("GenresShortFormat");
            }
        }

        public string GenresShortFormat
        {
            get
            {
                var format = GenresFormat;
                if (string.IsNullOrEmpty(format))
                    return "<Không có thể loại rõ ràng>";
                if (format.Length > max_length__GenresShortFormat)
                {
                    format = format.Remove(max_length__GenresShortFormat);
                    format += "...";
                }
                return format;
            }
        }

        public string ImageFormat
        {
            get
            {
                if (Image == null)
                    return DataManager.Current.FOLDER_IMAGES + "\\" + DataManager.Current.NO_IMAGE;
                if (File.Exists(Image))
                    return Image;
                else
                {
                    var path = DataManager.Current.FOLDER_IMAGES + "\\" + Image;
                    if (File.Exists(path))
                        return path;
                    else
                        return DataManager.Current.FOLDER_IMAGES + "\\" + DataManager.Current.NO_IMAGE;
                }
            }
        }

        public string PriceFormat
        {
            get
            {
                return Price.ToString("#,##0 vnđ");
            }
        }

        public string ID_Name
        {
            get
            {
                return string.Format("{0:000} - {1}", ID, Name);
            }
        }
        #endregion
    }
}
