using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("courses")]
    public class Course : Model
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public int MajorId { get; set; }

        public virtual Major Major { get; set; }
        
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
