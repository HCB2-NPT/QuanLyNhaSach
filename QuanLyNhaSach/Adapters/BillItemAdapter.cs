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
    public class BillItemAdapter
    {
        public static ObservableCollection<BillItem> GetBillItems(int billid)
        {
            ObservableCollection<BillItem> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaSach, SoLuong, DonGia from ChiTietHoaDon where MaHoaDon = " + billid);
                if (reader != null)
                {
                    result = new ObservableCollection<BillItem>();
                    while (reader.Read())
                    {
                        var item = new BillItem(BookAdapter.GetBook(reader.GetInt32(0)), reader.GetInt32(1), reader.GetInt32(2));
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
		
        public static int InsertNewBillItems(int billid, BillItem item)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("insert into ChiTietHoaDon " + 
                    "(MaHoaDon, MaSach, SoLuong, DonGia) " +
                    string.Format(" values ({0}, {1}, {2}, {3})", billid, item.Book.ID, item.Number, item.Price));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static void UpdateOldBillItems(Bill bill)
        {
            try
            {
                foreach (var item in bill.BillItems)
                {
                    DataConnector.ExecuteNonQuery(string.Format("update ChiTietHoaDon set SoLuong={0} where MaHoaDon={1} and MaSach = {2}", item.Number, bill.IDBill, item.Book.ID));
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
        }

        public static int DeleteBillItem(Book book)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("delete from ChiTietHoaDon where MaSach =" + book.ID);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }
    }
}
