using CommonLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class Major : ObservableObject
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
    }
}
