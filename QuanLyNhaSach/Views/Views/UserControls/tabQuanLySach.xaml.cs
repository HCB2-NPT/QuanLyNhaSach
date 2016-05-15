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

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for tabQuanLySach.xaml
    /// </summary>
    public partial class tabQuanLySach : UserControl
    {
        public tabQuanLySach()
        {
            InitializeComponent();
            Bus.FillData.Books(listbox_DSSach);
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

        private void aItemGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = textbox.Tag as Book;
            if (tag != null)
                listbox_DSSach.SelectedItems.Add(tag);
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
            Bus.FillData.Books(listbox_DSSach);
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

        }
    }
}
