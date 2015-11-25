using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProvider.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers.Tests
{
    [TestClass()]
    public class DispatchCourseProviderTests
    {
        [TestMethod()]
        public void QuitCourseTest()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ab,");

            Assert.AreEqual("ab", sb.ToString().Substring(0, sb.ToString().Length - 1));
        }
    }
}