using CommonLibrary.Domain;
using CommonLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CommonLibrary.Models
{
    public class ViewModelRelationship
    {
        public BaseViewModel ViewModel { get; set; }

        public object View { get; set; }

        public ViewModelRelationship(BaseViewModel model, object view)
        {
            ViewModel = model;
            View = view;

            if (model != null && view != null)
            {
                (view as UserControl).DataContext = ViewModel;
            }
        }
    }
}
