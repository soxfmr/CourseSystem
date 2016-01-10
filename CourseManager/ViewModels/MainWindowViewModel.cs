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
            MajorViewModel majorViewModel = new MajorViewModel(this, SessionId);
            ClassroomViewModel classroomViewModel = new ClassroomViewModel(this, SessionId);
            CourseViewModel courseViewModel = new CourseViewModel(this, SessionId);
            DispatchManageViewModel dispatchManageViewModel = new DispatchManageViewModel(this, SessionId);
            UserManageViewModel userManageViewModel = new UserManageViewModel(this, SessionId);

            AddChildView("profile", new ViewModelRelationship(new ProfileViewModel(this, SessionId), new ProfileView()));
            AddChildView("classroom", new ViewModelRelationship(classroomViewModel, new ClassroomView()));
            AddChildView("major", new ViewModelRelationship(majorViewModel, new MajorView()));
            AddChildView("course", new ViewModelRelationship(courseViewModel, new CourseView()));
            AddChildView("dispatch", new ViewModelRelationship(dispatchManageViewModel, new DispatchCourseView()));
            AddChildView("user", new ViewModelRelationship(userManageViewModel, new UserManageView()));
            AddChildView("about", new ViewModelRelationship(new AboutViewModel(), new AboutView()));
            AddChildView("logout", new ViewModelRelationship(new LogoutViewModel(this, SessionId), null));

            classroomViewModel.AddChildView("classroomCompose", new ViewModelRelationship(
                    new ClassroomComposeViewModel(this, ChildViewList["classroom"], SessionId),
                    new ClassroomComposeView()));

            majorViewModel.AddChildView("majorCompose", new ViewModelRelationship(
                    new MajorComposeViewModel(this, ChildViewList["major"], SessionId),
                    new MajorComposeView()));

            courseViewModel.AddChildView("courseCompose", new ViewModelRelationship(
                    new CourseComposeViewModel(this, ChildViewList["course"], majorViewModel, SessionId),
                    new CourseComposeView()));

            userManageViewModel.AddChildView("userCompose", new ViewModelRelationship(
                    new UserComposeViewModel(this, ChildViewList["user"], SessionId),
                    new UserComposeView()));

            dispatchManageViewModel.AddChildView("dispatchCompose", new ViewModelRelationship(
                    new DispatchComposeViewModel(this, ChildViewList["dispatch"], 
                                    classroomViewModel, courseViewModel, SessionId),
                    new DispatchComposeView()));

            MenuItemList = new List<MenuItem>();
            MenuItemList.Add(new MenuItem { Title = "个人信息", Icon = IconResources.ICON_USER, Relationship = ChildViewList["profile"] });
            MenuItemList.Add(new MenuItem { Title = "用户管理", Icon = IconResources.ICON_USER_MANAGE, Relationship = ChildViewList["user"] });
            MenuItemList.Add(null);
            MenuItemList.Add(new MenuItem { Title = "课室管理", Icon = IconResources.ICON_BUILDING, Relationship = ChildViewList["classroom"] });
            MenuItemList.Add(new MenuItem { Title = "专业管理", Icon = IconResources.ICON_MAJOR, Relationship = ChildViewList["major"] });
            MenuItemList.Add(new MenuItem { Title = "课程管理", Icon = IconResources.ICON_COURSE, Relationship = ChildViewList["course"] });
            MenuItemList.Add(new MenuItem { Title = "课程分配", Icon = IconResources.ICON_ATTENDANCE, Relationship = ChildViewList["dispatch"] });
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
