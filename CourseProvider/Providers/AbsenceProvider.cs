using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers
{
    public class AbsenceProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_GET_ALL_CHANGEABLE = 0x2;

        public const int RC_CREATE = 0x3;

        public EventHandler<AbsenceEventArgs> AbsenceEvent;

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/absence" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public void GetAllChangeable(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/absence/changeable" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL_CHANGEABLE, carrier);
        }

        public void Create(string reason, int dispatchId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/absence/store" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("reason", reason);
            carrier.ParamList.Add("dispatchId", dispatchId);

            Bridge.Connect(RC_CREATE, carrier);
        }

        public void Update(string reason, int reasonId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/absence/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("reason", reason);
            carrier.ParamList.Add("id", reasonId);

            Bridge.Connect(carrier);
        }

        public void Destroy(int reasonId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/absence/destroy" };
            carrier.AddAuth(sessionId);
            
            carrier.ParamList.Add("id", reasonId);

            Bridge.Connect(carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Absence> absenceList = null;

            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case RC_GET_ALL:
                    case RC_GET_ALL_CHANGEABLE:
                        absenceList = Parser.SerializeList<Absence>();
                        break;
                    default:
                        break;
                }
            }

            if (AbsenceEvent != null)
            {
                AbsenceEventArgs eventArgs = new AbsenceEventArgs(absenceList);
                eventArgs.LoadEventArgs(e);

                AbsenceEvent(this, eventArgs);
            }
        }
    }
}
