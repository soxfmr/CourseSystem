using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Controllers;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class RouteTests
    {
        [TestMethod()]
        public void getRouteListTest()
        {

        }

        [TestMethod()]
        public void AddTest()
        {
            //Route.Add("/index", "TestController@DoSomething", "name, age", true);
            //List<RouteInfo> routeInfoList = Route.getRouteList();

            //Assert.AreEqual(1, routeInfoList.Count);

            //Assert.AreEqual("/index", routeInfoList[0].Route);

            //Assert.AreEqual(typeof(TestController), routeInfoList[0].Handler.Handler);

            //Assert.AreEqual("age", routeInfoList[0].Params[1]);

            //Assert.IsTrue(routeInfoList[0].Cache);
        }

        [TestMethod()]
        public void AddTest1()
        {

        }

        [TestMethod()]
        public void AddTest2()
        {

        }

        [TestMethod()]
        public void AddTest3()
        {

        }

        [TestMethod()]
        public void ParseTest()
        {
            //RouteInfo info = Route.Parse("/index", "CourseServer.Controllers.TestController@DoSomething", null, false);

            //Assert.IsTrue(info.Instance is TestController);
        }
    }
}