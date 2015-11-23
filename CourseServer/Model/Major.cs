using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseServer.Model
{
    [Table("majors")]
    public class Major : Model
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<Course> Courses { get; set; }
    }
}