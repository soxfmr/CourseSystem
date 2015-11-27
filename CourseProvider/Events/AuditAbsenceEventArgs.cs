using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class AuditAbsenceEventArgs : BaseEventArgs
    {
        public List<AuditAbsence> AuditAbsenceList;

        public AuditAbsenceEventArgs(List<AuditAbsence> auditAbsenceList)
        {
            AuditAbsenceList = auditAbsenceList;
        }
    }
}
