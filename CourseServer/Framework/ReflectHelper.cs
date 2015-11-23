using CourseServer.Utils;
using System;
using System.Reflection;

namespace CourseServer.Framework
{
    public class ReflectHelper
    {
        public const string TAG = "ReflectHelper";

        private bool insecureReflect = false;

        private string globalNs = null;

        /// <summary>
        /// Set the global namespace which will be used when the class
        /// cannot be found in nomral. 
        /// </summary>
        /// <param name="globalNs"></param>
        public void setGlobalNamespace(string globalNs)
        {
            this.globalNs = globalNs;
        }
        
        public void setInsecureReflect(bool insecureReflect)
        {
            this.insecureReflect = insecureReflect;
        }

        public RouteHandlerInfo GetRouteHandler(string handler)
        {
            // Separate out the handle class and method
            string[] reflectInfo = handler.Split('@');

            // Invalid handler
            if (reflectInfo.Length < 2)
            {
                Dumper.Log(TAG, "Invalid route handler of string: " + handler);
                return null;
            }

            Type type = GetClassType(reflectInfo[0]);
            if (type == null) return null;

            MethodInfo method = GetMethodInfo(type, reflectInfo[1]);
            if (method == null) return null;

            ParameterInfo[] paramInfo = method.GetParameters();

            return new RouteHandlerInfo(type, method, paramInfo);
        }

        public Type GetClassType(string className)
        {
            // Try to give the type of the handler class 
            // without the global namesapce prefix
            Type type = Type.GetType(className);
            // Find with the global namespace prefix
            if (type == null && !TextUtils.isEmpty(globalNs))
            {
                string clsInfo = globalNs + "." + className;
                type = Type.GetType(clsInfo);
            }

            // Still found nothing, try to find it out from the all of classes.
            // This process may lead the insecure problem if the class which be
            // matching is unexpect.
            if (type == null && insecureReflect)
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                Type[] types = asm.GetTypes();

                foreach (Type t in types)
                {
                    // Find the first handle class which has the name as same as it
                    if (t.Name == className)
                    {
                        type = t;
                        break;
                    }
                }
            }

            // No luck
            if (type == null)
            {
                Dumper.Log(TAG, "Cannot found the handle classs: " + className);
            }

            return type;
        }

        public MethodInfo GetMethodInfo(Type classes, string methodName)
        {
            MethodInfo method = classes.GetMethod(methodName);
            if (method == null)
            {
                Dumper.Log(TAG, String.Format("Cannot found method {0} in handle class: {1}",
                    methodName, classes.Name));
            }

            return method;
        }

        public FieldInfo GetFieldInfo(Type classes, string fieldName)
        {
            FieldInfo field = classes.GetField(fieldName);
            if (field == null)
            {
                Dumper.Log(TAG, String.Format("Cannot found field {0} in handle class: {1}",
                    field, classes.Name));
            }

            return field;
        }

        public PropertyInfo GetPropertyInfo(Type classes, string propertyName)
        {
            PropertyInfo property = classes.GetProperty(propertyName);

            if (property == null)
            {
                Dumper.Log(TAG, String.Format("Cannot found property {0} in class: {1}",
                    property, classes.Name));
            }

            return property;
        }

        public bool HasConstructor(Type classes, params Type[] paramType)
        {
            return classes.GetConstructor(paramType) != null;
        }

        public object GetInstance(Type classes)
        {
            object instance = Activator.CreateInstance(classes);
            if (instance == null)
            {
                Dumper.Log(TAG, "Cannot create the instance from class: "
                    + classes.FullName);
            }

            return instance;
        }
    }
}
