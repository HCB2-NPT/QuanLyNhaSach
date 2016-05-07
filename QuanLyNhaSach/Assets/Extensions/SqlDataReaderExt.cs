using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach
{
    public static class SqlDataReaderExt
    {
        public static object GetValueDefault(this SqlDataReader reader, int index, object defaultValue)
        {
            return reader.IsDBNull(index) ? defaultValue : reader.GetValue(index);
        }
    }
}
