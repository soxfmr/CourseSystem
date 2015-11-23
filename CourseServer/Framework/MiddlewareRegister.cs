using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Middlewares;

namespace CourseServer.Framework
{
    public class MiddlewareRegister
    {
        public const string TAG = "MiddlewareRegister";

        private static Dictionary<string, Middleware> middlewarePair = 
            new Dictionary<string, Middleware>();

        public static Dictionary<string, Middleware> GetAllMiddleware()
        {
            return middlewarePair;
        }

        /// <summary>
        /// Register a middleware for a controller.
        /// e.i. MiddlewareRegister.register(typeof(UserController), "auth", "showProfile", "onLogout")
        /// The example above means that the controller which reference to THIS instance
        /// will bind to the middleware which has the alias AUTH. When the request handle
        /// by the SHOWPROFILE and ONLOGOUT method, first it will be redirect to the middleware
        /// and execute the HANDLE method in that. 
        /// The alias can be reference to a middleware instance via MiddlewareRegister.Add method.
        /// </summary>
        /// <param name="classes">The class type of the controller which has contain the methods</param>
        /// <param name="middlewareAlias">The alias of the middle</param>
        /// <param name="methodName">A list of method which will be bind to the middleware</param>
        public static void Register(Type classes, string middlewareAlias, params string[] methodName)
        {
            if (classes == null) return;

            if (TextUtils.isEmpty(middlewareAlias))
            {
                Dumper.Log(TAG, "Invalid alias to reference a middleware.");
                return;
            }

            if (middlewarePair.Count == 0 || !middlewarePair.ContainsKey(middlewareAlias))
            {
                Dumper.Log(TAG, string.Format("No alias as {0} middleware of instance found!", middlewareAlias));
                return;
            }

            if (methodName == null) return;

            string className = classes.FullName;
            foreach (string name in methodName)
            {
                if (middlewarePair[middlewareAlias].isProtected(className, name))
                {
                    continue;
                }

                middlewarePair[middlewareAlias].AddFilter(className, name);
            }
        }

        /// <summary>
        /// Append a middleware instance to the global container
        /// </summary>
        /// <param name="alias">The alias of the instance that used to reference a middleware instance
        /// when register a middleware in the somewhere.
        /// </param>
        /// <param name="middleware">A instance of the middleware</param>
        public static void Add(string alias, Middleware middleware)
        {
            if (TextUtils.isEmpty(alias) || middleware == null)
                return;

            if (middlewarePair.ContainsKey(alias))
            {
                middlewarePair[alias] = middleware;
            }
            else
            {
                middlewarePair.Add(alias, middleware);
            }
        }

        /// <summary>
        /// Give all of middleware that are reference to the method of the instance
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static List<Middleware> GetAllReferenceMiddleware(Type classes, string methodName)
        {
            if (middlewarePair.Count == 0)
                return null;

            string className = classes.FullName;
            List<Middleware> referenceMiddleware = new List<Middleware>();
            foreach(KeyValuePair<string, Middleware> pair in middlewarePair)
            {
                if (pair.Value.isProtected(className, methodName))
                {
                    referenceMiddleware.Add(pair.Value);
                }
            }

            return referenceMiddleware;
        }

        /// <summary>
        /// Give the alias of the middleware
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public static string GetAlias(Middleware middleware)
        {
            if (middlewarePair.Count == 0)
                return null;

            foreach (KeyValuePair<string, Middleware> pair in middlewarePair)
            {
                if (pair.Value == middleware)
                    return pair.Key;
            }

            return null;
        }
    }
}
