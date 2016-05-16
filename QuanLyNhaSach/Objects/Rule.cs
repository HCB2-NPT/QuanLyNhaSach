using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Objects
{
    public class Rule : Editable
    {
        private int _id;
        private DateTime _updateTime;
        private int _minNumberToImport = -1;
        private int _minNumberWhenImport = -1;
        private int _maxDebt = -1;
        private int _minNumberInStore = -1;
        private bool _allowGetMoneyGreaterDebt = true;

        #region Constructor
        public Rule()
            : base(true)
        {
            _id = 0;
        }

        public Rule(int id, DateTime updateTime)
            : base()
        {
            _id = id;
            _updateTime = updateTime;
        }
        #endregion

        #region Properties
        public int ID
        {
            get { return _id; }
        }

        public DateTime UpdateTime
        {
            get { return _updateTime; }
        }

        public int MinNumberToImport
        {
            get { return _minNumberToImport; }
            set { _minNumberToImport = value; NotifyPropertyChanged("MinNumberToImport"); }
        }

        public int MinNumberWhenImport
        {
            get { return _minNumberWhenImport; }
            set { _minNumberWhenImport = value; NotifyPropertyChanged("MinNumberWhenImport"); }
        }

        public int MaxDebt
        {
            get { return _maxDebt; }
            set { _maxDebt = value; NotifyPropertyChanged("MaxDebt"); }
        }

        public int MinNumberInStore
        {
            get { return _minNumberInStore; }
            set { _minNumberInStore = value; NotifyPropertyChanged("MinNumberInStore"); }
        }

        public bool AllowGetMoneyGreaterDebt
        {
            get { return _allowGetMoneyGreaterDebt; }
            set { _allowGetMoneyGreaterDebt = value; NotifyPropertyChanged("AllowGetMoneyGreaterDebt"); }
        }
        #endregion
    }
}
