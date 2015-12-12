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
    public class DispatchManageRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchManageRepository repo = new DispatchManageRepository();
            Assert.AreEqual(2, repo.GetAll().Count);
        }

        [TestMethod()]
        public void CreateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchManageRepository repo = new DispatchManageRepository();
            Assert.AreEqual(true, repo.Create("2", DateTime.Now, 20, 3, 3, 1));
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchManageRepository repo = new DispatchManageRepository();
            Assert.AreEqual(true, repo.Destroy(3));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchManageRepository repo = new DispatchManageRepository();
            Assert.AreEqual(true, repo.Update(2, "2", DateTime.Now, 20, 3, 1, true));
        }
    }
}