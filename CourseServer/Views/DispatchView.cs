using CourseServer.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class DispatchView : View<Dispatch>
    {
        public override string Show(List<Dispatch> recordSet)
        {
            if (Empty(recordSet)) return Error();

            JObject obj;
            JArray jArray = new JArray();
            foreach (Dispatch dispatch in recordSet)
            {
                obj = new JObject();
                obj.Add(new JProperty("Name", dispatch.Course.Name));
                obj.Add(new JProperty("TeacherName", dispatch.Teacher.Name));
                obj.Add(new JProperty("Weekday", dispatch.Weekday));
                obj.Add(new JProperty("At", dispatch.At));
                obj.Add(new JProperty("Location", dispatch.Classroom.Location + " " + dispatch.Classroom.Number));
                obj.Add(new JProperty("Limit", dispatch.Limit));
                obj.Add(new JProperty("Current", dispatch.Current));

                jArray.Add(obj);
            }

            return Success(jArray);
        }
    }
}
