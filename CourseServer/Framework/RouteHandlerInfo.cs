using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class RouteHandlerInfo
    {
        public Type Handler { get; set; }
        public MethodInfo Callback { get; set; }
        public ParameterInfo[] ParamInfo { get; set; }

        public PropertyInfo Transport { get; set; }

        public RouteHandlerInfo(Type handler, MethodInfo method, 
            ParameterInfo[] paramInfo, PropertyInfo transport = null)
        {
            this.Handler = handler;
            this.Callback = method;
            this.ParamInfo = paramInfo;
            this.Transport = transport;
        }
    }
}
