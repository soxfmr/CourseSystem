using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseTeacher
{
    /// <summary>
    /// Toolbar.xaml 的交互逻辑
    /// </summary>
    public partial class Toolbar : UserControl
    {
        public ICommand BackCommand
        {
            get
            {
                return GetValue(BackCommandProperty) as ICommand;
            }
            set
            {
                SetValue(BackCommandProperty, value);
            }
        }

        public ICommand RefershCommand
        {
            get
            {
                return GetValue(RefershCommandProperty) as ICommand;
            }
            set
            {
                SetValue(RefershCommandProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return GetValue(TitleProperty) as string;
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public Visibility BackVisibility
        {
            get
            {
                return (Visibility) GetValue(BackVisibilityProperty);
            }
            set
            {
                SetValue(BackVisibilityProperty, value);
            }
        }

        public Visibility RefreshVisibility
        {
            get
            {
                return (Visibility) GetValue(RefreshVisibilityProperty);
            }
            set
            {
                SetValue(RefreshVisibilityProperty, value);
            }
        }

        public int PositionAdjustment
        {
            get
            {
                return (int) GetValue(RefreshVisibilityProperty);
            }
            set
            {
                SetValue(RefreshVisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Toolbar));

        public static readonly DependencyProperty BackCommandProperty = DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(Toolbar));

        public static readonly DependencyProperty RefershCommandProperty = DependencyProperty.Register("RefershCommand", typeof(ICommand), typeof(Toolbar));

        public static readonly DependencyProperty PositionAdjustmentProperty = DependencyProperty.Register("PositionAdjustment", typeof(int), typeof(Toolbar));

        public static readonly DependencyProperty BackVisibilityProperty = DependencyProperty.Register("BackVisibility", typeof(Visibility), typeof(Toolbar), 
            new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty RefreshVisibilityProperty = DependencyProperty.Register("RefreshVisibility", typeof(Visibility), typeof(Toolbar),
            new PropertyMetadata(Visibility.Visible));

        public Toolbar()
        {
            InitializeComponent();

            DataContext = this;
        }
    }
}
