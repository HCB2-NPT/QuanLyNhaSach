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
        public static ObservableCollection<BillItem> GetBillItems(int id)
        {
            ObservableCollection<BillItem> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaSach, SoLuong, DonGia from ChiTietHoaDon where MaHoaDon = " + id);
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

    }
}
