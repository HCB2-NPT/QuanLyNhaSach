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
    public class SaveChanges
    {
        public static void SaveNewRules(Rule item)
        {
            if (item == null)
                return;
            if (!item.IsEditedItem)
                return;
            RuleAdapter.InsertNewRule(item);
        }

        public static void SaveChangesBooks(ObservableCollection<Book> books)
        {
            foreach (var item in books)
            {
                if (item.IsCreatedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                        InsertData.InsertNewBook(item);
                }
                else if (item.IsEditedItem)
                {
                    if (!string.IsNullOrEmpty(item.Name))
                        UpdateData.UpdateBook(item);
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
                                if (!b.Switch)
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
                                if (!b.Switch)
                                    Adapters.BookAdapter.AddAuthor(b.ID, item.ID);
                            }
                        }
                        Adapters.AuthorAdapter.Update(item);
                    }
                }
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

        public static void SaveChangesListAddedBook(ManagerListAddedBook SelectedMLAB)
        {
            foreach (var item in SelectedMLAB.ListAddedBook)
            {
                if (item.IsEditedItem)
                    Adapters.AddedBookAdapter.UpdateAddedBook(item);
                if (item.IsDeletedItem)
                    Adapters.AddedBookAdapter.DeleteAddedBook(item);
            }
            
        }

        public static void SaveChangeManagerListAddedBook(ManagerListAddedBook mlab)
        {
            Adapters.ManagerListAddedBookAdapter.Update(mlab);
        }
    }
}
