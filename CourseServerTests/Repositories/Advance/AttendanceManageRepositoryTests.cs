using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories.Advance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;

namespace CourseServer.Repositories.Advance.Tests
{
    [TestClass()]
    public class AttendanceManageRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceManageRepository repo = new AttendanceManageRepository();
            Assert.AreEqual(4, repo.GetAll().Count);
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceManageRepository repo = new AttendanceManageRepository();
            Assert.AreEqual(true, repo.Destroy(1014));
        }
    }
}