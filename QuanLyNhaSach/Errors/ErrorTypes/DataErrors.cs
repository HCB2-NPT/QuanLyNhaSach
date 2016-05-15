using QuanLyNhaSach.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public partial class ErrorManager
    {
        public Error DataCantBeRead
        {
            get
            {
                return new Error(
                    "Lỗi dữ liệu!",
                    "Dữ liệu đã truy vấn được nhưng xãy ra lỗi trong quá trình đọc!",
                    false);
            }
        }

        public Error DataCantBeUpdate
        {
            get
            {
                return new Error(
                    "Lỗi dữ liệu!",
                    "Đã truy vấn được nhưng có vấn đề xãy ra sau đó,\ncó thể do lỗi dữ liệu đầu vào!",
                    false);
            }
        }

        public Error DataCantBeInsert
        {
            get
            {
                return new Error(
                    "Lỗi dữ liệu!",
                    "Đã truy vấn được nhưng có vấn đề xãy ra sau đó,\ncó thể do lỗi dữ liệu đầu vào!",
                    false);
            }
        }

        public Error DataCantBeDelete
        {
            get
            {
                return new Error(
                    "Lỗi dữ liệu!",
                    "Có vấn đề xãy ra trong khi xóa dữ liệu.\nCó thể dữ liệu đã được xóa nhưng phát sinh lỗi sau đó.",
                    false);
            }
        }
    }
}
