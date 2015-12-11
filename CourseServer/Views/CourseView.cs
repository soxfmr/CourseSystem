using CourseServer.Model;
using CourseServer.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class CourseView : View<Course>
    {
        public override string Show(List<Course> recordSet)
        {
            if (Empty(recordSet)) return Error();

            JObject obj;
            JArray jArray = new JArray();
            foreach(Course course in recordSet)
            {
                obj = new JObject();
                obj.Add(new JProperty("Id", course.Id));
                obj.Add(new JProperty("Name", course.Name));
                obj.Add(new JProperty("Description", course.Description));
                obj.Add(new JProperty("MajorName", course.Major.Name));
                obj.Add(new JProperty("TeacherName", course.Teacher.Name));

                jArray.Add(obj);
            }

            return Success(jArray);
        }
    }
}
