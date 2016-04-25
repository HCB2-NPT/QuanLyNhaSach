using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public partial class ErrorManager
    {
        public Data.Error QueryError
        {
            get
            {
                return new Data.Error(
                    "Truy vấn dữ liệu thất bại!",
                    "Không thể truy vấn dữ liệu hiện tại hoặc có lỗi trong quá trình truy vấn!",
                    false);
            }
        }
    }
}
