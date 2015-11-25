using CourseProvider;
using CourseServer.Entities;
using CourseServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Repositories
{
    public class DispatchRepository : Repository
    {
        public Dictionary<string, List<Dispatch>> GetAvailableCourse()
        {
            Dictionary<string, List<Dispatch>> result = null;

            using (var context = GetDbContext())
            {
                DbSet<Dispatch> dispatches = context.Set<Dispatch>();
                
                ChainLoad(dispatches, "Course", "Teacher", "Classroom");
                // Available course which has no out of range yet
                var majorDispatch = dispatches.Where(d => d.Current < d.Limit).GroupBy(d => d.Course.Major);
                if (majorDispatch == null)
                    return null;

                // Mapping the key-value data
                result = new Dictionary<string, List<Dispatch>>();
                foreach (var data in majorDispatch)
                {
                    result.Add(data.Key.Name, data.ToList());
                }
            }

            return result;
        }

        public List<Dispatch> GetDispatchList(int userId, int mode)
        {
            using (var context = GetDbContext())
            {
                DbSet<Dispatch> dispatches = context.Set<Dispatch>();

                ChainLoad(dispatches, "Course", "Teacher", "Classroom");
                
                switch (mode)
                {
                    case CourseProviderContract.MODE_STUDENT:
                        ChainLoad(dispatches, "Students");

                        var result = dispatches.Where(d => d.Enable &&
                            d.Students.Where(s => s.Id == userId).FirstOrDefault() != null);

                        return result == null ? null : result.ToList();

                    case CourseProviderContract.MODE_TEACHER:
                        var teacherResult = dispatches.Where(d => d.Enable && d.TeacherId == userId);

                        return teacherResult == null ? null : teacherResult.ToList();
                }
            }

            return null;
        }

        public bool JoinCourse(int userId, int courseId)
        {
            using (var context = GetDbContext())
            {
                DbSet<Student> studentList = context.Set<Student>();
                ChainLoad(studentList, "Dispatches");

                Student student = studentList.Where(s => s.Id == userId).FirstOrDefault();
                // Already join
                if (student.Dispatches.Where(d => d.Id == courseId).FirstOrDefault() != null)
                {
                    return false;
                }

                DbSet<Dispatch> dispatchList = context.Set<Dispatch>();
                // No course match
                Dispatch dispatch = dispatchList.Where(d => d.Id == courseId && d.Current < d.Limit).FirstOrDefault();
                if (dispatch == null) return false;

                dispatch.Current += 1;

                student.Dispatches.Add(dispatch);

                context.SaveChanges();
            }
            return true;
        }

        //public bool RemoveCourse(int userId, int courseId)
        //{
        //    using (var context = GetDbContext())
        //    {
        //        DbSet<Student> studentList = context.Set<Student>();
        //        ChainLoad(studentList, "Dispatches");

        //        Student student = studentList.Where(s => s.Id == userId).FirstOrDefault();
                
        //        Dispatch dispatch = student.Dispatches.Where(d => d.Id == courseId).FirstOrDefault();
        //        if (dispatch != null)
        //        {
        //            dispatch.Current -= 1;
        //            student.Dispatches.Remove(dispatch);
        //        }

        //        context.SaveChanges();
        //    }
        //    return true;
        //}

        public bool RemoveCourseList(int userId, int[] courseIdList)
        {
            using (var context = GetDbContext())
            {
                DbSet<Student> studentList = context.Set<Student>();
                ChainLoad(studentList, "Dispatches");

                Student student = studentList.Where(s => s.Id == userId).FirstOrDefault();

                foreach (var courseId in courseIdList)
                {
                    Dispatch dispatch = student.Dispatches.Where(d => d.Id == courseId).FirstOrDefault();

                    if (dispatch != null)
                    {
                        dispatch.Current -= 1;
                        student.Dispatches.Remove(dispatch);
                    }
                }

                context.SaveChanges();
            }
            return true;
        }
    }
}
