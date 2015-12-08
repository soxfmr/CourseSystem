using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class AbsenceRepository : Repository
    {
        /// <summary>
        /// Get all of attendance record for a student
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                        info.Add("Type", absence.Type);
                        info.Add("CourseName", absence.Dispatch.Course.Name);
                        info.Add("CreateAt", absence.CreatedAt);

                        keeper.Add(info);
                    }

                    return keeper;
                }
            }

            return null;
        }

        /// <summary>
        /// Add a student who absent on the course to the database
        /// </summary>
        /// <param name="type"></param>
        /// <param name="studentId"></param>
        /// <param name="dispatchId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddStudentAbsence(string type, int studentId, int dispatchId, int userId)
        {
            bool bRet = false;
            using (var context = GetDbContext())
            {
                DbSet<Dispatch> dispatches = context.Set<Dispatch>();
                var dispatch = dispatches.Where(d => d.Id == dispatchId && d.TeacherId == userId &&
                                                d.Students.Where(s => s.Id == studentId).FirstOrDefault() != null).FirstOrDefault();

                if (dispatch == null)
                    return false;

                DbSet<Absence> absences = context.Set<Absence>();
                Absence record = new Absence()
                {
                    Type = type,
                    StudentId = studentId,
                    Dispatch = dispatch
                };

                absences.Add(record);

                context.SaveChanges();

                bRet = true;
            }

            return bRet;
        }
    }
}
