using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class InsertData
    {
        public static void NewBill(Bill newBill)
        {
            BillAdapter.InsertNewBill(newBill);
        }

        public static void NewPayDebtMoney(PayDebtMoney pdm)
        {
            PayDebtMoneyAdapter.InsertPayDebt(pdm);
            CustomerAdapter.UpdateDebt(pdm.Customer.ID, pdm.Customer.Debt);
        }

        public static void InsertNewBill(Bill newBill)
        {
            var k = Adapters.BillAdapter.InsertNewBill(newBill);
            if (k == 1)
            {
                var billid = Adapters.BillAdapter.GetLatestId();
                if (billid > 0)
                {
                    foreach (var item in newBill.BillItems)
                    {
                        Adapters.BillItemAdapter.InsertNewBillItem(billid, item);
                        var currentNumber = BookAdapter.GetNumber(item.Book.ID);
                        if (currentNumber != -1)
                            Adapters.BookAdapter.UpdateNumber(item.Book.ID, currentNumber - item.Number);
                    }
                }

                if (newBill.ReturnMoney < 0)
                    Adapters.CustomerAdapter.UpdateDebt(newBill.Customer.ID, newBill.Customer.Debt + Math.Abs(newBill.ReturnMoney));
            }
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
    }
}
