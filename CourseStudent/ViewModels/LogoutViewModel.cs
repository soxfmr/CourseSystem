using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;
using CourseProvider.Providers;

namespace CourseStudent.ViewModels
{
    public class LogoutViewModel : CommandViewModel
    {
        private IViewContainer Container;

        private string SessionId;

        private LoginProvider Provider;

        public LogoutViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new LoginProvider();
        }

        public override void Execute()
        {
            if (DialogHelper.Conirm("确定退出？"))
            {
                Provider.Logout(SessionId);

                LogoutToView();
            }
        }

        public void LogoutToView()
        {
            DialogHelper.Context.Closing += (o, e) =>
            {
                UserLoginView view = new UserLoginView();
                view.Show();
            };
            // Close current window
            DialogHelper.Context.Close();            
        }
    }
}
