using QuanLyNhaSach.Objects;
using QuanLyNhaSach.Views.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for tabBaoCaoCongNo.xaml
    /// </summary>
    public partial class tabBaoCaoCongNo : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<Customer> _data;
        public ObservableCollection<Customer> Data
        {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged("Data"); }
        }

        private int Month, Year;

        public tabBaoCaoCongNo()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Implements
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void TextBox_NumberOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) == -1;
        }

        private void getData(object sender, RoutedEventArgs e)
        {
            var c = month.SelectedItem as ComboBoxItem;
            if (c != null)
            {
                if (int.TryParse((string)c.Tag, out Month) && int.TryParse(year.Text, out Year))
                {
                    if (Year < DateTime.Now.Year || (Year == DateTime.Now.Year && Month < DateTime.Now.Month))
                    {
                        Data = Bus.SearchData.GetDebtorReportData(Month, Year);
                        btnReport.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        Data = null;
                        WarningBox.Show("Thông báo...", "Thời gian lập báo cáo không hợp lệ", "Thời gian lập báo cáo\nnên trước thời gian hiện tại ít nhất 1 tháng!");
                    }
                }
            }
        }

        private void preview(object sender, RoutedEventArgs e)
        {
            reportContainer.Visibility = System.Windows.Visibility.Visible;
            Bus.ReportHandler.CreateDebtorReport(documentViewer, Data);
            Bus.InsertData.InsertNewDebtorReport(Data, Month, Year);
        }

        private void reportBack(object sender, RoutedEventArgs e)
        {
            reportContainer.Visibility = System.Windows.Visibility.Hidden;
            btnReport.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
