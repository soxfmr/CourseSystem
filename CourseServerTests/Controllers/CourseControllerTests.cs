using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseServer.Controllers.Advance;

namespace CourseServer.Controllers.Tests
{
    [TestClass()]
    public class CourseControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);
            CourseController c = new CourseController();

            string ret = c.Index();
        }
    }
}