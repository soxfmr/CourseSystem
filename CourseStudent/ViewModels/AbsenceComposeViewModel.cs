using CommonLibrary.Domain;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseStudent.Helper;
using System.Collections.ObjectModel;

namespace CourseStudent.ViewModels
{
    public class AbsenceComposeViewModel : BaseViewModel
    {
        private ObservableCollection<DispatchCourse> dispatchCourseList;
        public ObservableCollection<DispatchCourse> DispatchCourseList
        {
            get
            {
                return dispatchCourseList;
            }
            set
            {
                dispatchCourseList = value;
                NotifyPropertyChanged("DispatchCourseList");
            }
        }

        private string reason;
        public string Reason {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
                NotifyPropertyChanged("Reason");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ViewModelRelationship Parent;

        private AbsenceProvider absenceProvider;
        private DispatchCourseProvider dispatchProvider;

        public AbsenceComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            absenceProvider = new AbsenceProvider();
            absenceProvider.AbsenceEvent += (parent.ViewModel as AbsenceViewModel).AbsenceLoadedEvent;
            // Local event
            absenceProvider.AbsenceEvent += AbsenceLoadedEvent;

            dispatchProvider = new DispatchCourseProvider();
            dispatchProvider.DispatchCouseEvent = DispatchLoadedEvent;
        }

        public override void Notify()
        {
            GetUserCourse();
        }

        public void GetUserCourse()
        {
            dispatchProvider.GetUserCourse(SessionId);
        }

        public void CreateAbsence(int dispatchId)
        {
            if (string.IsNullOrEmpty(Reason) || dispatchId == -1)
                return;

            DialogHelper.ShowProgressDialog("正在提交请求...");

            absenceProvider.Create(Reason, dispatchId, SessionId);
        }

        public void AbsenceLoadedEvent(object sender, AbsenceEventArgs e)
        {
            if (AbsenceProvider.RC_CREATE == e.RequestCode && e.IsSuccess)
            {
                DialogHelper.Close();

                DialogHelper.Show("请假条已成功提交");

                DialogHelper.Dispatcher.Invoke(delegate
                {
                    BackToPreviousCommand.Execute(null);
                });

                return;
            }

            DialogHelper.Show("添加失败，请重试");
        }

        public void DispatchLoadedEvent(object sender, DispatchCourseEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case DispatchCourseProvider.RC_GET_USER_COUSR:
                        DispatchCourseList = e.DispatchCourseList != null ? new ObservableCollection<DispatchCourse>(e.DispatchCourseList) : null;
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
                return new ActionCommand(p => GetUserCourse());
            }
        }

        public ActionCommand CreateAbsenceCommand
        {
            get
            {
                return new ActionCommand(p => CreateAbsence(p == null ? -1 : (int) p));
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
