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
    public class DumperTests
    {
        [TestMethod()]
        public void LogTest()
        {
            Dumper.Log("Main", "The test program has been started!");
        }
    }
}