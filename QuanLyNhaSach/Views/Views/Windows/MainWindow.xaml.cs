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
using QuanLyNhaSach.Assets.Scripts;

namespace QuanLyNhaSach.Views.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region EventHandler
        private WindowsDragger EventWindowDrag { get; set; }

        private WindowsResizer EventWindowResize { get; set; }

        private EventDoubleClick EventTitleMainDoubleClick { get; set; }

        private EventClickOut EventPanelFunctionClickOut { get; set; }
        #endregion

        #region Extensions
        private TitleMainRightClick TitleMainRightClick;

        private ToolBox ToolBox;

        private About About;
        #endregion

        #region Binding Controlers
        public Visibility ShowMdiContainer
        {
            get
            {
                if (mdiContainer == null)
                    return Visibility.Collapsed;
                if (mdiContainer.Children == null)
                    return Visibility.Collapsed;
                if (mdiContainer.Children.Count > 0)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            mdiContainer.Children.CollectionChanged += mdiContainer_ChildrenChanged;

            EventWindowDrag = new Assets.Scripts.WindowsDragger(this, TitleMain);
            EventWindowResize = new Assets.Scripts.WindowsResizer(this, resizer);
            EventTitleMainDoubleClick = new Assets.Scripts.EventDoubleClick(this, TitleMain, TitleMainDoubleClick);
            EventPanelFunctionClickOut = new Assets.Scripts.EventClickOut(this, DockChucNang, PanelFunctionClickOut);
            EventPanelFunctionClickOut.Without.Add(_menuFull);
            EventPanelFunctionClickOut.Without.Add(_menuMini);
            EventPanelFunctionClickOut.Without.Add(TitleMain);

            MaxHeight = Assets.Scripts.WpfScreen.GetScreenFrom(this).WorkingArea.Height;
            
            DataContext = Managers.Manager.Current;
		}
        #endregion

        #region Implements
        /*
         * INotifyPropertyChanged
         */
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
