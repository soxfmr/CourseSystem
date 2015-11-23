using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class LoginEventArgs : BaseEventArgs
    {
        public string SessionId;

        public LoginEventArgs(string SessionId)
        {
            this.SessionId = SessionId;
        }
    }
}
