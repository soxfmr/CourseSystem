using CommonLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class DispatchManage : ObservableObject
    {
        private bool isSelected;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private int id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string teacherId;

        public string TeacherId
        {
            get
            {
                return teacherId;
            }
            set
            {
                teacherId = value;
                NotifyPropertyChanged("TeacherId");
            }
        }

        private string teacherName;

        public string TeacherName
        {
            get
            {
                return teacherName;
            }
            set
            {
                teacherName = value;
                NotifyPropertyChanged("TeacherName");
            }
        }

        private int classroomId;

        public int ClassroomId
        {
            get
            {
                return classroomId;
            }
            set
            {
                classroomId = value;
                NotifyPropertyChanged("ClassroomId");
            }
        }

        private string location;

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private string weekday;

        public string Weekday
        {
            get
            {
                return weekday;
            }
            set
            {
                weekday = value;
                NotifyPropertyChanged("Weekday");
            }
        }

        private int limit;

        public int Limit
        {
            get
            {
                return limit;
            }
            set
            {
                limit = value;
                NotifyPropertyChanged("Limit");
            }
        }

        private DateTime at;

        public DateTime At
        {
            get
            {
                return at;
            }
            set
            {
                at = value;
                NotifyPropertyChanged("At");
            }
        }
    }
}
