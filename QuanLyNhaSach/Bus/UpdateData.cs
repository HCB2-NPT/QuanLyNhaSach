using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class UpdateData
    {
        public static void UpdateOldBill(Bill bill)
        {
            Adapters.BillAdapter.UpdateOldBill(bill); 
        }

        public static void DeleteOldBillSelected(Bill bill)
        {
            Adapters.BillAdapter.DeleteBill(bill);
        }

        public static void InsertNewBook(Book whichBook)
        {
            if (whichBook.IsCreatedItem)
            {
                if (Adapters.BookAdapter.InsertNewBook(whichBook) == 1)
                {
                    var id = Adapters.BookAdapter.GetLatestId();
                    if (id > 0)
                    {
                        foreach (var item in whichBook.AuthorsFormat.Split(','))
                        {
                            var i = item.Trim();
                            var k = Adapters.AuthorAdapter.Exist(i);
                            if (k == -1)
                            {
                                Adapters.AuthorAdapter.Insert(item);
                                k = Adapters.AuthorAdapter.Exist(item);
                            }
                            Adapters.BookAdapter.AddAuthor(id, k);
                        }
                        foreach (var item in whichBook.GenresFormat.Split(','))
                        {
                            var i = item.Trim();
                            var k = Adapters.GenreAdapter.Exist(i);
                            if (k == -1)
                            {
                                Adapters.GenreAdapter.Insert(item);
                                k = Adapters.GenreAdapter.Exist(item);
                            }
                            Adapters.BookAdapter.AddGenre(id, k);
                        }
                    }
                }
            }
        }

        public static void UpdateBook(Book whichBook)
        {
            if (whichBook.IsEditedItem)
            {
                Adapters.BookAdapter.ClearAuthors(whichBook.ID);
                Adapters.BookAdapter.ClearGenres(whichBook.ID);

                foreach (var item in whichBook.AuthorsFormat.Split(','))
                {
                    var i = item.Trim();
                    var k = Adapters.AuthorAdapter.Exist(i);
                    if (k == -1)
                    {
                        Adapters.AuthorAdapter.Insert(item);
                        k = Adapters.AuthorAdapter.Exist(item);
                    }
                    Adapters.BookAdapter.AddAuthor(whichBook.ID, k);
                }
                foreach (var item in whichBook.GenresFormat.Split(','))
                {
                    var i = item.Trim();
                    var k = Adapters.GenreAdapter.Exist(i);
                    if (k == -1)
                    {
                        Adapters.GenreAdapter.Insert(item);
                        k = Adapters.GenreAdapter.Exist(item);
                    }
                    Adapters.BookAdapter.AddGenre(whichBook.ID, k);
                }
                Adapters.BookAdapter.UpdateBook(whichBook);
            }
        }

        public static void SaveChangesCustomers(ObservableCollection<Customer> listcustomer)
        {
            foreach (var item in listcustomer)
            {
                if (item.IsCreatedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Phone))
                        Adapters.CustomerAdapter.InsertNewCustomer(item);
                }
                else if (item.IsEditedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Phone))
                        Adapters.CustomerAdapter.UpdateCustomer(item);
                }

                if (item.IsDeleted && !item.IsDeletedItem)
                {
                    Adapters.CustomerAdapter.RecoverCustomer(item);
                }
                else if (!item.IsDeleted && item.IsDeletedItem)
                {
                    Adapters.CustomerAdapter.DeleteCustomer(item);
                }
            }
        }

        public static void SaveChangesBooks(ObservableCollection<Book> books)
        {
            foreach (var item in books)
            {
                if (item.IsCreatedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                        InsertNewBook(item);
                }
                else if (item.IsEditedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                        UpdateBook(item);
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
        }

        public static void SaveChangesGenres(ObservableCollection<Genre> genres)
        {
            foreach (var item in genres)
            {
                if (item.IsDeletedItem)
                {
                    Adapters.GenreAdapter.ClearBooksOf(item.ID);
                    Adapters.GenreAdapter.Delete(item.ID);
                }
                else if (item.IsCreatedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        if (Adapters.GenreAdapter.Insert(item.Name) == 1)
                        {
                            var id = Adapters.GenreAdapter.Exist(item.Name);
                            if (id > 0)
                            {
                                var books = item.Tag as ObservableCollection<Book>;
                                if (books != null)
                                {
                                    foreach (var b in books)
                                    {
                                        Adapters.BookAdapter.AddGenre(b.ID, id);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (item.IsEditedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        Adapters.GenreAdapter.ClearBooksOf(item.ID);
                        var books = item.Tag as ObservableCollection<Book>;
                        if (books != null)
                        {
                            foreach (var b in books)
                            {
                                Adapters.BookAdapter.AddGenre(b.ID, item.ID);
                            }
                        }
                        Adapters.GenreAdapter.Update(item);
                    }
                }
            }
        }

        public static void SaveChangesAuthors(ObservableCollection<Author> authors)
        {
            foreach (var item in authors)
            {
                if (item.IsDeletedItem)
                {
                    Adapters.AuthorAdapter.ClearBooksOf(item.ID);
                    Adapters.AuthorAdapter.Delete(item.ID);
                }
                else if (item.IsCreatedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        if (Adapters.AuthorAdapter.Insert(item.Name) == 1)
                        {
                            var id = Adapters.AuthorAdapter.Exist(item.Name);
                            if (id > 0)
                            {
                                var books = item.Tag as ObservableCollection<Book>;
                                if (books != null)
                                {
                                    foreach (var b in books)
                                    {
                                        Adapters.BookAdapter.AddAuthor(b.ID, id);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (item.IsEditedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        Adapters.AuthorAdapter.ClearBooksOf(item.ID);
                        var books = item.Tag as ObservableCollection<Book>;
                        if (books != null)
                        {
                            foreach (var b in books)
                            {
                                Adapters.BookAdapter.AddAuthor(b.ID, item.ID);
                            }
                        }
                        Adapters.AuthorAdapter.Update(item);
                    }
                }
            }
        }
    }
}
