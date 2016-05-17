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
        }

        public static void SaveChangesGenres(ObservableCollection<Genre> genres)
        {
            //foreach (var item in genres)
            //{
            //    if (item.IsCreatedItem)
            //    {
            //        if (!string.IsNullOrEmpty(item.Name))
            //            Adapters.BookAdapter.InsertNewBook(item);
            //    }
            //    else if (item.IsEditedItem)
            //    {
            //        Adapters.BookAdapter.UpdateBook(item);
            //    }

            //    if (item.IsDeletedItem && !item.IsDeleted)
            //    {
            //        Adapters.BookAdapter.DeleteBook(item);
            //    }
            //    else if (!item.IsDeletedItem && item.IsDeleted)
            //    {
            //        Adapters.BookAdapter.RecoverBook(item);
            //    }
            //}
        }
    }
}
