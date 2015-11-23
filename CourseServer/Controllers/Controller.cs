using CourseProvider;
using CourseServer.Builders;
using CourseServer.Contract;
using CourseServer.Framework;
using CourseServer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Controllers
{
    public abstract class Controller
    {
        private RouteDispatchInfo _Request;

        public RouteDispatchInfo Request {
            get
            {
                return _Request;
            }
            set
            {
                _Request = value;
                if (_Request != null && 
                    _Request.GenericPairs != null && 
                    _Request.GenericPairs.ContainsKey(CourseProviderContract.KEY_AUTH))
                {
                    Auth = new Auth((string) _Request.GenericPairs[CourseProviderContract.KEY_AUTH]);
                }
            }
        }

        protected Auth Auth { get; private set; }
    }
}
