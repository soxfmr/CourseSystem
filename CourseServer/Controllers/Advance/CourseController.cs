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
        private CourseView courseView;
        private CourseRepository courseRepo;

        public CourseController()
        {
            courseView = new CourseView();
            courseRepo = new CourseRepository();
        }

        public string Index()
        {
            List<Course> courseList = courseRepo.All();
            return courseView.Show(courseList);
        }
    }
}
