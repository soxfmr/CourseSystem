using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers
{
    public class AttendanceProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;
        
        public EventHandler<AttendanceEventArgs> AbsenceEvent;

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/attendance" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Attendance> attendanceList = null;

            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case RC_GET_ALL:
                        attendanceList = Parser.SerializeList<Attendance>();
                        break;
                    default:
                        break;
                }
            }

            if (AbsenceEvent != null)
            {
                AttendanceEventArgs eventArgs = new AttendanceEventArgs(attendanceList);
                eventArgs.LoadEventArgs(e);

                AbsenceEvent(this, eventArgs);
            }
        }
    }
}
