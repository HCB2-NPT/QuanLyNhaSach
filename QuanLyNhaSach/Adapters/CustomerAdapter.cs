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
        public static ObservableCollection<Customer> GetAllCustomers()
        {
            ObservableCollection<Customer> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa from KhachHang");
                if (reader != null)
                {
                    result = new ObservableCollection<Customer>();
                    Customer c;
                    while (reader.Read())
                    {
                        c = new Customer(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null),
                            Debt = (int)reader.GetValueDefault(2, 0),
                            Adress = (string)reader.GetValueDefault(3, null),
                            Phone = (string)reader.GetValueDefault(4, null),
                            Email = (string)reader.GetValueDefault(5, null),
                            IsDeleted = (bool)reader.GetValueDefault(6, false),
                        };
                        result.Add(c);
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
