using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyNhaSach.Bus
{
    public class FillData
    {
        public static ObservableCollection<Bill> GetOldBill()
        {
            return Adapters.BillAdapter.GetOldBills();
        }

        public static ObservableCollection<Customer> GetAllCustomer()
        {
            return Adapters.CustomerAdapter.GetAll();
        }
    }
}
