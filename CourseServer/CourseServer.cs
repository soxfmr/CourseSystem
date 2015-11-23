using CourseServer.Controllers;
using CourseServer.Framework;
using CourseServer.Middlewares;
using System;

namespace CourseServer
{
    public static class CourseServer
    {
        [STAThread]
        static void Main()
        {
            Bootstrap.Load((config) =>
            {
                config.DatabaseInfo.Format = GlobalSettings.CONNECTION_STRING_SQLSERVER;
                // DbContextHelper.Init(typeof(CourseDbContext), config.DatabaseInfo.ToString());
                // Debug only
                DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 
                    config.DatabaseInfo.Timeout);

                MiddlewareRegister.Add("auth", new AuthMiddleware());

                MiddlewareRegister.Register(typeof(CourseController), "auth", "Index");
                MiddlewareRegister.Register(typeof(DispatchController), "auth", "Index");
                MiddlewareRegister.Register(typeof(AuthController), "auth", "OnLogout");
                MiddlewareRegister.Register(typeof(UserController), "auth", "Profile", "Update");

                Route.Add("/login", "AuthController@OnLogin", "email,pass,mode");
                Route.Add("/register", "AuthController@OnRegister", "email,user,pass");
                Route.Add("/logout", "AuthController@OnLogout");

                Route.Add("/courses", "CourseController@Index", true);

                Route.Add("/user/profile", "UserController@Profile");
                Route.Add("/user/update", "UserController@Update", "name,avatar,cellphone,newPwd,pwdConfirm,originPwd");
                // Attendance status for user
                Route.Add("/user/attendances", "AttendanceController@Index");
                // Dispatch for user
                Route.Add("/user/dispatches", "DispatchController@Index");
            });

            Console.CancelKeyPress += (sender, e) =>
            {
                Bootstrap.Release();

                Console.ReadKey();
            };
        }
    }
}
