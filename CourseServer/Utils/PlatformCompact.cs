using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CourseServer.Utils
{
    public class PlatformCompact
    {
        public const string TAG = "PlatformCompact";

        /// <summary>
        /// Compatibility check
        /// </summary>
        /// <returns></returns>
        public static bool Checkout()
        {
            if (! isSupportHttpListener())
            {
                Dumper.Log(TAG, "The server required Windows XP SP2 or Windows Server 2003 later.");
                Dumper.Log(TAG, "You currently running on " + Dumper.MachineInfo());
                return false;
            }

            return true;
        }

        public static bool isSupportHttpListener()
        {
            return HttpListener.IsSupported;
        }
    }
}
