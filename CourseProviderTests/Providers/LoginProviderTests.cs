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
    public class LoginProviderTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            LoginProvider lp = new LoginProvider();
            lp.LoginEvent += callback;
            lp.Login("john@gmail.com", "password", 1);
        }

        public void callback(object sender, LoginEventArgs e)
        {

        }
    }
}