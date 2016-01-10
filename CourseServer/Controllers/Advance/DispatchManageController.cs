using CourseServer.Framework;
using CourseServer.Repositories.Advance;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers.Advance
{
    public class DispatchManageController : Controller
    {
        private GenericView view;
        private DispatchManageRepository dispatchMgrRepo;

        public DispatchManageController()
        {
            view = new GenericView();
            dispatchMgrRepo = new DispatchManageRepository();
        }

        public string Store(string weekday, DateTime at,
            int limit,
            int teacherId, int courseId, int roomId)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { weekday, at.ToString(), limit + "",
                            teacherId + "", courseId + "", roomId + ""},
                new string[] { "required", "required", "required", "required", "required", "required" },
                new string[] { "weekday", "at", "limit", "teacherId", "courseId", "roomId" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = dispatchMgrRepo.Create(weekday, at, limit, teacherId, courseId, roomId);

            return bRet ? view.Success() : view.Error();
        }

        public string Destroy(int id)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { id + "" },
                new string[] { "required" },
                new string[] { "id" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = dispatchMgrRepo.Destroy(id);

            return bRet ? view.Success() : view.Error();
        }

        public string All()
        {
            var result = dispatchMgrRepo.GetAll();

            return view.Show(result);
        }

        public string Update(int id, string weekday, DateTime at, int limit,
            int teacherId, int roomId)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", weekday, at.ToString(), limit + "" },
                new string[] { "required", "required", "required", "required" },
                new string[] { "id", "weekday", "at", "limit" }))
            {
                return view.Error(validator.GetDetail());
            }
            // Enable the course by default
            bool bRet = dispatchMgrRepo.Update(id, weekday, at, limit, teacherId, roomId, true);

            return bRet ? view.Success() : view.Error();
        }
    }
}
