using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;

namespace CourseServer.Builders.Tests
{
    [TestClass()]
    public class JSONRouteBuilderTests
    {
        [TestMethod()]
        public void JSONRouteBuilderTest()
        {

        }

        [TestMethod()]
        public void buildDispatchInfoTest()
        {
            JSONRouteBuilder builder = new JSONRouteBuilder("{'_route' : 'user/login', '_param' : " + 
                "{ 'name' : 'username', 'pass' : 'password' }, " + 
                "'_generic' : { 'auth' : 'someSecretKey', 'other' : 'otherValue' }}");

            RouteDispatchInfo info = builder.buildDispatchInfo();

            Assert.AreEqual("user/login", info.Route);

            Assert.AreEqual("username", info.Params["name"]);

            Assert.AreEqual("someSecretKey", info.GenericPairs["auth"]);
        }
    }
}