using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("absences")]
    public class Absence : Model
    {
        public int AbsenceTypeId { get; set; }

        public virtual AbsenceType AbsenceType { get; set; }

        public int DispatchId { get; set; }

        public virtual Dispatch Dispatch { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
