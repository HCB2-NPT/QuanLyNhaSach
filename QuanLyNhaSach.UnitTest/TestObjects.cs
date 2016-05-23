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
        public void Account()
        {
            Account o;

            o = new Account();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Account(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Author()
        {
            Author o;

            o = new Author();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Author(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        [Test]
        [Category("LowLevel Objects")]
        public void Genre()
        {
            Genre o;

            o = new Genre();
            Assert.AreEqual(o.ID, 0);
            Assert.AreEqual(o.IsCreatedItem, true);

            o = new Genre(1);
            Assert.AreEqual(o.ID, 1);
            Assert.AreEqual(o.IsCreatedItem, false);

            o.Name = "tester2";
            Assert.AreEqual(o.IsEditedItem, true);
        }

        //==========
    }
}
