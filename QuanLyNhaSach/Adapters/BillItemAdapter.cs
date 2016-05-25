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
        public static ObservableCollection<BillItem> GetAll()
        {
            ObservableCollection<BillItem> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaSach, SoLuong, DonGia from ChiTietHoaDon");
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

        public static ObservableCollection<BillItem> GetAllBeforeDate(DateTime date)
        {
            ObservableCollection<BillItem> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select ct.MaSach, ct.SoLuong, ct.DonGia from ChiTietHoaDon ct, HoaDon hd where ct.MaHoaDon = hd.MaHoaDon and DATEDIFF(day, '{0}', hd.NgayLap) <= 0", date));
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
		
        public static int InsertNewBillItem(int billid, BillItem item)
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

        public static int UpdateBillItem(int billid, BillItem item)
        {
            try
            {
               return DataConnector.ExecuteNonQuery(string.Format("update ChiTietHoaDon set SoLuong={0} where MaHoaDon={1} and MaSach = {2}", item.Number, billid, item.Book.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int ClearBillItems(int billid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("delete from ChiTietHoaDon where MaHoaDon = {0}", billid));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }
    }
}
