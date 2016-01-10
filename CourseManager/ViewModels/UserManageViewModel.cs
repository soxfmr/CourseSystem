using CommonLibrary.ViewModels;
using System.Linq;
using CourseProvider.Events;
using CourseProvider.Models;
using System.Collections.ObjectModel;
using CommonLibrary.Domain;
using CourseProvider.Providers.Advance;
using CommonLibrary.Helper;
using System.Collections.Generic;

namespace CourseManager.ViewModels
{
    public class UserManageViewModel : LayerBaseViewModel
    {
        private ObservableCollection<UserMode> userModeList;
        public ObservableCollection<UserMode> UserModeList
        {
            get
            {
                return userModeList;
            }

            set
            {
                userModeList = value;
                NotifyPropertyChanged("UserModeList");
            }
        }

        private ObservableCollection<Profile> profileList;
        public ObservableCollection<Profile> ProfileList
        {
            get
            {
                return profileList;
            }
            set
            {
                profileList = value;
                NotifyPropertyChanged("ProfileList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private UserManageProvider Provider;

        public UserManageViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new UserManageProvider();
            Provider.ProfileEvent = ProfileLoadedEvent;
        }

        public override void Notify()
        {
            UserModeList = new ObservableCollection<UserMode>();
            UserModeList.Add(new UserMode
            {
                Mode = CourseProvider.CourseProviderContract.MODE_STUDENT,
                Name = "学生"
            });

            UserModeList.Add(new UserMode
            {
                Mode = CourseProvider.CourseProviderContract.MODE_TEACHER,
                Name = "教师"
            });

            UserModeList.Add(new UserMode
            {
                Mode = CourseProvider.CourseProviderContract.MODE_MANAGER,
                Name = "管理员"
            });

            // Load the stduents by default
            GetAll(0);

            NotifyChildViewModel();
        }

        public void GetAll(int mode)
        {
            if (mode == -1)
                return;

            Provider.GetAllUser(mode, SessionId);
        }

        public void RemoveSelected()
        {
            if (ProfileList == null || ProfileList.Count == 0)
            {
                return;
            }

            var preRemove = ProfileList.Where(a => a.IsSelected).ToList();

            if (preRemove.Count > 0 && DialogHelper.Conirm("确定删除选中用户吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemove)
                {
                    Provider.Remove(removed.Id, removed.Mode, SessionId);
                    // Remove from data list whatever it perform in successful on server or not
                    ProfileList.Remove(removed);
                }

                DialogHelper.Close();
            }
        }

        private void UpdateSelected()
        {
            if (ProfileList == null || ProfileList.Count == 0)
            {
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交更改...");

            foreach (var user in ProfileList)
            {
                if (user.IsSelected)
                {
                    user.IsSelected = false;

                    Provider.Update(user.Id, user.Mode, user.Name,
                        user.Avatar, user.Cellphone, SessionId);
                }
            }

            DialogHelper.Close();
        }

        public void ResetPassword()
        {
            if (ProfileList == null || ProfileList.Count == 0)
            {
                return;
            }

            List<Profile> resetedList = ProfileList.Where(p => p.IsSelected).ToList();
            if (resetedList.Count > 1)
            {
                DialogHelper.Show("重置密码只允许对单个用户进行操作！");
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交更改...");

            foreach (var user in resetedList)
            {
                Provider.ResetPassword(user.Id, user.Mode, SessionId);
                break;
            }
        }

        public void ProfileLoadedEvent(object sender, UserManageEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case UserManageProvider.RC_GET_ALL:
                        ProfileList = e.UserProfileList != null ?
                            new ObservableCollection<Profile>(e.UserProfileList) : null;
                        break;
                    case UserManageProvider.RC_CREATE:
                        DialogHelper.Dispatcher.Invoke(delegate
                        {
                            GetAll(0);
                        });
                        break;
                    case UserManageProvider.RC_RESET_PASSWORD:
                        DialogHelper.Close();
                        DialogHelper.Show("重置成功，密码为：" + e.RandomPassword, "请及时修改！");
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
                    Container.Show(ChildViewList["userCompose"].View);
                });
            }
        }

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetAll(p == null ? -1 : (int) p));
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

        public ActionCommand ResetPwdSelectedCommand
        {
            get
            {
                return new ActionCommand(p => ResetPassword());
            }
        }


        #region SelectedCommand

        public ActionCommand SelectAllCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (ProfileList == null || ProfileList.Count == 0)
                        return;

                    foreach (var user in ProfileList)
                    {
                        user.IsSelected = true;
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
                    if (ProfileList == null || ProfileList.Count == 0)
                        return;

                    foreach (var user in ProfileList)
                    {
                        user.IsSelected = !user.IsSelected;
                    }
                });
            }
        }

        #endregion
    }
}
