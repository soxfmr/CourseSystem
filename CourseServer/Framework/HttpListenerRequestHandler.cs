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
    public class HttpListenerRequestHandler : RequestHandler
    {
        public const string TAG = "HttpListenerRequestHandler";

        private HttpListenerRequest request;

        public HttpListenerRequestHandler(HttpListenerRequest request)
        {
            this.request = request;
        }

        public override string Format()
        {
            Stream iStream = request.InputStream;
            Encoding encoding = request.ContentEncoding;

            if (iStream == null || !iStream.CanRead)
                return null;

            // Default to UTF-8
            if (encoding == null)
                encoding = Encoding.UTF8;

            int len = 0;
            char[] buffer = new char[1024];

            StringBuilder sBuilder = new StringBuilder();

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(iStream, encoding);
                while ((len = reader.ReadBlock(buffer, 0, 1024)) != 0)
                {
                    sBuilder.Append(new string(buffer, 0, len));
                }
            }
            catch (Exception e)
            {
                Dumper.Log(TAG, "An error occur when format the request data: " + e.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (iStream != null && iStream.CanRead)
                {
                    request.InputStream.Close();
                }
            }

            return sBuilder.ToString();
        }

        /*public override DispatchSource GetDispatchSource()
        {
            if (TextUtils.isEmpty(request.UserHostAddress))
                return null;

            string[] sources = ParseAddress(request.UserHostAddress);

            return new DispatchSource { IP = sources[0], Port = int.Parse(sources[1]) };
        }*/

        public override DispatchSource GetDispatchSource()
        {
            return new DispatchSource { IP = request.RemoteEndPoint.Address.ToString(),
                Port = request.RemoteEndPoint.Port };
        }

        private string[] ParseAddress(string sources)
        {
            // It seems IPv6
            if (sources.Contains("]"))
            {
                int index = sources.LastIndexOf(":");
                return new string[] { sources.Substring(0, index),
                    sources.Substring(index + 1, sources.Length - index - 1) };
            }

            return sources.Split(':');
        }
    }
}
