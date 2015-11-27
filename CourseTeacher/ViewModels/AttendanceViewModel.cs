using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseTeacher.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTeacher.ViewModels
{
    public class AttendanceViewModel : LayerBaseViewModel
    {
        private ObservableCollection<CourseAttendance> courseAttendanceList;
        public ObservableCollection<CourseAttendance> CourseAttendanceList
        {
            get
            {
                return courseAttendanceList;
            }
            set
            {
                courseAttendanceList = value;
                NotifyPropertyChanged("CourseAttendanceList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private CourseAttendanceProvider Provider;

        public AttendanceViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new CourseAttendanceProvider();
            Provider.CourseAbsenceEvent = CourseAttendanceLoadedEvent;
        }

        public override void Notify()
        {
            GetAllCourseAttendance();

            NotifyChildViewModel();
        }

        public void GetAllCourseAttendance()
        {
            Provider.GetAll(SessionId);
        }

        public void CourseAttendanceLoadedEvent(object sender, CourseAttendanceEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case AttendanceProvider.RC_GET_ALL:
                        CourseAttendanceList = e.CourseAttendanceList != null ? 
                            new ObservableCollection<CourseAttendance>(e.CourseAttendanceList) : null;
                        break;
                    default:
                        break;
                }
            }
        }

        public ActionCommand ShowAttendanceComposeViewCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    Container.Show(ChildViewList["attendanceCompose"].View);
                });
            }
        }

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetAllCourseAttendance());
            }
        }
    }
}
