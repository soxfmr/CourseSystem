using CourseStudent.Domain;
using CourseStudent.Helper;
using CourseStudent.ViewModels;
using MahApps.Metro.Controls;

namespace CourseStudent
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserLoginView
    {
        public UserLoginView()
        {
            InitializeComponent();

            (DataContext as LoginViewModel).ShowMainWindowCommand =
                new ActionCommand(ShowMainWindow, Dispatcher);

            DialogHelper.SetupInvoke(this, "RootDialog");
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
