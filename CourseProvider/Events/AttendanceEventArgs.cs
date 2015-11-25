using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class AttendanceEventArgs : BaseEventArgs
    {
        public List<Attendance> AttendanceList;

        public AttendanceEventArgs(List<Attendance> attendanceList)
        {
            AttendanceList = attendanceList;
        }
    }
}
