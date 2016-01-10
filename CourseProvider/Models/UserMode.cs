using CommonLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class UserMode : ObservableObject
    {
        private int mode;

        public int Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                NotifyPropertyChanged("Mode");
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
    }
}
