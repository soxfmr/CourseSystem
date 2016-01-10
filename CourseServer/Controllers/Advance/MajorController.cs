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
    public class MajorController : Controller
    {
        private GenericView view;
        private MajorRepository majorRepo;

        public MajorController()
        {
            view = new GenericView();
            majorRepo = new MajorRepository();
        }

        public string Store(string name, string desc)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { name, desc },
                new string[] { "required", "required" },
                new string[] { "name", "desc" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = majorRepo.Create(name, desc);

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

            bool bRet = majorRepo.Destroy(id);

            return bRet ? view.Success() : view.Error();
        }

        public string All()
        {
            var result = majorRepo.GetAll();

            return view.Show(result);
        }

        public string Update(int id, string name, string desc)
        {
            Validator validator = new Validator();

            // Validate the user input here.
            if (!validator.Make(new string[] { id + "", name, desc },
                new string[] { "required", "required", "required" },
                new string[] { "id", "name", "desc" }))
            {
                return view.Error(validator.GetDetail());
            }

            bool bRet = majorRepo.Update(id, name, desc);

            return bRet ? view.Success() : view.Error();
        }
    }
}
