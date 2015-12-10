using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers
{
    public class CourseAttendanceProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_CREATE = 0x2;

        public EventHandler<CourseAttendanceEventArgs> CourseAbsenceEvent;

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/teacher/attendance" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public void Create(int dispatchId, int absence, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/teacher/attendance/create" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("dispatchId", dispatchId);
            carrier.ParamList.Add("absence", absence);

            Bridge.Connect(RC_CREATE, carrier);
        }
        
        public void Destroy(int id, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/teacher/attendance/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);

            Bridge.Connect(carrier);
        }

        public void AddStudent(string type, int studentId, int dispatchId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/teacher/attendance/student/add" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("type", type);
            carrier.ParamList.Add("studentId", studentId);
            carrier.ParamList.Add("dispatchId", dispatchId);

            Bridge.Connect(carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<CourseAttendance> courseAttendanceList = null;

            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case RC_GET_ALL:
                        courseAttendanceList = Parser.SerializeList<CourseAttendance>();
                        break;
                    default:
                        break;
                }
            }

            if (CourseAbsenceEvent != null)
            {
                CourseAttendanceEventArgs eventArgs = new CourseAttendanceEventArgs(courseAttendanceList);
                eventArgs.LoadEventArgs(e);

                CourseAbsenceEvent(this, eventArgs);
            }
        }
    }
}
