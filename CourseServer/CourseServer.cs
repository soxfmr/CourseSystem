using CourseProvider;
using CourseServer.Controllers;
using CourseServer.Controllers.Advance;
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
                DbContextHelper.Init(typeof(CourseDbContext), config.DatabaseInfo.ToString(), config.DatabaseInfo.Timeout);
                // Debug only
                /* DbContextHelper.Init(typeof(CourseDbContext), GlobalSettings.DATABASE.ConnectionString, 
                    config.DatabaseInfo.Timeout) */

                MiddlewareRegister.Add("auth", new AuthMiddleware(), priority : 9);
                MiddlewareRegister.Add("studentACL", new AccessControlMiddleware(CourseProviderContract.MODE_STUDENT));
                MiddlewareRegister.Add("teacherACL", new AccessControlMiddleware(CourseProviderContract.MODE_TEACHER));
                MiddlewareRegister.Add("managerACL", new AccessControlMiddleware(CourseProviderContract.MODE_MANAGER));
                
                MiddlewareRegister.Register(typeof(DispatchController),     "auth", "Index", "DispatchStudent");
                MiddlewareRegister.Register(typeof(AuthController),         "auth", "OnLogout");
                MiddlewareRegister.Register(typeof(UserController),         "auth", "Profile", "Update", 
                    "CreateDispatch", "ShowDispatch", "RemoveDispatch");
                MiddlewareRegister.Register(typeof(AbsenceController),      "auth", "Index", "AllChangeableAbsence",
                    "Store", "Update", "Destroy");
                MiddlewareRegister.Register(typeof(AttendanceController),   "auth", "Index");
                MiddlewareRegister.Register(typeof(UserManagerController),  "auth", "Store", "Profile", "Update", "Destroy", "AllUser", "ResetPassword");

                MiddlewareRegister.Register(typeof(ClassroomController),    "auth", "Store", "Update", "Destroy", "All");
                MiddlewareRegister.Register(typeof(MajorController),        "auth", "Store", "Update", "Destroy", "All");
                MiddlewareRegister.Register(typeof(CourseController),       "auth", "Store", "Update", "Destroy", "All");
                MiddlewareRegister.Register(typeof(DispatchManageController), "auth", "Store", "Update", "Destroy", "All");

                // Access Control List for student
                MiddlewareRegister.Register(typeof(UserController),         "studentACL", "CreateDispatch", "RemoveDispatch");
                MiddlewareRegister.Register(typeof(AbsenceController),      "studentACL", "Index", "AllChangeableAbsence", "Store", "Update", "Destroy");
                MiddlewareRegister.Register(typeof(AttendanceController),   "studentACL", "Index");
                // Access Control List for teacher
                MiddlewareRegister.Register(typeof(AbsenceController),      "teacherACL", "GetAuditableAbsence", "AuditAbsence");
                MiddlewareRegister.Register(typeof(AttendanceController),   "teacherACL", "CourseAttendance", "Store", "Destroy", "AddStudentAbsence");
                MiddlewareRegister.Register(typeof(DispatchController),     "teacherACL", "DispatchStudent");
                // Advanced Access Control List
                MiddlewareRegister.Register(typeof(UserManagerController),  "managerACL", "Store", "Profile", "Update", "Destroy", "AllUser", "ResetPassword");
                MiddlewareRegister.Register(typeof(ClassroomController),    "managerACL", "Store", "Update", "Destroy", "All");
                MiddlewareRegister.Register(typeof(MajorController),        "managerACL", "Store", "Update", "Destroy", "All");
                MiddlewareRegister.Register(typeof(CourseController),       "managerACL", "Store", "Update", "Destroy", "All");
                MiddlewareRegister.Register(typeof(DispatchManageController), "managerACL", "Store", "Update", "Destroy", "All");

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
                Route.Add("/teacher/attendance/create", "AttendanceController@Store", "dispatchId,absence");
                Route.Add("/teacher/attendance/destroy", "AttendanceController@Destroy", "id");
                Route.Add("/teacher/attendance/student/add", "AttendanceController@AddStudentAbsence", "type,studentId,dispatchId");

                // Advance function
                Route.Group("Advance", delegate
                {
                    Route.Add("/advance/user", "UserManagerController@AllUser", "mode");
                    Route.Add("/advance/user/profile", "UserManagerController@Profile", "id,mode");
                    Route.Add("/advance/user/store", "UserManagerController@Store", "email,user,pass,mode");
                    Route.Add("/advance/user/destroy", "UserManagerController@Destroy", "id,mode");
                    Route.Add("/advance/user/update", "UserManagerController@Update", "id,mode,name,avatar,cellphone");
                    Route.Add("/advance/user/reset", "UserManagerController@ResetPassword", "id,mode");

                    Route.Add("/advance/classroom", "ClassroomController@All");
                    Route.Add("/advance/classroom/destroy", "ClassroomController@Destroy", "id");
                    Route.Add("/advance/classroom/update", "ClassroomController@Update", "id,location");
                    Route.Add("/advance/classroom/store", "ClassroomController@Store", "location");

                    Route.Add("/advance/major", "MajorController@All");
                    Route.Add("/advance/major/destroy", "MajorController@Destroy", "id");
                    Route.Add("/advance/major/update", "MajorController@Update", "id,name,desc");
                    Route.Add("/advance/major/store", "MajorController@Store", "name,desc");

                    Route.Add("/advance/course", "CourseController@All");
                    Route.Add("/advance/course/destroy", "CourseController@Destroy", "id");
                    Route.Add("/advance/course/update", "CourseController@Update", "id,name,desc,majorId");
                    Route.Add("/advance/course/store", "CourseController@Store", "name,desc,majorId");

                    Route.Add("/advance/dispatch", "DispatchManageController@All");
                    Route.Add("/advance/dispatch/destroy", "DispatchManageController@Destroy", "id");
                    Route.Add("/advance/dispatch/update", "DispatchManageController@Update", "id,weekday,at,limit,teacherId,roomId");
                    Route.Add("/advance/dispatch/store", "DispatchManageController@Store", "weekday,at,limit,teacherId,courseId,roomId");
                });
            });

            Console.CancelKeyPress += (sender, e) =>
            {
                Bootstrap.Release();

                Console.ReadKey();
            };
        }
    }
}
