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

        private int _id;
        private string _name = null;
        private string _image = null;
        private int _number = 0;
        private int _price = 0;
        private bool _isdeleted = false;
        private ObservableCollection<Author> _authors = new ObservableCollection<Author>();
        private ObservableCollection<Genre> _genres = new ObservableCollection<Genre>();

        void Init()
        {
            _authors.CollectionChanged += _authors_CollectionChanged;
            _genres.CollectionChanged += _genres_CollectionChanged;
        }

        #region Events
        void _genres_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateGenresFormat();
        }

        void _authors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateAuthorsFormat();
        }
        #endregion

        #region Constructor
        public Book() : base(true)
        {
            _id = 0;
            Init();
        }

        public Book(int id) : base()
        {
            _id = id;
            Init();
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

        public int Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged("Number"); }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged("Price"); NotifyPropertyChanged("PriceFormat"); }
        }

        public bool IsDeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; NotifyPropertyChanged("IsDeleted"); }
        }

        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
        }

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
        }
        #endregion

        #region PropertiesFormat
        private void UpdateAuthorsFormat()
        {
            _authorsFormat = "";
            foreach (var item in Authors)
            {
                _authorsFormat += item.Name + ", ";
            }
            if (_authorsFormat.Length >= 2)
                _authorsFormat = _authorsFormat.Remove(_authorsFormat.Length - 2);
            if (string.IsNullOrEmpty(_authorsFormat))
                _authorsFormat = "<Không có tác giả rõ ràng>";
            NotifyPropertyChanged("AuthorsFormat");
            NotifyPropertyChanged("AuthorsShortFormat");
        }

        private string _authorsFormat = null;
        public string AuthorsFormat
        {
            get
            {
                return _authorsFormat;
            }
            set
            {
                _authorsFormat = value;
            }
        }

        public string AuthorsShortFormat
        {
            get
            {
                var format = AuthorsFormat;
                if (string.IsNullOrEmpty(format))
                    return null;
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
            _genresFormat = "";
            foreach (var item in Genres)
            {
                _genresFormat += item.Name + ", ";
            }
            if (_genresFormat.Length >= 2)
                _genresFormat = _genresFormat.Remove(_genresFormat.Length - 2);
            if (string.IsNullOrEmpty(_genresFormat))
                _genresFormat = "<Không có thể loại rõ ràng>";
            NotifyPropertyChanged("GenresFormat");
            NotifyPropertyChanged("GenresShortFormat");
        }

        private string _genresFormat = null;
        public string GenresFormat
        {
            get
            {
                return _genresFormat;
            }
            set
            {
                _genresFormat = value;
            }
        }

        public string GenresShortFormat
        {
            get
            {
                var format = GenresFormat;
                if (string.IsNullOrEmpty(format))
                    return null;
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
                if (string.IsNullOrEmpty(Image))
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

        public string BookInfo
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                    return null;
                if (string.IsNullOrEmpty(AuthorsShortFormat))
                    return string.Format("{0:000} - {1}", ID, Name);
                return string.Format("{0:000} - {1} - {2}", ID, Name, AuthorsShortFormat);
            }
        }
        #endregion
    }
}
