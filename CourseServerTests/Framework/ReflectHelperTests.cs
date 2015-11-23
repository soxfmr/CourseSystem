using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Controllers;
using System.Reflection;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class ReflectHelperTests
    {
        [TestMethod()]
        public void setGlobalNamespaceTest()
        {

        }

        [TestMethod()]
        public void setInsecureReflectTest()
        {

        }

        [TestMethod()]
        public void GetRouteHandlerTest()
        {
            ReflectHelper reflectHelper = new ReflectHelper();
            reflectHelper.setGlobalNamespace("CourseServer.Controllers");
            RouteHandlerInfo handlerInfo = reflectHelper.GetRouteHandler("CourseController@Index");

            Assert.AreEqual(typeof(CourseController), handlerInfo.Handler);

            Assert.AreEqual(0, handlerInfo.ParamInfo.Length);

            //Assert.AreEqual("DoSomething", handlerInfo.Callback.Name);
        }

        [TestMethod()]
        public void GetClassTypeTest()
        {
            //ReflectHelper reflectHelper = new ReflectHelper();
            //reflectHelper.setGlobalNamespace("CourseServer.Controllers");
            //Type t = reflectHelper.GetClassType("TestController");

            //Assert.AreEqual(typeof(TestController), t);
        }

        [TestMethod()]
        public void GetMethodInfoTest()
        {
            //ReflectHelper reflectHelper = new ReflectHelper();
            //reflectHelper.setGlobalNamespace("CourseServer.Controllers");
            //Type t = reflectHelper.GetClassType("TestController");
            //MethodInfo info = reflectHelper.GetMethodInfo(t, "DoSomething");

            //Assert.AreEqual("DoSomething", info.Name);
        }

        [TestMethod()]
        public void GetFieldInfoTest()
        {

        }
    }
}