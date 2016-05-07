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
        public static ObservableCollection<Book> GetAllBook()
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select Sach.MaSach,TenSach,TenTheLoai,TenTacGia,SoLuongTon,DonGia,AnhBia,BiXoa"+
                                                        "from Sach,TacGia,TheLoai,ChiTietTheLoaiSach,ChiTietTacGiaSach"+
                                                        "where Sach.MaSach = ChiTietTheLoaiSach.MaSach and ChiTietTheLoaiSach.MaTheLoai = TheLoai.MaTheLoai and Sach.MaSach=ChiTietTacGiaSach.MaSach and ChiTietTacGiaSach.MaTacGia=TacGia.MaTacGia");
                if (reader!=null)
                {
                    result = new ObservableCollection<Book>();
                    Book b;
                    while (reader.Read())
                    {
                        b = new Book();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Chưa xong
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static ObservableCollection<Book> GetAllBook(Genre g)
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select Sach.MaSach,TenSach,TenTheLoai,TenTacGia,SoLuongTon,DonGia,AnhBia,BiXoa" +
                                                        "from Sach,TacGia,TheLoai,ChiTietTheLoaiSach,ChiTietTacGiaSach" +
                                                        "where Sach.MaSach = ChiTietTheLoaiSach.MaSach and ChiTietTheLoaiSach.MaTheLoai = TheLoai.MaTheLoai and Sach.MaSach=ChiTietTacGiaSach.MaSach and ChiTietTacGiaSach.MaTacGia=TacGia.MaTacGia");
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    Book b;
                    while (reader.Read())
                    {
                        b = new Book();
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
