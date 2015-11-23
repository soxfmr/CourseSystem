using CourseProvider;
using CourseServer.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class LoginView : View<string>
    {
        public override string Show(List<string> recordSet)
        {
            return Success(new JProperty(CourseProviderContract.KEY_AUTH, recordSet[0]));
        }
    }
}
