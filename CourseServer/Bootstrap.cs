using ConfigurationLib;
using CourseServer.Contract;
using CourseServer.Framework;
using CourseServer.Middlewares;
using CourseServer.Utils;
using System;

namespace CourseServer
{
    public class Bootstrap
    {
        public const string TAG = "Bootstrap";

        private static bool IsReboot = false;

        private static Server courseServer = null;

        /// <summary>
        /// The real entry point of program
        /// </summary>
        public static void Load(Action<UserConfiguration> register)
        {
            if (IsReboot)
            {
                if (!PlatformCompact.Checkout())
                {
                    return;
                }

                IsReboot = true;
            }
            else
            { 
                Release();
            }

            // Load the configuration and booting the server.
            UserConfiguration config = new ConfigurationLoader().Load();
            
            Session.Init(config.SessionLifetime);

            IDispatcher dispatcher = new DispatcherImpl();
            dispatcher.SetCacheDriver(new FileSystemCacheDriver());

            courseServer = new Server(config.ServerInfo);
            courseServer.AddHandleListener(dispatcher.Handle);

            register(config);

            courseServer.Start();
        }
        
        public static void ReLoad()
        {
            Load(null);
        }

        public static void Release()
        {
            if (courseServer != null && courseServer.isRunning())
            {
                courseServer.Shutdown();
            }
        }
    }
}
