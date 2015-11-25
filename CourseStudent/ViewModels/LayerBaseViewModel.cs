using CourseStudent.Domain;
using CourseStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStudent.ViewModels
{
    public abstract class LayerBaseViewModel : BaseViewModel
    {
        public Dictionary<string, ViewModelRelationship> ChildViewList;

        public LayerBaseViewModel()
        {
            ChildViewList = new Dictionary<string, ViewModelRelationship>();
        }

        public void AddChildView(string key, ViewModelRelationship relation)
        {
            ChildViewList.Add(key, relation);
        }

        /// <summary>
        /// Notify the child ViewModel to invoke the Notify method.
        /// </summary>
        public void NotifyChildViewModel()
        {
            foreach (var childView in ChildViewList)
            {
                if (childView.Value.ViewModel is INotifiedableView)
                {
                    (childView.Value.ViewModel as INotifiedableView).Notify();
                }
            }
        }
    }
}
