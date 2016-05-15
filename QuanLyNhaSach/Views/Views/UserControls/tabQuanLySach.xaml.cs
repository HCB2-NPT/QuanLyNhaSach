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

        private void aItemGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = textbox.Tag as Book;
            if (tag != null)
                listbox_DSSach.SelectedItem = tag;
        }

        private void removeItem(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag as Book;
            if (tag != null)
                listbox_DSSach.Items.Remove(tag);
        }
    }
}
