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
                        result.Add(new Customer(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                            Debt = (int)reader.GetValueDefault(2, 0),
                            Adress = (string)reader.GetValueDefault(3, null),
                            Phone = (string)reader.GetValueDefault(4, null),
                            Email = (string)reader.GetValueDefault(5, null),
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
                        result.Add(new Customer(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                            Debt = (int)reader.GetValueDefault(2, 0),
                            Adress = (string)reader.GetValueDefault(3, null),
                            Phone = (string)reader.GetValueDefault(4, null),
                            Email = (string)reader.GetValueDefault(5, null),
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
                        result = new Customer(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                            Debt = (int)reader.GetValueDefault(2, 0),
                            Adress = (string)reader.GetValueDefault(3, null),
                            Phone = phoneNumber,
                            Email = (string)reader.GetValueDefault(4, null),
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
    }
}
