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
    class AttendanceController : Controller
    {
        /// <summary>
        /// Get all of attendances status of the user
        /// </summary>
        /// <returns></returns>
        public string Index()
        {
            ResultSetView view = new ResultSetView();
            AttendanceRepository repo = new AttendanceRepository();

            var result = repo.GetAll(Auth.User().Id);

            return view.Show(result);           
        }

        public string Store()
        {
            return "Store!";
        }

        public string Destory()
        {
            return "Destory!";
        }

        public string Update()
        {
            return "Updated!";
        }
    }
}
