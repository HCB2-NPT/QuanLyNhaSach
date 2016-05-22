using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyNhaSach.Bus
{
    public class FillData
    {
        public static ObservableCollection<Bill> GetOldBill()
        {
            return Adapters.BillAdapter.GetOldBills();
        }

        public static ObservableCollection<Customer> GetAllCustomer()
        {
            return Adapters.CustomerAdapter.GetAll();
        }

        public static ObservableCollection<Customer> GetAllDebtor()
        {
            return Adapters.CustomerAdapter.GetAllDebtor();
        }

        public static ObservableCollection<Book> GetAllBook()
        {
            return Adapters.BookAdapter.GetAll();
        }

        public static ObservableCollection<Account> GetAllAccount()
        {
            return Adapters.AccountAdapter.GetAll(true);
        }

        public static ObservableCollection<Genre> GetAllGenre()
        {
            return Adapters.GenreAdapter.GetAll();
        }

        public static ObservableCollection<Author> GetAllAuthor()
        {
            return Adapters.AuthorAdapter.GetAll();
        }
    }
}
