using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public class DataManager //constant
    {
        private static DataManager _current = null;
        public static DataManager Current
        {
            get
            {
                if (_current == null)
                    _current = new DataManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public string FOLDER_DATA { get { return "./Data/"; } }

        public string FOLDER_IMAGES { get { return FOLDER_DATA + "Images/"; } }
    }
}
