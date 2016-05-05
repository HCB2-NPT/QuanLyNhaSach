using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager.Adapter
{
    class CustomerAdapter
    {
        public static ObservableCollection<Data.Customer> GetAllCustomers()
        {
            ObservableCollection<Data.Customer> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaKhachHang,HoTen,SoTienNo,DiaChi,DienThoai,Email,BiXoa from KhachHang");
                if (reader != null)
                {
                    result = new ObservableCollection<Data.Customer>();
                    Data.Customer c;
                    while (reader.Read())
                    {
                        c = new Data.Customer(reader.GetInt32(0))
                        {
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            TienNo = reader.GetInt32(2),
                            Adress = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                            IsDeleted = reader.GetBoolean(6)
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
