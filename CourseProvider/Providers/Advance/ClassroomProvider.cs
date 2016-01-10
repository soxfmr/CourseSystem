using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers.Advance
{
    public class ClassroomProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_CREATE = 0x2;

        public EventHandler<ClassroomEventArgs> ClassroomEvent;

        public void Create(string location, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/classroom/store" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("location", location);

            Bridge.Connect(RC_CREATE, carrier);
        }

        public void Remove(int id, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/classroom/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);

            Bridge.Connect(carrier);
        }

        public void Update(int id, string location, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/classroom/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("location", location);

            Bridge.Connect(carrier);
        }

        public void GetAll(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/classroom" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_ALL, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Classroom> classroomList = null;

            switch (e.RequestCode)
            {
                case RC_GET_ALL:
                    if (e.IsSuccess)
                    {
                        classroomList = Parser.Serialize<List<Classroom>>();
                    }
                    break;
                default:
                    break;
            }


            if (ClassroomEvent != null)
            {
                ClassroomEventArgs classroomEventArgs = new ClassroomEventArgs(classroomList);
                classroomEventArgs.LoadEventArgs(e);

                ClassroomEvent(this, classroomEventArgs);
            }
        }
    }
}
