using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using System.Collections.ObjectModel;
using System.Linq;

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

        public void RemoveAttendance()
        {
            if (CourseAttendanceList == null || CourseAttendanceList.Count == 0)
            {
                return;
            }

            var preRemoveAttendance = CourseAttendanceList.Where(a => a.IsSelected).ToList();

            if (preRemoveAttendance.Count > 0 && DialogHelper.Conirm("确定删除选中课程考勤吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemoveAttendance)
                {
                    Provider.Destroy(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server or not
                    CourseAttendanceList.Remove(removed);
                }

                DialogHelper.Close();
            }
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
        
        public ActionCommand RemoveAttendanceCommand
        {
            get
            {
                return new ActionCommand(p => RemoveAttendance());
            }
        }


        #region SelectedCommand

        public ActionCommand SelectAllCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (CourseAttendanceList == null || CourseAttendanceList.Count == 0)
                        return;

                    foreach (var absence in CourseAttendanceList)
                    {
                        absence.IsSelected = true;
                    }
                });
            }
        }

        public ActionCommand ReverseCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (CourseAttendanceList == null || CourseAttendanceList.Count == 0)
                        return;

                    foreach (var absence in CourseAttendanceList)
                    {
                        absence.IsSelected = !absence.IsSelected;
                    }
                });
            }
        }

        #endregion 
    }
}
