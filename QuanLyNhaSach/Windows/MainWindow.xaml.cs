using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace QuanLyNhaSach.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Assets.Scripts.WindowsDragger EventWindowDrag { get; set; }

        private Assets.Scripts.EventDoubleClick EventTitleMainDoubleClick { get; set; }

        private Assets.Scripts.EventClickOut EventPanelFunctionClickOut { get; set; }

        private QuanLyNhaSach.Windows.Others.TitleMainRightClick TitleMainRightClick { get; set; }

        private QuanLyNhaSach.Windows.Others.ToolBox ToolBox { get; set; }

        private QuanLyNhaSach.Windows.Others.About About { get; set; }

        private string _MainWindow_MaxMin = Manager.CommonIconManager.Current.WindowState_Maximize;
        public string MainWindow_MaxMin { get { return _MainWindow_MaxMin; } set { _MainWindow_MaxMin = value; NotifyPropertyChanged("MainWindow_MaxMin"); } }

        public MainWindow()
        {
            InitializeComponent();
            
            EventWindowDrag = new Assets.Scripts.WindowsDragger(this, TitleMain);
            
            EventTitleMainDoubleClick = new Assets.Scripts.EventDoubleClick(this, TitleMain, TitleMainDoubleClick);
            
            EventPanelFunctionClickOut = new Assets.Scripts.EventClickOut(this, DockChucNang, PanelFunctionClickOut);
            EventPanelFunctionClickOut.Without.Add(_menuFull);
            EventPanelFunctionClickOut.Without.Add(_menuMini);
            EventPanelFunctionClickOut.Without.Add(TitleMain);

            MaxHeight = Assets.Scripts.WpfScreen.GetScreenFrom(this).WorkingArea.Height;
            
            DataContext = Manager.Manager.Current;
		}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
