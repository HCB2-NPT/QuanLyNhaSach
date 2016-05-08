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
                        result.Add(new Author(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        public static ObservableCollection<Author> GetAuthors(int bookid)
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
                        result.Add(new Author(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                        });
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
