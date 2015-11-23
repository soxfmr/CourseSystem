using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class ValidatorTests
    {
        [TestMethod()]
        public void ValidatorTest()
        {

        }

        [TestMethod()]
        public void MakeTest()
        {
            Validator validator = new Validator();

            Assert.IsFalse(validator.Make(new string[] { "soxfmrsgmail.com" },
                new string[] { "email" }, new string[] { "email" }));

            Assert.AreEqual("Invaid format for email email.", validator.ErrorDetail);
        }

        [TestMethod()]
        public void MatchRuleTest()
        {
            Validator validator = new Validator();

            Assert.IsFalse(
            validator.MatchRule("soxfmrsgmail.com", "email", "email"));
        }

        [TestMethod()]
        public void GetDetailTest()
        {

        }

        [TestMethod()]
        public void MakeTest1()
        {
            Validator validator = new Validator();

            Assert.IsTrue(validator.Make(new string[] { "name", "password", "a", "a" },
                new string[] { "required", "required", "match:newPassword_confirmation", "" },
                new string[] { "username", "password", "newPassword", "newPassword_confirmation" }));
        }
    }
}