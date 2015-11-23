using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class RegisterView : View<int>
    {
        public override string Show(List<int> recordSet)
        {
            return Error(recordSet[0]);
        }
    }
}
