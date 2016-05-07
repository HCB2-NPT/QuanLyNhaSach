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
        public Error CantOpenAppMoreTimes
        {
            get
            {
                return new Error(
                    "Lỗi chương trình!",
                    "Không thể mở chương trình này hai lần!\n(Chạy song song được nhưng không thích cho chạy.)");
            }
        }

        public Error CantConfig
        {
            get
            {
                return new Error(
                    "Lỗi chương trình!",
                    "Config không đúng, xin kiểm tra lại!");
            }
        }

        public Error OutLookError
        {
            get
            {
                return new Error(
                    "Outlook!",
                    "Outlook chưa được cài đặt.\nHoặc đã có lỗi trong quá trình mở!",
                    false);
            }
        }

        public Error AppCanNotUseNow
        {
            get
            {
                return new Error(
                    "Thiếu chức năng!",
                    "Chức năng chưa được làm.",
                    false);
            }
        }
    }
}
