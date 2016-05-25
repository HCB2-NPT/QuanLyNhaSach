using QuanLyNhaSach.Objects;
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
    /// Interaction logic for tabTaoTaiKhoanMoi.xaml
    /// </summary>
    public partial class tabTaoTaiKhoanMoi : UserControl
    {
        #region Properties

        private Account _account;

        public Account Account
        {
            get { return _account; }
            set
            {
                _account = value;
            }
        }

        #endregion

        #region Constructor
        
        public tabTaoTaiKhoanMoi()
        {
            InitializeComponent();
            listRoles.ItemsSource = Bus.FillData.GetAllRoles().Where(role => role.ID != 3);
        }

        #endregion

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {

        }
    }
}
