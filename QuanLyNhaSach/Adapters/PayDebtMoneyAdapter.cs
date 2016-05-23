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
    class PayDebtMoneyAdapter
    {
        public static int InsertPayDebt(PayDebtMoney pdm)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("insert into PhieuThuTien (MaKhachHang, NgayThu, SoTienThu , MaTaiKhoan)" +
                    string.Format(" values ({0}, '{1}', {2}, {3})",pdm.Customer.ID,pdm.DateCreated,pdm.PayMoney,pdm.IDManager));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static ObservableCollection<PayDebtMoney> GetAllPays()
        {
            ObservableCollection<PayDebtMoney> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaPhieuThu, MaKhachHang, NgayThu, SoTienThu, MaTaiKhoan from PhieuThuTien");
                if (reader != null)
                {
                    result = new ObservableCollection<PayDebtMoney>();
                    while (reader.Read())
                    {
                        result.Add(new PayDebtMoney()
                        {
                            IDPayDebt = reader.GetInt32(0),
                            Customer = Adapters.CustomerAdapter.GetCustomer(reader.GetInt32(1)),
                            DateCreated = reader.GetDateTime(2),
                            PayMoney = reader.GetInt32(3),
                            IDManager = reader.GetInt32(4)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return result;
        }
    }
}
