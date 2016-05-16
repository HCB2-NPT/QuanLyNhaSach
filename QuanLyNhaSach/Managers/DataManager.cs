using System;
using System.Collections.Generic;
using System.IO;
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

        public DataManager()
        {
            folder_project = Directory.GetCurrentDirectory();
            folder_data = folder_project + "\\Data";
            folder_images = folder_data + "\\Images";
            no_images = "no_image.png";
            popularCustomer_id = 13;
        }

        private string folder_project;
        private string folder_data;
        private string folder_images;
        private string no_images;
        private int popularCustomer_id;

        public string FOLDER_PROJECT { get { return folder_project; } }

        public string FOLDER_DATA { get { return folder_data; } }

        public string FOLDER_IMAGES { get { return folder_images; } }

        public string NO_IMAGE { get { return no_images; } }

        public int PopularCustomerID { get { return popularCustomer_id; } }
    }
}
