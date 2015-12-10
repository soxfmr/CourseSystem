using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;

namespace CourseManager.Helper
{
    public class DialogHelper : BaseDialogHelper
    {
        public static void ShowProgressDialog(string message)
        {
            if (Dispatcher != null && !string.IsNullOrWhiteSpace(Identifier))
            {
                Dispatcher.Invoke(delegate
                {
                    ProgressDialogView view = new ProgressDialogView();
                    view.DataContext = new DialogViewModel(message);

                    DialogHost.Show(view, Identifier);
                });
            }
        }
    }
}
