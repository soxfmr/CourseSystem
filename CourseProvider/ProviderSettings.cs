using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider
{
    public static class ProviderSettings
    {
        public static string ServerScheme = "http";

        public static string ServerHost = "localhost";

        public static int ServerPort = 2333;

        public static Encoding ServerEncoding = Encoding.UTF8;

        public new static string ToString()
        {
            return string.Format("{0}://{1}:{2}/", ServerScheme, ServerHost, ServerPort);
        }
    }
}
