using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Middlewares;

namespace CourseServer.Framework.Tests
{
    [TestClass()]
    public class MiddlewareRegisterTests
    {
        [TestMethod()]
        public void registerTest()
        {
            Object obj = new object();
            AuthMiddleware auth = new AuthMiddleware();
            MiddlewareRegister.Add("auth", auth);

            MiddlewareRegister.Register(GetType(), "auth", "test1");

            Assert.IsTrue(auth.isProtected(obj.GetType().FullName, "test1"));
        }

        [TestMethod()]
        public void AddTest()
        {
            MiddlewareRegister.Add("auth", new AuthMiddleware());
            Dictionary<string, Middleware> middlewares = MiddlewareRegister.GetAllMiddleware();

            Assert.AreEqual(typeof(AuthMiddleware), middlewares["auth"].GetType());
        }

        [TestMethod()]
        public void GetAllReferenceMiddlewareTest()
        {
            Object obj = new object();
            MiddlewareRegister.Add("auth", new AuthMiddleware());

            // MiddlewareRegister.Register(obj, "auth", "test1");

            // List<Middleware> middlewares = MiddlewareRegister.GetAllReferenceMiddleware(obj, "test1");

            /*Assert.AreEqual(1, middlewares.Count);

            Assert.AreEqual(true, middlewares[0].isProtected(obj.GetType().FullName, "test1"));

            Assert.AreEqual(typeof(AuthMiddleware), middlewares[0].GetType());*/
        }

        [TestMethod()]
        public void GetAliasTest()
        {
            AuthMiddleware auth = new AuthMiddleware();
            MiddlewareRegister.Add("auth", auth);

            Assert.AreEqual("auth", MiddlewareRegister.GetAlias(auth));
        }
    }
}