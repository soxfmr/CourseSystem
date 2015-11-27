using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers
{
    public class AuditAbsenceProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public EventHandler<AuditAbsenceEventArgs> AbsenceEvent;

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/teacher/absence" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public void AuditAbsence(int id, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/teacher/absence/audit" };
            carrier.AddAuth(sessionId);
            carrier.ParamList.Add("id", id);

            Bridge.Connect(carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<AuditAbsence> auditAbsenceList = null;

            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case RC_GET_ALL:
                        auditAbsenceList = Parser.SerializeList<AuditAbsence>();
                        break;
                    default:
                        break;
                }
            }

            if (AbsenceEvent != null)
            {
                AuditAbsenceEventArgs eventArgs = new AuditAbsenceEventArgs(auditAbsenceList);
                eventArgs.LoadEventArgs(e);

                AbsenceEvent(this, eventArgs);
            }
        }
    }
}
