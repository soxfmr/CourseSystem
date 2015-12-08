using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseTeacher.Domain;
using CourseTeacher.Helper;
using CourseTeacher.Models;
using System.Collections.Generic;

namespace CourseTeacher.ViewModels
{
    public class AttendanceComposeViewModel : BaseViewModel
    {
        public List<DispatchInfo> courseSutdentMapList;
        public List<DispatchInfo> CourseSutdentMapList
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
        private CourseAttendanceProvider courseAttendanceProvider;

        public AttendanceComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            dispatchStudentProvider = new DispatchStudentProvider();
            dispatchStudentProvider.DispatchStudentEvent += DispatchStudentLoadedEvent;

            courseAttendanceProvider = new CourseAttendanceProvider();
            courseAttendanceProvider.CourseAbsenceEvent += CourseAttendancetLoadedEvent;
        }

        public override void Notify()
        {
            GetCourseStudentMap();
        }

        public void GetCourseStudentMap()
        {
            dispatchStudentProvider.GetAll(SessionId);
        }

        public void CreateAttendance(object o)
        {
            if (o == null)
                return;

            int courseId = (int) o;
            
            foreach (var map in CourseSutdentMapList)
            {
                if (map.CourseId == courseId)
                {
                    map.Students.ForEach(s =>
                    {
                        courseAttendanceProvider.AddStudent("Absence Reason", 
                            s.StudentId, courseId, SessionId);
                    });

                    break;
                }
            }
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

        public void CourseAttendancetLoadedEvent(object sender, CourseAttendanceEventArgs e)
        {
            if (e.IsSuccess)
            {
            }
        }


        #region EventCommand

        public ActionCommand CreateAttendanceCommand
        {
            get
            {
                return new ActionCommand(CreateAttendance);
            }
        }

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
