using CommonLibrary.Domain;
using CommonLibrary.ViewModels;
using CourseProvider;
using CourseProvider.Events;
using CourseProvider.Providers;
using CourseStudent.Helper;
using System.Windows.Controls;

namespace CourseStudent.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string username;
        public string Username {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyPropertyChanged("Username");
            }
        }

        private string email;
        public string Email {
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

        private LoginProvider Provider;

        public RegisterViewModel()
        {
            Provider = new LoginProvider();
            Provider.LoginEvent += LoginEvent;
        }

        public void UserRegister(object o)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email))
            {
                return;
            }

            var passwordBox = o as PasswordBox;

            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                return;
            }

            DialogHelper.ShowProgressDialog("正在注册...");
            Provider.Register(Email, Username, passwordBox.Password);
        }

        public ActionCommand RegisterCommand
        {
            get 
            {
                return new ActionCommand(UserRegister);
            }
        }

        public ActionCommand LoginViewCommand { get; set; }

        private void LoginEvent(object sender, LoginEventArgs e)
        {
            if (e.IsSuccess)
            {
                DialogHelper.Show(string.Format("注册成功，邮箱：{0}", Email));
            }
            else
            {
                DialogHelper.Show(e.ErrorCode == CourseProviderContract.REG_ALREADY_EXISTS ?
                    "注册失败，邮箱已存在" :
                    "注册失败，请重试");
            }
        }
    }
}
