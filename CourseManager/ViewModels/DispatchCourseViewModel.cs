using CommonLibrary.Domain;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using System.Collections.ObjectModel;

namespace CourseManager.ViewModels
{
    public class DispatchCourseViewModel : BaseViewModel
    {
        private ObservableCollection<DispatchCourse> dispatchCourseList;

        public ObservableCollection<DispatchCourse> DispatchCourseList {
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

        private string SessionId;

        private DispatchCourseProvider Provider;

        public DispatchCourseViewModel(string sessionId)
        {
            SessionId = sessionId;

            Provider = new DispatchCourseProvider();
            Provider.DispatchCouseEvent += DispatchLoadedEvent;
        }

        public override void Notify()
        {
            GetUserCourse();
        }

        public void GetUserCourse()
        {
            Provider.GetUserCourse(SessionId);
        }

        #region Command

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetUserCourse());
            }
        }

        #endregion

        public void DispatchLoadedEvent(object sender, DispatchCourseEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case DispatchCourseProvider.RC_GET_USER_COUSR:
                        if (e.DispatchCourseList != null)
                        {
                            DispatchCourseList = new ObservableCollection<DispatchCourse>(e.DispatchCourseList);
                        }
                        break;
                    default:
                        break;
                }

                return;
            }
        }
    }
}
