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
    /// Interaction logic for WarningBox.xaml
    /// </summary>
    public partial class WarningBox : Window
    {
        public string Title { get { return tb_title.Text; } set { tb_title.Text = value; } }

        public string Name { get { return tb_name.Text; } set { tb_name.Text = value; } }

        public string Content { get { return tb_content.Text; } set { tb_content.Text = value; } }

        public bool IsCrash { get; set; }
		
		public WindowsDragger EventWindowDrag { get; set; }

        public WarningBox(string title, string name, string content, bool isCrash = false)
        {
            InitializeComponent();
            EventWindowDrag = new WindowsDragger(this, menu);
            DataContext = Manager.Manager.Current;
			
            Title = title;
            Name = name;
            Content = content;
            IsCrash = isCrash;
        }

        public static void Show(string title, string name, string content, bool isCrash = false)
        {
            var b = new WarningBox(title, name, content, isCrash);
            b.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsCrash)
                Application.Current.Shutdown();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
