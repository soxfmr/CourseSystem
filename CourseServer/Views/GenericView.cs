using CourseServer.Views;
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
            throw new NotImplementedException();
        }
    }
}
