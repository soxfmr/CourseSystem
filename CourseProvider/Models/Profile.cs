using CommonLibrary.Domain;
using System;

namespace CourseProvider.Models
{
    public class Profile : ObservableObject
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
    }
}
