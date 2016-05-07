using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Managers
{
    public class ConfigManager //constant
    {
        private static ConfigManager _current = null;
        public static ConfigManager Current
        {
            get
            {
                if (_current == null)
                    _current = new ConfigManager();
                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        private string _programName;
        private int _max_login_fail;
        private bool _testmode_passlogin;
        private string _testmode_passlogin_username;
        private string _testmode_passlogin_password;
        private int _id_administrator;

        private ConfigManager()
        {
            try
            {
                _programName = System.Configuration.ConfigurationManager.AppSettings["app_name"];
                _max_login_fail = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["max_login_fail"]);
                _testmode_passlogin = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["testmode_passlogin"]);
                _testmode_passlogin_username = System.Configuration.ConfigurationManager.AppSettings["testmode_passlogin_username"];
                _testmode_passlogin_password = System.Configuration.ConfigurationManager.AppSettings["testmode_passlogin_password"];
                _id_administrator = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["id_administrator"]);
            }
            catch
            {
                ErrorManager.Current.CantConfig.Call();
            }
        }

		public string ProgramName { get { return _programName; } }

        public int MaxTimesLoginFail { get { return _max_login_fail; } }

        public bool TestMode_PassLogin { get { return _testmode_passlogin; } }

        public string TestMode_PassLogin_Username { get { return _testmode_passlogin_username; } }

        public string TestMode_PassLogin_Password { get { return _testmode_passlogin_password; } }
    }
}
