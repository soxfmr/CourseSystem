using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories.Advance
{
    public class AttendanceManageRepository : Repository
    {
        public List<Dictionary<string, object>> GetAll()
        {
            List<Dictionary<string, object>> Ret = null;
            using (var context = GetDbContext())
            {
                DbSet<Attendance> attendances = context.Set<Attendance>();

                if (attendances.Count() <= 0)
                {
                    return Ret;
                }

                Dictionary<string, object> attendanceInfo;
                Ret = new List<Dictionary<string, object>>(attendances.Count());

                var result = attendances.ToList();

                foreach (var attendance in result)
                {
                    attendanceInfo = new Dictionary<string, object>();
                    attendanceInfo.Add("Id", attendance.Id);
                    attendanceInfo.Add("Population", attendance.Population);
                    attendanceInfo.Add("CourseName", attendance.Dispatch.Course.Name);
                    attendanceInfo.Add("CreatedAt", attendance.CreatedAt);

                    Ret.Add(attendanceInfo);
                }
            }

            return Ret;
        }

        public bool Destroy(int id)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Attendance> attendances = context.Set<Attendance>();
                Attendance attendance = attendances.Where(a => a.Id == id).FirstOrDefault();

                if (attendance != null)
                {
                    attendances.Remove(attendance);

                    context.SaveChanges();
                }

                bRet = true;
            }

            return bRet;
        }
    }
}
