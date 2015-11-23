using CourseServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class SessionInfo
    {
        public UserEntity UserEntity { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsValid { get { return UserEntity != null; } }
    }
}
