using System;
using CourseProvider.Events;

namespace CourseProvider.Providers
{
    public class LoginProvider : Provider
    {
        public const int RC_LOGIN = 0x1;

        public const int RC_REGISTER = 0x2;

        public EventHandler<LoginEventArgs> LoginEvent;

        public void Login(string email, string password, int mode)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/login" };
            carrier.ParamList.Add("email", email);
            carrier.ParamList.Add("pass", password);
            carrier.ParamList.Add("mode", mode);

            Bridge.Connect(RC_LOGIN, carrier);
        }

        public void Logout(string sessionId)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/logout" };
            carrier.GenericList.Add(CourseProviderContract.KEY_AUTH, sessionId);

            Bridge.Connect(carrier);
        }

        public void Register(string email, string username, string password)
        {
            ProviderCarrier carrier = new ProviderCarrier() { Route = "/register" };
            carrier.ParamList.Add("email", email);
            carrier.ParamList.Add("user", username);
            carrier.ParamList.Add("pass", password);

            Bridge.Connect(RC_REGISTER, carrier);
        }

        public override void ProviderLoaded(object sender, ProviderLoadedEventArgs e)
        {
            base.ProviderLoaded(sender, e);

            string sessionId = null;
            switch (e.RequestCode)
            {
                case RC_LOGIN:
                    if (e.IsSuccess)
                    {
                        sessionId = Parser.GetSessionId();
                    }
                    break;
                default:
                    break;
            }

            if (LoginEvent != null)
            {
                LoginEventArgs eventArgs = new LoginEventArgs(sessionId);
                eventArgs.LoadEventArgs(e);

                LoginEvent(this, eventArgs);
            }
        }
    }
}
