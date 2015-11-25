using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class DispatchCourseEventArgs : BaseEventArgs
    {
        public List<DispatchCourse> DispatchCourseList;

        public DispatchCourseEventArgs(List<DispatchCourse> dispatchCourseList)
        {
            DispatchCourseList = dispatchCourseList;
        }
    }
}
