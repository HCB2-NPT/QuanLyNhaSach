using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
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
                    string.Format(" values ({0}, '{1}', {2}, {3})",pdm.Customer.ID,pdm.DateCreate,pdm.MoneyRecieved,pdm.IDManager));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }
    }
}
