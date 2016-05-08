using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Views.Views.Windows
{
    public partial class About
    {
        #region Window Events
        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Bus.AppHandler.VirtualWindowClose(this);
        }
        #endregion
    }
}
