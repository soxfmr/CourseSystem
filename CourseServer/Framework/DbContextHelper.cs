using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class DbContextHelper
    {
        public const string TAG = "DbContextHelper";

        public const int DEFAULT_TIMEOUT = 8;

        private static Type dbContext;

        private static string connectionString;

        private static int timeout;

        public static void Init(Type dbContext, string connectionString, int timeout)
        {
            Type requireType = typeof(DbContext);

            if (!dbContext.IsSubclassOf(requireType))
            {
                Dumper.Log(TAG, "The context of database class must extend from " + requireType.FullName);
                return;
            }

            if (TextUtils.isEmpty(connectionString))
            {
                Dumper.Log(TAG, "Empty connection string.");
                return;
            }

            ReflectHelper reflectHelper = new ReflectHelper();
            if (!reflectHelper.HasConstructor(dbContext, new Type[] { typeof(string), typeof(int) }))
            {
                Dumper.Log(TAG, "Cannot found the constructor with a string type of argument " + 
                    "in the context of database classes " + dbContext.FullName);
                return;
            }

            if (timeout <= 0)
            {
                timeout = DEFAULT_TIMEOUT;

                Dumper.Log(TAG, "Invalid timeout value for the database connection, set up to the default value " + 
                    DEFAULT_TIMEOUT);
            }

            DbContextHelper.dbContext = dbContext;
            DbContextHelper.connectionString = connectionString;
            DbContextHelper.timeout = timeout;
        }

        public static DbContext NewInstance()
        {
            if (dbContext == null || TextUtils.isEmpty(connectionString))
                throw new ArgumentNullException("The context of database classes has no specified.");

            return (DbContext) Activator.CreateInstance(dbContext, connectionString, timeout);
        }
    }
}
