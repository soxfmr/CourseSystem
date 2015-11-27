using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using CourseProvider;
using CourseServer.Utils;
using CourseServer.Helper;
using CourseServer.Views;

namespace CourseServer.Middlewares
{
    public class AccessControlMiddleware : Middleware
    {
        private int mode = 0;

        private Auth Auth;

        /// <summary>
        /// User Access Control
        /// </summary>
        /// <param name="mode">The permission identity of the user</param>
        public AccessControlMiddleware(int mode)
        {
            this.mode = mode;

            Auth = new Auth();
        }

        public override bool Handle(RouteDispatchInfo dispatchInfo, ref string message)
        {
            string sessionId = dispatchInfo.GenericPairs[CourseProviderContract.KEY_AUTH] as string;

            if (! TextUtils.isEmpty(sessionId))
            {
                var user = Auth.User(sessionId);
                if (user != null && user.Mode == mode)
                {
                    return true;
                }
            }

            message = new GenericView().Error("Access denied!");
            return false;
        }
    }
}
