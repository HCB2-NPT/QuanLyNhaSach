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
    class CustomerAdapter
    {
        public static Customer GetCustomer(int _id)
        {
            Customer result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa from KhachHang where MaKhachHang = " + _id);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var item = new Customer(reader.GetInt32(0));
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Debt = (int)reader.GetValueDefault(2, 0);
                        item.Adress = (string)reader.GetValueDefault(3, null);
                        item.Phone = (string)reader.GetValueDefault(4, null);
                        item.Email = (string)reader.GetValueDefault(5, null);
                        item.IsDeleted = (bool)reader.GetValueDefault(6, false);
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

        public static ObservableCollection<Customer> GetAll(bool includeDeleted = true)
        {
            ObservableCollection<Customer> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa from KhachHang {0}", includeDeleted ? "" : "where BiXoa = 'false'"));
                if (reader != null)
                {
                    result = new ObservableCollection<Customer>();
                    while (reader.Read())
                    {
                        var item = new Customer(reader.GetInt32(0));
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Debt = (int)reader.GetValueDefault(2, 0);
                        item.Adress = (string)reader.GetValueDefault(3, null);
                        item.Phone = (string)reader.GetValueDefault(4, null);
                        item.Email = (string)reader.GetValueDefault(5, null);
                        item.IsDeleted = (bool)reader.GetValueDefault(6, false);
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
        
        public static ObservableCollection<Customer> GetDeletedCustomers()
        {
            ObservableCollection<Customer> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email from KhachHang where BiXoa = 'true'");
                if (reader != null)
                {
                    result = new ObservableCollection<Customer>();
                    while (reader.Read())
                    {
                        var item = new Customer(reader.GetInt32(0));
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Debt = (int)reader.GetValueDefault(2, 0);
                        item.Adress = (string)reader.GetValueDefault(3, null);
                        item.Phone = (string)reader.GetValueDefault(4, null);
                        item.Email = (string)reader.GetValueDefault(5, null);
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

        public static Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            Customer result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,Email,BiXoa from KhachHang where DienThoai like '%" + phoneNumber + "%'");
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var item = new Customer(reader.GetInt32(0));
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Debt = (int)reader.GetValueDefault(2, 0);
                        item.Adress = (string)reader.GetValueDefault(3, null);
                        item.Phone = phoneNumber;
                        item.Email = (string)reader.GetValueDefault(4, null);
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

        public static int UpdateDebt(int customerid, int newValue)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("update KhachHang " +
                    string.Format("set SoTienNo = {0} ", newValue) +
                    "where MaKhachHang = " + customerid);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static void UpdateCustomers(ObservableCollection<Customer> listcustomer)
        {
            foreach (Customer item in listcustomer)
            {
                if (item.IsCreatedItem)
                    DataConnector.ExecuteNonQuery(string.Format("Insert into KhachHang(HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa) " +
                                                    "values(N'{0}',0,N'{1}','{2}',N'{3}','{4}')", item.Name, item.Adress, item.Phone, item.Email,item.IsDeletedItem));
                else if (item.IsEditedItem)
                {
                    DataConnector.ExecuteNonQuery(string.Format("Update KhachHang set " +
                                                " HoTen=N'{0}'," +
                                                "SoTienNo={1}," +
                                                "DiaChi=N'{2}'," +
                                                "DienThoai='{3}'," +
                                                "Email=N'{4}'," +
                                                "BiXoa='{5}'" +
                                                " where MaKhachHang = {6}", item.Name, item.Debt, item.Adress, item.Phone, item.Email, item.IsDeletedItem, item.ID));
                }
                else if (item.IsDeleted && !item.IsDeletedItem)
                    DataConnector.ExecuteNonQuery("Update KhachHang set BiXoa = 0 where MaKhachHang=" + item.ID);
                else if (!item.IsDeleted && item.IsDeletedItem)
                    DataConnector.ExecuteNonQuery("Update KhachHang set BiXoa = 1 where MaKhachHang=" + item.ID);
            }
        }
    }
}
