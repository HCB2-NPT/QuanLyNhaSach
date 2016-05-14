using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class InsertData
    {
        public static void SaveNewRules(Rule item)
        {
            if (item == null)
                return;
            if (!item.IsEditedItem)
                return;
            RuleAdapter.InsertNewRule(item);
        }
    }
}
