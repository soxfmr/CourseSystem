using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Tests
{
    [TestClass()]
    public class ProviderParserTests
    {
        [TestMethod()]
        public void ProviderParserTest()
        {
            ProviderParser Parser = new ProviderParser("{'_err':0, '_auth':'USER_SESSION_KEY', '_payload':[{'p1':'pv1'}]}");

            Assert.AreEqual(true, Parser.IsSuccess);
            Assert.AreEqual("USER_SESSION_KEY", Parser.GetSessionId());
        }

        [TestMethod()]
        public void SerializeTest()
        {
            ProviderParser Parser = new ProviderParser("{'_err':0, '_payload':[{'name':'jay', 'age':20}]}");

            Assert.AreEqual(20, Parser.Serialize<A>().Age);
        }
    }

    public class A
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}