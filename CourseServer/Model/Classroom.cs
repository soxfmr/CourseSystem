using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("classrooms")]
    public class Classroom : Model
    {
        public string Location { get; set; }

        public virtual IList<Dispatch> Dispatches { get; set; }
    }
}
