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

        public string Store()
        {
            return null;
        }

        public string Destroy()
        {
            return null;
        }

        public string All()
        {
            return null;
        }

        public string Update()
        {
            return null;
        }
    }
}
