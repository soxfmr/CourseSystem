using System.Windows.Controls;
using CourseProvider.Events;
using CourseProvider.Providers;
using CourseStudent.Helper;
using CourseProvider;
using CommonLibrary.ViewModels;
using CommonLibrary.Domain;

namespace CourseStudent.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // Default mode to the student client
        public const int MODE_LOGIN = CourseProviderContract.MODE_STUDENT;

        public string Username { get; set; }

        public string SessionId { get; set; }
        
        private LoginProvider lProvider;

        public ActionCommand LoginCommand
        {
            get { return new ActionCommand(e => Login(Username, (e as PasswordBox).Password)); }
        }

        public ActionCommand ShowMainWindowCommand { get; set; }

        public ActionCommand ShowRegisterWindowCommand { get; set; }

        public LoginViewModel()
        {
            lProvider = new LoginProvider();
            lProvider.LoginEvent += LoginEvent;
        }

        private void Login(string username, string password)
        {
            DialogHelper.ShowProgressDialog("Login...");
            
            lProvider.Login(username, password, MODE_LOGIN);
        }

        private void LoginEvent(object sender, LoginEventArgs e)
        {
            DialogHelper.Close();

            if (e.IsSuccess && ShowMainWindowCommand != null)
            {
                ShowMainWindowCommand.Execute(e.SessionId);
            }
            else
            {
                DialogHelper.Show("登陆失败，请检查账号密码是否正确");
            }
        }
    }
}
