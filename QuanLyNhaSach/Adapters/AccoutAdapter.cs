using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Adapters
{
    public class AccoutAdapter
    {
        public static bool IsExists(string username, string password, out Account result, bool findDeletedToo = false)
        {
            result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    string.Format(@"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, pq.maphanquyen, tk.bixoa " +
                    "from taikhoan tk, phanquyen pq " +
                    "where tk.email = '{0}' and tk.matkhau = '{1}' {2} and tk.maphanquyen = pq.maphanquyen", username, password, findDeletedToo ? "" : "and tk.bixoa = 'false'"));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        result = new Account(reader.GetInt32(0))
                        {
                            Email = (string)reader.GetValueDefault(1, null),
                            Password = (string)reader.GetValueDefault(2, null),
                            Name = (string)reader.GetValueDefault(3, null),
                            AccessLevel = AccessLevel.AccessLevelList.FirstOrDefault(o => o.ID == reader.GetInt32(4)),
                            IsDeleted = (bool)reader.GetValueDefault(5, false),
                        };
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result != null;
        }
    }
}
