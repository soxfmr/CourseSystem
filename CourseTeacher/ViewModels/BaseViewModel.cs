using CourseTeacher.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CourseTeacher.ViewModels
{
    public abstract class BaseViewModel : ObservableObject, IDataErrorInfo,
        INotifiedableView
    {
        public string this[string propertyName]
        {
            get
            {
                return Validate(propertyName);
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public virtual void Notify() {}

        public virtual string Validate(string propertyName)
        {
            return null;
        }
    }
}
