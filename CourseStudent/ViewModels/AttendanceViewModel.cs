using CourseProvider.Events;
using CourseProvider.Models;
using CourseProvider.Providers;
using CourseStudent.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStudent.ViewModels
{
    public class AttendanceViewModel : BaseViewModel
    {
        private ObservableCollection<Attendance> attendanceList;
        public ObservableCollection<Attendance> AttendanceList
        {
            get
            {
                return attendanceList;
            }
            set
            {
                attendanceList = value;
                NotifyPropertyChanged("AttendanceList");
            }
        }

        public IViewContainer Container;

        public string SessionId;

        private AttendanceProvider Provider;

        public AttendanceViewModel(IViewContainer container, string sessionId)
        {
            Container = container;
            SessionId = sessionId;

            Provider = new AttendanceProvider();
            Provider.AbsenceEvent += AttendanceLoadedEvent;
        }

        public override void Notify()
        {
            GetAllAttendance();
        }

        public void GetAllAttendance()
        {
            Provider.GetAll(SessionId);
        }

        public void AttendanceLoadedEvent(object sender, AttendanceEventArgs e)
        {
            if (e.IsSuccess)
            {
                switch (e.RequestCode)
                {
                    case AttendanceProvider.RC_GET_ALL:
                        AttendanceList = e.AttendanceList != null ? new ObservableCollection<Attendance>(e.AttendanceList) : null;
                        break;
                    default:
                        break;
                }
            }
        }

        public ActionCommand RefreshCommand
        {
            get
            {
                return new ActionCommand(p => GetAllAttendance());
            }
        }
    }
}
