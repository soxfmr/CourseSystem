using CommonLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class DialogViewModel : ObservableObject
    {
        private string message;

        public string Message {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public DialogViewModel(string message)
        {
            Message = message;
        }
    }
}
