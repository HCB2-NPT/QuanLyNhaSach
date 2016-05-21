using QuanLyNhaSach.Objects;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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
	/// Interaction logic for tabPhieuNhapSach.xaml
	/// </summary>
	public partial class tabPhieuNhapSach : UserControl,INotifyPropertyChanged
	{
        private ObservableCollection<Book> _listBook = Bus.FillData.GetAllBook();
        public ObservableCollection<Book> ListBook
        {
            get { return _listBook; }
            set { _listBook = value; }
        }

        private ObservableCollection<AddedBook> _listAddedBook = new ObservableCollection<AddedBook>();

        public ObservableCollection<AddedBook> ListAddedBook
        {
            get { return _listAddedBook; }
            set { _listAddedBook = value; NotifyPropertyChanged("ListAddedBook"); }
        }


        #region Emplements

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
		public tabPhieuNhapSach()
		{
			this.InitializeComponent();
            num_UpDown.DataContext = Managers.RuleManager.Current;
            DataContext = this;
		}

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            var selectedbook = tb_BookName.SelectedItem as Book;
            if (selectedbook != null)
            {
                AddedBook ab = new AddedBook()
                {
                    Book = selectedbook,
                    Number = (int)num_UpDown.Value
                };
                var item = ListAddedBook.FirstOrDefault(x => x.Book.ID == ab.Book.ID);
                if (item != null)
                    item.Number += (int)num_UpDown.Value;
                else
                    ListAddedBook.Add(ab);
            }
            else
            {
                var item = ListBook.FirstOrDefault(x => x.Name == tb_BookName.Text);
                AddedBook ab = new AddedBook();
                if (item != null)
                {
                    ab.Book = item;
                    ab.Number = (int)num_UpDown.Value;   
                }
                else
                {
                    ab.Book = new Book();
                    ab.Book.Name = tb_BookName.Text;
                    ab.Book.AuthorsFormat = tb_Authors.Text;
                    ab.Book.GenresFormat = tb_Genres.Text;
                    ab.Number = (int)num_UpDown.Value;
                }
                ListAddedBook.Add(ab);
            }
        }
	}
}