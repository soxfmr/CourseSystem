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
    [Table("students")]
    public class Student : UserEntity
    {
        public Student()
        {
            Mode = CourseProviderContract.MODE_STUDENT;
        }

        public virtual IList<Absence> Absences { get; set; }

        public virtual IList<AbsenceReason> AbsenceReasons { get; set; }

        public virtual IList<Grade> Grades { get; set; }

        // public virtual IList<Join> Joins { get; set; }

        public virtual IList<Feedback> Feedbacks { get; set; }
        
        public virtual IList<Dispatch> Dispatches { get; set; }
    }
}
