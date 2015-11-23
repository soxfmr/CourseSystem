using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("joins")]
    public class Join : Model
    {
        public int DispatchId { get; set; }

        public virtual Dispatch Dispatch { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
