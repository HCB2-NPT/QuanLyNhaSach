using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class CheckData
    {
        public static bool IsAccountExist(string username, string password, out Account result, bool findDeletedToo = false)
        {
            return AccountAdapter.IsExists(username, password, out result, findDeletedToo);
        }
    }
}
