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

            Bridge.Connect(carrier);
        }

        public void Remove(int id, int mode, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user/destroy" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("id", id);
            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(carrier);
        }

        public void Update(int id, int mode, string name, string avatar, string cellphone,
            string newPwd, string pwdConfirm, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("avatar", avatar);
            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("cellphone", cellphone);
            carrier.ParamList.Add("newPwd", newPwd);
            carrier.ParamList.Add("pwdConfirm", pwdConfirm);

            Bridge.Connect(carrier);
        }

        public void GetAllUser(int mode, string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/advance/user" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(carrier);
        }        

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            List<Profile> profileList = null;

            switch (e.RequestCode)
            {
                case RC_GET_ALL:
                    if (e.IsSuccess)
                    {
                        profileList = Parser.SerializeList<Profile>();
                    }
                    break;
                default:
                    break;
            }


            if (ProfileEvent != null)
            {
                UserManageEventArgs profileEventArgs = new UserManageEventArgs(profileList);
                profileEventArgs.LoadEventArgs(e);

                ProfileEvent(this, profileEventArgs);
            }
        }
    }
}
