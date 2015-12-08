using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class DispatchStudentEventArgs : BaseEventArgs
    {
        public List<DispatchInfo> DispatchStudentList;

        public DispatchStudentEventArgs(List<DispatchInfo> dispatchStudentList)
        {
            DispatchStudentList = dispatchStudentList;
        }
    }
}
