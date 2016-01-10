using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using System.Security.Cryptography;

namespace CourseServer.Utils
{
    public class Guard
    {
        private const string TABLE = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+";

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

        public static string GenerateRandomPassword()
        {
            StringBuilder sBuilder = new StringBuilder();

            char c = '0';
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                c = TABLE[random.Next(TABLE.Length)];

                sBuilder.Append(c);
            }

            return sBuilder.ToString();
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
