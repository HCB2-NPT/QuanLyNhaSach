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
    public class AddedBookAdapter
    {
        public static int InsertAddedBookItem(int ID,int IDBook,AddedBook ab)
        {
            try
            {
                DataConnector.ExecuteNonQuery(string.Format("insert into ChiTietPhieuNhap(MaPhieuNhap,MaSach,SoLuong) values({0},{1},{2})",ID,IDBook,ab.Number));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static ObservableCollection<AddedBook> GetAllListAddedBook(int id)
        {
            ObservableCollection<AddedBook> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaPhieuNhap,MaSach,SoLuong from ChiTietPhieuNhap where MaPhieuNhap = " + id);
                if (reader != null)
                {
                    result = new ObservableCollection<AddedBook>();
                    while (reader.Read())
                    {
                        var ID = reader.GetInt32(0);
                        var item = new AddedBook(ID);
                        item.BeginInit();
                        item.Book = BookAdapter.GetBook(reader.GetInt32(1));
                        item.Number = reader.GetInt32(2);
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

        public static void UpdateAddedBook(AddedBook ab)
        {
            try
            {
                DataConnector.ExecuteNonQuery(string.Format("update ChiTietPhieuNhap set SoLuong={0} where MaPhieuNhap={1} and MaSach={2} ",ab.Number,ab.ID,ab.Book.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
        }

        public static void DeleteAddedBook(AddedBook ab)
        {
            try
            {
                DataConnector.ExecuteNonQuery(string.Format("delete from ChiTietPhieuNhap where MaPhieuNhap={0} and MaSach={1}", ab.ID, ab.Book.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
        }
    }
}
