using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;

namespace CourseServer.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void UpdateTest()
        {
        }

        [TestMethod()]
        public void CreateDispatchTest()
        {
        }

        [TestMethod()]
        public void RemoveDispatchTest()
        {

        }

        [TestMethod()]
        public void RemoveDispatchTest1()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);
            UserController uc = new UserController();
            
            Assert.AreEqual(true, uc.RemoveDispatch("1,2"));
        }
    }
}