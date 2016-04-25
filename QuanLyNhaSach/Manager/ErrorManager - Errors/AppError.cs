using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public partial class ErrorManager
    {
        public Data.Error CantOpenAppMoreTimes
        {
            get
            {
                return new Data.Error(
                    "Lỗi chương trình!",
                    "Không thể mở chương trình này hai lần!\n(Chạy song song được nhưng không thích cho chạy.)");
            }
        }

        public Data.Error CantConfig
        {
            get
            {
                return new Data.Error(
                    "Lỗi chương trình!",
                    "Config không đúng, xin kiểm tra lại!");
            }
        }

        public Data.Error OutLookError
        {
            get
            {
                return new Data.Error(
                    "Outlook!",
                    "Outlook chưa được cài đặt.\nHoặc đã có lỗi trong quá trình mở!",
                    false);
            }
        }

        public Data.Error AppCanNotUseNow
        {
            get
            {
                return new Data.Error(
                    "Thiếu chức năng!",
                    "Chức năng chưa được làm.",
                    false);
            }
        }
    }
}
