using CourseServer.Framework;
using CourseServer.Model;
using CourseServer.Repositories;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers.Advance
{
    public class CourseController : Controller
    {
        private GenericView view;
        private CourseRepository courseRepo;

        public CourseController()
        {
            view = new GenericView();
            courseRepo = new CourseRepository();
        }

        public string Store(string name, string desc, int majorId)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { name, desc, majorId + "" },
                new string[] { "required", "required", "required" },
                new string[] { "name", "desc", "majorId" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = courseRepo.Create(name, desc, majorId, Auth.User().Id);

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

            bool bRet = courseRepo.Destroy(id);

            return bRet ? view.Success() : view.Error();
        }

        public string All()
        {
            var result = courseRepo.All();

            return view.Show(result);
        }

        public string Update(int id, string name, string desc, int majorId)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", name, desc, majorId + "" },
                new string[] { "required", "required", "required", "required" },
                new string[] { "id", "name", "desc", "majorId" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = courseRepo.Update(id, name, desc, majorId);

            return bRet ? view.Success() : view.Error();
        }
    }
}
