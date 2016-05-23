using NUnit.Framework;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.UnitTest
{
    [TestFixture]
    public class TestObjects
    {
        [Test]
        [Category("HighLevel Objects")]
        public void Editable_TestProperties()
        {
            Editable o;

            //test states: available object
            o = new Editable();
            Assert.AreEqual(o.IsCreatedItem, false);
            Assert.AreEqual(o.IsDeletedItem, false);
            Assert.AreEqual(o.IsEditedItem, false);

            //test states: new object
            o = new Editable(true);
            Assert.AreEqual(o.IsCreatedItem, true);
            Assert.AreEqual(o.IsDeletedItem, false);
            Assert.AreEqual(o.IsEditedItem, false);

            //test custom properties: tag, switch, notswitch (not affect the states)
            o = new Editable();
            Assert.AreEqual(o.Tag, null);
            Assert.AreEqual(o.Switch, false);
            Assert.AreEqual(o.NotSwitch, true);

            o.Tag = 0;
            o.Switch = true;
            Assert.AreEqual(o.Tag, 0);
            Assert.AreEqual(o.Switch, true);
            Assert.AreEqual(o.NotSwitch, false);

            o.NotSwitch = true;
            Assert.AreEqual(o.Switch, false);
            Assert.AreEqual(o.NotSwitch, true);

            //is affect the states????
            Assert.AreEqual(o.IsCreatedItem, false);
            Assert.AreEqual(o.IsDeletedItem, false);
            Assert.AreEqual(o.IsEditedItem, false);
        }

        [Test]
        [Category("HighLevel Objects")]
        public void Editable_TestAutoChangeState()
        {
            var o = new Account();
            Assert.AreEqual(o.IsEditedItem, false);
            o.Name = "anything";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Book_TestFormattedProperties()
        {
            var o = new Book();

            o.Authors.Add(new Author() { Name = "A" });
            o.Authors.Add(new Author() { Name = "B" });
            o.Authors.Add(new Author() { Name = "C" });
            Assert.AreEqual(o.AuthorsFormat, "A, B, C");
            o.Authors.Add(new Author() { Name = "12345678900987654321_12345678900987654321" });
            Assert.AreEqual(o.AuthorsShortFormat, "A, B, C, 12345678900987654321_...");

            o.Genres.Add(new Genre() { Name = "a" });
            o.Genres.Add(new Genre() { Name = "b" });
            o.Genres.Add(new Genre() { Name = "c" });
            Assert.AreEqual(o.GenresFormat, "a, b, c");
            o.Genres.Add(new Genre() { Name = "12345678900987654321_12345678900987654321" });
            Assert.AreEqual(o.GenresShortFormat, "a, b, c, 12345678900987654321_1234567890...");

            o.Price = 1000000;
            Assert.AreEqual(o.PriceFormat, "1,000,000 vnđ");
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Customer_TestFormattedProperties()
        {
            var o = new Customer();
            o.Adress = "(12345678900987654321_12345678900987654321)(12345678900987654321_12345678900987654321)";
            Assert.AreEqual(o.AdressShortFormat, "(12345678900987654321_12345678...");

            o.Debt = 1000000;
            Assert.AreEqual(o.DebtFormat, "1,000,000 vnđ");

            o.Phone = "01203875665"; //my phone
            Assert.AreEqual(o.PhoneByInteger, 1203875665);
            Assert.AreEqual(o.PhoneFormat, "(+84) 0120 3875 665");
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Bill_TestFormattedProperties()
        {
            var o = new Bill();
            o.BillItems.Add(new BillItem() { Container = o, Number = 2, Price = 40000 });
            Assert.AreEqual(o.TotalMoney, 80000);
            Assert.AreEqual(o.TotalMoneyFormat, "80,000 vnđ");

            o.PayMoney = 100000;
            Assert.AreEqual(o.PayMoneyFormat, "100,000 vnđ");

            Assert.AreEqual(o.ReturnMoney, 20000);
            Assert.AreEqual(o.ReturnMoneyFormat, "20,000 vnđ");
        }

        [Test]
        [Category("LowLevel Objects")]
        public void BillItem_TestFormattedProperties()
        {
            var o = new BillItem();
            o.Number = 2;
            o.Price = 1000000;

            Assert.AreEqual(o.PriceFormat, "1,000,000 vnđ");

            Assert.AreEqual(o.Total, 2000000);
            Assert.AreEqual(o.TotalFormat, "2,000,000 vnđ");
        }
    }
}
