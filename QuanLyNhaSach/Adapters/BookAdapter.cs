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
        public static ObservableCollection<Book> GetAll(bool includeDeleted = true)
        {
            ObservableCollection<Book> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select MaSach,TenSach,AnhBia,SoLuongTon,DonGia,BiXoa " +
                                                        "from Sach " + 
                                                        string.Format("{0}", includeDeleted ? "" : "where BiXoa = 'false'"));
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
                        item.IsDeleted = (bool)reader.GetValueDefault(5, false);
                        foreach (var i in AuthorAdapter.GetAuthorsForBook(id))
                        {
                            item.Authors.Add(i);
                        }
                        foreach (var i in GenreAdapter.GetGenresForBook(id))
                        {
                            item.Genres.Add(i);
                        }
                        item.IsDeletedItem = item.IsDeleted;
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
                        item.IsDeleted = true;
                        foreach (var i in AuthorAdapter.GetAuthorsForBook(id))
                        {
                            item.Authors.Add(i);
                        }
                        foreach (var i in GenreAdapter.GetGenresForBook(id))
                        {
                            item.Genres.Add(i);
                        }
                        item.IsDeletedItem = item.IsDeleted;
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
                        item.IsDeleted = (bool)reader.GetValueDefault(5, false);
                        foreach (var i in AuthorAdapter.GetAuthorsForBook(id))
                        {
                            item.Authors.Add(i);
                        }
                        foreach (var i in GenreAdapter.GetGenresForBook(id))
                        {
                            item.Genres.Add(i);
                        }
                        item.IsDeletedItem = item.IsDeleted;
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
                        item.IsDeleted = (bool)reader.GetValueDefault(5, false);
                        foreach (var i in AuthorAdapter.GetAuthorsForBook(id))
                        {
                            item.Authors.Add(i);
                        }
                        foreach (var i in GenreAdapter.GetGenresForBook(id))
                        {
                            item.Genres.Add(i);
                        }
                        item.IsDeletedItem = item.IsDeleted;
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

        public static Book GetBook(int id)
        {
            Book result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery("select s.TenSach,s.AnhBia,s.SoLuongTon,s.DonGia,s.BiXoa " +
                                                        "from Sach s where s.MaSach = " + id);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var item = new Book(id);
                        item.BeginInit();
                        item.Name = (string)reader.GetValueDefault(0, null);
                        item.Image = (string)reader.GetValueDefault(1, null);
                        item.Number = (int)reader.GetValueDefault(2, 0);
                        item.Price = (int)reader.GetValueDefault(3, 0);
                        item.IsDeleted = (bool)reader.GetValueDefault(4, false);
                        foreach (var i in AuthorAdapter.GetAuthorsForBook(id))
                        {
                            item.Authors.Add(i);
                        }
                        foreach (var i in GenreAdapter.GetGenresForBook(id))
                        {
                            item.Genres.Add(i);
                        }
                        item.IsDeletedItem = item.IsDeleted;
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

        public static int GetNumber(int bookid)
        {
            try
            {
                var reader = DataConnector.ExecuteQuery("select SoLuongTon from Sach where MaSach = " + bookid);
                if (reader != null)
                {
                    if (reader.Read())
                        return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int UpdateNumber(int bookid, int newValue)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("update Sach " +
                    string.Format("set SoLuongTon = {0} ", newValue) +
                    "where MaSach = " + bookid);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int DeleteBook(Book whichBook)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("update Sach set BiXoa = 'true' where MaSach = " + whichBook.ID);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int RecoverBook(Book whichBook)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("update Sach set BiXoa = 'false' where MaSach = " + whichBook.ID);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int InsertNewBook(Book whichBook)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("insert into Sach (TenSach, AnhBia, SoLuongTon, DonGia, BiXoa)" +
                    string.Format(" values (N'{0}', {1}, 0, {2}, 'false')", whichBook.Name, whichBook.Image == null ? "NULL" : string.Format("N'{0}'", whichBook.Image), whichBook.Price));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int UpdateBook(Book whichBook)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("update Sach set TenSach = N'{0}', AnhBia = {1}, DonGia = {2} where MaSach = {3}",
                    whichBook.Name, whichBook.Image == null ? "NULL" : string.Format("N'{0}'", whichBook.Image), whichBook.Price, whichBook.ID));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeUpdate.Call(ex.Message);
            }
            return -1;
        }

        public static int ClearAuthors(int bookid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("delete from ChiTietTacGiaSach where MaSach = " + bookid);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }

        public static int ClearGenres(int bookid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery("delete from ChiTietTheLoaiSach where MaSach = " + bookid);
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }

        public static int AddAuthor(int bookid, int authorid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("insert into ChiTietTacGiaSach (MaSach, MaTacGia) values ({0}, {1})", bookid, authorid));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int AddGenre(int bookid, int genreid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("insert into ChiTietTheLoaiSach (MaSach, MaTheLoai) values ({0}, {1})", bookid, genreid));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        public static int GetLatestId()
        {
            try
            {
                var reader = DataConnector.ExecuteQuery("select MAX(MaSach) from Sach");
                if (reader != null)
                {
                    if (reader.Read())
                        return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return -1;
        }

        public static int RemoveGenreFromBook(int bookid, int genreid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("delete from ChiTietTheLoaiSach where MaSach = {0} and MaTheLoai = {1}", bookid, genreid));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }

        public static int RemoveAuthorFromBook(int bookid, int authorid)
        {
            try
            {
                return DataConnector.ExecuteNonQuery(string.Format("delete from ChiTietTacGiaSach where MaSach = {0} and MaTacGia = {1}", bookid, authorid));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeDelete.Call(ex.Message);
            }
            return -1;
        }
    }
}
