using CommonLibrary.Domain;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseProvider.Providers.Advance;
using System;
using CourseProvider.Events;
using CommonLibrary.Helper;

namespace CourseManager.ViewModels
{
    public class ClassroomComposeViewModel : BaseViewModel
    {
        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ViewModelRelationship Parent;

        private ClassroomProvider classroomProvider;

        public ClassroomComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            classroomProvider = new ClassroomProvider();
            classroomProvider.ClassroomEvent = ClassroomLoadEvent;
            classroomProvider.ClassroomEvent += (parent.ViewModel as ClassroomViewModel).ClassroomLoadedEvent;
        }

        public void Create()
        {
            if (string.IsNullOrEmpty(Location))
                return;

            DialogHelper.ShowProgressDialog("正在提交请求...");

            classroomProvider.Create(Location, SessionId);
        }

        private void ClassroomLoadEvent(object sender, ClassroomEventArgs e)
        {
            if (ClassroomProvider.RC_CREATE == e.RequestCode && e.IsSuccess)
            {
                DialogHelper.Close();

                DialogHelper.Show("成功添加课室");

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
    }
}
