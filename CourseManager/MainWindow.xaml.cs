using CommonLibrary.Domain;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseManager.Helper;
using CourseManager.ViewModels;
using System.Windows.Input;

namespace CourseManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow(string sessionId)
        {
            InitializeComponent();

            MainWindowViewModel mainModel = new MainWindowViewModel(sessionId);
            DataContext = mainModel;

            mainModel.ViewSwitchCommand = new ActionCommand(SwithView);

            // Re-focus to the content of the list item
            DrawerMenuList.AddHandler(MouseDownEvent, new MouseButtonEventHandler(ListItemClickEvent), true);

            // Default view
            ViewContainer.Content = mainModel.ChildViewList["profile"].View;

            DialogHelper.SetupInvoke(this, "MainRootDialog");
        }

        public void SwithView(object o)
        {
            ViewContainer.Content = o;
        }

        public void ListItemClickEvent(object sender, MouseButtonEventArgs e)
        {
            var item = DrawerMenuList.SelectedItem as MenuItem;

            var view = item.Relationship.View;
            if (view == null)
            {
                var viewModel = item.Relationship.ViewModel;

                if (viewModel != null &&
                    // Perform the Execute method if it's an instance of CommandViewModel  
                    viewModel.GetType().BaseType == typeof(CommandViewModel))
                {
                    (viewModel as CommandViewModel).Execute();
                }

                return;
            }

            ViewContainer.Content = view;
        }
    }
}
