using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseServer.Model
{
    [Table("dispatches")]
    public class Dispatch : Model
    {
        public int Limit { get; set; }

        public int Current { get; set; }

        [Index]
        public short Weekday { get; set; }

        [Index]
        public DateTime At { get; set; }

        public bool Enable { get; set; }
        
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int ClassroomId { get; set; }

        public virtual Classroom Classroom { get; set; }

        [JsonIgnore]
        public virtual IList<Attendance> Attendances { get; set; }

        [JsonIgnore]
        public virtual IList<Grade> Grades { get; set; }

        [JsonIgnore]
        public virtual IList<Student> Students { get; set; }
    }
}