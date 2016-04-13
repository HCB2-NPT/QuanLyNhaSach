using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager.Data
{
    public class Error
    {
        public static string ErrorTitle { get { return "Báo lỗi..."; } }

        public string Name { get; set; }

        public string Content { get; set; }

        public bool IsCrash { get; set; }

        public Error(string name, string content, bool isCrash = true)
        {
            WarningBox.Show(ErrorTitle, name, content, isCrash);
            Name = name;
            Content = content;
            IsCrash = isCrash;
        }
    }
}
