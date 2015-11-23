using System;
using System.IO;
using System.Text;
using CourseServer.Utils;

namespace CourseServer.Framework
{
    abstract class ResponseHandler
    {
        public const string TAG = "ResponseHandler";

        public Encoding DefaultEncoding { get { return Encoding.UTF8; } }

        public void NotFound()
        {
            Response(404);
        }

        public void Foribidden()
        {
            Response(403);
        }

        public void Response(int responseCode)
        {
            Response(responseCode, null);
        }

        /// <summary>
        /// Write out the data to the response stream
        /// </summary>
        /// <param name="oStream"></param>
        /// <param name="data"></param>
        public void RepresentData(Stream oStream, byte[] data)
        {         
            try
            {
                if (oStream != null && data != null && data.Length > 0)
                {
                    oStream.Write(data, 0, data.Length);
                    oStream.Flush();
                }
            }
            catch (Exception e)
            {
                Dumper.Log(TAG, "An error occur when represent the data to the client: " + e.Message);
            }
            finally
            {
                // We should close the connection stream otherwise the response data will
                // not be represent to the client side.
                if (oStream != null)
                    oStream.Close();
            }
        }

        public abstract void Response(int responseCode, byte[] data);
    }
}