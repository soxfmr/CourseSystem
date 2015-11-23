using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseProvider;
using CourseServer.Utils;
using CourseServer.Views;

namespace CourseServer.Middlewares
{
    public class AuthMiddleware : Middleware
    {
        public override bool Handle(RouteDispatchInfo dispatchInfo, ref string message)
        {
            if (dispatchInfo.GenericPairs != null)
            {
                string sessionId = (string) dispatchInfo.GenericPairs[CourseProviderContract.KEY_AUTH];
                if (! TextUtils.isEmpty(sessionId) && Session.Has(sessionId))
                {
                    return true;
                }
            }

            message = new GenericView().Error("Please login first!");
            
            return false;
        }
    }
}
