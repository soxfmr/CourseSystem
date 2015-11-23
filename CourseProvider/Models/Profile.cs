using CourseProvider.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Models
{
    public class Profile : ObservableObject
    {
        private string email;

        public string Email {
            get
            {
                return email;
            }
            set
            {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }

        private string avatar;

        public string Avatar {
            get
            {
                return avatar;
            }
            set
            {
                avatar = value;
                NotifyPropertyChanged("Avatar");
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
        
        private string cellphone;

        public string Cellphone {
            get
            {
                return cellphone;
            }
            set
            {
                cellphone = value;
                NotifyPropertyChanged("Cellphone");
            }
        }
    }
}
