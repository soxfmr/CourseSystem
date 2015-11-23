using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("feedbacks")]
    public class Feedback : Model
    {
        public string Title { get; set; }
        
        public string Content { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
