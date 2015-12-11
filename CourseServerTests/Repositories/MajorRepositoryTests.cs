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
    public class MajorRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            MajorRepository repo = new MajorRepository();
            Assert.AreEqual(2, repo.GetAll());
        }

        [TestMethod()]
        public void CreateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            MajorRepository repo = new MajorRepository();
            Assert.AreEqual(true, repo.Create("Game", "Playing game!"));
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            MajorRepository repo = new MajorRepository();
            Assert.AreEqual(true, repo.Destroy(3));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            MajorRepository repo = new MajorRepository();
            Assert.AreEqual(true, repo.Update(3, "Game", "Nitontan"));
        }
    }
}