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
                        result.Add(new Genre(reader.GetInt32(0))
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
                        result.Add(new Genre(reader.GetInt32(0))
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
