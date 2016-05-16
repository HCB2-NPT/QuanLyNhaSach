using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach
{
    public static class CollectionExt
    {
        public static List<object> ToList(this IList list)
        {
            var r = new List<object>();
            foreach (var item in list)
                r.Add(item);
            return r;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var r = new ObservableCollection<T>();
            foreach (var item in list)
                r.Add(item);
            return r;
        }
    }
}
