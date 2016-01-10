using CourseServer.Framework;
using CourseServer.Repositories;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers.Advance
{
    public class ClassroomController : Controller
    {
        private GenericView view;
        private ClassroomRepository classroomRepo;

        public ClassroomController()
        {
            view = new GenericView();
            classroomRepo = new ClassroomRepository();
        }

        public string All()
        {
            var result = classroomRepo.GetAll();

            return view.Show(result);
        }

        public string Store(string location)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { location },
                new string[] { "required" },
                new string[] { "location" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = classroomRepo.Create(location);

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

            bool bRet = classroomRepo.Destroy(id);

            return bRet ? view.Success() : view.Error();
        }

        public string Update(int id, string location)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", location },
                new string[] { "required", "required" },
                new string[] { "id", "location" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = classroomRepo.Update(id, location);

            return bRet ? view.Success() : view.Error();
        }
    }
}
