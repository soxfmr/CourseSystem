using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("permissions")]
    public class Permission : Model
    {
        public string Name { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}
