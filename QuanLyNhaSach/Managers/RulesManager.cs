using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public class RulesManager : INotifyPropertyChanged
    {
        private static RulesManager _current = null;
        public static RulesManager Current
        {
            get
            {
                if (_current == null)
                    _current = new RulesManager();
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

        public Rule Rule { get; set; }

        private RulesManager()
        {
            Rule = Bus.SearchData.FindLastRule();
        }
    }
}
