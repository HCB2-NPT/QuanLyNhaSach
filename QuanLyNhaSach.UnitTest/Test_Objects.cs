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
    public class Test_Objects
    {
        [Test]
        [Category("HighLevel Objects")]
        public void Editable()
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
        [Category("LowLevel Objects")]
        public void AccessLevel()
        {
            var o = new AccessLevel(0, "tester");
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.Name, "tester");
        }

        [Test]
        [Category("LowLevel Objects")]
        public void NewAccount()
        {
            var o = new Account()
            {
                Name = "tester",
                Password = "test",
                Email = "123@abc.com",
                AccessLevel = null
            };
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.Name, "tester");
            Assert.AreEqual(o.Password, "test");
            Assert.AreEqual(o.Email, "123@abc.com");
            Assert.AreEqual(o.AccessLevel, null);

            Assert.AreEqual(o.IsCreatedItem, true);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void AvailableAccount()
        {
            var o = new Account(1)
            {
                Name = "tester",
                Password = "test",
                Email = "123@abc.com",
                AccessLevel = null
            };
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.Name, "tester");
            Assert.AreEqual(o.Password, "test");
            Assert.AreEqual(o.Email, "123@abc.com");
            Assert.AreEqual(o.AccessLevel, null);

            Assert.AreEqual(o.IsCreatedItem, false);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void NewAuthor()
        {
            var o = new Author()
            {
                Name = "test"
            };
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.Name, "test");

            Assert.AreEqual(o.IsCreatedItem, true);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void AvailableAuthor()
        {
            var o = new Author(1)
            {
                Name = "test"
            };
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.Name, "test");

            Assert.AreEqual(o.IsCreatedItem, false);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void NewGenre()
        {
            var o = new Genre()
            {
                Name = "test"
            };
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.Name, "test");

            Assert.AreEqual(o.IsCreatedItem, true);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void AvailableGenre()
        {
            var o = new Genre(1)
            {
                Name = "test"
            };
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.Name, "test");

            Assert.AreEqual(o.IsCreatedItem, false);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        //==========
    }
}
