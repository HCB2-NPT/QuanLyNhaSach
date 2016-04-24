using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public partial class ErrorManager
    {
        public Data.Error DataCantBeRead
        {
            get
            {
                return new Data.Error(
                    "Lỗi dữ liệu!",
                    "Dữ liệu đã truy vấn được nhưng xãy ra lỗi trong quá trình đọc!",
                    false);
            }
        }
    }
}
