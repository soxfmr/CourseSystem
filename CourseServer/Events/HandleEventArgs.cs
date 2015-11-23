using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CourseServer.Events
{
    public class HandleEventArgs : EventArgs
    {
        public HttpListenerContext Context;

        public HandleEventArgs(HttpListenerContext context)
        {
            Context = context;
        }
    }
}
