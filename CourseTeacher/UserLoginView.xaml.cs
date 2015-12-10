using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CourseTeacher.ViewModels;

namespace CourseTeacher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserLoginView
    {
        public UserLoginView()
        {
            InitializeComponent();
            
            var viewModel = new LoginViewModel();
            viewModel.ShowMainWindowCommand = new ActionCommand(ShowMainWindow, Dispatcher);

            DataContext = viewModel;

            DialogHelper.SetupInvoke(this, "LoginRootDialog");
        }

        /// <summary>
        /// Login sucess, redirect to the main window
        /// </summary>
        /// <param name="param"></param>
        public void ShowMainWindow(object param)
        {
            string sessionId = param as string;

            MainWindow mainWin = new MainWindow(sessionId);
            mainWin.Show();

            Close();
        }
    }
}
