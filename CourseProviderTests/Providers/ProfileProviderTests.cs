using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProvider.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProvider.Events;

namespace CourseProvider.Providers.Tests
{
    [TestClass()]
    public class ProfileProviderTests
    {
        [TestMethod()]
        public void GetProfileTest()
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/update" };
            carrier.AddAuth("B79DA991925500E2B7E26858AFBB6511");

            carrier.ParamList.Add("avatar", "a");
            carrier.ParamList.Add("name", "John David");
            carrier.ParamList.Add("cellphone", "233-233333");
            carrier.ParamList.Add("newPwd", "");
            carrier.ParamList.Add("pwdConfirm", "");
            carrier.ParamList.Add("originPwd", "password");

            ProviderBridge bridge = new ProviderBridge();
            bridge._Connect(0, carrier);
        }

        public void UserProfileEvent(object sender, ProfileEventArgs e)
        {
            Console.WriteLine(e.Payload);
        }
    }
}