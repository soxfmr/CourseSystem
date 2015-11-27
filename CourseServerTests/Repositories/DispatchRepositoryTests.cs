using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseServer.Views;
using Newtonsoft.Json.Linq;

namespace CourseServer.Repositories.Tests
{
    [TestClass()]
    public class DispatchRepositoryTests
    {
        [TestMethod()]
        public void GetDispatchListTest()
        {
            /*DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);
            DispatchRepository dispatchRepo = new DispatchRepository();

            var list = dispatchRepo.GetDispatchList(new Entities.UserEntity { Id = 1, Mode = 1 });
            DispatchView view = new DispatchView();
            string s = view.Show(list);

            JObject obj = JObject.Parse(s);*/
        }

        [TestMethod()]
        public void GetDispatchListTest1()
        {

        }

        [TestMethod()]
        public void JoinCourseTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            Assert.AreEqual(true, dp.JoinCourse(1, 1));
            Assert.AreEqual(false, dp.JoinCourse(1, 2));
        }

        [TestMethod()]
        public void RemoveCourseTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            Assert.AreEqual(true, dp.RemoveCourseList(1, new int[] { 1 }));
        }

        [TestMethod()]
        public void GetDispatchListTest2()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            Assert.AreEqual(1, dp.GetDispatchList(1, 0).Count);
        }

        [TestMethod()]
        public void GetApplyDispatchCourseTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            var list = dp.GetAvailableCourse();
        }

        [TestMethod()]
        public void RemoveCourseTest1()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            Assert.AreEqual(true, dp.RemoveCourseList(1, new int[] { 1 }));
        }

        [TestMethod()]
        public void GetDispatchListTest3()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            Assert.AreEqual(1, dp.GetDispatchList(1, 1).Count);
        }

        [TestMethod()]
        public void GetDispatchStudentTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            DispatchRepository dp = new DispatchRepository();

            // Assert.AreEqual(1, dp.GetDispatchStudent(2, 1).Count);
        }
    }
}