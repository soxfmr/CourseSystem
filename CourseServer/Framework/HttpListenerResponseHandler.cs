using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    class HttpListenerResponseHandler : ResponseHandler
    {
        public new const string TAG = "HttpListenerResponseHandler";

        private HttpListenerResponse response;

        public HttpListenerResponseHandler(HttpListenerResponse response)
        {
            this.response = response;
        }

        public override void Response(int responseCode, byte[] data)
        {
            response.StatusCode = responseCode;
            response.ContentLength64 = data.Length;
            response.ContentEncoding = DefaultEncoding;

            RepresentData(response.OutputStream, data);
        }
    }
}
