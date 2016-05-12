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
                var reader = DataConnector.ExecuteQuery("select MaSach,TenSach,AnhBia,SoLuongTon,DonGia,BiXoa " +
                                                        "from Sach ");
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new Book(id);
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Image = (string)reader.GetValueDefault(2, null);
                        item.Number = (int)reader.GetValueDefault(3, 0);
                        item.Price = (int)reader.GetValueDefault(4, 0);
                        item.IsDelete = (bool)reader.GetValueDefault(5, false);
                        item.Authors = AuthorAdapter.GetAuthorsForBook(id);
                        item.Genres = GenreAdapter.GetGenresForBook(id);
                        item.EndInit();
                        result.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        public static ObservableCollection<Book> GetDeletedBooks()
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaSach,TenSach,AnhBia,SoLuongTon,DonGia " +
                                                        "from Sach " +
                                                        "where BiXoa = 'true'");
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new Book(id);
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Image = (string)reader.GetValueDefault(2, null);
                        item.Number = (int)reader.GetValueDefault(3, 0);
                        item.Price = (int)reader.GetValueDefault(4, 0);
                        item.IsDelete = true;
                        item.Authors = AuthorAdapter.GetAuthorsForBook(id);
                        item.Genres = GenreAdapter.GetGenresForBook(id);
                        item.EndInit();
                        result.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        public static ObservableCollection<Book> GetBooksForGenre(int genreid)
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select s.MaSach,s.TenSach,s.AnhBia,s.SoLuongTon,s.DonGia,s.BiXoa " +
                                                        "from Sach s, ChiTietTheLoaiSach ct " +
                                                        "where s.MaSach = ct.MaSach and ct.MaTheLoai = " + genreid);
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new Book(id);
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Image = (string)reader.GetValueDefault(2, null);
                        item.Number = (int)reader.GetValueDefault(3, 0);
                        item.Price = (int)reader.GetValueDefault(4, 0);
                        item.IsDelete = (bool)reader.GetValueDefault(5, false);
                        item.Authors = AuthorAdapter.GetAuthorsForBook(id);
                        item.Genres = GenreAdapter.GetGenresForBook(id);
                        item.EndInit();
                        result.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        public static ObservableCollection<Book> GetBooksForAuthor(int authorid)
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select s.MaSach,s.TenSach,s.AnhBia,s.SoLuongTon,s.DonGia,s.BiXoa " +
                                                        "from Sach s, ChiTietTacGiaSach ct " +
                                                        "where s.MaSach = ct.MaSach and ct.MaTacGia = " + authorid);
                if (reader != null)
                {
                    result = new ObservableCollection<Book>();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var item = new Book(id);
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(1, null);
                        item.Image = (string)reader.GetValueDefault(2, null);
                        item.Number = (int)reader.GetValueDefault(3, 0);
                        item.Price = (int)reader.GetValueDefault(4, 0);
                        item.IsDelete = (bool)reader.GetValueDefault(5, false);
                        item.Authors = AuthorAdapter.GetAuthorsForBook(id);
                        item.Genres = GenreAdapter.GetGenresForBook(id);
                        item.EndInit();
                        result.Add(item);
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
