using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("system_logs")]
    public class SystemLog : Model
    {
        public string Operation { get; set; }

        public string IP { get; set; }

        public string Platform { get; set; }

        public int ManagerId { get; set; }

        public Manager Manager { get; set; }
    }
}
