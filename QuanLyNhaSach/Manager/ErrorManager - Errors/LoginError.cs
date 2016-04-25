using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public partial class ErrorManager
    {
        public Data.Error LoginDuplicate
        {
            get
            {
                return new Data.Error(
                    "Lỗi đăng nhập!",
                    "Không thể đăng nhập khi đang đăng nhập một tài khoản khác.\n" +
                    "Vui lòng đăng xuất trước!",
                    false);
            }
        }

        public Data.Error NoOneLogin
        {
            get
            {
                return new Data.Error(
                    "Lỗi đăng xuất!",
                    "Không tồn tại tài khoản nào để đăng xuất!",
                    false);
            }
        }

        public Data.Error NotExistsAccount
        {
            get
            {
                return new Data.Error(
                    "Báo lỗi tài khoản!",
                    "Không tìm thấy tài khoản mà bạn cần!",
                    false);
            }
        }

        public Data.Error WrongUsernameOrPassword
        {
            get
            {
                return new Data.Error(
                    "Đăng nhập thất bại!",
                    "Sai tên tài khoản hoặc mật khẩu, vui lòng kiểm tra lại!",
                    false);
            }
        }

        public Data.Error LoginWrongManyTimes
        {
            get
            {
                return new Data.Error(
                    "Cảnh cáo đăng nhập thất bại!",
                    "Bạn đã đăng nhập thất bại quá số lần quy định.\nChương trình sẽ tự động thoát để tránh các chương trình thăm dò thông tin tài khoản!");
            }
        }
    }
}
