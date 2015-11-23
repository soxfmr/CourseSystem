using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CourseProvider.Tests
{
    [TestClass()]
    public class JSONTest
    {
        [TestMethod()]
        public void ConvertTest()
        {
            JObject o = JObject.Parse("{'err':0,'payload':{'name':'John'}}");
            int i = o["err"].Value<int>();

            Assert.AreEqual(0, i);
        }
    }
}