using CommonLibrary.Domain;
using CommonLibrary.Models;
using CommonLibrary.ViewModels;
using CourseStudent.Helper;
using CourseStudent.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace CourseStudent
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
            DrawerMenuList.AddHandler(MouseDownEvent,
                    new MouseButtonEventHandler(delegate
                    {
                        var item = DrawerMenuList.SelectedItem as MenuItem;

                        var view = item.Relationship.View;
                        if (view == null)
                        {
                            (item.Relationship.ViewModel as CommandViewModel).Execute();
                            return;
                        }

                        ViewContainer.Content = view;
                    }), true);

            // Default view
            ViewContainer.Content = mainModel.ChildViewList["profile"].View;

            DialogHelper.SetupInvoke(this, "MainRootDialog");
        }

        public void SwithView(object o)
        {
            ViewContainer.Content = o;
        }
    }
}
