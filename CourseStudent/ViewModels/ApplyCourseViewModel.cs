using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseStudent.Domain;
using CourseStudent.Helper;
using CourseStudent.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStudent.ViewModels
{
    public class ApplyCourseViewModel : BaseViewModel
    {
        private Dictionary<string, List<DispatchCourse>> majorDispatchList;

        public Dictionary<string, List<DispatchCourse>> MajorDispatchList
        {
            get
            {
                return majorDispatchList;
            }
            set
            {
                majorDispatchList = value;
                NotifyPropertyChanged("MajorDispatchList");
            }
        }

        private ViewModelRelationship Parent;

        private IViewContainer Container;
        private string SessionId;

        private DispatchCourseProvider Provider;

        private int PreviousAppliedCourseId = 0;

        public ApplyCourseViewModel(IViewContainer container, ViewModelRelationship parent,
            string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            Provider = new DispatchCourseProvider();
            Provider.AvailableCourseEvent += AvailableCourseLoadedEvent;
        }

        public override void Notify()
        {
            GetAvailableCourse();
        }

        public void GetAvailableCourse()
        {
            Provider.GetAvaildableCourse(SessionId);
        }

        public void ApplyCourse(object id)
        {
            DialogHelper.ShowProgressDialog("正在加入选择课程...");

            PreviousAppliedCourseId = (int)id;

            DispatchCourse dispatch = GetAppliedCourse(PreviousAppliedCourseId);
            if (dispatch.Current >= dispatch.Limit)
            {
                DialogHelper.Close();
                DialogHelper.Show("已达到上限人数");
                return;
            }

            Provider.JoinCourse(SessionId, PreviousAppliedCourseId);
        }

        public void UpdateAvailable(int dispatchId, int available)
        {
            DispatchCourse dispatch = GetAppliedCourse(dispatchId);

            if (dispatch != null)
                dispatch.Current -= available;
        }

        private DispatchCourse GetAppliedCourse(int id)
        {
            foreach (var pair in MajorDispatchList)
            {
                foreach (var course in pair.Value)
                {
                    if (course.Id == id) return course;
                }
            }

            return null;
        }

        private void UpdateToParentViewModel()
        {
            var viewModel = Parent.ViewModel as DispatchCourseViewModel;

            viewModel.GetUserCourse();
        }

        public void AvailableCourseLoadedEvent(object sender, AvailableCourseEventArgs e)
        {
            DialogHelper.Close();

            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case DispatchCourseProvider.RC_GET_AVAILABLE_COURSE:
                        MajorDispatchList = e.AvaliableCourseList;
                        break;
                    case DispatchCourseProvider.RC_JOIN_COURSE:

                        UpdateAvailable(PreviousAppliedCourseId, -1);

                        UpdateToParentViewModel();

                        DialogHelper.Show("已成功加入所选课程");
                        break;
                    default:
                        break;
                }
                return;
            }

            DialogHelper.ShowError("操作失败", e.ErrorMessage == null ? null : e.ErrorMessage.ToArray());
        }

        #region Command

        public ActionCommand BackToPreviousCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    Container.Show(Parent.View);
                });
            }
        }

        public ActionCommand ApplyCourseCommand
        {
            get
            {
                return new ActionCommand(ApplyCourse);
            }
        }

        #endregion

    }
}
