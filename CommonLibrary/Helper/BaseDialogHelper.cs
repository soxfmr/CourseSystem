using CommonLibrary.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace CommonLibrary.Helper
{
    public class BaseDialogHelper
    {
        public const string TAG = "DialogHelper";

        private static ResourceDictionary DialogDictionary = 
            new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml") };

        private static MetroDialogSettings dialogSettings = new MetroDialogSettings()
        {
            CustomResourceDictionary = DialogDictionary,
            NegativeButtonText = "确定",
            SuppressDefaultResources = true
        };

        public static MetroWindow Context;

        public static Dispatcher Dispatcher;

        public static string Identifier;

        /// <summary>
        /// Set up the instance of the UI thread and the indentifier of the dialog host instance.
        /// </summary>
        /// <param name="invoker"></param>
        /// <param name="identifier"></param>
        public static void SetupInvoke(MetroWindow context, string identifier)
        {
            if (context == null)
                throw new ArgumentNullException("The context of UI thread shouldn't be null.");

            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException("The indentifier of dialog host instance shouldn't be null.");

            Context = context;
            Identifier = identifier;
            Dispatcher = context.Dispatcher;
        }

        public static void ShowError(string msg, params string[] errorMsg)
        {
            List<string> msgList = new List<string>();
            msgList.Add(msg);

            if (errorMsg != null && errorMsg.Length > 0)
            {
                msgList[0] = msg + "，详情：";
                msgList.AddRange(errorMsg);
            }

            Show(msgList.ToArray());
        }

        public static void Show(params string[] message)
        {
            // Reset the previous session if possible
            Close();

            if (message == null || message.Length == 0)
                return;

            if (Dispatcher == null)
                return;

            StringBuilder sBuilder = new StringBuilder();
            foreach (var item in message)
            {
                sBuilder.AppendLine(item);
            }

            Dispatcher.Invoke(delegate
            {
                Context.ShowMessageAsync("提示", sBuilder.ToString(), MessageDialogStyle.Affirmative, dialogSettings);
            });

            //Context.ShowMessageAsync(Context, "提示", sBuilder.ToString(),
            //   MessageDialogStyle.AffirmativeAndNegative, metroDialogSettings);

            //if (Invoker != null && !string.IsNullOrWhiteSpace(Identifier))
            //{
            //    Invoker.Invoke(delegate
            //    {
            //        StringBuilder sBuilder = new StringBuilder();
            //        foreach (var item in message)
            //        {
            //            sBuilder.AppendLine(item);
            //        }

            //        NoticeDialog view = new NoticeDialog();
            //        view.DataContext = new DialogViewModel(sBuilder.ToString());

            //        DialogHost.Show(view, Identifier);
            //    });
            //}
        }

        public static bool Conirm(string msg)
        {
            var result = MessageBox.Show(Context, msg, "提示", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            return result == MessageBoxResult.OK ? true : false;
        }

        public static void Close()
        {
            if (Dispatcher == null)
                return;

            Dispatcher.Invoke(delegate
            {
                DialogHost.CloseDialogCommand.Execute(true, null);
            });
        }
    }
}
