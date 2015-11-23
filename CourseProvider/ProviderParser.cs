using System.Collections.Generic;
using CourseProvider.Utils;
using Newtonsoft.Json.Linq;

namespace CourseProvider
{
    public class ProviderParser
    {
        private JObject jObject;

        private string _Payload { get; set; }

        public string Payload {
            get { return _Payload; }
            set { _Payload = value; jObject = Serialize(value); }
        }

        public ProviderParser() : this(null) {}

        public ProviderParser(string json)
        {
            Payload = json;
        }

        public JObject Serialize(string json)
        {
            if (TextUtils.isEmpty(json))
            {
                return null;
            }

            return JObject.Parse(json);
        }

        public int ErrorCode
        {
            get
            {
                JToken token = GetToken(CourseProviderContract.KEY_ERROR);
                return token == null ? CourseProviderContract.RESULT_FAILED : token.Value<int>();
            }
        }

        public bool IsSuccess
        {
            get { return ErrorCode == CourseProviderContract.RESULT_SUCCESS; }
        }

        public string GetSessionId()
        {
            JToken token = GetToken(CourseProviderContract.KEY_AUTH);
            return token == null ? null : token.Value<string>();
        }

        public List<string> GetErrorMessage()
        {
            JToken token = GetToken(CourseProviderContract.KEY_VALIDATOR);
            if (token != null)
            {
                return token.ToObject<List<string>>();
            }

            return null;
        }

        public M Serialize<M>()
        {
            JToken token = GetPayloadToken();
            if (token != null)
            {
                List<M> PayloadList = token.ToObject<List<M>>();
                if (PayloadList != null && PayloadList.Count > 0)
                {
                    return PayloadList[0];
                }
            }

            return default(M);
        }

        public JToken GetPayloadToken()
        {
            return GetToken(CourseProviderContract.KEY_PAYLOAD);
        }

        public JToken GetToken(string propertyName)
        {
            if (jObject == null)
            {
                return null;
            }

            JToken token = null;
            jObject.TryGetValue(propertyName, out token);

            return token;
        }
    }
}
