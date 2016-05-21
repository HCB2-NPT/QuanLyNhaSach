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
    /// Interaction logic for tabQuyDinh.xaml
    /// </summary>
    public partial class tabQuyDinh : UserControl
    {
        public tabQuyDinh()
        {
            InitializeComponent();
            DataContext = Managers.Manager.Current;
        }

        private void numberOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void btn_saveRules_Click(object sender, RoutedEventArgs e)
        {
            Bus.SaveChanges.SaveNewRules(Managers.RuleManager.Current.Rule);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Managers.RuleManager.Current.Rule.AllowGetMoneyGreaterDebt = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Managers.RuleManager.Current.Rule.AllowGetMoneyGreaterDebt = true;
        }
    }
}
