﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Utils
{
    class TextUtils
    {
        public static bool isEmpty(string arg0)
        {
            return string.IsNullOrWhiteSpace(arg0);
        }

        public static int Find(string[] sources, string arg0)
        {
            int Ret = -1;

            if (sources == null || sources.Length == 0 || TextUtils.isEmpty(arg0))
                return Ret;

            for (int i = 0, len = sources.Length; i < len; i++)
            {
                if (arg0.Equals(sources[i], StringComparison.CurrentCultureIgnoreCase))
                {
                    Ret = i;
                    break;
                }
            }

            return Ret;
        }
    }
}
