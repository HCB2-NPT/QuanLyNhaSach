using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public class ErrorManager
    {
        private static ErrorManager _current = null;
        public static ErrorManager Current
        {
            get
            {
                if (_current == null)
                    _current = new ErrorManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public Data.Error TestError()
        {
            return new Data.Error("Tên lỗi...",
                "Fix vầy nè...");
        }
    }
}
