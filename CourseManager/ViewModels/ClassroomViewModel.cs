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
    public class ClassroomViewModel : LayerBaseViewModel
    {
        private ObservableCollection<Classroom> classroomList;
        public ObservableCollection<Classroom> ClassroomList
        {
            get
            {
                return classroomList;
            }
            set
            {
                classroomList = value;
                NotifyPropertyChanged("ClassroomList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private ClassroomProvider Provider;

        public ClassroomViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new ClassroomProvider();
            Provider.ClassroomEvent = ClassroomLoadedEvent;
        }

        public override void Notify()
        {
            GetAllClassroom();

            NotifyChildViewModel();
        }

        public void GetAllClassroom()
        {
            Provider.GetAll(SessionId);
        }

        public void RemoveClassroom()
        {
            if (ClassroomList == null || ClassroomList.Count == 0)
            {
                return;
            }

            var preRemoveClassroom = ClassroomList.Where(a => a.IsSelected).ToList();

            if (preRemoveClassroom.Count > 0 && DialogHelper.Conirm("确定删除选中课室吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemoveClassroom)
                {
                    Provider.Remove(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server or not
                    ClassroomList.Remove(removed);
                }

                DialogHelper.Close();
            }
        }

        private void UpdateSelected()
        {
            if (ClassroomList == null || ClassroomList.Count == 0)
            {
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交更改...");

            foreach (var classroom in ClassroomList)
            {
                if (classroom.IsSelected)
                {
                    classroom.IsSelected = false;

                    Provider.Update(classroom.Id, classroom.Location, SessionId);
                }
            }

            DialogHelper.Close();
        }

        public void ClassroomLoadedEvent(object sender, ClassroomEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case ClassroomProvider.RC_GET_ALL:
                        ClassroomList = e.ClassroomList != null ?
                            new ObservableCollection<Classroom>(e.ClassroomList) : null;
                        break;
                    case ClassroomProvider.RC_CREATE:
                        DialogHelper.Dispatcher.Invoke(delegate
                        {
                            GetAllClassroom();
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
                    Container.Show(ChildViewList["classroomCompose"].View);
                });
            }
        }

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetAllClassroom());
            }
        }

        public ActionCommand RemoveClassroomCommand
        {
            get
            {
                return new ActionCommand(p => RemoveClassroom());
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
                    if (ClassroomList == null || ClassroomList.Count == 0)
                        return;

                    foreach (var classroom in ClassroomList)
                    {
                        classroom.IsSelected = true;
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
                    if (ClassroomList == null || ClassroomList.Count == 0)
                        return;

                    foreach (var classroom in ClassroomList)
                    {
                        classroom.IsSelected = !classroom.IsSelected;
                    }
                });
            }
        }

        #endregion 
    }
}
