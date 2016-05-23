using CodeReason.Reports;
using QuanLyNhaSach.Objects;
using QuanLyNhaSach.Views.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;

namespace QuanLyNhaSach.Bus
{
    public class ReportHandler
    {
        public static void CreateDebtorReport(DocumentViewer documentViewer, ObservableCollection<Customer> Data)
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();

                StreamReader reader = new StreamReader(new FileStream(@"..\..\Assets\ReportTemplates\DebtorReport\DebtorReport.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Assets\ReportTemplates\DebtorReport\");
                reader.Close();

                ReportData data = new ReportData();

                DataTable table = new DataTable("DebtorReport");
                table.Columns.Add("ID", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Debt", typeof(string));
                table.Columns.Add("Phone", typeof(string));
                foreach (var item in Data)
                {
                    table.Rows.Add(new object[] { item.ID, item.Name, string.Format("Tháng này: {0}\nTháng trước: {1:#,###,##0} vnđ", item.DebtFormat, item.Tag), item.PhoneFormat });
                }
                data.DataTables.Add(table);

                XpsDocument xps = reportDocument.CreateXpsDocument(data);
                documentViewer.Document = xps.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                WarningBox.Show("Thông báo...", "Xãy ra sự cố!", "Có vấn đề khi lập báo cáo với dữ liệu được tra!", false, ex.Message);
            }
        }
    }
}
