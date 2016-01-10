using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class CourseEventArgs : BaseEventArgs
    {
        public List<Course> CourseList;

        public CourseEventArgs(List<Course> courseList)
        {
            CourseList = courseList;
        }
    }
}
