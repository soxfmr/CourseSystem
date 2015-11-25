using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProvider.Models;

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

        [TestMethod()]
        public void SerializeTest1()
        {
            ProviderParser Parser = new ProviderParser("{'_err':0, '_payload':[{'name':'john', 'age':22},{'name':'jay', 'age':20}]}");

            Assert.AreEqual(20, Parser.SerializeList<A>()[1].Age);
        }

        [TestMethod()]
        public void SerializeDictTest()
        {
            ProviderParser Parser = new ProviderParser("{\r\n  \"_err\": 0,\r\n  \"_payload\": {\r\n    \"电子系\": [\r\n      {\r\n        \"Name\": \"C#\",\r\n        \"Description\": \"Microsoft .Net\",\r\n        \"TeacherName\": \"sJohn David\",\r\n        \"Weekday\": 1,\r\n        \"At\": \"2015-11-21T03:42:40.783\",\r\n        \"Location\": \"Little place 404\",\r\n        \"Limit\": 10,\r\n        \"Current\": 2\r\n      }\r\n    ],\r\n    \"萌系\": [\r\n      {\r\n        \"Name\": \"反卖萌的研究\",\r\n        \"Description\": \"深入了解卖萌心里，卖萌比卖淫更可耻！\",\r\n        \"TeacherName\": \"sJohn David\",\r\n        \"Weekday\": 1,\r\n        \"At\": \"2015-11-24T19:31:15.007\",\r\n        \"Location\": \"Little place 302\",\r\n        \"Limit\": 10,\r\n        \"Current\": 2\r\n      }\r\n    ]\r\n  }\r\n}");

            Assert.AreEqual(2, Parser.SerializeDict<string, List<DispatchCourse>>().Count);
        }
    }

    public class A
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}