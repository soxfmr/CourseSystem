using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.ViewModels
{
    public class CourseComposeViewModel : BaseViewModel
    {
        // Shared object to references the majors
        private MajorViewModel majorViewModel;
        public MajorViewModel mMajorViewModel
        {
            get
            {
                return majorViewModel;
            }

            set
            {
                majorViewModel = value;
                NotifyPropertyChanged("mMajorViewModel");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ViewModelRelationship Parent;

        private CourseProvider.Providers.Advance.CourseProvider Provider;

        public CourseComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            MajorViewModel majorViewModel,
            string sessionId)
        {
            Parent = parent;
            mMajorViewModel = majorViewModel;
            Container = container;
            SessionId = sessionId;

            Provider = new CourseProvider.Providers.Advance.CourseProvider();
            Provider.CourseEvent = CourseLoadedEvent;
            Provider.CourseEvent += (parent.ViewModel as CourseViewModel).CourseLoadedEvent;
        }

        public void Create(int majorId)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
                return;

            if (majorId == -1)
                return;

            DialogHelper.ShowProgressDialog("正在提交请求...");

            Provider.Create(Name, Description, majorId, SessionId);
        }

        private void CourseLoadedEvent(object sender, CourseEventArgs e)
        {
            if (CourseProvider.Providers.Advance.CourseProvider.RC_CREATE == e.RequestCode && e.IsSuccess)
            {
                DialogHelper.Close();

                DialogHelper.Show("成功添加课程");

                DialogHelper.Dispatcher.Invoke(delegate
                {
                    BackToPreviousCommand.Execute(null);
                });

                return;
            }

            DialogHelper.Show("添加失败，请重试");
        }

        #region EventCommand

        public ActionCommand CreateCommand
        {
            get
            {
                return new ActionCommand(p => Create(p == null ? -1 : (int) p));
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
