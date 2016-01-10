using ConfigurationLib.Model;
using CourseServer.Events;
using CourseServer.Utils;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace CourseServer.Framework
{
    public class Server
    {
        public const string TAG = "Server";

        public const string SCHEME_HTTP = "http";
        public const string SCHEME_HTTPS = "https";

        public const int PORT_MAX = 65535;
        public const int PORT_MIN = 0;

        private static bool RUN = false;

        private HttpListener httpListener;

        private Thread threadHandle = null;
        private Thread threadSessionDetect = null;

        private event EventHandler<HandleEventArgs> handleListener;

        public Server(ServerInfo serverInfo)
        {            
            if (serverInfo == null)
                throw new ArgumentNullException("The ServerInfo should'n be null.");

            if (TextUtils.isEmpty(serverInfo.Scheme) || serverInfo.Scheme.ToLower() != SCHEME_HTTP &&
                serverInfo.Scheme.ToLower() != SCHEME_HTTPS)
            {
                throw new ArgumentException("Invalid scheme for the server: " + serverInfo.Scheme);
            }
            
            if (TextUtils.isEmpty(serverInfo.Hostname))
                throw new ArgumentException("Invalid hostname for the server: " + 
                    serverInfo.Hostname);

            if (serverInfo.Port < PORT_MIN || serverInfo.Port > PORT_MAX)
                throw new ArgumentException("Invalid port for the server: " + 
                    serverInfo.Port);

            string str = serverInfo.ToString();

            httpListener = new HttpListener();
            httpListener.Prefixes.Add(serverInfo.ToString());

            Dumper.Log(TAG, "Listen on: " + serverInfo.ToString());
        }

        public void AddHandleListener(EventHandler<HandleEventArgs> e)
        {
            handleListener += e;
        }

        public void RemoveHandleListener(EventHandler<HandleEventArgs> e)
        {
            handleListener -= e;
        }

        public bool isRunning()
        {
            return httpListener != null && httpListener.IsListening &&
                threadHandle != null && threadHandle.IsAlive;
        }

        public void Start()
        {
            if (isRunning())
                return;

            httpListener.Start();
            Dumper.Log(TAG, "Server started.");

            triggerLooper(true);

            threadHandle = new Thread(new ThreadStart(Accept));
            threadHandle.Start();
            Dumper.Log(TAG, "The handle thread has been created.");

            threadSessionDetect = new Thread(new ThreadStart(SessionLifetimeDetect));
            threadSessionDetect.Start();
            Dumper.Log(TAG, "The thread to detect the lifetime of session has been created.");
        }

        public void Shutdown()
        {
            if (httpListener != null && httpListener.IsListening)
            {
                httpListener.Stop();
                httpListener.Close();
            }

            if (threadHandle != null && threadHandle.IsAlive)
            {
                triggerLooper(false);
                threadHandle.Interrupt();

                Dumper.Log(TAG, "The handle thread has been released.");
            }

            if (threadSessionDetect != null && threadSessionDetect.IsAlive)
            {
                triggerLooper(false);
                threadSessionDetect.Interrupt();

                Dumper.Log(TAG, "The thread to detect the lifetime of session has been released.");
            }

            Dumper.Log(TAG, "Server shutdown.");
        }

        private void SessionLifetimeDetect()
        {
            while (RUN)
            {
                try
                {
                    Session.Fresh();
                    // Trigger the event at the next due date time
                    Thread.Sleep(Session.Lifetime * 60 * 1000);
                }
                catch (Exception e)
                {
                    Dumper.Log(TAG, "Error occured when fresh the session, " +
                        "may this thread has been intrrupted: " + e.Message);
                }
            }
        }

        private void Accept()
        {
            HttpListenerContext context = null;
            while (RUN)
            {
                try
                {
                    context = httpListener.GetContext();
                    // Add a new sesion into thread pool for the incoming connection
                    if (context != null)
                    {
                        ThreadPool.QueueUserWorkItem(newSession, context);
                        Dumper.Log(TAG, "Client connected: " + context.Request.RemoteEndPoint);
                    }
                }
                catch (Exception e)
                {
                    Dumper.Log(TAG, "Error occured when retrieve the context, " + 
                        "may this thread has been intrrupted: " + e.Message);
                }
            }
        }

        private void newSession(object context)
        {
            // Catching the global exception to avoid the program crashing
            try
            {
                // Trigger the listener to dispatch the route
                handleListener(this, new HandleEventArgs((HttpListenerContext) context));

                Dumper.Log(TAG, "A new session has been generated in thread: " + Thread.CurrentThread.ManagedThreadId);
            } catch (Exception e)
            {
                Dumper.Log(TAG, "An error occured when handle the incoming request: " + e.Message);
            }
            finally
            {
                // Release the session if possible
                releaseSession((HttpListenerContext) context);
            }
        }

        private void releaseSession(HttpListenerContext context)
        {
            HttpListenerRequest request;
            HttpListenerResponse response;
            try
            {
                request = context.Request;
                response = context.Response;

                if (request != null && request.InputStream.CanRead)
                {
                    request.InputStream.Close();
                }

                if (response != null && response.OutputStream.CanWrite)
                {
                    response.OutputStream.Close();
                }
            } catch(Exception e)
            {
                Dumper.Log(TAG, "An error occured when try to release a session: " + e.Message);
            }
        }

        private void triggerLooper(bool run)
        {
            RUN = run;
        }
    }
}
