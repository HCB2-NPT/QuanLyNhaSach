using NUnit.Framework;
using QuanLyNhaSach.Adapters;
using QuanLyNhaSach.Assets.Extensions;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.UnitTest
{
    [TestFixture]
    public class AccountTesting
    {
        [Test]
        public void Account_TestInsertNewAccount()
        {
            //using (var scope = new System.Transactions.TransactionScope())
            //{
                Account account = new Account();
                Security crypto = new Security();

                account.Email = "test1@gmail.com";
                account.Name = "Harry James";
                account.Password = crypto.encodeMD5(crypto.encodeSHA1(account.Email.Split('@')[0]));
                account.AccessLevel.ID = 1;

                bool result = (AccountAdapter.InsertAccount(account) > -1) ? true : false;
                Assert.AreEqual(true, result);

                //scope.Complete();
            //}
        }
    }
}
