using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class DeleteData
    {
        public static void DeleteOldBill(Bill bill)
        {
            if (bill.ID > 0)
            {
                foreach (var item in bill.BillItems)
                {
                    if (item.Number>0)
                    {
                        Adapters.BookAdapter.UpdateNumber(item.Book.ID, item.Book.Number + item.Number);
                    }
                }
                Adapters.BillItemAdapter.ClearBillItems(bill.ID);
                Adapters.BillAdapter.DeleteBill(bill.ID);
            }
        }

        public static void DeleteManagerListAddedBook(ManagerListAddedBook mlab)
        {
            if (mlab.ListAddedBook.Count <= 0)
                Adapters.ManagerListAddedBookAdapter.Delete(mlab);
            else
            {
                foreach (AddedBook item in mlab.ListAddedBook)
                {
                    item.IsDeletedItem = true;
                    Bus.SaveChanges.SaveChangesListAddedBook(mlab);
                }
                Adapters.ManagerListAddedBookAdapter.Delete(mlab);
            }
        }
    }
}
