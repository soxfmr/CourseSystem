using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Controllers;
using System.Reflection;
using CourseServer.Controllers.Advance;
using CourseServerTests.Controllers.Deepth;

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
            reflectHelper.setInsecureReflect(true);
            RouteHandlerInfo handlerInfo = reflectHelper.GetRouteHandler("Advance.UserManagerController@AllUser");

            Assert.AreEqual(typeof(UserManagerController), handlerInfo.Handler);

            Assert.AreEqual(1, handlerInfo.ParamInfo.Length);

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

        [TestMethod()]
        public void GetClassTypeTest1()
        {
            ReflectHelper reflectHelper = new ReflectHelper();
            reflectHelper.setGlobalNamespace("CourseServer.Controllers");
            Type t = reflectHelper.GetClassType("TestController");

            Assert.IsNull(t);
        }
    }
}