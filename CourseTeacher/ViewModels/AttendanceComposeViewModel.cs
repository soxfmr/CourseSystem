using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseTeacher.Domain;
using CourseTeacher.Helper;
using CourseTeacher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTeacher.ViewModels
{
    public class AttendanceComposeViewModel : BaseViewModel
    {
        public Dictionary<string, List<DispatchStudent>> courseSutdentMapList;
        public Dictionary<string, List<DispatchStudent>> CourseSutdentMapList
        {
            get
            {
                return courseSutdentMapList;
            }
            set
            {
                courseSutdentMapList = value;
                NotifyPropertyChanged("CourseSutdentMapList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ViewModelRelationship Parent;
        
        private DispatchStudentProvider dispatchStudentProvider;

        public AttendanceComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            dispatchStudentProvider = new DispatchStudentProvider();
            dispatchStudentProvider.DispatchStudentEvent += DispatchStudentLoadedEvent;
        }

        public override void Notify()
        {
            GetCourseStudentMap();
        }

        public void GetCourseStudentMap()
        {
            dispatchStudentProvider.GetAll(SessionId);
        }

        public void DispatchStudentLoadedEvent(object sender, DispatchStudentEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case DispatchCourseProvider.RC_GET_USER_COUSR:
                        CourseSutdentMapList = e.DispatchStudentList;
                        break;
                    default:
                        break;
                }
            }
        }

        #region EventCommand

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetCourseStudentMap());
            }
        }

        #endregion

        #region BackCommand

        public ActionCommand BackToPreviousCommand
        {
            get
            {
                return new ActionCommand(p => Container.Show(Parent.View));
            }
        }

        #endregion
    }
}
