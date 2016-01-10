using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class UserManageEventArgs : BaseEventArgs
    {
        public List<Profile> UserProfileList;

        public string RandomPassword;

        public UserManageEventArgs(List<Profile> userProfileList)
        {
            UserProfileList = userProfileList;
        }
    }
}
