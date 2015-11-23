using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class RouteInfo
    {
        public RouteHandlerInfo HandlerInfo { get; private set; }

        public string Route { get; private set; }

        public string[] Params { get; private set; }

        public bool Cache { get; private set; }

        public RouteInfo(string route, bool cache, RouteHandlerInfo routeHandler) :
            
            this(route, null, cache, routeHandler) {}

        public RouteInfo(string route, string[] args, bool cache, RouteHandlerInfo routeHandler)
        {
            Route = route;
            Cache = cache;

            HandlerInfo = routeHandler;
            Params = args;
        }        
    }
}
