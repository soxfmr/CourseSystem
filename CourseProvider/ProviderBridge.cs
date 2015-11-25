using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using CourseProvider.Utils;
using CourseProvider.Events;
using Newtonsoft.Json.Linq;

namespace CourseProvider
{
    public class ProviderBridge
    {
        private ProviderBridge Instance { get { return this; } }

        public EventHandler<ProviderLoadedEventArgs> ProviderLoadedEvent;

        /// <summary>
        /// Send the carrier to the server
        /// </summary>
        /// <param name="carrier"></param>
        public void Connect(ProviderCarrier carrier)
        {
            Connect(0, carrier);
        }

        /// <summary>
        /// Send the carrier to the server with the request code which will be passed to the callback function
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="carrier"></param>
        public void Connect(int requestCode, ProviderCarrier carrier)
        {
            Thread thread = new Thread(new ThreadStart(delegate 
            {
                _Connect(requestCode, carrier);
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        public void _Connect(int requestCode, ProviderCarrier carrier)
        {
            if (carrier == null)
            {
                Handle(new ProviderLoadedEventArgs(0, 0, null));
                return;
            }

            int status = 0;
            string json = null;
            HttpWebRequest request = null;
            HttpWebResponse respone = null;

            try
            {
                string uriScheme = ProviderSettings.ToString();
                string equipmentDesc = EquipmentUtils.EquipmentDescription();
                // Payload data to be send
                byte[] payload = Encoding.UTF8.GetBytes(carrier.ToString());

                request = (HttpWebRequest) WebRequest.Create(uriScheme);
                request.Method = "POST";
                request.ContentLength = payload.Length;
                request.UserAgent = equipmentDesc;

                WriteStream(request.GetRequestStream(), payload);

                respone = (HttpWebResponse) request.GetResponse();

                status = (int) respone.StatusCode;

                json = ReadStream(respone.GetResponseStream(), respone.ContentEncoding);
            }
            catch (Exception) {}

            Handle(new ProviderLoadedEventArgs(requestCode, status, json));
        }

        private void WriteStream(Stream oStream, byte[] payload)
        {
            if (oStream == null)
                return;

            try
            {
                oStream.Write(payload, 0, payload.Length);
                oStream.Flush();
            }
            finally
            {
                if (oStream != null)
                {
                    oStream.Close();
                }
            }
        }

        private string ReadStream(Stream iStream, string charset)
        {
            if (iStream == null)
                return null;

            Encoding encoding = ProviderSettings.ServerEncoding;
            StringBuilder sBuilder = new StringBuilder();

            // Try to retrieve the type of encoding from the request
            if (!TextUtils.isEmpty(charset))
            {
                try
                {
                    encoding = Encoding.GetEncoding(charset);
                }
                catch (Exception)
                {
                    encoding = ProviderSettings.ServerEncoding;
                }
            }

            try
            {
                int len = 0;
                byte[] buffer = new byte[4096];
                while ((len = iStream.Read(buffer, 0, 4096)) != 0)
                {
                    sBuilder.Append(encoding.GetChars(buffer, 0, len));
                }
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                }
            }

            return sBuilder.ToString();
        }

        private void Handle(ProviderLoadedEventArgs eventArgs)
        {
            if (ProviderLoadedEvent != null)
                ProviderLoadedEvent(Instance, eventArgs);
        }
    }
}
