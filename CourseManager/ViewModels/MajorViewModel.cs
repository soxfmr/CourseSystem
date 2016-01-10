using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers.Advance;
using System.Collections.ObjectModel;
using System.Linq;

namespace CourseManager.ViewModels
{
    public class MajorViewModel : LayerBaseViewModel
    {
        private ObservableCollection<Major> majorList;
        public ObservableCollection<Major> MajorList
        {
            get
            {
                return majorList;
            }
            set
            {
                majorList = value;
                NotifyPropertyChanged("MajorList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private MajorProvider Provider;

        public MajorViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new MajorProvider();
            Provider.MajorEvent = MajorLoadedEvent;
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
            if (MajorList == null || MajorList.Count == 0)
            {
                return;
            }

            var preRemove= MajorList.Where(a => a.IsSelected).ToList();

            if (preRemove.Count > 0 && DialogHelper.Conirm("确定删除选中专业吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemove)
                {
                    Provider.Remove(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server or not
                    MajorList.Remove(removed);
                }

                DialogHelper.Close();
            }
        }

        private void UpdateSelected()
        {
            if (MajorList == null || MajorList.Count == 0)
            {
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交更改...");

            foreach (var major in MajorList)
            {
                if (major.IsSelected)
                {
                    major.IsSelected = false;

                    Provider.Update(major.Id, major.Name, major.Description, SessionId);
                }
            }

            DialogHelper.Close();
        }

        public void MajorLoadedEvent(object sender, MajorEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case ClassroomProvider.RC_GET_ALL:
                        MajorList = e.MajorList != null ?
                            new ObservableCollection<Major>(e.MajorList) : null;
                        break;
                    case ClassroomProvider.RC_CREATE:
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
                    Container.Show(ChildViewList["majorCompose"].View);
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

        public ActionCommand UpdateSelectedCommand
        {
            get
            {
                return new ActionCommand(p => UpdateSelected());
            }
        }


        #region SelectedCommand

        public ActionCommand SelectAllCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (MajorList == null || MajorList.Count == 0)
                        return;

                    foreach (var major in MajorList)
                    {
                        major.IsSelected = true;
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
                    if (MajorList == null || MajorList.Count == 0)
                        return;

                    foreach (var major in MajorList)
                    {
                        major.IsSelected = !major.IsSelected;
                    }
                });
            }
        }

        #endregion
    }
}
