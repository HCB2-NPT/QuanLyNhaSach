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

        public static void NewBill(Bill newBill)
        {
            BillAdapter.InsertNewBill(newBill);
        }

        public static void NewPayDebtMoney(PayDebtMoney pdm)
        {
            PayDebtMoneyAdapter.InsertPayDebt(pdm);
            CustomerAdapter.UpdateDebt(pdm.Customer.ID, pdm.Customer.Debt);
        }
    }
}
