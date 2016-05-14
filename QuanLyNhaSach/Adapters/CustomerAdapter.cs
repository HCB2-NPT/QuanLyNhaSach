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
            Customer c = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa from KhachHang where MaKhachHang = " + _id);
                if (reader != null)
                {
                    c = new Customer();
                    while (reader.Read())
                    {
                        var item = new Customer(reader.GetInt32(0));
                        c.BeginInit();
                        c.Name = (string)reader.GetValueDefault(1, null);
                        c.Debt = (int)reader.GetValueDefault(2, 0);
                        c.Adress = (string)reader.GetValueDefault(3, null);
                        c.Phone = (string)reader.GetValueDefault(4, null);
                        c.Email = (string)reader.GetValueDefault(5, null);
                        c.IsDeleted = (bool)reader.GetValueDefault(6, false);
                        c.EndInit();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return c;
        }

        public static ObservableCollection<Customer> GetAll()
        {
            ObservableCollection<Customer> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa from KhachHang");
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
    }
}
