using CommonLibrary.Domain;
using System;

namespace CourseProvider.Models
{
    public class Attendance : ObservableObject
    {
        private DateTime createAT;

        public DateTime CreateAt
        {
            get
            {
                return createAT;
            }
            set
            {
                createAT = value;
                NotifyPropertyChanged("CreateAt");
            }
        }

        private string type;

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }

        private string courseName;

        public string CourseName
        {
            get
            {
                return courseName;
            }
            set
            {
                courseName = value;
                NotifyPropertyChanged("CourseName");
            }
        }
    }
}
