using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Middlewares
{
    public abstract class Middleware
    {
        protected List<string> methodFilter = null;

        public Middleware()
        {
            methodFilter = new List<string>();
        }

        public bool isProtected(string classes, string methodName)
        {
            return methodFilter.Contains(GetRegisterName(classes, methodName));
        }

        public void AddFilter(string classes, string methodName)
        {
            methodFilter.Add(GetRegisterName(classes, methodName));
        }

        public string GetRegisterName(string classes, string methodName)
        {
            return classes + "@" + methodName;
        }

        /// <summary>
        /// Handle a imcoming request in this method
        /// </summary>
        /// <param name="dispatchInfo"></param>
        /// <param name="message"></param>
        /// <returns>return True if the request can be continue to pass-throught or False to 
        /// interrupt the reuqest and display some message.
        /// </returns>
        public abstract bool Handle(RouteDispatchInfo dispatchInfo, ref string message);
    }
}
