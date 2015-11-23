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
    }
}
