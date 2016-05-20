using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Function
    {
        public string Title { get; set; }

        public object Data { get; set; }

        public bool CanDuplicate { get; set; }

        public object Tag { get; set; }

        public ObservableCollection<Function> Children { get; private set; }

        public Function(string title = null)
        {
            Title = title;
            Data = null;
            CanDuplicate = false;
            Children = new ObservableCollection<Function>();
        }
    }
}
