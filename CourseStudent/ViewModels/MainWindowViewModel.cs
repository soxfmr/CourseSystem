using CourseProvider.Models;
using CourseStudent.Domain;
using CourseStudent.Models;
using CourseStudent.Resources;
using System.Collections.Generic;
using System;

namespace CourseStudent.ViewModels
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
            DispatchCourseViewModel dispatchCourseViewModel = new DispatchCourseViewModel(this, SessionId);
            AttendanceViewModel attendanceCourseViewModel = new AttendanceViewModel(this, SessionId);
            AbsenceViewModel absenceViewModel = new AbsenceViewModel(this, SessionId);

            AddChildView("profile", new ViewModelRelationship(new ProfileViewModel(this, SessionId), new ProfileView()));
            AddChildView("course", new ViewModelRelationship(dispatchCourseViewModel, new CourseView()));
            AddChildView("attendance", new ViewModelRelationship(attendanceCourseViewModel, new AttendanceView()));
            AddChildView("absence", new ViewModelRelationship(absenceViewModel, new AbsenceView()));
            AddChildView("about", new ViewModelRelationship(new AboutViewModel(), new AboutView()));
            AddChildView("logout", new ViewModelRelationship(new LogoutViewModel(this, SessionId), null));

            dispatchCourseViewModel.AddChildView("applyCourse", new ViewModelRelationship(
                new ApplyCourseViewModel(this, ChildViewList["course"], SessionId),
                new ApplyCourseView()));

            absenceViewModel.AddChildView("absenceCompose", new ViewModelRelationship(
                new AbsenceComposeViewModel(this, ChildViewList["absence"], SessionId),
                new AbsenceComposeView()));

            MenuItemList = new List<MenuItem>();
            MenuItemList.Add(new MenuItem { Title = "个人信息", Icon = IconResources.ICON_USER, Relationship = ChildViewList["profile"] });
            MenuItemList.Add(new MenuItem { Title = "选修课程", Icon = IconResources.ICON_COURSE, Relationship = ChildViewList["course"] });
            MenuItemList.Add(new MenuItem { Title = "课堂考勤", Icon = IconResources.ICON_ATTENDANCE, Relationship = ChildViewList["attendance"] });
            MenuItemList.Add(new MenuItem { Title = "请假条", Icon = IconResources.ICON_ABSENCE, Relationship = ChildViewList["absence"] });
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
