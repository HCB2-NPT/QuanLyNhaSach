using QuanLyNhaSach.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyNhaSach.Bus
{
    public class Fill
    {
        public static void Customers(ItemsControl ic)
        {
            ic.ItemsSource = CustomerAdapter.GetAllCustomers();
        }
    }
}
