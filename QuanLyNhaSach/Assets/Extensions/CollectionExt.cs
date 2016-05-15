using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach
{
    public static class CollectionExt
    {
        public static List<object> ToList(this System.Collections.IList list)
        {
            var r = new List<object>();
            foreach (var item in list)
                r.Add(item);
            return r;
        }
    }
}
