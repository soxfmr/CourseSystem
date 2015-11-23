using CourseProvider;
using CourseServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("teachers")]
    public class Teacher : UserEntity
    {
        public Teacher()
        {
            Mode = CourseProviderContract.MODE_TEACHER;
        }

        public virtual List<Course> Courses { get; set; }

        public virtual List<CourseAppliy> CourseApplies { get; set; }

        public virtual List<Dispatch> Dispatches { get; set; }

        public virtual List<Feedback> Feedbacks { get; set; }
    }
}
