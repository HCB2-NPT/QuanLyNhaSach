using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public class Manager //constant
    {
        private static Manager _current = null;
        public static Manager Current
        {
            get
            {
                if (_current == null)
                    _current = new Manager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public ConfigManager Config { get { return ConfigManager.Current; } }

        public ErrorManager Error { get { return ErrorManager.Current; } }

        public UserManager User { get { return UserManager.Current; } }

        public DataManager Data { get { return DataManager.Current; } }

        public RuleManager Rule { get { return RuleManager.Current; } }
    }
}
