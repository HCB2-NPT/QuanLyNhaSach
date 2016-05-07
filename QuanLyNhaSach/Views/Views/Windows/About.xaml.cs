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
using System.Windows.Shapes;

namespace QuanLyNhaSach.Views.Views.Windows
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        #region Constructor
        public About()
        {
            InitializeComponent();
            var workingArea = Assets.Scripts.WpfScreen.GetScreenFrom(this).WorkingArea;
            Top = workingArea.Height - Height - 16;
            Left = workingArea.Width - Width - 16;
        }
        #endregion
    }
}
