using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using QuanLyNhaSach.Objects;
using System.ComponentModel;
using System.IO;

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for tabQuanLySach.xaml
    /// </summary>
    public partial class tabQuanLySach : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books = Adapters.BookAdapter.GetAll();
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; NotifyPropertyChanged("Books"); }
        }

        public tabQuanLySach()
        {
            InitializeComponent();
            listbox_DSSach.ItemsSource = Books;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        void RemoveItem(Book item)
        {
            if (item.IsCreatedItem)
            {
                if (listbox_DSSach.ItemsSource != null)
                    (listbox_DSSach.ItemsSource as ObservableCollection<Book>).Remove(item);
            }
            else
                item.IsDeletedItem = true;
        }

        void searchItem(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    listbox_DSSach.ItemsSource = Books;
                }
                else
                {
                    listbox_DSSach.ItemsSource = Books.Where(x =>
                        x.ID.ToString().ToLower().Contains(key) ||
                        x.Name.ToLower().Contains(key) ||
                        x.Number.ToString().ToLower().Contains(key) ||
                        x.Price.ToString().ToLower().Contains(key) ||
                        x.PriceFormat.ToLower().Contains(key) ||
                        x.AuthorsFormat.ToLower().Contains(key) ||
                        x.GenresFormat.ToLower().Contains(key) ||
                        (x.Image == null ? Managers.DataManager.Current.NO_IMAGE.ToLower().Contains(key) : x.Image.ToLower().Contains(key))
                        ).ToObservableCollection<Book>();
                }
            }
            catch { }
        }

        private void aItemGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = textbox.Tag as Book;
            if (tag != null)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    listbox_DSSach.SelectedItems.Add(tag);
                else
                    listbox_DSSach.SelectedItem = tag;
            }
        }

        private void removeItem(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Book;
            if (tag != null)
                RemoveItem(tag);
        }

        private void recoverItem(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Book;
            if (tag != null)
                tag.IsDeletedItem = false;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearch.Text))
                Books = Adapters.BookAdapter.GetAll();
            else
                tbSearch.Text = "";
            listbox_DSSach.ItemsSource = Books;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listbox_DSSach.SelectedItems.ToList())
            {
                RemoveItem(item as Book);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listbox_DSSach.ItemsSource != null)
            {
                var newBook = new Book();
                (listbox_DSSach.ItemsSource as ObservableCollection<Book>).Add(newBook);
                listbox_DSSach.SelectedItem = newBook;
                listbox_DSSach.ScrollIntoView(listbox_DSSach.SelectedItem);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (Book item in listbox_DSSach.SelectedItems.ToList())
            {
                if (item.IsDeletedItem)
                    ;
                else if (item.IsCreatedItem)
                    ;
                else if (item.IsEditedItem)
                    ;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchItem(tbSearch.Text.ToLower());
        }

        private void tbSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                searchItem(tbSearch.Text.ToLower());
        }

        private void selectImage(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Book;
            if (tag != null)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Image files (*.png, *jpg)|*.png; *jpg|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        tag.Image = openFileDialog1.FileName;
                    }
                    catch (Exception ex)
                    {
                        //WarningBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }
        }
    }
}
