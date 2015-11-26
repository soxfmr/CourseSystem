using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseStudent.Domain;
using CourseStudent.Helper;
using System.Windows.Input;
using System.Windows.Threading;

namespace CourseStudent.ViewModels
{
    public class ProfileViewModel : BaseViewModel, INotifiedableView
    {
        private Profile _UserProfile = new Profile();

        public Profile UserProfile {
            get
            {
                return _UserProfile;
            }
            set
            {
                _UserProfile = value;
                NotifyPropertyChanged("UserProfile");
            }
        }

        private ProfileProvider profileProvider;

        private MainWindowViewModel MainViewModel;

        private Dispatcher UIThreadDispatcher;

        private string SessionId;

        public ProfileViewModel(MainWindowViewModel MainViewModel, string SessionId)
        {
            this.MainViewModel = MainViewModel;
            this.SessionId = SessionId;
        }

        public override void Notify()
        {
            UIThreadDispatcher = GetRelationView().Dispatcher;

            profileProvider = new ProfileProvider();
            profileProvider.ProfileEvent += UserProfileEvent;

            GetUserProfile();
        }

        /// <summary>
        /// Trigger the update event
        /// </summary>
        public ActionCommand UpdateProfileCommand { get { return new ActionCommand(e => UpdateProfile()); } }


        public ActionCommand RefreshCommand { get { return new ActionCommand(e => GetUserProfile()); } }

        /// <summary>
        /// Reset the password box fields after update the profile
        /// </summary>
        public ActionCommand ResetPasswordViewCommand { get { return new ActionCommand(e => ResetPasswordView(), UIThreadDispatcher); } }

        #region Remote Preform Event

        /// <summary>
        /// Retrieve the user profile from remote server
        /// </summary>
        public void GetUserProfile()
        {
            profileProvider.GetProfile(SessionId);
        }

        /// <summary>
        /// Update the user profile to the server
        /// </summary>
        public void UpdateProfile()
        {
            DialogHelper.ShowProgressDialog("正在更新...");

            var view = GetRelationView();
            // Retrieve the password
            profileProvider.UpdateProfile(SessionId, UserProfile.Avatar, 
                UserProfile.Name, UserProfile.Cellphone, 
                view.PasswordBoxNew.Password, view.PasswordBoxConfirm.Password, view.PasswordBoxOrigin.Password);
        }

        public void UserProfileEvent(object sender, ProfileEventArgs e)
        {
            DialogHelper.Close();

            switch (e.RequestCode)
            {
                case ProfileProvider.RC_GET_PROFILE:
                    if (e.IsSuccess)
                    {
                        UserProfile = e.UserProfile;
                        MainViewModel.UserProfile = UserProfile;
                    }
                    break;
                default:
                    if (e.IsSuccess)
                    {
                        MainViewModel.UserProfile = UserProfile;
                        // Reset the secure field
                        ResetPasswordViewCommand.Execute(null);
                        // Notice the result
                        DialogHelper.Show("更新完成");
                    } else
                    {
                        DialogHelper.ShowError("更新失败，请稍后重试", e.ErrorMessage == null ? null : 
                            e.ErrorMessage.ToArray());
                    }
                    break;
            }
        }
        #endregion

        protected ProfileView GetRelationView()
        {
            return MainViewModel.ChildViewList["profile"].View as ProfileView;
        }

        private void ResetPasswordView()
        {
            var view = GetRelationView();
            view.PasswordBoxNew.Clear();
            view.PasswordBoxConfirm.Clear();
            view.PasswordBoxOrigin.Clear();
        }
    }
}
