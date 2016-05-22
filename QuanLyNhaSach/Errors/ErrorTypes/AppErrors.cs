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

        public Error MinNumberLimitBookInStorage
        {
            get
            {
                return new Error(
                    "Lỗi thông số!",
                    "Số lượng trong kho của sản phẩm nhỏ hơn so với quy định.",
                    false);
            }
        }

        public Error LimitMaxDebtMoney
        {
            get
            {
                return new Error(
                    "Lỗi thông số!",
                    "Lượng tiền nợ sau khi mua của khách hàng lớn hơn so với qui định.",
                    false);
            }
        }

        public Error UnknowCustomer
        {
            get
            {
                return new Error(
                    "Lỗi thông tin!",
                    "Không có thông tin khách hàng.",
                    false);
            }
        }

        public Error PopularCustomer
        {
            get
            {
                return new Error(
                    "Lỗi thông số!",
                    "Khách hàng thông thường không cho phép nợ!",
                    false);
            }
        }

        public Error BillEmpty
        {
            get
            {
                return new Error(
                    "Lỗi thông số!",
                    "Hóa đơn rỗng.",
                    false);
            }
        }

        public Error BookCantInsert
        {
            get
            {
                return new Error(
                    "Lỗi số lượng!",
                    "Số lượng trong kho còn nhiều , không thể nhập.",
                    false);
            }
        }

        public Error WrongDateTime
        {
            get
            {
                return new Error(
                    "Lỗi thông số!",
                    "Ngày nhập kho phải tính từ hôm nay trở đi.",
                    false);
            }
        }

        public Error InfoIsNull
        {
            get
            {
                return new Error(
                    "Lỗi thông số!",
                    "Dữ liệu không tồn tại hoặc trống.",
                    false);
            }
        }
    }
}
