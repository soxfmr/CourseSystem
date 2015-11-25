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
                MiddlewareRegister.Register(typeof(UserController), "auth", "Profile", "Update", 
                    "CreateDispatch", "ShowDispatch", "RemoveDispatch");
                MiddlewareRegister.Register(typeof(AbsenceController), "auth", "Index", "AllChangeableAbsence",
                    "Store", "Update", "Destroy");
                MiddlewareRegister.Register(typeof(AttendanceController), "auth", "Index");

                Route.Add("/login", "AuthController@OnLogin", "email,pass,mode");
                Route.Add("/register", "AuthController@OnRegister", "email,user,pass");
                Route.Add("/logout", "AuthController@OnLogout");

                Route.Add("/dispatch", "DispatchController@Index");

                Route.Add("/user/attendance", "AttendanceController@Index");

                Route.Add("/user/profile", "UserController@Profile");
                Route.Add("/user/update", "UserController@Update", "name,avatar,cellphone,newPwd,pwdConfirm,originPwd");
                // Dispatch for user
                Route.Add("/user/dispatch", "UserController@ShowDispatch");
                Route.Add("/user/dispatch/create", "UserController@CreateDispatch", "id");
                Route.Add("/user/dispatch/remove", "UserController@RemoveDispatch", "id");
                // Attendance status for user
                Route.Add("/user/absence", "AbsenceController@Index");
                Route.Add("/user/absence/changeable", "AbsenceController@AllChangeableAbsence");
                Route.Add("/user/absence/store", "AbsenceController@Store", "reason,dispatchId");
                Route.Add("/user/absence/update", "AbsenceController@Update", "reason,id");
                Route.Add("/user/absence/destroy", "AbsenceController@Destroy", "id");
            });

            Console.CancelKeyPress += (sender, e) =>
            {
                Bootstrap.Release();

                Console.ReadKey();
            };
        }
    }
}
