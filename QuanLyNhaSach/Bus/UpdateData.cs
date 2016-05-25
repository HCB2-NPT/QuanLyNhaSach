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
                Bill b = (Adapters.BillAdapter.GetOldBills().FirstOrDefault(x => x.ID == bill.ID)) as Bill;
                if (b!=null)
                {
                    foreach (var item in b.BillItems)
                    {
                        Adapters.BookAdapter.UpdateNumber(item.Book.ID, item.Book.Number + item.Number);
                    }
                }
                Adapters.BillItemAdapter.ClearBillItems(bill.ID);
                foreach (var item in bill.BillItems)
                {
                    Adapters.BillItemAdapter.InsertNewBillItem(bill.ID, item);
                    var currentNumber = Adapters.BookAdapter.GetNumber(item.Book.ID);
                    if (currentNumber != -1)
                        Adapters.BookAdapter.UpdateNumber(item.Book.ID, currentNumber - item.Number);
                }

                int k = Convert.ToInt32(bill.Tag);
                if (k < 0)
                {
                    bill.Customer.Debt += k;
                    Adapters.CustomerAdapter.UpdateDebt(bill.Customer.ID, bill.Customer.Debt);
                }

                if (bill.Customer.Debt + Math.Abs(bill.PayMoney - bill.TotalMoney)<=Managers.RuleManager.Current.Rule.MaxDebt)
                    if (bill.PayMoney < bill.TotalMoney)
                        Adapters.CustomerAdapter.UpdateDebt(bill.Customer.ID, bill.Customer.Debt + Math.Abs(bill.PayMoney - bill.TotalMoney));   
                
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

        public static bool UpdateAccount(Account account)
        {
            if (account.IsDeleted == false)
            {
                if (Adapters.AccountAdapter.UpdateAccount(account) > 0)
                    return true;
            }

            return false;
        }

        public static bool ResetPassword(Account account)
        {
            if (account.IsDeleted == false)
            {
                if (Adapters.AccountAdapter.ResetPassword(account) > 0)
                    return true;
            }

            return false;
        }
    }
}
