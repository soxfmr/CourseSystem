using CommonLibrary.Domain;
using CommonLibrary.Helper;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace CourseManager.ViewModels
{
    public class CourseViewModel : LayerBaseViewModel
    {
        private ObservableCollection<Course> courseList;
        public ObservableCollection<Course> CourseList
        {
            get
            {
                return courseList;
            }
            set
            {
                courseList = value;
                NotifyPropertyChanged("CourseList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private CourseProvider.Providers.Advance.CourseProvider Provider;

        public CourseViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new CourseProvider.Providers.Advance.CourseProvider();
            Provider.CourseEvent = CourseLoadedEvent;
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
            if (CourseList == null || CourseList.Count == 0)
            {
                return;
            }

            var preRemove = CourseList.Where(a => a.IsSelected).ToList();

            if (preRemove.Count > 0 && DialogHelper.Conirm("确定删除选中课程吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemove)
                {
                    Provider.Remove(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server or not
                    CourseList.Remove(removed);
                }

                DialogHelper.Close();
            }
        }

        private void UpdateSelected()
        {
            if (CourseList == null || CourseList.Count == 0)
            {
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交更改...");

            foreach (var course in CourseList)
            {
                if (course.IsSelected)
                {
                    course.IsSelected = false;

                    Provider.Update(course.Id, course.Name, course.Description, 
                        course.MajorId, SessionId);
                }
            }

            DialogHelper.Close();
        }

        public void CourseLoadedEvent(object sender, CourseEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case CourseProvider.Providers.Advance.CourseProvider.RC_GET_ALL:
                        CourseList = e.CourseList != null ?
                            new ObservableCollection<Course>(e.CourseList) : null;
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
                    Container.Show(ChildViewList["courseCompose"].View);
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
                    if (CourseList == null || CourseList.Count == 0)
                        return;

                    foreach (var course in CourseList)
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
                    if (CourseList == null || CourseList.Count == 0)
                        return;

                    foreach (var course in CourseList)
                    {
                        course.IsSelected = !course.IsSelected;
                    }
                });
            }
        }

        #endregion
    }
}
