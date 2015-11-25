using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseStudent.Domain;
using CourseStudent.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStudent.ViewModels
{
    public class AbsenceViewModel : LayerBaseViewModel
    {
        private ObservableCollection<Absence> normalAbsenceList;
        public ObservableCollection<Absence> NormalAbsenceList
        {
            get
            {
                return normalAbsenceList;
            }
            set
            {
                normalAbsenceList = value;
                NotifyPropertyChanged("NormalAbsenceList");
            }
        }

        private ObservableCollection<Absence> changeableAbsenceList;
        public ObservableCollection<Absence> ChangeableAbsenceList
        {
            get
            {
                return changeableAbsenceList;
            }
            set
            {
                changeableAbsenceList = value;
                NotifyPropertyChanged("ChangeableAbsenceList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private AbsenceProvider Provider;

        public AbsenceViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new AbsenceProvider();
            Provider.AbsenceEvent += AbsenceLoadedEvent;
        }

        public override void Notify()
        {
            GetAllAbsence();
            GetAllChangeableAbsence();

            NotifyChildViewModel();
        }

        public void GetAllAbsence()
        {
            Provider.GetAll(SessionId);
        }

        public void GetAllChangeableAbsence()
        {
            Provider.GetAllChangeable(SessionId);
        }

        private void UpdateSelectedAbsence()
        {
            if (ChangeableAbsenceList == null || ChangeableAbsenceList.Count == 0)
            {
                return;
            }

            DialogHelper.ShowProgressDialog("正在提交更改...");

            foreach (var absence in ChangeableAbsenceList)
            {
                if (absence.IsSelected)
                {
                    absence.IsSelected = false;

                    Provider.Update(absence.Reason, absence.Id, SessionId);
                }
            }

            DialogHelper.Close();
        }


        public void RemoveSelectedAbsence()
        {
            if (ChangeableAbsenceList == null || ChangeableAbsenceList.Count == 0)
            {
                return;
            }

            List<Absence> preRemoveAbsence = new List<Absence>();
            foreach (var absence in ChangeableAbsenceList)
            {
                if (absence.IsSelected)
                    preRemoveAbsence.Add(absence);
            }

            if (preRemoveAbsence.Count > 0 && DialogHelper.Conirm("确定退订当前课程吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemoveAbsence)
                {
                    Provider.Destroy(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server
                    ChangeableAbsenceList.Remove(removed);
                }

                DialogHelper.Close();
            }
        }

        public void AbsenceLoadedEvent(object sender, AbsenceEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case AbsenceProvider.RC_GET_ALL:
                        NormalAbsenceList = e.AbsenceList != null ? new ObservableCollection<Absence>(e.AbsenceList) : null;
                        break;
                    case AbsenceProvider.RC_GET_ALL_CHANGEABLE:
                        ChangeableAbsenceList = e.AbsenceList != null ? new ObservableCollection<Absence>(e.AbsenceList) : null;
                        break;
                    case AbsenceProvider.RC_CREATE:
                        DialogHelper.Dispatcher.Invoke(delegate
                        {
                            GetAllChangeableAbsence();
                        });
                        break;
                    default:
                        break;
                }
            }
        }

        #region EventCommand

        public ActionCommand RemoveSelectedCommand
        {
            get
            {
                return new ActionCommand(p => RemoveSelectedAbsence());
            }
        }

        public ActionCommand UpdateSelectedCommand
        {
            get
            {
                return new ActionCommand(p => UpdateSelectedAbsence());
            }
        }

        #endregion

        #region SelectCommand

        public ActionCommand ShowAbsenceComposeViewCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    Container.Show(ChildViewList["absenceCompose"].View);
                });
            }
        }

        public ActionCommand SelectAllCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (ChangeableAbsenceList == null || ChangeableAbsenceList.Count == 0)
                        return;

                    foreach (var absence in ChangeableAbsenceList)
                    {
                        absence.IsSelected = true;
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
                    if (ChangeableAbsenceList == null || ChangeableAbsenceList.Count == 0)
                        return;

                    foreach (var absence in ChangeableAbsenceList)
                    {
                        absence.IsSelected = !absence.IsSelected;
                    }
                });
            }
        }
        #endregion


    }
}
