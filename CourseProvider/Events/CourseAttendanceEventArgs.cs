using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class CourseAttendanceEventArgs : BaseEventArgs
    {
        public List<CourseAttendance> CourseAttendanceList;

        public CourseAttendanceEventArgs(List<CourseAttendance> courseAttendanceList)
        {
            CourseAttendanceList = courseAttendanceList;
        }
    }
}
