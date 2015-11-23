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
    public class HttpListenerRequestHandlerTests
    {
        [TestMethod()]
        public void ParseAddressTest()
        {
            //HttpListenerRequestHandler handler = new HttpListenerRequestHandler(null);

            //Assert.AreEqual("[::]", handler.ParseAddress("[::]:23333")[0]);
            //Assert.AreEqual("23333", handler.ParseAddress("[::]:23333")[1]);
            //Assert.AreEqual("127.0.0.1", handler.ParseAddress("127.0.0.1:23333")[0]);
        }
    }
}