using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Controllers;
using CourseServer.Middlewares;
using CourseServer.Builders;
using System.Net;
using CourseServer.Events;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class DispatcherImplTests
    {
        [TestMethod()]
        public void setCacheDriverTest()
        {

        }

        [TestMethod()]
        public void DispatcherImplTest()
        {

        }

        [TestMethod()]
        public void HandleTest()
        {
            /*Route.Add("/test", "CourseServer.Controllers.TestController@DoSomething", "name,age", false);
            DispatcherImpl dispatcher = new DispatcherImpl();

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:2333/");
            listener.Start();

            HttpListenerContext context = listener.GetContext();
            dispatcher.Handle(this, new HandleEventArgs(context));*/
        }

        [TestMethod()]
        public void DispatchTest()
        {
            /*Type type = typeof(TestController);
            TestController c = new TestController();

            RouteHandlerInfo handlerInfo = new RouteHandlerInfo(type, type.GetMethod("DoSomething"),
                type.GetMethod("DoSomething").GetParameters());
            RouteInfo info = new RouteInfo("/test", c, new string[] { "name", "age" }, true, handlerInfo);

            JSONRouteBuilder builder = new JSONRouteBuilder("{'_route' : '/test', '_param' : " +
               "{ 'name' : 'username', 'age' : 23 }}");

            RouteDispatchInfo dispatchInfo = builder.buildDispatchInfo();

            DispatcherImpl dispatcher = new DispatcherImpl();
            dispatcher.setCacheDriver(new FileSystemCacheDriver());

            Assert.AreEqual("username:23", dispatcher.Dispatch(info, dispatchInfo));*/
        }

        [TestMethod()]
        public void requireMiddlewareTest()
        {
            /*Type type = typeof(TestController);
            TestController c = new TestController();

            RouteHandlerInfo handlerInfo = new RouteHandlerInfo(type, type.GetMethod("DoSomething"),
                type.GetMethod("DoSomething").GetParameters());
            RouteInfo info = new RouteInfo("/test", c, false, handlerInfo);

            MiddlewareRegister.Add("auth", new AuthMiddleware());
            MiddlewareRegister.Register(c, "auth", "DoSomething");

            RouteDispatchInfo dispatchInfo = new RouteDispatchInfo();
            dispatchInfo.Route = "/test";

            string ret = string.Empty;
            DispatcherImpl dispatcher = new DispatcherImpl();

            Assert.IsTrue(dispatcher.requireMiddleware(info, dispatchInfo, ref ret));*/

            // Assert.AreEqual("failed", ret);
        }

        [TestMethod()]
        public void requireCacheDriverTest()
        {
        }

        [TestMethod()]
        public void redirectHandleTest()
        {
            /*Type type = typeof(TestController);
            RouteHandlerInfo handlerInfo = new RouteHandlerInfo(type, type.GetMethod("DoSomething"), 
                type.GetMethod("DoSomething").GetParameters());

            RouteInfo info = new RouteInfo("/test", new TestController(), false, handlerInfo);

            DispatcherImpl dispatcher = new DispatcherImpl();
            string ret = dispatcher.redirectHandle(info, new object[] { "John",
                Convert.ChangeType(23, typeof(uint)) });

            Assert.AreEqual("John:23", ret);*/
        }
    }
}