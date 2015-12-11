using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class CourseRepository : Repository
    {
        /// <summary>
        /// Retrieve all of courses from database without pagination
        /// </summary>
        public List<Course> All()
        {
            using(var context = GetDbContext())
            {
                return All<Course>(context, "Major", "Teacher");
            }
        }

        public bool Create(string name, string description, int majorId, int creatorId)
        {
            bool bRet = false;
            using (var context = GetDbContext())
            {
                DbSet<Major> majors = context.Set<Major>();

                var major = majors.Where(m => m.Id == majorId).FirstOrDefault();
                if (major != null)
                {
                    DbSet<Course> courses = context.Set<Course>();
                    Course course = new Course()
                    {
                        Name = name,
                        Description = description,
                        Major = major,
                        TeacherId = creatorId
                    };

                    courses.Add(course);

                    context.SaveChanges();

                    bRet = true;
                }
            }
            return bRet;
        }

        public void Remove(int id)
        {
            using (var context = GetDbContext())
            {
                DbSet<Course> courses = context.Set<Course>();
                var course = courses.Where(c => c.Id == id).FirstOrDefault();

                if (course != null)
                {
                    course.Dispatches = null;
                    courses.Remove(course);

                    context.SaveChanges();
                }                
            }
        }

        public bool Update(int id, string name, string description, int majorId)
        {
            bool bRet = false;
            using (var context = GetDbContext())
            {
                DbSet<Course> courses = context.Set<Course>();
                DbSet<Major> majors = context.Set<Major>();

                var course = courses.Where(c => c.Id == id).FirstOrDefault();
                var major = majors.Where(m => m.Id == majorId).FirstOrDefault();

                if (course != null && major != null)
                {
                    course.Name = name;
                    course.Description = description;
                    course.Major = major;

                    context.SaveChanges();

                    bRet = true;
                }
            }

            return bRet;
        }
    }
}
