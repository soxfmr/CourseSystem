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
        public List<Dictionary<string, object>> All()
        {
            List<Dictionary<string, object>> Ret = null;
            using (var context = GetDbContext())
            {
                DbSet<Course> courses = context.Set<Course>();

                if (courses.Count() <= 0)
                {
                    return Ret;
                }

                Dictionary<string, object> courseInfo;
                Ret = new List<Dictionary<string, object>>(courses.Count());

                var courseList = courses.ToList();

                foreach (var course in courseList)
                {
                    courseInfo = new Dictionary<string, object>();
                    courseInfo.Add("Id", course.Id);
                    courseInfo.Add("Name", course.Name);
                    courseInfo.Add("Description", course.Description);
                    courseInfo.Add("MajorId", course.Major.Id);
                    courseInfo.Add("Major", course.Major.Name);

                    Ret.Add(courseInfo);
                }
            }

            return Ret;
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

        public bool Destroy(int id)
        {
            bool bRet = false;
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

                bRet = true;
            }

            return bRet;
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
