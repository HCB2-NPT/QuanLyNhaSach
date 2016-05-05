using QuanLyNhaSach.Manager.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager.Adapter
{
    public class AccessLevelAdapter
    {
        public static ObservableCollection<AccessLevel> GetList()
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
                            Name = reader.GetString(1)
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
