﻿using System;
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
        #region Properties
        private ObservableCollection<Book> _books = Adapters.BookAdapter.GetAll();
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; NotifyPropertyChanged("Books"); }
        }
        #endregion

        #region Constructor
        public tabQuanLySach()
        {
            InitializeComponent();
            listbox_DSSach.ItemsSource = Books;
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

        #region Custom Functions
        void Clear()
        {
            Books = Adapters.BookAdapter.GetAll();
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
                        {
                            var data =
                                x.ID.ToString() +
                                (x.Name == null ? "" : x.Name.ToLower()) +
                                x.Number.ToString() +
                                x.Price.ToString() +
                                x.PriceFormat +
                                (x.AuthorsFormat == null ? "" : x.AuthorsFormat.ToLower()) +
                                (x.GenresFormat == null ? "" : x.GenresFormat.ToLower()) +
                                (x.IsDeletedItem ? "bị xóa" : "") +
                                (x.Image == null ? Managers.DataManager.Current.NO_IMAGE.ToLower() : x.Image.ToLower());
                            return data.Contains(key);
                        }
                        ).ToObservableCollection<Book>();
                }
            }
            catch { }
        }
        #endregion

        #region Events
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
                Clear();
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
                Books.Add(newBook);
                listbox_DSSach.ItemsSource = Books;
                listbox_DSSach.SelectedItem = newBook;
                listbox_DSSach.ScrollIntoView(listbox_DSSach.SelectedItem);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (Book item in Books)
            {
                if (item.IsCreatedItem && !string.IsNullOrEmpty(item.Name))
                {
                    Adapters.BookAdapter.InsertNewBook(item);
                }
                else if (item.IsEditedItem)
                {
                    Adapters.BookAdapter.UpdateBook(item);
                }

                if (item.IsDeletedItem && !item.IsDeleted)
                {
                    Adapters.BookAdapter.DeleteBook(item);
                }
                else if (!item.IsDeletedItem && item.IsDeleted)
                {
                    Adapters.BookAdapter.RecoverBook(item);
                }
            }
            Clear();
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

                openFileDialog1.Filter = "Image files (*.png, *jpg)|*.png; *jpg|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tag.Image = openFileDialog1.FileName;
                }
            }
        }

        private void changePrice(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            var control = sender as MahApps.Metro.Controls.NumericUpDown;
            var tag = control.Tag as Book;
            if (tag != null)
            {
                tag.Price = Convert.ToInt32(control.Value);
            }
        }

        private void changeAuthorsGenres(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = textbox.Tag as Book;
            if (tag != null)
            {
                tag.IsEditedItem = true;
            }
        }

        private void editCurrent(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Book;
            if (tag != null)
            {
                if (tag.ID > 0)
                {
                    tag.IsEditedItem = false;
                    Adapters.BookAdapter.UpdateBook(tag);
                }
                else
                {
                    tag.IsCreatedItem = false;
                    tag.IsEditedItem = false;
                    Adapters.BookAdapter.InsertNewBook(tag);
                }
            }
        }

        private void refreshCurrent(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Book;
            if (tag != null)
            {
                var source = listbox_DSSach.ItemsSource as ObservableCollection<Book>;
                var index = source.IndexOf(tag);
                source.RemoveAt(index);
                if (tag.ID > 0)
                    source.Insert(index, Adapters.BookAdapter.GetBook(tag.ID));
            }
        }
        #endregion
    }
}
