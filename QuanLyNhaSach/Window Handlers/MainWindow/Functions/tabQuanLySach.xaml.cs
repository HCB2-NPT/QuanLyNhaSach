using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyNhaSach.Window_Handlers.MainWindow.Functions.UserControls;

namespace QuanLyNhaSach.Window_Handlers.MainWindow.Functions
{
    /// <summary>
    /// Interaction logic for tabQuanLySach.xaml
    /// </summary>
    public partial class tabQuanLySach : UserControl
    {
        public tabQuanLySach()
        {
            InitializeComponent();
            ListViewItemUserControl lvi = new ListViewItemUserControl();
        }
    }
}
