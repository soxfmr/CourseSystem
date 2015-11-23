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
    public class ProviderCarrierTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            ProviderCarrier carrier = new ProviderCarrier();
            carrier.Route = "/login";
            carrier.ParamList.Add("email", "test@email.com");
            carrier.GenericList.Add("other", "another thing here.");
            carrier.GenericList.Add("money", 3);

            carrier.ToString();
        }
    }
}