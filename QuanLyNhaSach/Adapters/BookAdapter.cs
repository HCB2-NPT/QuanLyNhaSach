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
    class BookAdapter
    {
        public static ObservableCollection<Book> GetAll()
        {
            
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select Sach.MaSach,TenSach,TenTheLoai,TenTacGia,SoLuongTon,DonGia,AnhBia,BiXoa" +
                                                        "from Sach,ChiTietTheLoaiSach,ChiTietTacGiaSach" +
                                                        "where Sach.MaSach = ChiTietTheLoaiSach.MaSach and ChiTietTheLoaiSach.MaTheLoai = TheLoai.MaTheLoai and Sach.MaSach=ChiTietTacGiaSach.MaSach and ChiTietTacGiaSach.MaTacGia=TacGia.MaTacGia");
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    while (reader.Read())
                    {
                        result.Add(FindBook(new Book(reader.GetInt32(0))));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }
        public static ObservableCollection<Book> GetBook(Genre g)
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select Sach.MaSach,TenSach,TenTheLoai,TenTacGia,SoLuongTon,DonGia,AnhBia,BiXoa" +
                                                        "from Sach,TacGia,TheLoai,ChiTietTheLoaiSach,ChiTietTacGiaSach" +
                                                        "where Sach.MaSach = ChiTietTheLoaiSach.MaSach and ChiTietTheLoaiSach.MaTheLoai = TheLoai.MaTheLoai and Sach.MaSach=ChiTietTacGiaSach.MaSach and ChiTietTacGiaSach.MaTacGia=TacGia.MaTacGia and TheLoai.MaTheLoai = " + g.ID);
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    while (reader.Read())
                    {
                        result.Add(FindBook(new Book(reader.GetInt32(0))));
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
