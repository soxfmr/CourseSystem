using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("roles")]
    public class Role : Model
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IList<Permission> Permissions { get; set; }
    }
}
