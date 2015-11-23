using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Contract
{
    public interface ICacheDriver
    {
        void Cache(string key, string data);

        string LoadCache(string key);

        bool isCached(string key);

        string GetCacheKey(Type classes, string methodName);
    }
}
