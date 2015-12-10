using CommonLibrary.Domain;
using CommonLibrary.ViewModels;
using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseTeacher.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CourseTeacher.ViewModels
{
    public class AbsenceViewModel : BaseViewModel
    {
        private ObservableCollection<AuditAbsence> auditAbsenceList;
        public ObservableCollection<AuditAbsence> AuditAbsenceList
        {
            get
            {
                return auditAbsenceList;
            }
            set
            {
                auditAbsenceList = value;
                NotifyPropertyChanged("AuditAbsenceList");
            }
        }

        public string SessionId;

        private AuditAbsenceProvider Provider;

        public AbsenceViewModel(string sessionId)
        {
            SessionId = sessionId;

            Provider = new AuditAbsenceProvider();
            Provider.AbsenceEvent += AuditAbsenceLoadedEvent;
        }

        public override void Notify()
        {
            GetAllAuditAbsence();
        }

        public void GetAllAuditAbsence()
        {
            Provider.GetAll(SessionId);
        }

        public void AuditSelectedAbsence()
        {
            if (AuditAbsenceList == null || AuditAbsenceList.Count == 0)
            {
                return;
            }

            List<AuditAbsence> preRemoveAbsence = AuditAbsenceList.Where(a => a.IsSelected).ToList();

            if (preRemoveAbsence.Count > 0 && DialogHelper.Conirm("确定通过选中请假条吗？"))
            {
                DialogHelper.ShowProgressDialog("正在提交更改...");

                foreach (var removed in preRemoveAbsence)
                {
                    Provider.AuditAbsence(removed.Id, SessionId);
                    // Remove from data list whatever it perform in successful on server
                    AuditAbsenceList.Remove(AuditAbsenceList.Where(a => removed.Id == a.Id).First());
                }

                DialogHelper.Close();
            }
        }

        public void AuditAbsenceLoadedEvent(object sender, AuditAbsenceEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case AbsenceProvider.RC_GET_ALL:
                        AuditAbsenceList = e.AuditAbsenceList != null ? new ObservableCollection<AuditAbsence>(e.AuditAbsenceList) : null;
                        break;
                    default:
                        break;
                }
            }
        }

        #region EventCommand

        public ActionCommand AuditSelectedCommand
        {
            get
            {
                return new ActionCommand(p => AuditSelectedAbsence());
            }
        }

        #endregion

        #region SelectCommand

        public ActionCommand SelectAllCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    if (AuditAbsenceList == null || AuditAbsenceList.Count == 0)
                        return;

                    foreach (var absence in AuditAbsenceList)
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
                    if (AuditAbsenceList == null || AuditAbsenceList.Count == 0)
                        return;

                    foreach (var absence in AuditAbsenceList)
                    {
                        absence.IsSelected = !absence.IsSelected;
                    }
                });
            }
        }

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p =>
                {
                    GetAllAuditAbsence();
                });
            }
        }

        #endregion


    }
}
