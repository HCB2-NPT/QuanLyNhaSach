using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Bus
{
    public class SearchData
    {
        public static Rule FindLastRule()
        {
            return RuleAdapter.GetLastRules();
        }

        public static ObservableCollection<Book> SearchBook(ObservableCollection<Book> list, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return list;
            }
            else
            {
                try
                {
                    return list.Where(x =>
                    {
                        var data =
                            x.ID.ToString() +
                            (x.Name == null ? "" : x.Name.ToLower()) +
                            x.Number.ToString() +
                            x.Price.ToString() +
                            x.PriceFormat +
                            (x.AuthorsFormat == null ? "" : x.AuthorsFormat.ToLower()) +
                            (x.GenresFormat == null ? "" : x.GenresFormat.ToLower()) +
                            (x.IsDeletedItem ? "xóa" : "") +
                            (x.Image == null ? Managers.DataManager.Current.NO_IMAGE.ToLower() : x.Image.ToLower());
                        return data.Contains(key);
                    }
                        ).ToObservableCollection<Book>();
                }
                catch { }
            }
            return null;
        }

        public static ObservableCollection<Book> SearchBook(string key)
        {
            return SearchBook(Adapters.BookAdapter.GetAll(), key);
        }

        public static ObservableCollection<Customer> SearchCustomer(ObservableCollection<Customer> list, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return list;
            }
            else
            {
                try
                {
                    return list.Where(x =>
                    {
                        var data =
                            x.ID.ToString() +
                            x.Name.ToLower() +
                            x.Phone.ToLower() +
                            (x.Adress == null ? "" : x.Adress.ToLower()) +
                            (x.Email == null ? "" : x.Email.ToLower());
                        return data.Contains(key);
                    }
                        ).ToObservableCollection<Customer>();
                }
                catch { }
            }
            return null;
        }

        public static ObservableCollection<Customer> SearchCustomer(string key)
        {
            return SearchCustomer(Adapters.CustomerAdapter.GetAll(), key);
        }

        public static ObservableCollection<Account> SearchAccount(ObservableCollection<Account> list, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return list;
            }
            else
            {
                try
                {
                    return list.Where(x =>
                        {
                            var data =
                                x.ID.ToString() +
                                x.Name.ToLower() +
                                x.Email.ToLower() +
                                x.AccessLevel.Name.ToLower();
                            return data.Contains(key);
                        }
                        ).ToObservableCollection<Account>();
                }
                catch { }
            }
            return null;
        }

        public static ObservableCollection<Account> SearchAccount(string key)
        {
            return SearchAccount(Adapters.AccountAdapter.GetAll(true), key);
        }

        public static Book GetBookById(int id)
        {
            return Adapters.BookAdapter.GetBook(id);
        }

        public static ObservableCollection<Customer> GetDebtor(int month, int year, bool K = true)
        {
            var exist = Adapters.ReportAdapter.GetDebtorReportData(month, year);
            if (exist == null)
            {
                var customers = Adapters.CustomerAdapter.GetAll();
                var bills = Adapters.BillAdapter.GetOldBillsOfDebtors();
                if (customers != null && bills != null)
                {
                    var time = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    foreach (var c in customers)
                    {
                        c.Debt = 0;
                    }
                    foreach (var b in bills)
                    {
                        if (b.PayMoney < 0)
                        {
                            var k = DateTime.Compare(time, b.DateCreated);
                            if (k == 0 || k > 0)
                            {
                                var c = customers.FirstOrDefault(x => x.ID == b.Customer.ID);
                                if (c != null)
                                    c.Debt += Math.Abs(b.PayMoney);
                            }
                        }
                    }
                    var result = customers.Where(x => x.Debt > 0).ToObservableCollection<Customer>();
                    //=====
                    if (K)
                    {
                        month--;
                        if (month <= 0)
                        {
                            year--;
                            month = 12;
                        }
                        var olds = GetDebtor(month, year, false);

                        foreach (var c in result)
                        {
                            var k = olds.FirstOrDefault(x => x.ID == c.ID);
                            if (k != null)
                                c.Tag = k.Debt;
                            else
                                c.Tag = 0;
                        }
                    }
                    //=====
                    return result;
                }
                return null;
            }
            else
            {
                return exist;
            }
        }
    }
}
