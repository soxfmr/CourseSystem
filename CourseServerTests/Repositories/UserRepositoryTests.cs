using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Model;
using CourseServer.Utils;
using CourseServer.Framework;
using CourseServer.Entities;

namespace CourseServer.Repositories.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        [TestMethod()]
        public void LoginTest()
        {

        }

        [TestMethod()]
        public void RegisterTest()
        {
            //DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString);

            //UserRepository repo = new UserRepository();

            //Assert.IsTrue(0 == repo.Register("bullshit@gmail.com", "bullshit", "bullshit", 0));
        }

        [TestMethod()]
        public void hasUserTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            UserRepository repo = new UserRepository();

            // Assert.IsTrue(repo.hasUser<Student>("bullshit@gmail.com", "bullshit"));
            Assert.IsTrue(repo.Login("bullshit@gmail.com", "bullshit", 0));
        }

        [TestMethod()]
        public void newUserTest()
        {
            //DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString);

            //UserRepository repo = new UserRepository();

            //Assert.IsTrue(repo.newUser(
            //    new Student { Email = "email@email.com", Name = "Soxfmr", Password = Guard.Encrypt("bullshit") }));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            UserRepository repo = new UserRepository();

            Assert.AreEqual(true, repo.Exists("john@gmail.com", 1));

            repo.Update(repo.CurrentUser, "d", "d", "d", "password");
        }
    }
}