using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class ProviderLoadedEventArgs : BaseEventArgs
    {
        public ProviderLoadedEventArgs(int RequestCode, int Status, string Payload)
        {
            this.RequestCode = RequestCode;
            this.Status = Status;
            this.Payload = Payload;
        }
    }
}
