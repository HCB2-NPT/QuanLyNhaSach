using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Adapters
{
    class BillAdapter
    {
        public static ObservableCollection<Bill> GetOldBills()
        {
            ObservableCollection<Bill> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaHoaDon,MaKhachHang,NgayLap,TienTra from HoaDon");
                if (reader != null)
                {
                    result = new ObservableCollection<Bill>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new Bill(id);
                        item.BeginInit();
                        item.Customer = CustomerAdapter.GetCustomer(reader.GetInt32(1));
                        item.DateCreate = reader.GetDateTime(2);
                        item.PayMoney = reader.GetInt32(3);
                        foreach (var i in BillItemAdapter.GetBillItems(id))
                        {
                            item.BillItems.Add(i);
                        }
                        item.EndInit();

                        result.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        public static int GetLatestId()
        {
            try
            {
                var reader = DataConnector.ExecuteQuery("select MAX(MaHoaDon) from HoaDon");
                if (reader != null)
                    return reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return -1;
        }

        public static int InsertNewBill(Bill newBill)
        {
            try
            {
                int result = DataConnector.ExecuteNonQuery("insert into HoaDon " +
                    "(MaKhachHang, NgayLap, TienTra, TongTien, DaLuu) " + 
                    string.Format("({0}, {1}, {2}, {3}, {4})", newBill.Customer.ID, newBill.DateCreate, newBill.ReturnMoney, newBill.TotalMoney, false));
                if (result == 0)
                {
                    var billid = GetLatestId();
                    if (billid > 0)
                    {
                        foreach (var item in newBill.BillItems)
                        {
                            BillItemAdapter.InsertNewBillItems(billid, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }
    }
}
