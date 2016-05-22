using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Adapters
{
    public class ManagerListAddedBookAdapter
    {
        public static int InsertNewManagerListAddedBook(ManagerListAddedBook mlab)
        {
            try
            {
                DataConnector.ExecuteNonQuery(string.Format("insert into PhieuNhap(NgayNhapKho,NgayTaoPhieu,MaTaiKhoan) values('{0}','{1}',{2})", DateTime.Now, mlab.DateAddIntoStorage, mlab.IDManager));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int GetLatestId()
        {
            try
            {
                var reader = DataConnector.ExecuteQuery("select MAX(MaPhieuNhap) from PhieuNhap");
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
    }
}
