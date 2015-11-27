using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseServer.Views;

namespace CourseServer.Repositories.Tests
{
    [TestClass()]
    public class AbsenceRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(2, repo.GetAll(1).Count);
        }

        [TestMethod()]
        public void GetAllChangeableAbsenceTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(2, repo.GetAllChangeableAbsence(1).Count);
        }

        [TestMethod()]
        public void CreateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(true, repo.Create("I'm gonna to sleep...", 1, 1));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(true, repo.Update("I'm gonna to sleep and do something more.", 1, 1));
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(true, repo.Destroy(1, 1));
        }

        [TestMethod()]
        public void GetAllChangeableAbsenceTest2()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            var result = new AbsenceView().Show(repo.GetAllChangeableAbsence(1));
        }

        [TestMethod()]
        public void GetAuditableAbsenceTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(1, repo.GetAuditableAbsence(1).Count);
        }

        [TestMethod()]
        public void AuditAbsenceTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceRepository repo = new AbsenceRepository();
            Assert.AreEqual(true, repo.AuditAbsence(5, 1));
        }
    }
}