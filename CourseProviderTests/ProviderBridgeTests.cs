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
    public class ProviderBridgeTests
    {
        [TestMethod()]
        public void _ConnectTest()
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/login" };
            carrier.ParamList.Add("email", "john@gmail.com");
            carrier.ParamList.Add("pass", "password");
            carrier.ParamList.Add("mode", 1);

            ProviderBridge bridge = new ProviderBridge();
            bridge.Connect(0, carrier);
        }
    }
}