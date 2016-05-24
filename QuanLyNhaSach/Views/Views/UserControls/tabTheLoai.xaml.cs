using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for tabTheLoai.xaml
    /// </summary>
    public partial class tabTheLoai : UserControl, INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Genre> _genres = Bus.FillData.GetAllGenre();
        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
                NotifyPropertyChanged("Genres");
            }
        }

        private ObservableCollection<Book> _booksOfSelectedGenre;
        public ObservableCollection<Book> BooksOfSelectedGenre
        {
            get
            {
                return _booksOfSelectedGenre;
            }
            set
            {
                _booksOfSelectedGenre = value;
                NotifyPropertyChanged("BooksOfSelectedGenre");
            }
        }

        private ObservableCollection<Book> _books = Bus.FillData.GetAllBook();
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; NotifyPropertyChanged("Books"); }
        }
        #endregion

        #region Constructor
        public tabTheLoai()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Implements
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void selectGenre(object sender, SelectionChangedEventArgs e)
        {
            var selected = lv_DSTheLoai.SelectedItem as Genre;
            if (selected != null)
            {
                if (selected.Tag != null)
                {
                    BooksOfSelectedGenre = selected.Tag as ObservableCollection<Book>;
                }
                else
                {
                    BooksOfSelectedGenre = Bus.FillData.GetBooksOfGenre(selected.ID);
                    selected.Tag = BooksOfSelectedGenre;
                }
            }
            else
            {
                BooksOfSelectedGenre = null;
            }
        }

        private void searchBoxEnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var control = sender as TextBox;
                var key = control.Text.ToLower();
                if (!string.IsNullOrEmpty(key))
                {
                    lv_DSTheLoai.SelectedItems.Clear();
                    foreach (var item in Genres.Where(x => x.Name.ToLower().Contains(key)))
                    {
                        lv_DSTheLoai.SelectedItems.Add(item);
                    }
                }
            }
        }

        private void saveChanges(object sender, RoutedEventArgs e)
        {
            Bus.SaveChanges.SaveChangesGenres(Genres);
            Clear();
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            Genres = Bus.FillData.GetAllGenre();
            Books = Bus.FillData.GetAllBook();
        }

        #region GenresBoard
        private void recoverItem(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Genre;
            if (tag != null)
                tag.IsDeletedItem = false;
        }

        private void deleteItem(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Genre;
            if (tag != null)
            {
                if (tag.IsCreatedItem)
                    Genres.Remove(tag);
                else
                    tag.IsDeletedItem = true;
            }
        }

        private void deleteSelectedGenres(object sender, RoutedEventArgs e)
        {
            foreach (Genre item in lv_DSTheLoai.SelectedItems.ToList())
            {
                if (item.IsCreatedItem)
                    Genres.Remove(item);
                else
                    item.IsDeletedItem = true;
            }
        }

        private void addNewGenre(object sender, RoutedEventArgs e)
        {
            var g = new Genre();
            Genres.Add(g);
            lv_DSTheLoai.SelectedItem = g;
            lv_DSTheLoai.ScrollIntoView(g);
        }
        #endregion

        #region BooksOf_Board
        private void removeLink(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Book;
            if (tag != null)
            {
                if (tag.Tag != null)
                    BooksOfSelectedGenre.Remove(tag);
                else
                    tag.Switch = true;
                var item = lv_DSTheLoai.SelectedItem as Genre;
                if (item != null)
                    item.IsEditedItem = true;
            }
        }

        private void recoverLink(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Book;
            if (tag != null)
                tag.Switch = false;
        }

        private void addBookOf(object sender, RoutedEventArgs e)
        {
            var b = NameBook.SelectedItem as Book;
            if (b != null)
            {
                var item = lv_DSTheLoai.SelectedItem as Genre;
                if (item != null)
                {
                    if (!BooksOfSelectedGenre.Any(x => x.Name == b.Name))
                    {
                        b.Tag = "New Link";
                        BooksOfSelectedGenre.Add(b);
                        item.IsEditedItem = true;
                    }
                }
            }
        }
        #endregion

        private void aItemGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = textbox.Tag as Genre;
            if (tag != null)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    lv_DSTheLoai.SelectedItems.Add(tag);
                else
                    lv_DSTheLoai.SelectedItem = tag;
            }
        }
    }
}
