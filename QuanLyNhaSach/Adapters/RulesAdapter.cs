using QuanLyNhaSach.Managers;
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
        public static void GetLastRules(RulesManager rm)
        {
            try
            {
                var reader = DataConnector.ExecuteQuery("select " + 
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
                        rm.SoLuongSachTonToiThieuDeNhap = reader.GetInt32(1);
                        rm.SoLuongSachTonToiThieuSauKhiBan = reader.GetInt32(2);
                        rm.SoLuongSachNhapToiThieu = reader.GetInt32(0);
                        rm.TienNoToiDa = reader.GetInt32(3);
                        rm.DuocThuVuotSoTienKhachHangDangNo = reader.GetBoolean(4);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
        }
    }
}
