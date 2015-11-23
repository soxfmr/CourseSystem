using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("password_resets")]
    public class PasswordReset : Model
    {
        public string Email { get; set; }
        
        public string Token { get; set; }
    }
}
