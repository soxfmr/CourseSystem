using CourseProvider.Domain;
using System;

namespace CourseProvider.Models
{
    public class DispatchCourse : ObservableObject
    {
        private bool isSelected;

        public bool IsSelected {
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

        private string name;

        public string Name {
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

        private string teacherName;

        public string TeacherName {
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

        private int weekday;

        public int Weekday {
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

        private DateTime at;

        public DateTime At {
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

        private string location;

        public string Location {
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

        private int limit;

        public int Limit {
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

        private int current;

        public int Current {
            get
            {
                return current;
            }
            set
            {
                current = value;
                NotifyPropertyChanged("Current");
            }
        }
    }
}
