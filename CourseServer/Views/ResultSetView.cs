using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class ResultSetView : View<Dictionary<string, object>>
    {
        public override string Show(List<Dictionary<string, object>> recordSet)
        {
            if (Empty(recordSet)) return Success();

            JToken result = JToken.FromObject(recordSet);

            return Success(result);
        }
    }
}
