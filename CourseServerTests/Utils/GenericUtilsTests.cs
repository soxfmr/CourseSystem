using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CourseServer.Utils.Tests
{
    [TestClass()]
    public class GenericUtilsTests
    {
        [TestMethod()]
        public void GetUniqueStringTest()
        {
            // Assert.AreEqual(GenericUtils.GetUniqueString(), GenericUtils.GetUniqueString());
        }

        [TestMethod()]
        public void GetHashTest()
        {
            Assert.AreEqual("098F6BCD4621D373CADE4E832627B4F6", GenericUtils.GetHash("test"));
        }

        [TestMethod()]
        public void GetTimestampTest()
        {
            // Assert.AreEqual("", GenericUtils.GetTimestamp());
        }
    }
}