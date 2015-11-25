using CourseServer.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class AbsenceView : View<Dictionary<string, object>>
    {
        public override string Show(List<Dictionary<string, object>> recordSet)
        {
            if (Empty(recordSet)) return Success();

            JToken jArray = JToken.FromObject(recordSet);

            return Success(jArray);
        }
    }
}
