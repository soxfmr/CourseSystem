﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Model
{
    [Table("absence_reasons")]
    public class AbsenceReason : Model
    {
        public AbsenceReason()
        {
            Changeable = true;
        }

        public string Reason { get; set; }

        public bool Changeable { get; set; }

        public int DispatchId { get; set; }

        public virtual Dispatch Dispatch { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
