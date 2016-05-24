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
    public class ReportAdapter
    {
        public static int ExistDebtorReport(int month, int year)
        {
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select MaBaoCao from BaoCaoCongNo where Thang = {0} and Nam = {1}", month, year));
                if (reader != null)
                {
                    while (reader.Read())
                        return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return -1;
        }

        public static int ExistNumberReport(int month, int year)
        {
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select MaBaoCao from BaoCaoTon where Thang = {0} and Nam = {1}", month, year));
                if (reader != null)
                {
                    while (reader.Read())
                        return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return -1;
        }

        public static ObservableCollection<Customer> GetDebtorReportData(int month, int year)
        {
            ObservableCollection<Customer> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select MaBaoCao from BaoCaoCongNo where Thang = {0} and Nam = {1}", month, year));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        result = new ObservableCollection<Customer>();

                        var r = DataConnector.ExecuteQuery(string.Format("select MaKhachHang, SoTienNoDau, SoTienNoCuoi from ChiTietBaoCaoCongNo where MaBaoCao = {0}", id));
                        if (r != null)
                        {
                            while (r.Read())
                            {
                                var c = Adapters.CustomerAdapter.GetCustomer(r.GetInt32(0));
                                c.BeginInit();
                                c.Debt = (int)r.GetValueDefault(2, 0);
                                c.Tag = (int)r.GetValueDefault(1, 0);
                                c.EndInit();
                                result.Add(c);
                            }
                        }
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

        public static ObservableCollection<Book> GetNumberReportData(int month, int year)
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(string.Format("select MaBaoCao from BaoCaoTon where Thang = {0} and Nam = {1}", month, year));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        result = new ObservableCollection<Book>();

                        var r = DataConnector.ExecuteQuery(string.Format("select MaSach, SoLuongTonDau, SoLuongTonCuoi from ChiTietBaoCaoTon where MaBaoCao = {0}", id));
                        if (r != null)
                        {
                            while (r.Read())
                            {
                                var c = Adapters.BookAdapter.GetBook(r.GetInt32(0));
                                c.BeginInit();
                                c.Number = (int)r.GetValueDefault(2, 0);
                                c.Tag = (int)r.GetValueDefault(1, 0);
                                c.EndInit();
                                result.Add(c);
                            }
                        }
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

        public static int InsertNewDebtorReport(ObservableCollection<Customer> data, int month, int year)
        {
            try
            {
                var result = DataConnector.ExecuteNonQuery(string.Format("insert into BaoCaoCongNo (Thang, Nam) values ({0}, {1})", month, year));
                if (result == 1)
                {
                    var id = ExistDebtorReport(month, year);
                    foreach (var c in data)
                    {
                        DataConnector.ExecuteNonQuery(string.Format("insert into ChiTietBaoCaoCongNo (MaBaoCao, MaKhachHang, SoTienNoDau, SoTienNoCuoi) values ({0}, {1}, {2}, {3})", id, c.ID, c.Tag, c.Debt));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int InsertNewNumberReport(ObservableCollection<Book> data, int month, int year)
        {
            try
            {
                var result = DataConnector.ExecuteNonQuery(string.Format("insert into BaoCaoTon (Thang, Nam) values ({0}, {1})", month, year));
                if (result == 1)
                {
                    var id = ExistNumberReport(month, year);
                    foreach (var c in data)
                    {
                        DataConnector.ExecuteNonQuery(string.Format("insert into ChiTietBaoCaoTon (MaBaoCao, MaSach, SoLuongTonDau, SoLuongTonCuoi) values ({0}, {1}, {2}, {3})", id, c.ID, c.Tag, c.Number));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }
    }
}
