using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Utils.Tests
{
    [TestClass()]
    public class GuardTests
    {
        [TestMethod()]
        public void GenerateRandomPasswordTest()
        {
            Assert.AreEqual("a", Guard.GenerateRandomPassword());
        }
    }
}