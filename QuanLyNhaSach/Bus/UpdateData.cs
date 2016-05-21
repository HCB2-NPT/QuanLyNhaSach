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
            if (bill.ID > 0)
            {
                Adapters.BillItemAdapter.ClearBillItems(bill.ID);
                foreach (var item in bill.BillItems)
                {
                    Adapters.BillItemAdapter.InsertNewBillItem(bill.ID, item);
                    var currentNumber = Adapters.BookAdapter.GetNumber(item.Book.ID);
                    if (currentNumber != -1)
                        Adapters.BookAdapter.UpdateNumber(item.Book.ID, currentNumber - item.Number);
                }
                Adapters.BillAdapter.UpdateOldBill(bill); 
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
    }
}
