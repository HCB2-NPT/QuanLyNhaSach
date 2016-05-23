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

        public static ObservableCollection<ManagerListAddedBook> GetAllManagerListAddedBook()
        {
            ObservableCollection<ManagerListAddedBook> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaPhieuNhap,NgayNhapKho,NgayTaoPhieu,MaTaiKhoan from PhieuNhap");
                if (reader != null)
                {
                    result = new ObservableCollection<ManagerListAddedBook>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new ManagerListAddedBook(id);
                        item.BeginInit();
                        item.DateCreate = reader.GetDateTime(1);
                        item.DateAddIntoStorage = reader.GetDateTime(2);
                        item.IDManager = (int)reader.GetValueDefault(3, 0);
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

        public static void DeleteManagerListAddedBook(ManagerListAddedBook mlab)
        {
            try
            {
                DataConnector.ExecuteNonQuery("delete from PhieuNhap where MaPhieuNhap=" + mlab.ID);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
        }

        public static void UpdateMLAB(ManagerListAddedBook mlab)
        {
            try
            {
                DataConnector.ExecuteNonQuery(string.Format("update PhieuNhap set NgayNhapKho='{0}' where MaPhieuNhap={1}",mlab.DateAddIntoStorage,mlab.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
        }
    }
}
