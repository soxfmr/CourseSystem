using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseServer.Model;
using CourseServer.Utils;

namespace CourseServer.Tests
{
    [TestClass()]
    public class CourseDbContextTests
    {
        [TestMethod()]
        public void CourseDbContextTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);
            
            using(var db = DbContextHelper.NewInstance())
            {
                var courses = db.Set<Course>().ToList();
                var teacher = courses[0].Teacher;


                Assert.AreEqual("John", teacher.Name);
            }
        }

        [TestMethod()]
        public void InsertseDbContextTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            using (var db = DbContextHelper.NewInstance())
            {
                var majors = db.Set<Major>();
                Major major = new Major { Name = "电子系", Description = "计算机" };
                majors.Add(major);

                var teachers = db.Set<Teacher>();
                Teacher teacher = new Teacher { Name = "John", Email = "john@gmail.com", Password = Guard.Encrypt("password") };
                teachers.Add(teacher);

                var courses = db.Set<Course>();
                Course course = new Course { Name = "C#", Description = "Microsoft .Net", Major = major, Teacher = teacher };
                courses.Add(course);

                var classrooms = db.Set<Classroom>();
                Classroom classroom = new Classroom { Number = 404, Location = "Little place" };
                classrooms.Add(classroom);

                var dispatches = db.Set<Dispatch>();
                Dispatch dispatch = new Dispatch
                {
                    Teacher = teacher,
                    Course = course,
                    Enable = true,
                    Current = 2,
                    Weekday = 1,
                    At = DateTime.Now,
                    Limit = 10,
                    Classroom = classroom
                };
                dispatches.Add(dispatch);

                db.SaveChanges();
                
                /*var courses = db.Set<Course>();
                var teachers = db.Set<Teacher>();

                Teacher teacher = teachers.Where(t => t.Id == 1).ToList()[0];
                Course course = courses.Where(c => c.Id == 1).ToList()[0];*/
            }
        }
    }
}
 