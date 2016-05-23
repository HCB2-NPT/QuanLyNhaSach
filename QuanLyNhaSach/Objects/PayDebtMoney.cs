using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class PayDebtMoney
    {
        #region Properties
        private int _iDPayDebt;

        public int IDPayDebt
        {
            get { return _iDPayDebt; }
            set { _iDPayDebt = value; }
        }
        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }
        private DateTime _dateCreate;

        public DateTime DateCreated
        {
            get { return _dateCreate; }
            set { _dateCreate = value; }
        }
        private int _moneyRecieved;

        public int PayMoney
        {
            get { return _moneyRecieved; }
            set { _moneyRecieved = value; }
        }
        private int _iDManager;

        public int IDManager
        {
            get { return _iDManager; }
            set { _iDManager = value; }
        }
        #endregion
    }
}
