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

namespace QuanLyNhaSach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			EventWindowDrag = new WindowsDragger(this, TitleMain);
			EventTitleMainDoubleClick = new EventDoubleClick(this, TitleMain, TitleMainDoubleClick);
            DataContext = Manager.Manager.Current;
		}
		
		public WindowsDragger EventWindowDrag { get; set; }
		
		public EventDoubleClick EventTitleMainDoubleClick { get; set; }

        public TitleMainRightClick TitleMainRightClick { get; set; }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

		private void TitleMainDoubleClick(EventDoubleClick sender)
		{
			if (WindowState == System.Windows.WindowState.Maximized)
				WindowState = System.Windows.WindowState.Normal;
			else
				WindowState = System.Windows.WindowState.Maximized;
		}

        private void TitleMain_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TitleMainRightClick == null)
                TitleMainRightClick = new TitleMainRightClick(this);
            var location1 = e.GetPosition(TitleMain);
            var location2 = TitleMain.PointToScreen(new Point(0, 0));
            TitleMainRightClick.Left = location1.X + location2.X - 2;
            TitleMainRightClick.Top = location1.Y + location2.Y - 2;
            if (!TitleMainRightClick.IsVisible)
                TitleMainRightClick._Show();
            TitleMainRightClick.Focus();
        }


        private void Minisize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnMaxMin_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                Manager.Manager.Current.Data.MainWindow_MaxMin = Manager.Manager.Current.Icon.WindowState_Normal;
            }
            else
            {
                WindowState = System.Windows.WindowState.Normal;
                Manager.Manager.Current.Data.MainWindow_MaxMin = Manager.Manager.Current.Icon.WindowState_Maximize;
            }
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			Close();
        }
    }
}
