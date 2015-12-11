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
        public void AddUser()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);
            using (var db = DbContextHelper.NewInstance())
            {
                var managers = db.Set<Manager>();
                var roles = db.Set<Role>();
                var permissions = db.Set<Permission>();

                Permission perm = new Permission() { Name = "All" };
                permissions.Add(perm);
                
                Role role = new Role() { Name = "Admin", Description = "Grant all of permission" };
                roles.Add(role);

                Manager manager = new Manager() { Name = "Yuge", Email = "yuge@gmail.com", Password = Guard.Encrypt("password"), Role = role };
                managers.Add(manager);

                db.SaveChanges();
            }
        }

        [TestMethod()]
        public void InsertseDbContextTest()
        {
            DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 8);

            using (var db = DbContextHelper.NewInstance())
            {
                var majors = db.Set<Major>();
                Major major = new Major { Name = "萌系", Description = "专业卖萌20年" };
                Major majorEle = new Major { Name = "电子系", Description = "专业修电脑20年" };
                majors.Add(major);
                majors.Add(majorEle);

                var teachers = db.Set<Teacher>();
                Teacher teacher = new Teacher { Name = "John", Email = "john@gmail.com", Password = Guard.Encrypt("password") };
                teachers.Add(teacher);

                var students = db.Set<Student>();
                Student student = new Student { Name = "Yuge", Email = "yuge@gmail.com", Password = Guard.Encrypt("password") };
                students.Add(student);

                // var teacher = teachers.Where(t => t.Id == 1).FirstOrDefault();

                var courses = db.Set<Course>();
                Course course = new Course { Name = "反卖萌的研究", Description = "深入了解卖萌心里，卖萌比卖淫更可耻！", Major = major, Teacher = teacher };
                Course courseCS = new Course { Name = "C#", Description = "Microsoft .Net 平台开发", Major = majorEle, Teacher = teacher };
                courses.Add(course);
                courses.Add(courseCS);

                var classrooms = db.Set<Classroom>();
                Classroom classroom = new Classroom { Number = 404, Location = "Little place" };
                classrooms.Add(classroom);

                var dispatches = db.Set<Dispatch>();
                Dispatch dispatch = new Dispatch
                {
                    Teacher = teacher,
                    Course = course,
                    Enable = true,
                    Current = 0,
                    Weekday = 1,
                    At = DateTime.Now,
                    Limit = 50,
                    Classroom = classroom
                };
                Dispatch dispatchCS = new Dispatch
                {
                    Teacher = teacher,
                    Course = courseCS,
                    Enable = true,
                    Current = 0,
                    Weekday = 2,
                    At = DateTime.Now,
                    Limit = 100,
                    Classroom = classroom
                };
                dispatches.Add(dispatch);
                dispatches.Add(dispatchCS);

                db.SaveChanges();
                
                /*var courses = db.Set<Course>();
                var teachers = db.Set<Teacher>();

                Teacher teacher = teachers.Where(t => t.Id == 1).ToList()[0];
                Course course = courses.Where(c => c.Id == 1).ToList()[0];*/
            }
        }
    }
}
 