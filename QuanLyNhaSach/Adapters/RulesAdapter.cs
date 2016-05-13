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
    public class RulesAdapter
    {
        public static Rule GetLastRules()
        {
            Rule result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaQuyDinh, NgayCapNhat, " + 
                    "SoLuongSachNhapToiThieu, " + 
                    "SoLuongSachTonToiThieuDeNhap, " + 
                    "SoLuongSachTonToiThieuSauKhiBan, " + 
                    "TienNoToiDa, " + 
                    "DuocThuVuotSoTienKhachHangDangNo " + 
                    "from QuyDinh order by NgayCapNhap desc");
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var item = new Rule(reader.GetInt32(0), reader.GetDateTime(1));
                        item.BeginInit();
                        item.MinNumberWhenImport = reader.GetInt32(2);
                        item.MinNumberToImport = reader.GetInt32(3);
                        item.MinNumberInStore = reader.GetInt32(4);
                        item.MaxDebt = reader.GetInt32(5);
                        item.AllowGetMoneyGreaterDebt = reader.GetBoolean(6);
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

        public static int InsertNewRule(Rule item)
        {
            var error = 0;
            try
            {
                error = DataConnector.ExecuteNonQuery("INSERT INTO QuyDinh () VALUES (value1,value2,value3,...)");
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return error;
        }
    }
}
