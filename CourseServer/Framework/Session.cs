using CourseServer.Entities;
using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;

namespace CourseServer.Framework
{
    public class Session
    {
        public static ConcurrentDictionary<string, SessionInfo> UserSessions = new ConcurrentDictionary<string, SessionInfo>();

        public static int Lifetime { get; set; }

        public static void Init(int lifetime)
        {
            if (lifetime <= 0)
                throw new ArgumentException("Invalid time of session life: " + lifetime);

            Lifetime = lifetime;
        }

        /// <summary>
        /// Add a new session to the session pool
        /// </summary>
        /// <param name="si"></param>
        /// <returns></returns>
        public static string Add(SessionInfo si)
        {
            if (si == null || !si.IsValid)
                return null;

            si.DueDate = DateTime.Now.AddMinutes(Lifetime > 0 ? 
                Lifetime : GlobalSettings.DEFAULT_SESSION_LIFETIME);
                        
            string sessionId = GenericUtils.GetUniqueString();

            UserSessions.TryAdd(sessionId, si);

            return sessionId;
        }

        /// <summary>
        /// Determind that current user session already in the session pool or not 
        /// </summary>
        /// <param name="sessionId">The session id generate by Add method</param>
        /// <returns></returns>
        public static bool Has(string sessionId)
        {
            SessionInfo si = null;
            if (! UserSessions.TryGetValue(sessionId, out si))
            {
                return false;
            }

            return si.IsValid;
        }

        public static void Remove(string sessionId)
        {
            SessionInfo si;

            if (! UserSessions.TryGetValue(sessionId, out si))
            {
                return;
            }

            UserSessions.TryRemove(sessionId, out si);
        }

        /// <summary>
        /// Retrive a session via session id
        /// </summary>
        /// <param name="sessionId">The session id generate by Add method</param>
        /// <returns></returns>
        public static SessionInfo Retrieve(string sessionId)
        {
            SessionInfo si;

            if (!UserSessions.TryGetValue(sessionId, out si) || ! si.IsValid)
            {
                return null;
            }

            return si;
        }

        /// <summary>
        /// Remove the session which is out of due date from session pool
        /// </summary>
        public static void Fresh()
        {
            if (UserSessions.Count > 0)
            {
                List<string> removeList = new List<string>();
                foreach (var session in UserSessions)
                {
                    if (session.Value.DueDate <= DateTime.Now)
                    {
                        session.Value.UserEntity = null;
                        removeList.Add(session.Key);
                    }
                }

                SessionInfo si;
                foreach (var sessionId in removeList)
                {
                    UserSessions.TryRemove(sessionId, out si);
                }
            }
        }
    }
}
