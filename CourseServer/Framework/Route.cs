using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CourseServer.Framework
{
    public class Route
    {
        public const string TAG = "Route";

        private static List<RouteInfo> routeList =  new List<RouteInfo>();

        public static List<RouteInfo> getRouteList()
        {
            return routeList;
        }

        /// <summary>
        /// Add a route without params
        /// </summary>
        /// <param name="route">The unique route string</param>
        /// <param name="handler">The information of the handle class and method, e.i. ArticleController@show.
        /// You can specify the namespace of the class in App.config via NAMESAPCE key instead of type-hint
        /// full of name for every handler
        /// </param>
        public static void Add(string route, string handler)
        {
            Add(route, handler, null, false);
        }

        /// <summary>
        /// Add a route without params
        /// </summary>
        /// <param name="route">The unique route string</param>
        /// <param name="handler">The information of the handle class and method, e.i. ArticleController@show.
        /// You can specify the namespace of the class in App.config via NAMESAPCE key instead of type-hint
        /// full of name for every handler
        /// </param>
        /// <param name="cache">determine that the response of this route can be cached.</param>
        public static void Add(string route, string handler, bool cache)
        {
            Add(route, handler, null, cache);
        }

        /// <summary>
        /// Add a route with special params
        /// </summary>
        /// <param name="route">The unique route string</param>
        /// <param name="handler">The information of the handle class and method, e.i. ArticleController@show.
        /// You can specify the namespace of the class in App.config via NAMESAPCE key instead of type-hint
        /// full of name for every handler
        /// </param>
        /// <param name="args">The arguments of the method which will be pass to when the method be invoke.
        /// Each of argument follow by a comma
        /// </param>
        public static void Add(string route, string handler, string args)
        {
            Add(route, handler, args, false);
        }

        /// <summary>
        /// Add a route with special params
        /// </summary>
        /// <param name="route">The unique route string</param>
        /// <param name="handler">The information of the handle class and method, e.i. ArticleController@show.
        /// You can specify the namespace of the class in App.config via NAMESAPCE key instead of type-hint
        /// full of name for every handler
        /// </param>
        /// <param name="args">The arguments of the method which will be pass to when the method be invoke.
        /// Each of argument follow by a comma
        /// </param>
        /// <param name="cache">determine that the response of this route can be cached.</param>
        public static void Add(string route, string handler, string args, bool cache)
        {
            RouteInfo routeInfo = Parse(route, handler, args, cache);

            if (routeInfo != null)
            {
                routeList.Add(routeInfo);
            }
        }

        private static RouteInfo Parse(string route, string handler, 
            string args, bool cache)
        {
            if (TextUtils.isEmpty(route) || TextUtils.isEmpty(handler))
                return null;

            ReflectHelper reflectHelper = new ReflectHelper();
            // Set up the default namespace for the handle class
            reflectHelper.setGlobalNamespace( GlobalSettings.NAMESPACE );
            // Separate out the handle class and method
            RouteHandlerInfo routeHandler = reflectHelper.GetRouteHandler(handler);

            if (routeHandler == null)
                return null;

            string[] expectArgs = null;
            if (!TextUtils.isEmpty(args))
            {
                expectArgs = args.Split(',');
                List<string> argBuilder = new List<string>();
                // Clear up the arguments
                foreach (string arg in expectArgs)
                {
                    if (TextUtils.isEmpty(arg)) continue;

                    argBuilder.Add(arg.Trim());
                }
                // Rebuild the arguments
                expectArgs = argBuilder.Count == 0 ? null : argBuilder.ToArray();
            }

            return new RouteInfo(route, expectArgs, cache, routeHandler);
        }

    }
}
