using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CourseStudent.Models
{
    public class MenuItem
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public ViewModelRelationship Relationship { get; set; }
    }
}
