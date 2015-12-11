using CommonLibrary.Domain;

namespace CourseProvider.Models
{
    public class DispatchStudent : ObservableObject
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

        private int studentId;

        public int StudentId
        {
            get
            {
                return studentId;
            }
            set
            {
                studentId = value;
                NotifyPropertyChanged("StudentId");
            }
        }

        private string studentName;

        public string StudentName
        {
            get
            {
                return studentName;
            }
            set
            {
                studentName = value;
                NotifyPropertyChanged("StudentName");
            }
        }
    }
}
