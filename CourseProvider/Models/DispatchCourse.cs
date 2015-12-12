using CommonLibrary.Domain;
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

        private string description;

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                NotifyPropertyChanged("Description");
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

        private string weekday;

        public string Weekday {
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
                Remain = Limit - Current;

                NotifyPropertyChanged("Current");
            }
        }

        private int remain;

        public int Remain
        {
            get
            {
                return remain;
            }
            set
            {
                remain = value;
                NotifyPropertyChanged("Remain");
            }
        }
    }
}
