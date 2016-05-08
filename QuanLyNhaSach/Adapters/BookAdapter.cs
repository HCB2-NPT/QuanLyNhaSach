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
                var reader = DataConnector.ExecuteQuery("select Sach.MaSach,TenSach,TenTheLoai,TenTacGia,SoLuongTon,DonGia,AnhBia,BiXoa" +
                                                        "from Sach,TacGia,TheLoai,ChiTietTheLoaiSach,ChiTietTacGiaSach" +
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
        public static ObservableCollection<Book> GetAllBook(Genre g)
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

        public static Book FindBook(Book _book)
        {
            Book b = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select Sach.MaSach,TenSach,TenTheLoai,TenTacGia,SoLuongTon,DonGia,AnhBia,BiXoa" +
                                                        "from Sach,TacGia,TheLoai,ChiTietTheLoaiSach,ChiTietTacGiaSach" +
                                                        "where Sach.MaSach = ChiTietTheLoaiSach.MaSach and ChiTietTheLoaiSach.MaTheLoai = TheLoai.MaTheLoai and Sach.MaSach=ChiTietTacGiaSach.MaSach and ChiTietTacGiaSach.MaTacGia=TacGia.MaTacGia and Sach.MaSach = " + _book.ID);
                if (reader != null)
                {
                    int flag = 0;
                    while (reader.Read())
                    {
                        if (flag==0)
                        {
                            Author a = new Author() { Name = (string)reader.GetValueDefault(3, null) };
                            Genre g = new Genre() { Name = (string)reader.GetValueDefault(2, null) };
                            b = new Book(_book.ID)
                            {
                                Image = (string)reader.GetValueDefault(6,null),
                                IsDelete = (bool)reader.GetValueDefault(7,0),
                                Name = (string)reader.GetValueDefault(1,null),
                                Number= (int)reader.GetValueDefault(5,0),
                                Price = (int)reader.GetValueDefault(4,0)
                            };
                            b.ListAuthor.Add(a);
                            b.ListGenre.Add(g);
                        }
                        else
                        {
                            Author a = new Author() { Name = (string)reader.GetValueDefault(3, null) };
                            Genre g = new Genre() { Name = (string)reader.GetValueDefault(2, null) };
                            if (!b.ListAuthor.Contains(a))
                                b.ListAuthor.Add(a);
                            if (!b.ListGenre.Contains(g))
                                b.ListGenre.Add(g);    
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return b;
        }
    }
}
