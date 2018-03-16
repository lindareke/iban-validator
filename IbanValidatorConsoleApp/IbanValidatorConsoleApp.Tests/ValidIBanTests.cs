using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IbanValidatorConsoleApp;

namespace IbanValidatorConsoleApp.Tests
{
    [TestClass]
    public class ValidIBanTests
    {
        [TestMethod]
        public void TestIban1_NOK()
        {
            string iban = "AA051245445454552117989";
            Assert.IsFalse(IbanValidator.IsValidIban(iban));
        }

        [TestMethod]
        public void TestIban2_OK()
        {
            string iban = "LT647044001231465456";
            Assert.IsTrue(IbanValidator.IsValidIban(iban));
        }

        [TestMethod]
        public void TestIban3_OK()
        {
            string iban = "LT517044077788877777";
            Assert.IsTrue(IbanValidator.IsValidIban(iban));
        }

        [TestMethod]
        public void TestIban4_NOK()
        {
            string iban = "LT227044077788877777";
            Assert.IsFalse(IbanValidator.IsValidIban(iban));
        }

        [TestMethod]
        public void TestIban5_NOK()
        {
            string iban = "CC051245445454552117989";
            Assert.IsFalse(IbanValidator.IsValidIban(iban));
        }
    }
}
