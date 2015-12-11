using CommonLibrary.Domain;
using System;

namespace CourseProvider.Models
{
    public class CourseAttendance : ObservableObject
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

        private DateTime createdAt;

        public DateTime CreatedAt
        {
            get
            {
                return createdAt;
            }
            set
            {
                createdAt = value;
                NotifyPropertyChanged("CreatedAt");
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

        private int population;

        public int Population
        {
            get
            {
                return population;
            }
            set
            {
                population = value;
                NotifyPropertyChanged("Population");
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
