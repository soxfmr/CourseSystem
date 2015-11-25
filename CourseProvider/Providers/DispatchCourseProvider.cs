using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers
{
    public class DispatchCourseProvider : Provider
    {
        public const int RC_GET_USER_COUSR = 0x1;

        public const int RC_GET_AVAILABLE_COURSE = 0x2;

        public const int RC_JOIN_COURSE = 0x3;

        public const int RC_QUIT_COURSE = 0x4;

        public EventHandler<DispatchCourseEventArgs> DispatchCouseEvent;

        public EventHandler<AvailableCourseEventArgs> AvailableCourseEvent;

        public void GetUserCourse(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/dispatch" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_USER_COUSR, carrier);
        }

        public void JoinCourse(string sessionId, int id)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/dispatch/create" };
            carrier.AddAuth(sessionId);
            carrier.ParamList.Add("id", id);

            Bridge.Connect(RC_JOIN_COURSE, carrier);
        }

        public void QuitCourse(string sessionId, List<DispatchCourse> dispatchList)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/dispatch/remove" };
            carrier.AddAuth(sessionId);

            StringBuilder sBuilder = new StringBuilder();
            foreach (var item in dispatchList)
            {
                sBuilder.Append(item.Id);
                sBuilder.Append(",");
            }
            string payload = sBuilder.ToString();
            // Remove the comma
            carrier.ParamList.Add("id", payload.Substring(0, sBuilder.Length - 1));

            Bridge.Connect(RC_QUIT_COURSE, carrier);
        }

        public void GetAvaildableCourse(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/dispatch" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_AVAILABLE_COURSE, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<DispatchCourse> dispatchList = null;
            Dictionary<string, List<DispatchCourse>> availableCourseList = null;

            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case RC_GET_USER_COUSR:
                        dispatchList = Parser.SerializeList<DispatchCourse>();
                        break;
                    case RC_GET_AVAILABLE_COURSE:
                        availableCourseList = Parser.SerializeDict<string, List<DispatchCourse>>();
                        break;
                    default:
                        break;
                }
            }

            if (DispatchCouseEvent != null)
            {
                DispatchCourseEventArgs eventArgs = new DispatchCourseEventArgs(dispatchList);
                eventArgs.LoadEventArgs(e);

                DispatchCouseEvent(this, eventArgs);
            }

            if (AvailableCourseEvent != null)
            {
                AvailableCourseEventArgs eventArgs = new AvailableCourseEventArgs(availableCourseList);
                eventArgs.LoadEventArgs(e);

                AvailableCourseEvent(this, eventArgs);
            }
        }
    }
}
