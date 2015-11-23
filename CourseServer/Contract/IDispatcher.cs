using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CourseServer.Events;

namespace CourseServer.Contract
{
    public interface IDispatcher
    {
        void Handle(object sender, HandleEventArgs e);

        void SetCacheDriver(ICacheDriver cacheDriver);
    }
}
