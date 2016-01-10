using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class DispatchManageEventArgs : BaseEventArgs
    {
        public List<DispatchManage> DispatchManageList;

        public DispatchManageEventArgs(List<DispatchManage> dispatchManageList)
        {
            DispatchManageList = dispatchManageList;
        }
    }
}
