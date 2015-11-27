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
        public Dictionary<string, List<DispatchStudent>> DispatchStudentList;

        public DispatchStudentEventArgs(Dictionary<string, List<DispatchStudent>> dispatchStudentList)
        {
            DispatchStudentList = dispatchStudentList;
        }
    }
}
