using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;

namespace CourseServer.Utils
{
    public class Guard
    {
        public static string Encrypt(string origin)
        {
            origin = Mixed(origin);

            return BCryptHelper.HashPassword(origin, BCryptHelper.GenerateSalt());
        }

        public static bool IsMatched(string origin, string hashed)
        {
            origin = Mixed(origin);

            return BCryptHelper.CheckPassword(origin, hashed);
        }

        private static string Mixed(string arg0)
        {
            if (!TextUtils.isEmpty(GlobalSettings.APPKEY))
            {
                arg0 += GlobalSettings.APPKEY;
            }

            return arg0;
        }
    }
}
