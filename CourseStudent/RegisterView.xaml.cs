using CommonLibrary.Domain;
using CourseStudent.Helper;
using CourseStudent.ViewModels;
using System.Windows;

namespace CourseStudent
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView
    {
        private bool CLOSE_FROM_CLOSE_BUTTON = true;

        public RegisterView()
        {
            InitializeComponent();

            var viewModel = new RegisterViewModel();
            viewModel.LoginViewCommand = new ActionCommand(p => ShowLoginView());

            DataContext = viewModel;

            Closing += RegisterView_Closing;
            Loaded += RegisterView_Loaded;
        }

        private void RegisterView_Loaded(object sender, RoutedEventArgs e)
        {
            DialogHelper.SetupInvoke(this, "RegisterRootDialog");
        }

        private void RegisterView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Press the close button directly
            if (! CLOSE_FROM_CLOSE_BUTTON)
            {
                UserLoginView view = new UserLoginView();
                view.Show();
            }
        }

        public void ShowLoginView()
        {
            CLOSE_FROM_CLOSE_BUTTON = false;

            Close();
        }
    }
}
