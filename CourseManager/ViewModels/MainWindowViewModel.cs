using CourseProvider.Models;
using System.Collections.Generic;
using CommonLibrary.ViewModels;
using CommonLibrary.Domain;
using CommonLibrary.Models;
using CommonLibrary.Resources;

namespace CourseManager.ViewModels
{
    public class MainWindowViewModel : LayerBaseViewModel, IViewContainer
    {
        public string SessionId;

        private Profile userProfile;

        public Profile UserProfile {
            get
            {
                return userProfile;
            }
            set
            {
                userProfile = value;
                NotifyPropertyChanged("UserProfile");
            }
        }

        public List<MenuItem> MenuItemList { get; set; }

        public MainWindowViewModel(string sessionId)
        {
            SessionId = sessionId;

            CreateViewModelData();

            NotifyChildViewModel();
        }

        public void CreateViewModelData()
        {
            AttendanceViewModel attendanceCourseViewModel = new AttendanceViewModel(this, SessionId);
            AbsenceViewModel absenceViewModel = new AbsenceViewModel(SessionId);

            AddChildView("profile", new ViewModelRelationship(new ProfileViewModel(this, SessionId), new ProfileView()));
            AddChildView("course", new ViewModelRelationship(new DispatchCourseViewModel(SessionId), new CourseView()));
            AddChildView("attendance", new ViewModelRelationship(attendanceCourseViewModel, new AttendanceView()));
            AddChildView("absence", new ViewModelRelationship(absenceViewModel, new AbsenceView()));

            AddChildView("about", new ViewModelRelationship(new AboutViewModel(), new AboutView()));
            AddChildView("logout", new ViewModelRelationship(new LogoutViewModel(this, SessionId), null));

            attendanceCourseViewModel.AddChildView("attendanceCompose", new ViewModelRelationship(
                new AttendanceComposeViewModel(this, ChildViewList["attendance"], SessionId),
                new AttendanceComposeView()));

            MenuItemList = new List<MenuItem>();
            MenuItemList.Add(new MenuItem { Title = "个人信息", Icon = IconResources.ICON_USER, Relationship = ChildViewList["profile"] });
            MenuItemList.Add(new MenuItem { Title = "课程表", Icon = IconResources.ICON_COURSE, Relationship = ChildViewList["course"] });
            MenuItemList.Add(new MenuItem { Title = "课堂考勤", Icon = IconResources.ICON_ATTENDANCE, Relationship = ChildViewList["attendance"] });
            MenuItemList.Add(new MenuItem { Title = "请假条审核", Icon = IconResources.ICON_ABSENCE, Relationship = ChildViewList["absence"] });
            MenuItemList.Add(null);
            MenuItemList.Add(new MenuItem { Title = "关于", Icon = IconResources.ICON_ABOUT, Relationship = ChildViewList["about"] });
            MenuItemList.Add(new MenuItem { Title = "退出", Icon = IconResources.ICON_LOGOUT, Relationship = ChildViewList["logout"] });
        }

        #region View Container
        public ActionCommand ViewSwitchCommand { get; set; }

        public void Show(object view)
        {
            if (ViewSwitchCommand != null)
            {
                ViewSwitchCommand.Execute(view);
            }
        }
        #endregion
    }
}
