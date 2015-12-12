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
