using CourseServer.Framework;
using CourseServer.Repositories;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers
{
    class AttendanceController : Controller
    {
        private ResultSetView resultSetView;
        private AttendanceRepository attendanceRepo;

        public AttendanceController()
        {
            resultSetView = new ResultSetView();
            attendanceRepo = new AttendanceRepository();
        }

        /// <summary>
        /// Get all of attendances status of the user
        /// </summary>
        /// <returns></returns>
        public string Index()
        {
            AbsenceRepository absenceRepo = new AbsenceRepository();
            var result = absenceRepo.GetAll(Auth.User().Id);

            return resultSetView.Show(result);           
        }

        public string CourseAttendance()
        {
            var result = attendanceRepo.GetAllCourseAttendance(Auth.User().Id);
            return resultSetView.Show(result);
        }

        /// <summary>
        /// Record a student who absent on the course
        /// </summary>
        /// <param name="type"></param>
        /// <param name="studentId"></param>
        /// <param name="dispatchId"></param>
        /// <returns></returns>
        public string AddStudentAbsence(string type, int studentId, int dispatchId)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            if (!validator.Make(new string[] { type, studentId + "", dispatchId + "" },
                new string[] { "required", "required", "required" },
                new string[] { "type", "studentId", "dispatchId" }))
            {
                return resultSetView.Error(validator.GetDetail());
            }

            AbsenceRepository absenceRepo = new AbsenceRepository();

            bool ret = absenceRepo.AddStudentAbsence(type, studentId, dispatchId, Auth.User().Id);

            return ret ? resultSetView.Success() : resultSetView.Error();
        }

        public string Store(int dispatchId, int population)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            if (! validator.Make(new string[] { dispatchId + "", population + "" },
                new string[] { "required", "required|min:0" },
                new string[] { "dispatchId", "population" }))
            {
                return resultSetView.Error(validator.GetDetail());
            }

            bool ret = attendanceRepo.Create(dispatchId, population, Auth.User().Id);
            return ret ? resultSetView.Success() : resultSetView.Error();
        }

        public string Destroy(int id)
        {
            Validator validator = new Validator();
            // Validate the user input here.
            // Validate the user input here.
            if (!validator.MatchRule(id + "", "required", "id"))
            {
                return resultSetView.Error(validator.GetDetail());
            }

            bool ret = attendanceRepo.Destroy(id, Auth.User().Id);
            return ret ? resultSetView.Success() : resultSetView.Error();
        }
    }
}
