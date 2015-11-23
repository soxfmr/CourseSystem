using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider
{
    public class ProviderCarrier
    {
        public string Route { get; set; }

        public Dictionary<string, object> ParamList { get; set; }

        public Dictionary<string, object> GenericList { get; set; }

        public ProviderCarrier()
        {
            ParamList = new Dictionary<string, object>();
            GenericList = new Dictionary<string, object>();
        }

        public void AddAuth(string sessionId)
        {
            GenericList.Add(CourseProviderContract.KEY_AUTH, sessionId);
        }

        public override string ToString()
        {
            JObject jObject = new JObject();
            jObject.Add(new JProperty(CourseProviderContract.KEY_ROUTE, Route));
            jObject.Add(new JProperty(CourseProviderContract.KEY_PARAM, JObject.FromObject(ParamList)));
            jObject.Add(new JProperty(CourseProviderContract.KET_GENERIC, JObject.FromObject(GenericList)));

            return jObject.ToString();
        }
    }
}
