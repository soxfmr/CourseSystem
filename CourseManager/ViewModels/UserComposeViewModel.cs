using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers.Advance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseManager.ViewModels
{
    public class UserComposeViewModel : BaseViewModel
    {
        // Shared object to references the user mode
        public UserManageViewModel mUserManageViewModel
        {
            get
            {
                return Parent.ViewModel as UserManageViewModel;
            }

            set
            {
                NotifyPropertyChanged("mUserManageViewModel");
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

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ViewModelRelationship Parent;

        private UserManageProvider Provider;

        public UserComposeViewModel(IViewContainer container, ViewModelRelationship parent, string sessionId)
        {
            Parent = parent;
            Container = container;
            SessionId = sessionId;

            Provider = new UserManageProvider();
            Provider.ProfileEvent = ProfileLoadedEvent;
            Provider.ProfileEvent += (parent.ViewModel as UserManageViewModel).ProfileLoadedEvent;
        }

        public void Create(int mode)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email))
                return;

            if (mode == -1)
                return;

            PasswordBox boxPasswd = GetRelationView().InitPassword;
            if (string.IsNullOrEmpty(boxPasswd.Password))
                return;

            DialogHelper.ShowProgressDialog("正在提交请求...");

            Provider.Create(Email, Name, boxPasswd.Password, mode, SessionId);
        }

        private void ProfileLoadedEvent(object sender, UserManageEventArgs e)
        {
            if (CourseProvider.Providers.Advance.CourseProvider.RC_CREATE == e.RequestCode && e.IsSuccess)
            {
                DialogHelper.Close();

                DialogHelper.Show("成功添加");

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

        protected UserComposeView GetRelationView()
        {
            return (Parent.ViewModel as 
                LayerBaseViewModel).ChildViewList["userCompose"].View as UserComposeView;
        }
    }
}
