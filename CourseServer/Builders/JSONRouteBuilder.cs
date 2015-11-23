using CourseServer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseServer.Framework;
using Newtonsoft.Json.Linq;
using CourseServer.Utils;
using CourseProvider;

namespace CourseServer.Builders
{
    /// <summary>
    /// {'_route' : 'user/login', '_param' : { 'name' : 'username', 'pass' : 'password' }, 
    /// '_generic' : { 'auth' : 'someSecretKey', 'other' : 'otherValue' }}
    /// </summary>
    public class JSONRouteBuilder : IRouteBuilder
    {
        public const string TAG = "JSONRouteBuilder";        

        private string jsonData;

        public JSONRouteBuilder(string jsonData)
        {
            this.jsonData = jsonData;
        }

        public RouteDispatchInfo buildDispatchInfo()
        {
            JObject jObj = null;

            try
            {
                jObj = JObject.Parse(jsonData);
            } catch (Exception e)
            {
                Dumper.Log(TAG, "An error occured when formating the JSON data, it may due to the invalid format: " + e.Message);
                Dumper.Log(TAG, "Raw request data represent: " + jsonData);
                return null;
            }

            if (jObj == null)
            {
                Dumper.Log(TAG, "No request data represent.");
                return null;
            }

            string route = jObj[CourseProviderContract.KEY_ROUTE].ToString();
            // Empty route
            if (TextUtils.isEmpty(route))
            {
                Dumper.Log(TAG, "No route unique string represent.");
                return null;
            }

            Dictionary<string, object> args = null, generic = null;

            // Parse the arguments
            if (jObj[CourseProviderContract.KEY_PARAM] != null)
            {
                args = jObj[CourseProviderContract.KEY_PARAM].ToObject<Dictionary<string, object>>();
            }

            // Parse the generic params
            if (jObj[CourseProviderContract.KET_GENERIC] != null)
            {
                generic = jObj[CourseProviderContract.KET_GENERIC].ToObject<Dictionary<string, object>>();
            }

            return new RouteDispatchInfo(route, args, generic);
        }
    }
}
