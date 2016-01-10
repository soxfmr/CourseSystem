using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;

namespace CourseProvider.Providers.Advance
{
    public class CourseProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_CREATE = 0x2;

        public EventHandler<CourseEventArgs> CourseEvent;

        public void Create(string name, string desc, int majorId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/course/store" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("desc", desc);
            carrier.ParamList.Add("majorId", majorId);

            Bridge.Connect(RC_CREATE, carrier);
        }

        public void Remove(int id, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/course/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);

            Bridge.Connect(carrier);
        }

        public void Update(int id, string name, string desc, int majorId, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/course/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("desc", desc);
            carrier.ParamList.Add("majorId", majorId);

            Bridge.Connect(carrier);
        }

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/course" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Course> courseList = null;

            switch (e.RequestCode)
            {
                case RC_GET_ALL:
                    if (e.IsSuccess)
                    {
                        courseList = Parser.Serialize<List<Course>>();
                    }
                    break;
                default:
                    break;
            }


            if (CourseEvent != null)
            {
                CourseEventArgs courseEventArgs = new CourseEventArgs(courseList);
                courseEventArgs.LoadEventArgs(e);

                CourseEvent(this, courseEventArgs);
            }
        }
    }
}
