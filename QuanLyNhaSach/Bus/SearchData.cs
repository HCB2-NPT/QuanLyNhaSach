using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class SearchData
    {
        public static Account FindAccountById(int id, bool findDeletedToo = false)
        {
            return AccountAdapter.GetAccount(id, findDeletedToo);
        }

        public static ObservableCollection<Book> FindBooksOfGenre(int genreid)
        {
            return BookAdapter.GetBooksForGenre(genreid);
        }

        public static ObservableCollection<Book> FindBooksOfAuthor(int authorid)
        {
            return BookAdapter.GetBooksForAuthor(authorid);
        }

        public static Customer FindCustomerByPhoneNumber(string phoneNumber)
        {
            return CustomerAdapter.GetCustomerByPhoneNumber(phoneNumber);
        }

        public static ObservableCollection<Genre> FindGenresOfBook(int bookid)
        {
            return GenreAdapter.GetGenresForBook(bookid);
        }

        public static ObservableCollection<Author> FindAuthorsOfBook(int bookid)
        {
            return AuthorAdapter.GetAuthorsForBook(bookid);
        }

        public static Rule FindLastRule()
        {
            return RulesAdapter.GetLastRules();
        }
    }
}
