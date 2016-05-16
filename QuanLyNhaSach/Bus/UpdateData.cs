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
            Adapters.CustomerAdapter.UpdateCustomers(listcustomer);
        }
    }
}
