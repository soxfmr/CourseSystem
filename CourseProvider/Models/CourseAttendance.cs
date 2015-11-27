using CourseProvider.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class CourseAttendance : ObservableObject
    {
        private DateTime createdAT;

        public DateTime CreatedAt
        {
            get
            {
                return createdAT;
            }
            set
            {
                createdAT = value;
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
