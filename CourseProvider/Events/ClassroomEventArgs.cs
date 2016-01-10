using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class ClassroomEventArgs : BaseEventArgs
    {
        public List<Classroom> ClassroomList;

        public ClassroomEventArgs(List<Classroom> classroomList)
        {
            ClassroomList = classroomList;
        }
    }
}
