using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("broadcasts")]
    public class Broadcast : Model
    {
        public string Title { get; set; }
        
        public string Content { get; set; }

        public int Visited { get; set; }

        public int AuthorId { get; set; }

        public virtual Manager Author { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
