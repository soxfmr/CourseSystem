using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Utils
{
    public class Dumper
    {
        public const bool DEBUG_MODE = true;

        public static void Log(string tag, string log)
        {
            if (DEBUG_MODE)
                Console.WriteLine(string.Format("{0} {1}: {2}", GenericUtils.GetTimestamp(),
                    tag, log));
        }

        public static string MachineInfo()
        {
            return Environment.OSVersion.VersionString;
        }
    }
}
