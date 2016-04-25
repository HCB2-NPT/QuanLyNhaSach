using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Manager.Data
{
    public class Error //constant
    {
        public Windows.Others.WarningBox WarningBox { get; private set; }

        public Error LastCreatedError { get; private set; }

        public Error(string name, string content, bool isCrash = true, string exception = null)
        {
            if (!ErrorManager.Current.Ignore)
                WarningBox = new QuanLyNhaSach.Windows.Others.WarningBox(ErrorManager.ErrorTitle, name, content, isCrash, exception);
            LastCreatedError = this;
        }

        public Data.Error Call(string exception = null)
        {
            WarningBox.ErrorException = exception;
            WarningBox.ShowDialog();
            return this;
        }
    }
}
