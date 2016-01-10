using CommonLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class Classroom : ObservableObject
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
                NotifyPropertyChanged("Reason");
            }
        }
    }
}
