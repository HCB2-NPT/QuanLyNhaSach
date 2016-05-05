using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.MDI;

namespace QuanLyNhaSach.Windows
{
    public partial class MainWindow
    {
		private void Luna_Click(object sender, RoutedEventArgs e)
		{
			Luna.IsChecked = true;
			Aero.IsChecked = false;
            Metro.IsChecked = false;

			mdiContainer.Theme = ThemeType.Luna;
		}

        private void Aero_Click(object sender, RoutedEventArgs e)
        {
            Luna.IsChecked = false;
            Aero.IsChecked = true;
            Metro.IsChecked = false;

            mdiContainer.Theme = ThemeType.Aero;
        }

        private void Metro_Click(object sender, RoutedEventArgs e)
        {
            Luna.IsChecked = false;
            Aero.IsChecked = false;
            Metro.IsChecked = true;

            mdiContainer.Theme = ThemeType.Metro;
        }

        private void mdiPlus_closeThis_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in mdiContainer.Children)
            {
                if (item.IsFocused)
                {
                    mdiContainer.Children.Remove(item);
                    break;
                }
            }
        }

        private void mdiPlus_closeAllButThis_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in mdiContainer.Children)
            {
                if (!item.IsFocused)
                {
                    mdiContainer.Children.Remove(item);
                }
            }
        }

        private void mdiPlus_closeAll_Click(object sender, RoutedEventArgs e)
        {
            mdiContainer.Children.Clear();
        }
    }
}
