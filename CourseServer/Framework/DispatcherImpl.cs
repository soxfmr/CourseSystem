using CourseServer.Contract;
using System;
using System.Collections.Generic;
using System.Net;
using CourseServer.Builders;
using CourseServer.Utils;
using CourseServer.Events;
using CourseServer.Middlewares;

namespace CourseServer.Framework
{
    public class DispatcherImpl : IDispatcher
    {
        public const string TAG = "DispatcherImpl";

        private List<RouteInfo> routeInfoList;

        private ICacheDriver cacheDriver = null;

        private ReflectHelper reflectHelper = null;

        public void SetCacheDriver(ICacheDriver cacheDriver)
        {
            this.cacheDriver = cacheDriver;
        }

        public DispatcherImpl()
        {
            // Retrieve the route list
            routeInfoList = Route.getRouteList();

            reflectHelper = new ReflectHelper();
        }

        /// <summary>
        /// Handle an incoming request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Handle(object sender, HandleEventArgs e)
        {
            HttpListenerContext context = e.Context;
            RequestHandler requestHandler = new HttpListenerRequestHandler(context.Request);
            ResponseHandler responeHandler = new HttpListenerResponseHandler(context.Response);

            string responseData = string.Empty;

            string jsonData = requestHandler.Format();

            // Build the route dispatch information from incoming request
            IRouteBuilder builder = new JSONRouteBuilder(jsonData);
            RouteDispatchInfo dispatchInfo = builder.buildDispatchInfo();

            if (dispatchInfo == null)
            {
                responeHandler.NotFound();

                Dumper.Log(TAG, "Cannot found the dispatch information in the request carrier.");
                return;
            }

            if (routeInfoList == null)
            {
                responeHandler.NotFound();

                Dumper.Log(TAG, string.Format("Cannot dispatch the route {0} due to empty route list.",
                    dispatchInfo.Route));
                return;
            }

            // Disptach the route to the handle class
            // The dispatch source will only be extract from the request
            // and attach to the instance when the route was found for the performance
            foreach (RouteInfo info in routeInfoList)
            {
                if (info.Route == dispatchInfo.Route)
                {
                    dispatchInfo.DispatchSource = requestHandler.GetDispatchSource();

                    responseData = Dispatch(info, dispatchInfo);
                    break;
                }
            }

            // Represent the data
            if (!TextUtils.isEmpty(responseData))
            {
                responeHandler.Response(200, responeHandler.DefaultEncoding.GetBytes(responseData));
                Dumper.Log(TAG, string.Format("The route {0} has been dispatched.", dispatchInfo.Route));
            }
            else
            {
                responeHandler.NotFound();
                Dumper.Log(TAG, string.Format("No route matched for {0} or empty result.", dispatchInfo.Route));
            }
        }

        private string Dispatch(RouteInfo routeInfo, RouteDispatchInfo dispatchInfo)
        {
            RouteHandlerInfo handlerInfo = routeInfo.HandlerInfo;

            object[] arguments = null;

            Type paramType = null;

            string ret = string.Empty;

            // Validate the middleware and try to 
            // load the data from cache for performanace
            if (!requireMiddleware(routeInfo, dispatchInfo, ref ret))
            {
                return ret;
            }
            
            if (requireCacheDriver(routeInfo, ref ret))
            {
                return ret;
            }

            if (handlerInfo.ParamInfo != null && handlerInfo.ParamInfo.Length > 0)
            {
                if (dispatchInfo.Params == null || dispatchInfo.Params.Count == 0)
                {
                    Dumper.Log(TAG, "The count of argument doesn't matched.");
                    return ret;
                }

                if (handlerInfo.ParamInfo.Length != routeInfo.Params.Length ||
                    handlerInfo.ParamInfo.Length != dispatchInfo.Params.Count)
                {
                    Dumper.Log(TAG, "The count of argument doesn't matched.");
                    return ret;
                }

                try
                {
                    int count = handlerInfo.ParamInfo.Length;
                    arguments = new object[count];

                    object arg;

                    for (int i = 0; i < count; i++)
                    {
                        paramType = handlerInfo.ParamInfo[i].ParameterType;
                        // Retrieve the value of param from the request data
                        arg = dispatchInfo.Params[routeInfo.Params[i]];
                        // Raise an error while no argument found
                        /* if (arg == null)
                            throw new ArgumentNullException(string.Format("Cannot retrieve the argument {0} from a RouteDispatchInfo instance.", 
                                routeInfo.Params[i]));*/

                        arguments[i] = arg == null ? null : Convert.ChangeType(arg, paramType);
                    }
                }
                catch (Exception e)
                {
                    Dumper.Log(TAG, string.Format("An error occur when try to dispatch the route {0}: {1}",
                        dispatchInfo.Route, e.Message)); 
                    return ret;
                }
            }

            // Redirect the handler to the controller
            return redirectHandle(routeInfo, dispatchInfo, arguments);
        }

        private bool requireMiddleware(RouteInfo routeInfo, RouteDispatchInfo dispatchInfo, ref string message)
        {
            List<Middleware> middlewareList = 
                MiddlewareRegister.GetAllReferenceMiddleware(routeInfo.HandlerInfo.Handler, routeInfo.HandlerInfo.Callback.Name);

            // No middleware reference to this instance
            if (middlewareList == null || middlewareList.Count == 0)
                return true;
            
            foreach (Middleware middleware in middlewareList)
            {
                if (! middleware.Handle(dispatchInfo, ref message))
                {
                    Dumper.Log(TAG, string.Format("The request {0} interrupt by the middleware: {1}", dispatchInfo.Route,
                        MiddlewareRegister.GetAlias(middleware)));
                    return false;
                }
            }

            return true;
        }

        private bool requireCacheDriver(RouteInfo routeInfo, ref string data)
        {
            if (cacheDriver == null || !routeInfo.Cache)
                return false;

            // TODO: Store the key in somewhere
            string key = cacheDriver.GetCacheKey(routeInfo.HandlerInfo.Handler, routeInfo.HandlerInfo.Callback.Name);
            // Load the data from cache
            data = cacheDriver.LoadCache(key);

            bool loadSuccess = ! TextUtils.isEmpty(data);
            if (loadSuccess)
            {
                Dumper.Log(TAG, string.Format("Load the data from cache via {0} key for route {1}", 
                    key, routeInfo.Route));
            }

            return loadSuccess;
        }

        private string redirectHandle(RouteInfo routeInfo, RouteDispatchInfo dispatchInfo, object[] args)
        {
            string data = string.Empty;
            object instance = null;

            RouteHandlerInfo handlerInfo = routeInfo.HandlerInfo;

            if (! initHandlerInstance(routeInfo, dispatchInfo, out instance))
            {
                return data;
            }

            object ret = handlerInfo.Callback.Invoke(instance, args);
            if (ret != null)
            {
                data = ret.ToString();
                    
                // Caching the result
                if (cacheDriver != null && routeInfo.Cache)
                {
                    string key = cacheDriver.GetCacheKey(handlerInfo.Handler, handlerInfo.Callback.Name);
                    cacheDriver.Cache(key, data);

                    Dumper.Log(TAG, 
                        string.Format("Store the data of route {0} to cache file via {1} key", routeInfo.Route, key));
                }
            }

            return data;          
        }

        private bool initHandlerInstance(RouteInfo routeInfo, RouteDispatchInfo dispatchInfo, out object instance)
        {
            instance = reflectHelper.GetInstance(routeInfo.HandlerInfo.Handler);
            if (instance == null)
            {
                Dumper.Log(TAG, "Cannot create the instance of the handle class :" + routeInfo.HandlerInfo.Handler.FullName);
                return false;
            }

            // Retrieve the bridage variant from the class
            if (routeInfo.HandlerInfo.Transport == null)
            {
                routeInfo.HandlerInfo.Transport = reflectHelper.GetPropertyInfo(routeInfo.HandlerInfo.Handler, "Request");
            }

            routeInfo.HandlerInfo.Transport.SetValue(instance, dispatchInfo);

            return true;
        }

    }
}
