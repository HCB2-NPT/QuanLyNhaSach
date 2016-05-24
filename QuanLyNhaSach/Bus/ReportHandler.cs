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

                data.ReportDocumentValues.Add("PrintDate", DateTime.Now);

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

        public static void CreateNumberReport(DocumentViewer documentViewer, ObservableCollection<Book> Data)
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();

                StreamReader reader = new StreamReader(new FileStream(@"..\..\Assets\ReportTemplates\NumberReport\NumberReport.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"..\..\Assets\ReportTemplates\NumberReport\");
                reader.Close();

                ReportData data = new ReportData();

                data.ReportDocumentValues.Add("PrintDate", DateTime.Now);

                DataTable table = new DataTable("NumberReport");
                table.Columns.Add("Id", typeof(string));
                table.Columns.Add("ItemInfo", typeof(string));
                table.Columns.Add("IDCode", typeof(string));
                table.Columns.Add("Number", typeof(string));
                foreach (var item in Data)
                {
                    table.Rows.Add(new object[] { item.ID, string.Format("Tên sách: {0}\nTác giả: {1}\nThể loại: {2}", item.Name, item.AuthorsShortFormat, item.GenresShortFormat), string.Format("9001{0:00000000}", item.ID), string.Format("Tháng này: {0} quyển\nTháng trước: {1} quyển", item.Number, item.Tag) });
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
