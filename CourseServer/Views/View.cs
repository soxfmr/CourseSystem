using CourseProvider;
using CourseServer.Builders;
using CourseServer.Contract;
using CourseServer.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public abstract class View<M>
    {
        /// <summary>
        /// Default error with the error code
        /// </summary>
        /// <returns></returns>
        public string Error()
        {
            return Error(CourseProviderContract.RESULT_FAILED);
        }

        /// <summary>
        /// Error with the custom error code
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public string Error(int errCode)
        {
            JObject responseData = new JObject();
            responseData.Add(new JProperty(CourseProviderContract.KEY_ERROR, errCode));

            return responseData.ToString();
        }

        /// <summary>
        /// Error with the message payload
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string Error(params string[] msg)
        {
            JObject responseData = new JObject();
            responseData.Add(new JProperty(CourseProviderContract.KEY_ERROR, CourseProviderContract.RESULT_FAILED));

            if (msg != null && msg.Length > 0)
            {
                JArray jArray = new JArray();
                foreach (string m in msg)
                {
                    jArray.Add(new JValue(m));
                }

                responseData.Add(CourseProviderContract.KEY_VALIDATOR, jArray);
            }

            return responseData.ToString();
        }

        /// <summary>
        /// Default successful response
        /// </summary>
        /// <returns></returns>
        public string Success()
        {
            return Success(jArray : null);
        }

        public string Success(JArray jArray)
        {
            JObject responseData = new JObject();
            responseData.Add(new JProperty(CourseProviderContract.KEY_ERROR, CourseProviderContract.RESULT_SUCCESS));

            if (jArray != null)
                responseData.Add(CourseProviderContract.KEY_PAYLOAD, jArray);

            return responseData.ToString();
        }

        public string Success(JProperty jProperty)
        {
            JObject responseData = new JObject();
            responseData.Add(new JProperty(CourseProviderContract.KEY_ERROR, CourseProviderContract.RESULT_SUCCESS));

            if (jProperty != null)
                responseData.Add(jProperty);

            return responseData.ToString();
        }

        public string Success(JObject jObject)
        {
            JObject responseData = new JObject();
            responseData.Add(new JProperty(CourseProviderContract.KEY_ERROR, CourseProviderContract.RESULT_SUCCESS));

            if (jObject != null)
                responseData.Add(jObject);

            return responseData.ToString();
        }

        public bool Empty(List<M> recordSet)
        {
            return recordSet == null || recordSet.Count == 0;
        }

        public string Show(M record)
        {
            List<M> list = new List<M>();
            list.Add(record);

            return Show(list);
        }

        public abstract string Show(List<M> recordSet);
    }
}
