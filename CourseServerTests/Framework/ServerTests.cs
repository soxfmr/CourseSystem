using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class ServerTests
    {
        [TestMethod()]
        public void ServerTest()
        {
            //Route.Add("/test", "CourseServer.Controllers.Controller@DoSomething", "name,age", true);

            //ServerInfo info = new ServerInfo();
            //info.Hostname = "localhost";
            //info.Port = 2333;
            //info.Scheme = Server.SCHEME_HTTP;

            //Server server = new Server(info);

            //DispatcherImpl dispatcher = new DispatcherImpl();
            //dispatcher.setCacheDriver(new FileSystemCacheDriver());
            //server.AddHandleListener(dispatcher.Handle);

            //server.Start();

            //Assert.IsTrue(server.isRunning());

            //server.Shutdown();
        }

        [TestMethod()]
        public void AddHandleListenerTest()
        {
            //ServerInfo info = new ServerInfo();
            //info.Hostname = "localhost";
            //info.Port = 2333;
            //info.Scheme = Server.SCHEME_HTTP;

            //Server server = new Server(info);
            //server.AddHandleListener(new DispatcherImpl().Handle);
            //server.Start();

            //Assert.IsTrue(server.isRunning());

            //server.Shutdown();

        }

        [TestMethod()]
        public void RemoveHandleListenerTest()
        {

        }

        [TestMethod()]
        public void isRunningTest()
        {

        }

        [TestMethod()]
        public void StartTest()
        {
        }

        [TestMethod()]
        public void ShutdownTest()
        {

        }
    }
}