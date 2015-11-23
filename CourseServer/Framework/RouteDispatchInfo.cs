using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class RouteDispatchInfo
    {
        public string Route { get; set; }

        public Dictionary<string, object> Params { get; set; }

        public Dictionary<string, object> GenericPairs { get; set; }

        public DispatchSource DispatchSource { get; set; }

        public RouteDispatchInfo() {}

        public RouteDispatchInfo(string route, Dictionary<string, object> args,
            Dictionary<string, object> generic)
        {
            Route = route;
            Params = args;
            GenericPairs = generic;
        }

    }
}
