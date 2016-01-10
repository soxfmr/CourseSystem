using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;

namespace CourseProvider.Providers.Advance
{
    public class DispatchManageProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_CREATE = 0x2;

        public EventHandler<DispatchManageEventArgs> DispatchManageEvent;

        public void Create(string weekday, DateTime at,
            int limit,
            int teacherId, int courseId, int roomId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/dispatch/store" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("weekday", weekday);
            carrier.ParamList.Add("at", at);
            carrier.ParamList.Add("limit", limit);
            carrier.ParamList.Add("teacherId", teacherId);
            carrier.ParamList.Add("courseId", courseId);
            carrier.ParamList.Add("roomId", roomId);

            Bridge.Connect(RC_CREATE, carrier);
        }

        public void Remove(int id, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/dispatch/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);

            Bridge.Connect(carrier);
        }

        public void Update(int id, string weekday, DateTime at, 
            int limit,
            int teacherId, int roomId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/dispatch/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("weekday", weekday);
            carrier.ParamList.Add("at", at);
            carrier.ParamList.Add("limit", limit);
            carrier.ParamList.Add("teacherId", teacherId);
            carrier.ParamList.Add("roomId", roomId);

            Bridge.Connect(carrier);
        }

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/dispatch" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<DispatchManage> dispatcManageList = null;

            switch (e.RequestCode)
            {
                case RC_GET_ALL:
                    if (e.IsSuccess)
                    {
                        dispatcManageList = Parser.Serialize<List<DispatchManage>>();
                    }
                    break;
                default:
                    break;
            }


            if (DispatchManageEvent != null)
            {
                DispatchManageEventArgs dispatchEventArgs = new DispatchManageEventArgs(dispatcManageList);
                dispatchEventArgs.LoadEventArgs(e);

                DispatchManageEvent(this, dispatchEventArgs);
            }
        }
    }
}
