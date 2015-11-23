using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class DispatchSource
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public Dictionary<string, string> Identities { get; set; }
    }
}
