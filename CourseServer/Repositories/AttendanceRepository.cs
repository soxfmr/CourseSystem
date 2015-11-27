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


        public List<Dictionary<string, object>> GetAllCourseAttendance(int userId)
        {
            using (var context = GetDbContext())
            {
                DbSet<Attendance> attendances = context.Set<Attendance>();
                ChainLoad(attendances, "Dispatch");

                var result = attendances.Where(a => a.Dispatch.TeacherId == userId);
                if (result != null)
                {
                    var data = result.ToList();

                    Dictionary<string, object> info;
                    var keeper = new List<Dictionary<string, object>>();

                    foreach (var attendance in data)
                    {
                        info = new Dictionary<string, object>();
                        info.Add("Id", attendance.Id);
                        info.Add("CourseName", attendance.Dispatch.Course.Name);
                        info.Add("Population", attendance.Population);
                        info.Add("CreatedAt", attendance.CreatedAt);

                        keeper.Add(info);
                    }

                    return keeper;
                }
            }

            return null;
        }

        public bool Create(int dispatchId, int population, int userId)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Dispatch> dispatches = context.Set<Dispatch>();

                var dispatch = dispatches.Where(d => d.Id == dispatchId && d.TeacherId == userId).FirstOrDefault();
                if (dispatch != null)
                {
                    Attendance attendance = new Attendance() { Dispatch = dispatch, Population = population };
                    dispatch.Attendances.Add(attendance);

                    bRet = true;
                }

                context.SaveChanges();
            }

            return bRet;
        }

        public bool Destroy(int id, int userId)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Attendance> attendances = context.Set<Attendance>();
                ChainLoad(attendances, "Dispatch");

                var attendace = attendances.Where(a => a.Id == id &&
                        a.Dispatch.TeacherId == userId).FirstOrDefault();

                if (attendace != null)
                {
                    attendances.Remove(attendace);

                    context.SaveChanges();
                }

                bRet = true;
            }

            return bRet;
        }
    }    
}
