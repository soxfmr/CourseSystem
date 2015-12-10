using CourseProvider.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class DispatchInfo : ObservableObject
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

        private string courseName;
        public string CourseName {
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

        private int courseId;
        public int CourseId {
            get
            {
                return courseId;
            }
            set
            {
                courseId = value;
                NotifyPropertyChanged("CourseId");
            }
        }

        private List<DispatchStudent> students;
        public List<DispatchStudent> Students {
            get
            {
                return students;
            }
            set
            {
                students = value;
                NotifyPropertyChanged("Students");
            }
        }
    }
}
