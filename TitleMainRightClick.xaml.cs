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

namespace QuanLyNhaSach
{
    /// <summary>
    /// Interaction logic for TitleMainRightClick.xaml
    /// </summary>
    public partial class TitleMainRightClick : Window
    {
		public Window Host { get; set; }
		
        public TitleMainRightClick(Window host)
        {
            InitializeComponent();
			DataContext = Manager.Manager.Current;
			Host = host;
        }

        public void _Show()
        {
            minimize.IsEnabled = !(Host.WindowState == System.Windows.WindowState.Minimized);
            maximize.IsEnabled = !(Host.WindowState == System.Windows.WindowState.Maximized);
            Show();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Hide();
        }

        private void minimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Host.WindowState = System.Windows.WindowState.Minimized;
            Hide();
        }

        private void maximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Host.WindowState = System.Windows.WindowState.Maximized;
            Hide();
        }

        private void close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Host.Close();
        }
    }
}
