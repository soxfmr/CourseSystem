using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Providers.Advance
{
    public class UserManageProvider : Provider
    {
        public const int RC_GET_ALL = 0x1;

        public const int RC_CREATE = 0x2;

        public const int RC_RESET_PASSWORD = 0x3;

        public EventHandler<UserManageEventArgs> ProfileEvent;

        public void Create(string email, string user, string pass, 
            int mode, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user/store" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("email", email);
            carrier.ParamList.Add("user", user);
            carrier.ParamList.Add("pass", pass);
            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(RC_CREATE, carrier);
        }

        public void Remove(int id, int mode, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(carrier);
        }

        public void ResetPassword(int id, int mode, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user/reset" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(RC_RESET_PASSWORD, carrier);
        }

        public void Update(int id, int mode, string name, string avatar, 
            string cellphone, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("mode", mode);
            carrier.ParamList.Add("avatar", avatar);
            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("cellphone", cellphone);

            Bridge.Connect(carrier);
        }

        public void GetAllUser(int mode, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(RC_GET_ALL, carrier);
        }        

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Profile> profileList = null;
            string randomPwd = null;

            switch (e.RequestCode)
            {
                case RC_GET_ALL:
                    if (e.IsSuccess)
                    {
                        profileList = Parser.Serialize<List<Profile>>();
                    }
                    break;
                case RC_RESET_PASSWORD:
                    if (e.IsSuccess)
                    {
                        randomPwd = Parser.Serialize<string>();
                    }
                    break;
                default:
                    break;
            }


            if (ProfileEvent != null)
            {
                UserManageEventArgs userManageEventArgs = new UserManageEventArgs(profileList);
                userManageEventArgs.RandomPassword = randomPwd;
                userManageEventArgs.LoadEventArgs(e);

                ProfileEvent(this, userManageEventArgs);
            }
        }
    }
}
