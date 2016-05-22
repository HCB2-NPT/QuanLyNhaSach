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
    public class AddedBookAdapter
    {
        public static int InsertAddedBookItem(int ID,int IDBook,AddedBook ab)
        {
            try
            {
                DataConnector.ExecuteNonQuery(string.Format("insert into ChiTietPhieuNhap(MaPhieuNhap,MaSach,SoLuong) values({0},{1},{2})",ID,IDBook,ab.Number));
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeInsert.Call(ex.Message);
            }
            return -1;
        }

        //public static ObservableCollection<AddedBook> GetAllAddedBook()
        //{
        //    //ObservableCollection<Book> result = null;
        //    //try
        //    //{
        //    //    var reader = DataConnector.ExecuteQuery("select ");
        //    //    if (reader != null)
        //    //    {
        //    //        result = new ObservableCollection<Book>();
        //    //        while (reader.Read())
        //    //        {
        //    //            var id = reader.GetInt32(0);
        //    //            var item = new Book(id);
        //    //            item.BeginInit();
        //    //            item.Name = (string)reader.GetValueDefault(1, null);
        //    //            item.Image = (string)reader.GetValueDefault(2, null);
        //    //            item.Number = (int)reader.GetValueDefault(3, 0);
        //    //            item.Price = (int)reader.GetValueDefault(4, 0);
        //    //            item.IsDeleted = (bool)reader.GetValueDefault(5, false);
        //    //            foreach (var i in AuthorAdapter.GetAuthorsForBook(id))
        //    //            {
        //    //                item.Authors.Add(i);
        //    //            }
        //    //            foreach (var i in GenreAdapter.GetGenresForBook(id))
        //    //            {
        //    //                item.Genres.Add(i);
        //    //            }
        //    //            item.IsDeletedItem = item.IsDeleted;
        //    //            item.EndInit();
        //    //            result.Add(item);
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ErrorManager.Current.DataCantBeRead.Call(ex.Message);
        //    //}
        //    //return result;
        //}
    }
}
