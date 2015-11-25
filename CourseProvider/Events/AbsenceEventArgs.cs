using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class AbsenceEventArgs : BaseEventArgs
    {
        public List<Absence> AbsenceList;

        public AbsenceEventArgs(List<Absence> absenceList)
        {
            AbsenceList = absenceList;
        }
    }
}
