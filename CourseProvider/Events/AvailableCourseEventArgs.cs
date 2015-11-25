using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class AvailableCourseEventArgs : BaseEventArgs
    {
        public Dictionary<string, List<DispatchCourse>> AvaliableCourseList;

        public AvailableCourseEventArgs(Dictionary<string, List<DispatchCourse>> avaliableCourseList)
        {
            AvaliableCourseList = avaliableCourseList;
        }
    }
}
