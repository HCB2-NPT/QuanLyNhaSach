using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class AccessLevel
    {
        public AccessLevel(int id)
        {
            ID = id;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}
