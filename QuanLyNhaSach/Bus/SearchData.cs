using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class SearchData
    {
        public static Rule FindLastRule()
        {
            return RuleAdapter.GetLastRules();
        }
    }
}
