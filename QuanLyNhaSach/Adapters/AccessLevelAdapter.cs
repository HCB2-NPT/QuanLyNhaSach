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
    public class AccessLevelAdapter
    {
        public static ObservableCollection<AccessLevel> GetAll()
        {
            var result = new ObservableCollection<AccessLevel>();
            try
            {
                var reader = DataConnector.ExecuteQuery(@"select maphanquyen, tenphanquyen from phanquyen");
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var a = new AccessLevel(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null)
                        };
                        result.Add(a);
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
