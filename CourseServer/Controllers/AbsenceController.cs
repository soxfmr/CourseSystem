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
    public class AbsenceController : Controller
    {
        private AbsenceRepository absenceRepo;

        private AbsenceView absenceView;

        public AbsenceController()
        {
            absenceView = new AbsenceView();
            absenceRepo = new AbsenceRepository();
        }

        /// <summary>
        /// Dispaly all of appling absence record of the user
        /// </summary>
        /// <returns></returns>
        public string Index()
        {
            var absenceReasons = absenceRepo.GetAll(Auth.User().Id);

            return absenceView.Show(absenceReasons);
        }

        public string AllChangeableAbsence()
        {
            var absenceReasons = absenceRepo.GetAllChangeableAbsence(Auth.User().Id);

            return absenceView.Show(absenceReasons);
        }

        /// <summary>
        /// Add a new absence record
        /// </summary>
        /// <returns></returns>
        public string Store(string reason, int courseId)
        {
            Validator validator = new Validator();
            if (! validator.Make(new string[] { reason, courseId + "" }, 
                new string[] { "required", "required" }, 
                new string[] { "reason", "courseId" }))
            {
                return absenceView.Error(validator.GetDetail());
            }

            bool ret = absenceRepo.Create(reason, courseId, Auth.User().Id);

            return ret ? absenceView.Success() : absenceView.Error();
        }

        /// <summary>
        /// Update a absence record which has not been apply yet
        /// </summary>
        /// <returns></returns>
        public string Update(string reason, int reasonId)
        {
            Validator validator = new Validator();
            if (!validator.Make(new string[] { reason, reasonId + "" },
                new string[] { "required", "required" },
                new string[] { "reason", "reasonId" }))
            {
                return absenceView.Error(validator.GetDetail());
            }

            bool ret = absenceRepo.Update(reason, reasonId, Auth.User().Id);

            return ret ? absenceView.Success() : absenceView.Error();
        }

        /// <summary>
        /// Remove a absence record which has not been apply yet
        /// </summary>
        /// <returns></returns>
        public string Destroy(int id)
        {
            Validator validator = new Validator();
            if (!validator.MatchRule(id.ToString(), "required", "reasonId"))
            {
                return absenceView.Error(validator.GetDetail());
            }

            bool ret = absenceRepo.Destroy(id, Auth.User().Id);

            return ret ? absenceView.Success() : absenceView.Error();
        }

        /// <summary>
        /// Give all of aduitable absence for teacher
        /// </summary>
        /// <returns></returns>
        public string GetAuditableAbsence()
        {
            var result = absenceRepo.GetAuditableAbsence(Auth.User().Id);

            return absenceView.Show(result);
        }

        public string AuditAbsence(int id)
        {
            Validator validator = new Validator();
            if (!validator.MatchRule(id.ToString(), "required", "reasonId"))
            {
                return absenceView.Error(validator.GetDetail());
            }

            bool ret = absenceRepo.AuditAbsence(id, Auth.User().Id);

            return ret ? absenceView.Success() : absenceView.Error();
        }
    }
}
