using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public class ConfigManager
    {
        private static ConfigManager _current = null;
        public static ConfigManager Current
        {
            get
            {
                if (_current == null)
                    _current = new ConfigManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }
    }
}
