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
                    "Lỗi Số Lượng!",
                    "Số lượng trong kho của sản phẩm nhỏ hơn so với quy định.",
                    false);
            }
        }

        public Error LimitMaxDebtMoney
        {
            get
            {
                return new Error(
                    "Lỗi tiền nợ khách hàng!",
                    "Lượng tiền nợ sau khi mua của khách hàng lớn hơn so với qui định.",
                    false);
            }
        }

        public Error UnknowCustomer
        {
            get
            {
                return new Error(
                    "Lỗi khách hàng!",
                    "Khách hàng không có ID không được nợ tiền!",
                    false);
            }
        }
    }
}
