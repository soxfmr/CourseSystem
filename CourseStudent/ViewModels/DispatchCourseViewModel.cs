using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseStudent.ViewModels
{
    public class DispatchCourseViewModel : LayerBaseViewModel
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

        private IViewContainer Container;

        private string SessionId;

        private DispatchCourseProvider Provider;

        private List<DispatchCourse> PreRemoveCousreList;

        public DispatchCourseViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new DispatchCourseProvider();
            Provider.DispatchCouseEvent += DispatchLoadedEvent;
        }

        public override void Notify()
        {
            GetUserCourse();

            NotifyChildViewModel();
        }

        public void GetUserCourse()
        {
            Provider.GetUserCourse(SessionId);
        }

        public void RemoveUserCourse()
        {
            if (DispatchCourseList == null)
                return;

            PreRemoveCousreList = new List<DispatchCourse>();
            foreach (var course in DispatchCourseList)
            {
                if (course.IsSelected)
                    PreRemoveCousreList.Add(course);
            }

            if (PreRemoveCousreList.Count == 0)
            {
                return;
            }

            if (DialogHelper.Conirm("确定退订当前课程吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                Provider.QuitCourse(SessionId, PreRemoveCousreList);
            }            
        }

        public void UpdateChildViewModel()
        {
            var viewModel = ChildViewList["applyCourse"].ViewModel as ApplyCourseViewModel;
            foreach (var dispatch in PreRemoveCousreList)
            {
                viewModel.UpdateAvailable(dispatch.Id, 1);
            }
        }

        public void CleanRemovedCourse()
        {
            DialogHelper.Dispatcher.Invoke(delegate
            {
                foreach (var course in PreRemoveCousreList)
                {
                    DispatchCourseList.Remove(course);
                }
            });
        }

        #region Command

        public ActionCommand SelectAllCommand {
            get
            {
                return new ActionCommand(p =>
                {
                    if (DispatchCourseList == null || DispatchCourseList.Count == 0)
                        return;

                    foreach (var dispatch in DispatchCourseList)
                    {
                        dispatch.IsSelected = true;
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
                    if (DispatchCourseList == null || DispatchCourseList.Count == 0)
                        return;

                    foreach (var dispatch in DispatchCourseList)
                    {
                        dispatch.IsSelected = ! dispatch.IsSelected;
                    }
                });
            }
        }

        public ActionCommand ShowApplyCourseViewCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    Container.Show(ChildViewList["applyCourse"].View);
                });
            }
        }

        public ActionCommand RemoveCourseCommand
        {
            get
            {
                return new ActionCommand(p => RemoveUserCourse());
            }
        }

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
                    case DispatchCourseProvider.RC_QUIT_COURSE:
                        CleanRemovedCourse();
                        // Update to the child view
                        UpdateChildViewModel();
                        // Update from remote server
                        GetUserCourse();

                        DialogHelper.Show("更改已提交");
                        break;
                    default:
                        break;
                }

                return;
            }

            DialogHelper.ShowError("操作失败", e.ErrorMessage == null ? null : e.ErrorMessage.ToArray());
        }
    }
}
