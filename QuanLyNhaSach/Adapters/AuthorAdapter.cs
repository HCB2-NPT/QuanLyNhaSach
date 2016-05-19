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
    public class AuthorAdapter
    {
        public static ObservableCollection<Author> GetAll()
        {
            ObservableCollection<Author> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaTacGia,TenTacGia from TacGia");
                if (reader != null)
                {
                    result = new ObservableCollection<Author>();
                    while (reader.Read())
                    {
                        var item = new Author(reader.GetInt32(0));
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

        public static ObservableCollection<Author> GetAuthorsForBook(int bookid)
        {
            ObservableCollection<Author> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select tg.MaTacGia, tg.TenTacGia from TacGia tg, ChiTietTacGiaSach ct where tg.MaTacGia = ct.MaTacGia and ct.MaSach = " + bookid);
                if (reader != null)
                {
                    result = new ObservableCollection<Author>();
                    while (reader.Read())
                    {
                        var item = new Author(reader.GetInt32(0));
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
                return DataConnector.ExecuteNonQuery(string.Format("insert into TacGia (TenTacGia) values (N'{0}')", name));
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
                return DataConnector.ExecuteNonQuery(string.Format("delete from TacGia where MaTacGia = {0}", id));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }

        public static int Update(Author which)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("update TacGia set TenTacGia = N'{0}' where MaTacGia = {1}", which.Name, which.ID));
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
                var reader = DataConnector.ExecuteQuery(string.Format("select MaTacGia from TacGia where TenTacGia = N'{0}'", name));
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
                return DataConnector.ExecuteNonQuery(string.Format("delete from ChiTietTacGiaSach where MaTacGia = {0}", id));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }
    }
}
