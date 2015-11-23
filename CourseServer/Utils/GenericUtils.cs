using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CourseServer.Utils
{
    public class GenericUtils
    {
        public static string GetUniqueString()
        {
            string unique = DateTime.Now.ToString() + DateTime.Now.Millisecond + GlobalSettings.APPKEY;
            return GetHash(unique);
        }

        public static string GetHash(string input)
        {
            byte[] bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0, count = bytes.Length; i < count; i++)
            {
                sBuilder.Append(bytes[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();
        }

        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
