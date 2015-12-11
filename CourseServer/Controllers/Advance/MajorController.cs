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

        public string Store(string name, string desc, int majorId, int creatorId)
        {
            return null;
        }

        public string Destroy(int id)
        {
            return null;
        }

        public string All()
        {
            return null;
        }

        public string Update(string name, string desc, int majorId)
        {
            return null;
        }
    }
}
