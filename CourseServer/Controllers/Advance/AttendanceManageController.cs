using CourseServer.Repositories.Advance;
using CourseServer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers.Advance
{
    public class AttendanceManageController : Controller
    {
        private GenericView view;
        private AttendanceManageRepository attendanceMgrRepo;

        public AttendanceManageController()
        {
            view = new GenericView();
            attendanceMgrRepo = new AttendanceManageRepository();
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
