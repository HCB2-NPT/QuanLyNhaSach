using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public class CommonIconManager
    {
        private static CommonIconManager _current = null;
        public static CommonIconManager Current
        {
            get
            {
                if (_current == null)
                    _current = new CommonIconManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public string WindowState_Minimize { get { return @"Assets/Images/Minimize.png"; } }

        public string WindowState_Maximize { get { return @"Assets/Images/Maximize.png"; } }

        public string WindowState_Normal { get { return @"Assets/Images/restore.png"; } }
		
		public string WindowClose { get { return @"Assets/Images/close.png"; } }
		
		public string WindowState_Minimize_Black { get { return @"Assets/Images/minimize_black.png"; } }

        public string WindowState_Maximize_Black { get { return @"Assets/Images/maximize_black.png"; } }

        //public string WindowState_Normal_Black { get { return ""; } }
		
		public string WindowClose_Black { get { return @"Assets/Images/close_black.png"; } }

        public string Error { get { return @"Assets/Images/Error-40.png"; } }
    }
}
