using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Assets.Scripts
{
    public class DialogHelper
    {
        public static System.Windows.Forms.OpenFileDialog CreateFileDialog(string initDirectory, string filter, int defaultSelectedFilter, bool restoreDirectory = true)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.InitialDirectory = initDirectory;
            openFileDialog.Filter = filter;
            openFileDialog.FilterIndex = defaultSelectedFilter;
            openFileDialog.RestoreDirectory = restoreDirectory;

            return openFileDialog;
        }
    }
}
