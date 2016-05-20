using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class SearchData
    {
        public static Rule FindLastRule()
        {
            return RuleAdapter.GetLastRules();
        }

        public static ObservableCollection<Book> SearchBooks(ObservableCollection<Book> list, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return list;
            }
            else
            {
                try
                {
                    return list.Where(x =>
                        {
                            var data =
                                x.ID.ToString() +
                                (x.Name == null ? "" : x.Name.ToLower()) +
                                x.Number.ToString() +
                                x.Price.ToString() +
                                x.PriceFormat +
                                (x.AuthorsFormat == null ? "" : x.AuthorsFormat.ToLower()) +
                                (x.GenresFormat == null ? "" : x.GenresFormat.ToLower()) +
                                (x.IsDeletedItem ? "bị xóa" : "") +
                                (x.Image == null ? Managers.DataManager.Current.NO_IMAGE.ToLower() : x.Image.ToLower());
                            return data.Contains(key);
                        }
                        ).ToObservableCollection<Book>();
                }
                catch { }
            }
            return null;
        }

        public static ObservableCollection<Book> SearchBooks(string key)
        {
            var list = Adapters.BookAdapter.GetAll();
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            else
            {
                try
                {
                    return list.Where(x =>
                    {
                        var data =
                            x.ID.ToString() +
                            (x.Name == null ? "" : x.Name.ToLower()) +
                            x.Number.ToString() +
                            x.Price.ToString() +
                            x.PriceFormat +
                            (x.AuthorsFormat == null ? "" : x.AuthorsFormat.ToLower()) +
                            (x.GenresFormat == null ? "" : x.GenresFormat.ToLower()) +
                            (x.IsDeletedItem ? "bị xóa" : "") +
                            (x.Image == null ? Managers.DataManager.Current.NO_IMAGE.ToLower() : x.Image.ToLower());
                        return data.Contains(key);
                    }
                        ).ToObservableCollection<Book>();
                }
                catch { }
            }
            return null;
        }
    }
}
