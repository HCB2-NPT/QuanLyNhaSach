﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager
{
    public class Manager
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

        public DataManager Data { get { return DataManager.Current; } }

        public ConfigManager Config { get { return ConfigManager.Current; } }

        public CommonIconManager Icon { get { return CommonIconManager.Current; } }

        public ErrorManager Error { get { return ErrorManager.Current; } }
    }
}
