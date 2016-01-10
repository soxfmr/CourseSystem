using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class MajorEventArgs : BaseEventArgs
    {
        public List<Major> MajorList;

        public MajorEventArgs(List<Major> majorList)
        {
            MajorList = majorList;
        }
    }
}
