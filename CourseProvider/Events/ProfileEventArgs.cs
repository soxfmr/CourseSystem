using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class ProfileEventArgs : BaseEventArgs
    {
        public Profile UserProfile;

        public ProfileEventArgs(Profile UserProfile)
        {
            this.UserProfile = UserProfile;
        }
    }
}
