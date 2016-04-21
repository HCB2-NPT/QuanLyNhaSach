using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager.Data
{
    public class Binding
    {
        public object Data { get; set; }

        public object Tag { get; set; }

        public ObservableCollection<Binding> Children { get; private set; }

        public Binding(object data = null)
        {
            Data = data;
            Tag = null;
            Children = new ObservableCollection<Binding>();
        }
    }
}
