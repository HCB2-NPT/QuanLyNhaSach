using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Editable
    {
        public bool IsNewItem { get; set; }

        public bool IsEditedItem { get; set; }

        public bool IsDeletedItem { get; set; }

        protected Editable()
        {
            IsNewItem = false;
            IsEditedItem = false;
            IsDeletedItem = false;
        }
    }
}
