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
    public class GenreAdapter
    {
        public static ObservableCollection<Genre> GetAll()
        {
            ObservableCollection<Genre> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaTheLoai,TenTheLoai from TheLoai");
                if (reader != null)
                {
                    result = new ObservableCollection<Genre>();
                    while (reader.Read())
                    {
                        var item = new Genre(reader.GetInt32(0));
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
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

        public static ObservableCollection<Genre> GetGenresForBook(int bookid)
        {
            ObservableCollection<Genre> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select tl.MaTheLoai, tl.TenTheLoai from TheLoai tl, ChiTietTheLoaiSach ct where tl.MaTheLoai = ct.MaTheLoai and ct.MaSach = " + bookid);
                if (reader != null)
                {
                    result = new ObservableCollection<Genre>();
                    while (reader.Read())
                    {
                        var item = new Genre(reader.GetInt32(0));
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
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

        public static int Insert(string name)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("insert into TheLoai (TenTheLoai) values (N'{0}')", name));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int Delete(int id)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("delete from TheLoai where MaTheLoai = {0}", id));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }

        public static int Update(Genre which)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("update TheLoai set TenTheLoai = N'{0}' where MaTheLoai = {1}", which.Name, which.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int Exist(string name)
        {
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select MaTheLoai from TheLoai where TenTheLoai = N'{0}'", name));
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

        public static int ClearBooksOf(int id)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("delete from ChiTietTheLoaiSach where MaTheLoai = {0}", id));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }
    }
}
