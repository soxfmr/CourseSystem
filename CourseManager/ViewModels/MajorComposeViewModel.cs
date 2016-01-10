using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Providers.Advance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.ViewModels
{
    public class MajorComposeViewModel : BaseViewModel
    {
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

        private MajorProvider majorProvider;

        public MajorComposeViewModel(IViewContainer container, ViewModelRelationship parent,
            string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            majorProvider = new MajorProvider();
            majorProvider.MajorEvent = MajorLoadedEvent;
            majorProvider.MajorEvent += (parent.ViewModel as MajorViewModel).MajorLoadedEvent;
        }

        public void Create()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
                return;

            DialogHelper.ShowProgressDialog("正在提交请求...");

            majorProvider.Create(Name, Description, SessionId);
        }

        private void MajorLoadedEvent(object sender, MajorEventArgs e)
        {
            if (ClassroomProvider.RC_CREATE == e.RequestCode && e.IsSuccess)
            {
                DialogHelper.Close();

                DialogHelper.Show("成功添加专业");

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
