using CourseServer.Entities;
using CourseServer.Framework;
using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Helper
{
    public class Auth
    {
        public const string TAG = "Auth";

        public string SessionId { get; set; }

        public Auth() {}

        public Auth(string sessionId)
        {
            this.SessionId = sessionId;
        }

        /// <summary>
        /// Retrieve the session id of user from session pool via the unique field
        /// </summary>
        /// <param name="unique"></param
        /// <returns></returns>
        public string FindByUnique(string unique)
        {
            if (TextUtils.isEmpty(unique))
            {
                Dumper.Log(TAG, "Empty unique value.");
                return null;
            }

            foreach (var session in Session.UserSessions)
            {
                if (session.Value.IsValid && 
                    session.Value.UserEntity.Email == unique)
                {
                    Dumper.Log(TAG, string.Format("Session was found via unique {0}.", unique));
                    return session.Key;
                }
            }

            return null;
        }

        public UserEntity User()
        {
            return User(SessionId);
        }

        public UserEntity User(string sessionId)
        {
            if (TextUtils.isEmpty(sessionId))
            {
                return null;
            }

            SessionInfo info = Session.Retrieve(sessionId);
            if (info != null && info.IsValid)
            {
                return info.UserEntity;
            }

            return null;
        }
    }
}
