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
    public class AbsenceManageRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceManageRepository repo = new AbsenceManageRepository();
            Assert.AreEqual(1, repo.GetAll().Count);
        }

        [TestMethod()]
        public void GetAllAuditableTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceManageRepository repo = new AbsenceManageRepository();
            Assert.AreEqual(1, repo.GetAllAuditable().Count);
        }

        [TestMethod()]
        public void AuditAbsenceTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceManageRepository repo = new AbsenceManageRepository();
            Assert.AreEqual(true, repo.AuditAbsence(1002));
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AbsenceManageRepository repo = new AbsenceManageRepository();
            Assert.AreEqual(true, repo.Destroy(1002));
        }
    }
}