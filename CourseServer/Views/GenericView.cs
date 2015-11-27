using CourseServer.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class GenericView : View<object>
    {
        public override string Show(List<object> recordSet)
        {
            if (Empty(recordSet)) return Success();

            JToken token = JToken.FromObject(recordSet);
            return Success(token);
        }
    }
}
