using CourseServer.Framework;
using CourseServer.Repositories;
using CourseServer.Views;

namespace CourseServer.Controllers
{
    public class AbsenceController : Controller
    {
        private AbsenceReasonRepository absenceReasonRepo;

        private AbsenceView absenceView;

        public AbsenceController()
        {
            absenceView = new AbsenceView();
            absenceReasonRepo = new AbsenceReasonRepository();
        }

        /// <summary>
        /// Dispaly all of appling absence record of the user
        /// </summary>
        /// <returns></returns>
        public string Index()
        {
            var absenceReasons = absenceReasonRepo.GetAll(Auth.User().Id);

            return absenceView.Show(absenceReasons);
        }

        public string AllChangeableAbsence()
        {
            var absenceReasons = absenceReasonRepo.GetAllChangeableAbsence(Auth.User().Id);

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

            bool ret = absenceReasonRepo.Create(reason, courseId, Auth.User().Id);

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

            bool ret = absenceReasonRepo.Update(reason, reasonId, Auth.User().Id);

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

            bool ret = absenceReasonRepo.Destroy(id, Auth.User().Id);

            return ret ? absenceView.Success() : absenceView.Error();
        }

        /// <summary>
        /// Give all of aduitable absence for teacher
        /// </summary>
        /// <returns></returns>
        public string GetAuditableAbsence()
        {
            var result = absenceReasonRepo.GetAuditableAbsence(Auth.User().Id);

            return absenceView.Show(result);
        }

        public string AuditAbsence(int id)
        {
            Validator validator = new Validator();
            if (!validator.MatchRule(id.ToString(), "required", "reasonId"))
            {
                return absenceView.Error(validator.GetDetail());
            }

            bool ret = absenceReasonRepo.AuditAbsence(id, Auth.User().Id);

            return ret ? absenceView.Success() : absenceView.Error();
        }
    }
}
