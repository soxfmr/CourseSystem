using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class AbsenceReasonRepository : Repository
    {
        /// <summary>
        /// Get all of absence record for a student 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAll(int userId)
        {
            return GetAllByCondition(userId, a => !a.Changeable);
        }

        /// <summary>
        /// Get all of absence record which is changeable.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAllChangeableAbsence(int userId)
        {
            return GetAllByCondition(userId, a => a.Changeable);
        }

        private List<Dictionary<string, object>> GetAllByCondition(int userId, Func<AbsenceReason, bool> condition)
        {
            using (var context = GetDbContext())
            {
                DbSet<Student> students = context.Set<Student>();
                ChainLoad(students, "AbsenceReasons");

                var student = students.Where(s => s.Id == userId).FirstOrDefault();

                var result = student.AbsenceReasons.Where(condition);
                if (result != null)
                {
                    Dictionary<string, object> info;
                    var keeper = new List<Dictionary<string, object>>();
                    foreach (var areason in result)
                    {
                        info = new Dictionary<string, object>();
                        info.Add("Id", areason.Id);
                        info.Add("Reason", areason.Reason);
                        info.Add("CourseName", areason.Dispatch.Course.Name);

                        keeper.Add(info);
                    }

                    return keeper;
                }
            }

            return null;
        }

        /// <summary>
        /// Get all of absence record relate to the course for a teahcer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAuditableAbsence(int userId)
        {

            using (var context = GetDbContext())
            {
                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();
                ChainLoad(absenceReasons, "Dispatch");

                var result = absenceReasons.Where(a => a.Dispatch.TeacherId == userId && a.Changeable);
                if (result != null)
                {
                    var data = result.ToList();

                    Dictionary<string, object> info;
                    List<Dictionary<string, object>> keeper = new List<Dictionary<string, object>>();

                    foreach (var areason in data)
                    {
                        info = new Dictionary<string, object>();
                        info.Add("Id", areason.Id);
                        info.Add("Reason", areason.Reason);
                        info.Add("StudentName", areason.Student.Name);

                        keeper.Add(info);
                    }

                    return keeper;
                }
            }

            return null;
        }

        /// <summary>
        /// Signing a absence for a student
        /// </summary>
        /// <param name="reasonId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AuditAbsence(int reasonId, int userId)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();
                var aReason = absenceReasons.Where(a => a.Id == reasonId &&
                    a.Dispatch.TeacherId == userId).FirstOrDefault();

                if (aReason != null)
                {
                    aReason.Changeable = false;
                }

                context.SaveChanges();
                bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// Create a new record of the absence reason
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="dispatchId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Create(string reason, int dispatchId, int userId)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Student> students = context.Set<Student>();
                ChainLoad(students, "Dispatches");

                var student = students.Where(s => s.Id == userId).FirstOrDefault();
                // Find out the course from user
                var dispatch = student.Dispatches.Where(d => d.Id == dispatchId).FirstOrDefault();
                // If no course available
                if (dispatch == null)
                {
                    return false;
                }

                // Out of the time of the course
                if (dispatch.At >= DateTime.Now)
                {
                    return false;
                }

                ChainLoad(students, "AbsenceReasons");

                AbsenceReason aReason = new AbsenceReason { Reason = reason, Dispatch = dispatch, Student = student };

                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();
                absenceReasons.Add(aReason);
                student.AbsenceReasons.Add(aReason);

                context.SaveChanges();

                bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// Update a record of the absence reason
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="reasonId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Update(string reason, int reasonId, int userId)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<Student> students = context.Set<Student>();
                ChainLoad(students, "AbsenceReasons");

                var student = students.Where(s => s.Id == userId).FirstOrDefault();
                var aReason = student.AbsenceReasons.Where(a => a.Id == reasonId && a.Changeable).FirstOrDefault();

                if (aReason == null)
                {
                    return false;
                }

                aReason.Reason = reason;

                context.SaveChanges();
                bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// Remove a record of the absence record
        /// </summary>
        /// <param name="reasonId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Destroy(int reasonId, int userId)
        {
            bool bRet = false;

            using (var context = GetDbContext())
            {
                DbSet<AbsenceReason> absenceReasons = context.Set<AbsenceReason>();

                var aReason = absenceReasons.Where(a => a.Id == reasonId &&
                    a.Changeable && a.StudentId == userId).FirstOrDefault();
                if (aReason == null)
                {
                    return false;
                }

                absenceReasons.Remove(aReason);

                context.SaveChanges();
                bRet = true;
            }

            return bRet;
        }
    }
}
