using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public class RuleManager : INotifyPropertyChanged
    {
        private static RuleManager _current = null;
        public static RuleManager Current
        {
            get
            {
                if (_current == null)
                    _current = new RuleManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        private Rule _rule;

        public Rule Rule
        {
            get { return _rule; }
            set { _rule = value; NotifyPropertyChanged("Rule"); }
        }

        private RuleManager()
        {
            Rule = Bus.SearchData.FindLastRule();
        }
    }
}
