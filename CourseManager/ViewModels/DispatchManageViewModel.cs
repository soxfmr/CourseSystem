using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers.Advance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.ViewModels
{
    public class DispatchManageViewModel : LayerBaseViewModel
    {
        private ObservableCollection<DispatchManage> dispatchManageList;
        public ObservableCollection<DispatchManage> DispatchManageList
        {
            get
            {
                return dispatchManageList;
            }
            set
            {
                dispatchManageList = value;
                NotifyPropertyChanged("DispatchManageList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private DispatchManageProvider Provider;

        public DispatchManageViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new DispatchManageProvider();
            Provider.DispatchManageEvent = DispatchManageLoadedEvent;
        }

        public override void Notify()
        {
            GetAll();

            NotifyChildViewModel();
        }

        public void GetAll()
        {
            Provider.GetAll(SessionId);
        }

        public void RemoveSelected()
        {
            if (DispatchManageList == null || DispatchManageList.Count == 0)
            {
                return;
            }

            var preRemove = DispatchManageList.Where(a => a.IsSelected).ToList();

            if (preRemove.Count > 0 && DialogHelper.Conirm("确定删除选中课程分配信息吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemove)
                {
                    Provider.Remove(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server or not
                    DispatchManageList.Remove(removed);
                }

                DialogHelper.Close();
            }
        }

        public void DispatchManageLoadedEvent(object sender, DispatchManageEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case CourseProvider.Providers.Advance.CourseProvider.RC_GET_ALL:
                        DispatchManageList = e.DispatchManageList != null ?
                            new ObservableCollection<DispatchManage>(e.DispatchManageList) : null;
                        break;
                    case CourseProvider.Providers.Advance.CourseProvider.RC_CREATE:
                        DialogHelper.Dispatcher.Invoke(delegate
                        {
                            GetAll();
                        });
                        break;
                    default:
                        break;
                }
            }
        }

        public ActionCommand ShowComposeViewCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    Container.Show(ChildViewList["dispatchCompose"].View);
                });
            }
        }

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetAll());
            }
        }

        public ActionCommand RemoveCommand
        {
            get
            {
                return new ActionCommand(p => RemoveSelected());
            }
        }

        #region SelectedCommand

        public ActionCommand SelectAllCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (DispatchManageList == null || DispatchManageList.Count == 0)
                        return;

                    foreach (var course in DispatchManageList)
                    {
                        course.IsSelected = true;
                    }
                });
            }
        }

        public ActionCommand ReverseCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (DispatchManageList == null || DispatchManageList.Count == 0)
                        return;

                    foreach (var course in DispatchManageList)
                    {
                        course.IsSelected = !course.IsSelected;
                    }
                });
            }
        }

        #endregion
    }
}
