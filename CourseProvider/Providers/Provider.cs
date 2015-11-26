using CourseProvider.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers
{
    public abstract class Provider
    {
        protected ProviderBridge Bridge;

        protected ProviderParser Parser;

        public Provider()
        {
            Bridge = new ProviderBridge();
            Parser = new ProviderParser();

            Bridge.ProviderLoadedEvent += ProviderLoaded;
        }

        public virtual void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            Parser.Payload = e.Payload;

            e.IsSuccess = Parser.IsSuccess;
            // Try to extract the error messages
            e.ErrorMessage = Parser.GetErrorMessage();

            e.ErrorCode = Parser.ErrorCode;
        }
    }
}
