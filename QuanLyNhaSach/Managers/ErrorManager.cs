using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public partial class ErrorManager //constant
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

        private bool _ignore = false;
        public bool Ignore { get { return _ignore; } set { _ignore = value; } }

        public static string ErrorTitle { get { return "Báo lỗi..."; } }
    }
}
