﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Model;
using CourseServer.Framework;

namespace CourseServer.Repositories.Tests
{
    [TestClass()]
    public class CourseRepositoryTests
    {
        [TestMethod()]
        public void AllTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            CourseRepository repo = new CourseRepository();
            List<Course> courseList = repo.All();

            Assert.AreEqual("电子系", courseList[0].Major.Name);
        }

        [TestMethod()]
        public void CreateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            CourseRepository repo = new CourseRepository();

            Assert.AreEqual(true, repo.Create("Computer Network", "Foundation", 2, 3));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            CourseRepository repo = new CourseRepository();

            repo.Remove(2);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            CourseRepository repo = new CourseRepository();

            Assert.AreEqual(true, repo.Update(3, "Comupter Network", "Compter Network Foundation", 2));
        }
    }
}