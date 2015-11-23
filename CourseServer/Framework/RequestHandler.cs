using CourseServer.Utils;
using System;
using System.IO;
using System.Text;

namespace CourseServer.Framework
{
    public abstract class RequestHandler
    {
        public abstract string Format();

        public abstract DispatchSource GetDispatchSource();
    }
}
