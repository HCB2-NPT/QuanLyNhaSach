using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Views.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyNhaSach.Errors
{
    public class Error //constant
    {
        public WarningBox WarningBox { get; private set; }

        public Error LastCreatedError { get; private set; }

        public Error(string name, string content, bool isCrash = true, string exception = null)
        {
            if (!ErrorManager.Current.Ignore)
                WarningBox = new WarningBox(ErrorManager.ErrorTitle, name, content, isCrash, exception);
            LastCreatedError = this;
        }

        public Error Call(string exception = null)
        {
            if (WarningBox != null)
            {
                WarningBox.ErrorException = exception;
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.IsActive)
                    {
                        WarningBox.Owner = item;
                        break;
                    }
                }
                WarningBox.ShowDialog();
            }
            return this;
        }
    }
}
