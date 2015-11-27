using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTeacher.ViewModels
{
    public abstract class CommandViewModel : BaseViewModel
    {
        public abstract void Execute();
    }
}
