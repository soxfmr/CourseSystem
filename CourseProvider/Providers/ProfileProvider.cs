using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProvider.Events;
using CourseProvider.Models;

namespace CourseProvider.Providers
{
    public class ProfileProvider : Provider
    {
        public const int RC_GET_PROFILE = 0x1;

        public EventHandler<ProfileEventArgs> ProfileEvent;

        public void GetProfile(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/profile" };
            carrier.AddAuth(sessionId);

            Bridge.Connect(RC_GET_PROFILE, carrier);
        }

        public void UpdateProfile(string sessionId, string avatar, string name, string cellphone,
            string newPwd, string pwdConfirm, string originPwd)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/user/update" };
            carrier.AddAuth(sessionId);

            carrier.ParamList.Add("avatar", avatar);
            carrier.ParamList.Add("name", name);
            carrier.ParamList.Add("cellphone", cellphone);
            carrier.ParamList.Add("newPwd", newPwd);
            carrier.ParamList.Add("pwdConfirm", pwdConfirm);
            carrier.ParamList.Add("originPwd", originPwd);

            Bridge.Connect(carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);
            
            Profile userProfile = null;

            switch (e.RequestCode)
            {
                case RC_GET_PROFILE:
                    if (e.IsSuccess)
                    {
                        userProfile = Parser.Serialize<Profile>();
                    }
                    break;
                default:
                    break;
            }


            if (ProfileEvent != null)
            {
                ProfileEventArgs profileEventArgs = new ProfileEventArgs(userProfile);
                profileEventArgs.LoadEventArgs(e);

                ProfileEvent(this, profileEventArgs);
            }
        }
    }
}
