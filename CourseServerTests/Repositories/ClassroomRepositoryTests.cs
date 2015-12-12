using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;

namespace CourseServer.Repositories.Tests
{
    [TestClass()]
    public class ClassroomRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            ClassroomRepository repo = new ClassroomRepository();
            Assert.AreEqual(2, repo.GetAll().Count);
        }

        [TestMethod()]
        public void CreateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            ClassroomRepository repo = new ClassroomRepository();
            Assert.IsTrue(repo.Create("GameRoom"));
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            ClassroomRepository repo = new ClassroomRepository();
            Assert.IsTrue(repo.Destroy(2));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            ClassroomRepository repo = new ClassroomRepository();
            Assert.IsTrue(repo.Update(2, "GameRoom"));
        }
    }
}