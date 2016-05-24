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
    public partial class tabTacGia : UserControl, INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Author> _authors = Bus.FillData.GetAllAuthor();
        public ObservableCollection<Author> Authors
        {
            get
            {
                return _authors;
            }
            set
            {
                _authors = value;
                NotifyPropertyChanged("Authors");
            }
        }

        private ObservableCollection<Book> _booksOfSelectedAuthor;
        public ObservableCollection<Book> BooksOfSelectedAuthor
        {
            get
            {
                return _booksOfSelectedAuthor;
            }
            set
            {
                _booksOfSelectedAuthor = value;
                NotifyPropertyChanged("BooksOfSelectedAuthor");
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
        public tabTacGia()
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

        private void selectAuthor(object sender, SelectionChangedEventArgs e)
        {
            var selected = lv_DSTacGia.SelectedItem as Author;
            if (selected != null)
            {
                if (selected.Tag != null)
                {
                    BooksOfSelectedAuthor = selected.Tag as ObservableCollection<Book>;
                }
                else
                {
                    BooksOfSelectedAuthor = Bus.FillData.GetBooksOfAuthor(selected.ID);
                    selected.Tag = BooksOfSelectedAuthor;
                }
            }
            else
            {
                BooksOfSelectedAuthor = null;
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
                    lv_DSTacGia.SelectedItems.Clear();
                    foreach (var item in Authors.Where(x => x.Name.ToLower().Contains(key)))
                    {
                        lv_DSTacGia.SelectedItems.Add(item);
                    }
                }
            }
        }

        private void saveChanges(object sender, RoutedEventArgs e)
        {
            Bus.SaveChanges.SaveChangesAuthors(Authors);
            Clear();
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            Authors = Bus.FillData.GetAllAuthor();
            Books = Bus.FillData.GetAllBook();
        }

        #region AuthorsBoard
        private void recoverItem(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Author;
            if (tag != null)
                tag.IsDeletedItem = false;
        }

        private void deleteItem(object sender, RoutedEventArgs e)
        {
            var control = sender as Button;
            var tag = control.Tag as Author;
            if (tag != null)
            {
                if (tag.IsCreatedItem)
                    Authors.Remove(tag);
                else
                    tag.IsDeletedItem = true;
            }
        }

        private void deleteSelectedAuthors(object sender, RoutedEventArgs e)
        {
            foreach (Author item in lv_DSTacGia.SelectedItems.ToList())
            {
                if (item.IsCreatedItem)
                    Authors.Remove(item);
                else
                    item.IsDeletedItem = true;
            }
        }

        private void addNewAuthor(object sender, RoutedEventArgs e)
        {
            var g = new Author();
            Authors.Add(g);
            lv_DSTacGia.SelectedItem = g;
            lv_DSTacGia.ScrollIntoView(g);
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
                    BooksOfSelectedAuthor.Remove(tag);
                else
                    tag.Switch = true;
                var item = lv_DSTacGia.SelectedItem as Author;
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
                var item = lv_DSTacGia.SelectedItem as Author;
                if (item != null)
                {
                    if (!BooksOfSelectedAuthor.Any(x => x.Name == b.Name))
                    {
                        b.Tag = "New Link";
                        BooksOfSelectedAuthor.Add(b);
                        item.IsEditedItem = true;
                    }
                }
            }
        }
        #endregion

        private void aItemGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = textbox.Tag as Author;
            if (tag != null)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    lv_DSTacGia.SelectedItems.Add(tag);
                else
                    lv_DSTacGia.SelectedItem = tag;
            }
        }
    }
}
