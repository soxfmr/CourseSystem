using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("absence_types")]
    public class AbsenceType : Model
    {
        public string Type { get; set; }

        public virtual IList<Absence> Absences { get; set; }
    }
}
