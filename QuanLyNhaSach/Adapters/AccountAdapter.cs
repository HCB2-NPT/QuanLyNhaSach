using QuanLyNhaSach.Assets.Extensions;
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
        private static Security crypto = new Security();

        public static ObservableCollection<Account> GetAll(bool findDeletedToo = false)
        {
            ObservableCollection<Account> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    string.Format(@"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, tk.maphanquyen, tk.bixoa " +
                    "from taikhoan tk " +
                    "{0}", findDeletedToo ? "" : "where tk.bixoa = 'false'"));
                if (reader != null)
                {
                    result = new ObservableCollection<Account>();
                    while (reader.Read())
                    {
                        var item = new Account(reader.GetInt32(0));
                        item.BeginInit();
                        item.Email = (string)reader.GetValueDefault(1, null);
                        item.Password = (string)reader.GetValueDefault(2, null);
                        item.Name = (string)reader.GetValueDefault(3, null);
                        item.AccessLevel = AccessLevelAdapter.GetAcessLevelById(reader.GetInt32(4));
                        item.IsDeleted = (bool)reader.GetValueDefault(5, false);
                        item.IsDeletedItem = item.IsDeleted;
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

        public static ObservableCollection<Account> GetDeletedAccounts()
        {
            ObservableCollection<Account> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(
                    @"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, tk.maphanquyen " +
                    "from taikhoan tk " +
                    "where tk.bixoa = 'true'");
                if (reader != null)
                {
                    result = new ObservableCollection<Account>();
                    while (reader.Read())
                    {
                        var item = new Account(reader.GetInt32(0));
                        item.BeginInit();
                        item.Email = (string)reader.GetValueDefault(1, null);
                        item.Password = (string)reader.GetValueDefault(2, null);
                        item.Name = (string)reader.GetValueDefault(3, null);
                        item.AccessLevel = AccessLevelAdapter.GetAcessLevelById(reader.GetInt32(4));
                        item.IsDeleted = true;
                        item.IsDeletedItem = item.IsDeleted;
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
                        var item = new Account(reader.GetInt32(0));
                        item.BeginInit();
                        item.Email = (string)reader.GetValueDefault(1, null);
                        item.Password = (string)reader.GetValueDefault(2, null);
                        item.Name = (string)reader.GetValueDefault(3, null);
                        item.AccessLevel = AccessLevelAdapter.GetAcessLevelById(reader.GetInt32(4));
                        item.IsDeleted = (bool)reader.GetValueDefault(5, false);
                        item.IsDeletedItem = item.IsDeleted;
                        item.EndInit();
                        result = item;
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
                password = crypto.encodeMD5(crypto.encodeSHA1(password));

                var reader = DataConnector.ExecuteQuery(
                    string.Format(@"select tk.mataikhoan, tk.email, tk.matkhau, tk.hoten, tk.maphanquyen, tk.bixoa " +
                    "from taikhoan tk " +
                    "where tk.email = '{0}' and tk.matkhau = '{1}' {2}", username, password, findDeletedToo ? "" : "and tk.bixoa = 'false'"));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var item = new Account(reader.GetInt32(0));
                        item.BeginInit();
                        item.Email = (string)reader.GetValueDefault(1, null);
                        item.Password = (string)reader.GetValueDefault(2, null);
                        item.Name = (string)reader.GetValueDefault(3, null);
                        item.AccessLevel = AccessLevelAdapter.GetAcessLevelById(reader.GetInt32(4));
                        item.IsDeleted = (bool)reader.GetValueDefault(5, false);
                        item.IsDeletedItem = item.IsDeleted;
                        item.EndInit();
                        result = item;
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

        public static int UpdateAccount(Account account)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(
                    String.Format("UPDATE TaiKhoan SET HoTen = N'{0}', MaPhanQuyen = {1}, BiXoa = '{2}' WHERE MaTaiKhoan = {3}",
                        account.Name, account.AccessLevel.ID, account.IsDeleted, account.ID
                    ));
            } catch (Exception exception)
            {
                ErrorManager.Current.DataCantBeUpdate.Call("Có lỗi xảy ra khi cập nhật tài khoản");
            }

            return -1;
        }

        public static int ResetPassword(Account account)
        {
            try
            {
                string password = account.Email.Split('@')[0];
                password = crypto.encodeMD5(crypto.encodeSHA1(password));

                return DataConnector.ExecuteNonQuery(
                    String.Format("UPDATE TaiKhoan SET MatKhau = '{0}' WHERE MaTaiKhoan = {1}",
                        password, account.ID
                    ));
            }
            catch (Exception exception)
            {
                ErrorManager.Current.DataCantBeUpdate.Call("Có lỗi xảy ra khi reset mật khẩu");
            }

            return -1;
        }

        public static int InsertAccount(Account account)
        {
            try
            {
                string password = account.Email.Split('@')[0];
                password = crypto.encodeMD5(crypto.encodeSHA1(password));

                return DataConnector.ExecuteNonQuery(
                    String.Format("INSERT INTO TaiKhoan VALUES(N'{0}', '{1}', N'{2}', {3}, 0)",
                        account.Email, password, account.Name, account.AccessLevel.ID
                    ));
            }
            catch (Exception exception)
            {
                ErrorManager.Current.DataCantBeUpdate.Call("Có lỗi xảy ra khi tạo tài khoản mới");
            }

            return -1;
        }
    }
}
