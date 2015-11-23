using CourseServer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Contract
{
    public interface IRouteBuilder
    {
        RouteDispatchInfo buildDispatchInfo();
    }
}
