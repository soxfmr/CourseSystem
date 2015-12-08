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
    public class AttendanceRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceRepository repo = new AttendanceRepository();
            //Assert.AreEqual(1, repo.GetAll(1).Count);
        }

        [TestMethod()]
        public void GetAllTest1()
        {

        }

        [TestMethod()]
        public void GetAllCourseAttendanceTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceRepository repo = new AttendanceRepository();
            Assert.AreEqual(1, repo.GetAllCourseAttendance(1).Count);
        }

        [TestMethod()]
        public void CreateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceRepository repo = new AttendanceRepository();
            Assert.AreEqual(true, repo.Create(1, 233, 1));
        }

        [TestMethod()]
        public void DestroyTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceRepository repo = new AttendanceRepository();
            Assert.AreEqual(true, repo.Destroy(1, 1));
        }

        [TestMethod()]
        public void AddStudentAbsenceTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            AttendanceRepository repo = new AttendanceRepository();
            //Assert.AreEqual(false, repo.AddStudentAbsence("Other", 2, 2, 1));
        }
    }
}