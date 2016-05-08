using QuanLyNhaSach.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyNhaSach.Bus
{
    public class FillData
    {
        public static void AccessLevels(ItemsControl ic)
        {
            ic.ItemsSource = AccessLevelAdapter.GetAll();
        }

        public static void Accounts(ItemsControl ic)
        {
            ic.ItemsSource = AccountAdapter.GetAll();
        }

        public static void DeletedAccounts(ItemsControl ic)
        {
            ic.ItemsSource = AccountAdapter.GetDeletedAccounts();
        }

        public static void Authors(ItemsControl ic)
        {
            ic.ItemsSource = AuthorAdapter.GetAll();
        }

        public static void Genres(ItemsControl ic)
        {
            ic.ItemsSource = GenreAdapter.GetAll();
        }

        public static void Books(ItemsControl ic)
        {
            ic.ItemsSource = BookAdapter.GetAll();
        }

        public static void DeletedBooks(ItemsControl ic)
        {
            ic.ItemsSource = BookAdapter.GetDeletedBooks();
        }

        public static void Customers(ItemsControl ic)
        {
            ic.ItemsSource = CustomerAdapter.GetAll();
        }

        public static void DeleteCustomers(ItemsControl ic)
        {
            ic.ItemsSource = CustomerAdapter.GetDeletedCustomers();
        }

        public static void BooksOfGenre(ItemsControl ic, int genreid)
        {
            ic.ItemsSource = BookAdapter.GetBooksForGenre(genreid);
        }

        public static void BooksOfAuthors(ItemsControl ic, int authorid)
        {
            ic.ItemsSource = BookAdapter.GetBooksForAuthor(authorid);
        }

        public static void GenresOfBook(ItemsControl ic, int bookid)
        {
            ic.ItemsSource = GenreAdapter.GetGenresForBook(bookid);
        }

        public static void AuthorsOfBook(ItemsControl ic, int bookid)
        {
            ic.ItemsSource = AuthorAdapter.GetAuthorsForBook(bookid);
        }
    }
}
