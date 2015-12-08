using CourseProvider;
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

                MiddlewareRegister.Add("auth", new AuthMiddleware(), priority : 9);
                MiddlewareRegister.Add("studentACL", new AccessControlMiddleware(CourseProviderContract.MODE_STUDENT));
                MiddlewareRegister.Add("teacherACL", new AccessControlMiddleware(CourseProviderContract.MODE_TEACHER));

                MiddlewareRegister.Register(typeof(CourseController), "auth", "Index");
                MiddlewareRegister.Register(typeof(DispatchController), "auth", "Index", "DispatchStudent");
                MiddlewareRegister.Register(typeof(AuthController), "auth", "OnLogout");
                MiddlewareRegister.Register(typeof(UserController), "auth", "Profile", "Update", 
                    "CreateDispatch", "ShowDispatch", "RemoveDispatch");
                MiddlewareRegister.Register(typeof(AbsenceController), "auth", "Index", "AllChangeableAbsence",
                    "Store", "Update", "Destroy");
                MiddlewareRegister.Register(typeof(AttendanceController), "auth", "Index");
                // Access Control List for student
                MiddlewareRegister.Register(typeof(UserController), "studentACL", "CreateDispatch", "RemoveDispatch");
                MiddlewareRegister.Register(typeof(AbsenceController), "studentACL", "Index", "AllChangeableAbsence", "Store", "Update", "Destroy");
                MiddlewareRegister.Register(typeof(AttendanceController), "studentACL", "Index");
                // Access Control List for teacher
                MiddlewareRegister.Register(typeof(AbsenceController), "teacherACL", "GetAuditableAbsence", "AuditAbsence");
                MiddlewareRegister.Register(typeof(AttendanceController), "teacherACL", "CourseAttendance", "Store", "Destroy", "AddStudentAbsence");
                MiddlewareRegister.Register(typeof(DispatchController), "teacherACL", "DispatchStudent");

                Route.Add("/login", "AuthController@OnLogin", "email,pass,mode");
                Route.Add("/register", "AuthController@OnRegister", "email,user,pass");
                Route.Add("/logout", "AuthController@OnLogout");

                Route.Add("/dispatch", "DispatchController@Index");

                Route.Add("/dispatch/student", "DispatchController@DispatchStudent");

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
                // Absence manager for teacher
                Route.Add("/teacher/absence", "AbsenceController@GetAuditableAbsence");
                Route.Add("/teacher/absence/audit", "AbsenceController@AuditAbsence", "id");
                // Attendance manager for teacher
                Route.Add("/teacher/attendance", "AttendanceController@CourseAttendance");
                Route.Add("/teacher/attendance/create", "AttendanceController@Store", "dispatchId,population");
                Route.Add("/teacher/attendance/destroy", "AttendanceController@Destroy", "id");
                Route.Add("/teacehr/attendance/student/add", "AttendanceController@AddStudentAbsence", "type,studentId,dispatchId");
            });

            Console.CancelKeyPress += (sender, e) =>
            {
                Bootstrap.Release();

                Console.ReadKey();
            };
        }
    }
}
