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
                var reader = DataConnector.ExecuteQuery("select MaHoaDon, NgayLap, MaKhachHang, TienTra from HoaDon");
                if (reader != null)
                {
                    result = new ObservableCollection<Bill>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new Bill(id, reader.GetDateTime(1));
                        item.BeginInit();
                        item.Customer = CustomerAdapter.GetCustomer(reader.GetInt32(2));
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
                {
                    if (reader.Read())
                        return reader.GetInt32(0);
                }
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
                    string.Format("values ({0}, '{1}', {2}, {3}, 'false')", newBill.Customer == null ? 13 : newBill.Customer.ID, DateTime.Now, newBill.ReturnMoney, newBill.TotalMoney));
                if (result == 1)
                {
                    var billid = GetLatestId();
                    if (billid > 0)
                    {
                        foreach (var item in newBill.BillItems)
                        {
                            BillItemAdapter.InsertNewBillItems(billid, item);
                            BookAdapter.UpdateNumber(item.Book.ID, item.Book.Number - item.Number);
                        }
                    }

                    if (newBill.ReturnMoney < 0)
                        CustomerAdapter.UpdateDebt(newBill.Customer.ID, newBill.Customer.Debt + Math.Abs(newBill.ReturnMoney));
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int FixedBillsOverTime()
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("update HoaDon set DaLuu=1 where DaLuu=0 and DATEDIFF(day, '{0}', NgayLap) < 0", DateTime.Now));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int UpdateOldBill(Bill bill)
        {
            try
            {
                BillItemAdapter.UpdateOldBillItems(bill);
                return DataConnector.ExecuteNonQuery(string.Format("update HoaDon set TienTra={0},TongTien={1} where MaHoaDon={2} and DaLuu=0",bill.PayMoney,bill.TotalMoney,bill.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }


        public static int DeleteBill(Bill bill)
        {
            try
            {
                BillItemAdapter.DeleteBillItems(bill);
                return DataConnector.ExecuteNonQuery("delete from HoaDon where MaHoaDon = " + bill.IDBill);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }
    }
}
