using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("attendances")]
    public class Attendance : Model
    {
        public int Population { get; set; }

        public int DispatchId { get; set; }

        public virtual Dispatch Dispatch { get; set; }
    }
}
