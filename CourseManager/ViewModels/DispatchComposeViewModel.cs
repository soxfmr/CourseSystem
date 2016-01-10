using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers.Advance;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace CourseManager.ViewModels
{
    public class DispatchComposeViewModel : BaseViewModel
    {
        private ClassroomViewModel classroomViewModel;
        public ClassroomViewModel refClassroomViewModel
        {
            get
            {
                return classroomViewModel;
            }

            set
            {
                classroomViewModel = value;
                NotifyPropertyChanged("refClassroomViewModel");
            }
        }

        private CourseViewModel courseViewModel;
        public CourseViewModel refCourseViewModel
        {
            get
            {
                return courseViewModel;
            }

            set
            {
                courseViewModel = value;
                NotifyPropertyChanged("refCourseViewModel");
            }
        }

        private ObservableCollection<Profile> teacherList;
        public ObservableCollection<Profile> TeacherList
        {
            get
            {
                return teacherList;
            }
            set
            {
                teacherList = value;
                NotifyPropertyChanged("TeacherList");
            }
        }

        private int limit;
        public int Limit
        {
            get
            {
                return limit;
            }
            set
            {
                limit = value;
                NotifyPropertyChanged("Limit");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ViewModelRelationship Parent;

        private DispatchManageProvider Provider;
        private UserManageProvider userMgrProvider;

        public DispatchComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            ClassroomViewModel classroomViewModel,
            CourseViewModel courseViewModel,
            string sessionId)
        {
            Parent = parent;
            refClassroomViewModel = classroomViewModel;
            refCourseViewModel = courseViewModel;
            Container = container;
            SessionId = sessionId;

            Provider = new DispatchManageProvider();
            Provider.DispatchManageEvent = DispatchManageLoadedEvent;
            Provider.DispatchManageEvent += (parent.ViewModel as DispatchManageViewModel).DispatchManageLoadedEvent;

            userMgrProvider = new UserManageProvider();
            userMgrProvider.ProfileEvent = ProfileELoadedEvent;
        }

        public override void Notify()
        {
            userMgrProvider.GetAllUser(CourseProvider.CourseProviderContract.MODE_TEACHER, SessionId);
        }

        public void Create()
        {
            int currentDay = 0;
            string weekday = "";

            var view = GetRelationView();

            // Determine that the relation information is supplied
            int teacherId = (int) view.CBoxTeacherList.SelectedValue;
            int roomId = (int) view.CBoxRoomList.SelectedValue;
            int courseId = (int) view.CBoxCourseList.SelectedValue;

            if (teacherId == 0 || roomId == 0 || courseId == 0)
                return;

            if (Limit <= 0)
            {
                DialogHelper.Show("课程上限人数不能为非正整数...");
                return;
            }

            // Compose the course time
            CheckBox[] weekayCheckBox = { view.CheckBoxMon, view.CheckBoxTues, view.CheckBoxWed,
                                    view.CheckBoxThur, view.CheckBoxFri};

            for (int i = 0, len = weekayCheckBox.Length; i < len; i++)
            {
                currentDay = i + 1;

                if ((bool) weekayCheckBox[i].IsChecked)
                {
                    weekday += currentDay;
                    // Append a comma in the end of each day expect the last
                    if (currentDay < len) weekday += ",";
                }
            }

            if (string.IsNullOrEmpty(weekday))
            {
                DialogHelper.Show("一周都没课吗...");
                return;
            }

            // Format the detail time
            DateTime at = default(DateTime);
            try
            {
                at = DateTime.Parse(view.TimePickerAt.Text);
            } catch (Exception)
            {
                DialogHelper.Show("无法识别火星时间...");
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交请求...");

            Provider.Create(weekday, at, limit, teacherId, courseId, roomId, SessionId);
        }
        
        private void ProfileELoadedEvent(object sender, UserManageEventArgs e)
        {
            if (DispatchManageProvider.RC_GET_ALL == e.RequestCode &&
                e.IsSuccess)
            {
                TeacherList = new ObservableCollection<Profile>(e.UserProfileList);
            }
        }

        private void DispatchManageLoadedEvent(object sender, DispatchManageEventArgs e)
        {
            if (DispatchManageProvider.RC_CREATE == e.RequestCode && e.IsSuccess)
            {
                DialogHelper.Close();

                DialogHelper.Show("成功分配课程");

                DialogHelper.Dispatcher.Invoke(delegate
                {
                    BackToPreviousCommand.Execute(null);
                });

                return;
            }

            DialogHelper.Show("分配失败，请重试");
        }

        #region EventCommand

        public ActionCommand CreateCommand
        {
            get
            {
                return new ActionCommand(p => Create());
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

        protected DispatchComposeView GetRelationView()
        {
            return (Parent.ViewModel as
                LayerBaseViewModel).ChildViewList["dispatchCompose"].View as DispatchComposeView;
        }
    }
}
