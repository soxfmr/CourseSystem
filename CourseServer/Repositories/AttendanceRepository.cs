using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class AttendanceRepository : Repository
    {
        public List<Dictionary<string, object>> GetAll(int userId)
        {
            using (var context = GetDbContext())
            {
                DbSet<Student> students = context.Set<Student>();
                var student = students.Where(s => s.Id == userId).FirstOrDefault();
                
                if (student.Absences.Count > 0)
                {
                    Dictionary<string, object> info;
                    var keeper = new List<Dictionary<string, object>>();

                    foreach (var absence in student.Absences)
                    {
                        info = new Dictionary<string, object>();
                        info.Add("CourseName", absence.Dispatch.Course.Name);
                        info.Add("Type", absence.Type);

                        keeper.Add(info);
                    }

                    return keeper;
                }
            }

            return null;
        }
    }
}
