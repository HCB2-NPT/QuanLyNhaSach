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
        public static ObservableCollection<Genre> GetAllGenres()
        {
            ObservableCollection<Genre> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaTheLoai,TenTheLoai from TheLoai");
                if (reader != null)
                {
                    result = new ObservableCollection<Genre>();
                    Genre g;
                    while (reader.Read())
                    {
                        g = new Genre(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                        };
                        result.Add(g);
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
