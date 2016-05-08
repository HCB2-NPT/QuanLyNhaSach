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
    public class AccountAdapter
    {
        public static ObservableCollection<Account> GetAll(bool findDeletedToo = false)
        {
            ObservableCollection<Account> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    string.Format(@"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, pq.maphanquyen, pq.tenphanquyen, tk.bixoa " +
                    "from taikhoan tk, phanquyen pq " +
                    "where {0} tk.maphanquyen = pq.maphanquyen", findDeletedToo ? "" : "tk.bixoa = 'false' and"));
                if (reader != null)
                {
                    result = new ObservableCollection<Account>();
                    while (reader.Read())
                    {
                        result.Add(new Account(reader.GetInt32(0))
                        {
                            Email = (string)reader.GetValueDefault(1, null),
                            Password = (string)reader.GetValueDefault(2, null),
                            Name = (string)reader.GetValueDefault(3, null),
                            AccessLevel = new AccessLevel(reader.GetInt32(4)) { Name = (string)reader.GetValueDefault(5, null) },
                            IsDeleted = (bool)reader.GetValueDefault(6, false),
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

        public static ObservableCollection<Account> GetDeletedAccounts()
        {
            ObservableCollection<Account> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    @"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, pq.maphanquyen, pq.tenphanquyen " +
                    "from taikhoan tk, phanquyen pq " +
                    "where tk.maphanquyen = pq.maphanquyen and tk.bixoa = 'true'");
                if (reader != null)
                {
                    result = new ObservableCollection<Account>();
                    while (reader.Read())
                    {
                        result.Add(new Account(reader.GetInt32(0))
                        {
                            Email = (string)reader.GetValueDefault(1, null),
                            Password = (string)reader.GetValueDefault(2, null),
                            Name = (string)reader.GetValueDefault(3, null),
                            AccessLevel = new AccessLevel(reader.GetInt32(4)) { Name = (string)reader.GetValueDefault(5, null) },
                            IsDeleted = true,
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

        public static Account GetAccount(int id, bool findDeletedToo = false)
        {
            Account result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    string.Format(@"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, tk.maphanquyen, tk.bixoa " +
                    "from taikhoan tk " +
                    "where tk.mataikhoan = {0} {1} and tk.maphanquyen = pq.maphanquyen", id, findDeletedToo ? "" : "and tk.bixoa = 'false'"));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        result = new Account(reader.GetInt32(0))
                        {
                            Email = (string)reader.GetValueDefault(1, null),
                            Password = (string)reader.GetValueDefault(2, null),
                            Name = (string)reader.GetValueDefault(3, null),
                            AccessLevel = AccessLevelAdapter.GetAcessLevelById(reader.GetInt32(4)),
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
            return result;
        }

        public static bool IsExists(string username, string password, out Account result, bool findDeletedToo = false)
        {
            result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    string.Format(@"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, tk.maphanquyen, tk.bixoa " +
                    "from taikhoan tk " +
                    "where tk.email = '{0}' and tk.matkhau = '{1}' {2}", username, password, findDeletedToo ? "" : "and tk.bixoa = 'false'"));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        result = new Account(reader.GetInt32(0))
                        {
                            Email = (string)reader.GetValueDefault(1, null),
                            Password = (string)reader.GetValueDefault(2, null),
                            Name = (string)reader.GetValueDefault(3, null),
                            AccessLevel = AccessLevelAdapter.GetAcessLevelById(reader.GetInt32(4)),
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
