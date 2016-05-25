using NUnit.Framework;
using QuanLyNhaSach.Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.UnitTest
{
    [TestFixture]
    public class SecurityTesting
    {
        private Security security;

        public SecurityTesting()
        {
            this.security = new Security();
        }

        [Test]
        public void Security_TestEncodeMD5()
        {
            string expectedString = "e0d00b9f337d357c6faa2f8ceae4a60d";
            string actualString = this.security.encodeMD5("cryptography");

            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void Security_TestEncodeMD5_WithSpace()
        {
            string expectedString = "d41d8cd98f00b204e9800998ecf8427e";
            string actualString = this.security.encodeMD5("");

            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void Security_TestEncodeSHA1()
        {
            string expectedString = "48c910b6614c4a0aa5851aa78571dd1e3c3a66ba";
            string actualString = this.security.encodeSHA1("cryptography");

            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void Security_TestEncodeSHA1_WithSpace()
        {
            string expectedString = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
            string actualString = this.security.encodeSHA1("");

            Assert.AreEqual(expectedString, actualString);
        }
    }
}
