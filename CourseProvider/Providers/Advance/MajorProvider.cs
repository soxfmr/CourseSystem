using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;

namespace CourseProvider.Providers.Advance
{
    public class MajorProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_CREATE = 0x2;

        public EventHandler<MajorEventArgs> MajorEvent;

        public void Create(string name, string desc, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/major/store" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("desc", desc);

            Bridge.Connect(RC_CREATE, carrier);
        }

        public void Remove(int id, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/major/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);

            Bridge.Connect(carrier);
        }

        public void Update(int id, string name, string desc, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/major/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("desc", desc);

            Bridge.Connect(carrier);
        }

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/major" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Major> majorList = null;

            switch (e.RequestCode)
            {
                case RC_GET_ALL:
                    if (e.IsSuccess)
                    {
                        majorList = Parser.Serialize<List<Major>>();
                    }
                    break;
                default:
                    break;
            }


            if (MajorEvent != null)
            {
                MajorEventArgs majorEventArgs = new MajorEventArgs(majorList);
                majorEventArgs.LoadEventArgs(e);

                MajorEvent(this, majorEventArgs);
            }
        }
    }
}
