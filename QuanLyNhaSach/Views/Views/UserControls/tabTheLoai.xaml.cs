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

namespace QuanLyNhaSach.Views.Views.UserControls
{
    /// <summary>
    /// Interaction logic for tabTheLoai.xaml
    /// </summary>
    public partial class tabTheLoai : UserControl
    {
        public tabTheLoai()
        {
            InitializeComponent();
            lv_DSTheLoai.ItemsSource = Adapters.GenreAdapter.GetAllGenres();
        }
    }
}
