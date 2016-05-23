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
        public void Editable_TestConstructor()
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
        public void AccessLevel_TestConstructor()
        {
            var o = new AccessLevel(0, "tester");
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.Name, "tester");
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Account_TestConstructor()
        {
            Account o;

            o = new Account();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Account(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Author_TestConstructor()
        {
            Author o;

            o = new Author();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Author(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Genre_TestConstructor()
        {
            Genre o;

            o = new Genre();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Genre(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Rule_TestConstructor()
        {
            Rule o;
            DateTime now = DateTime.Now;

            o = new Rule();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Rule(1, now);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.UpdateTime, now);
            Assert.AreEqual(o.IsCreatedItem, false);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Book_TestConstructor()
        {
            Book o;

            o = new Book();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Book(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);
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
            //o.Authors.Add(new Author() { Name = "12345678900987654321_12345678900987654321" });
            //Assert.AreEqual(o.AuthorsShortFormat, "A, B, C, 12345678900987654321_...");

            o.Genres.Add(new Genre() { Name = "a" });
            o.Genres.Add(new Genre() { Name = "b" });
            o.Genres.Add(new Genre() { Name = "c" });
            Assert.AreEqual(o.GenresFormat, "a, b, c");
            //o.Genres.Add(new Genre() { Name = "12345678900987654321_12345678900987654321" });
            //Assert.AreEqual(o.GenresShortFormat, "a, b, c, 12345678900987654321_1234567890...");
        }
    }
}
