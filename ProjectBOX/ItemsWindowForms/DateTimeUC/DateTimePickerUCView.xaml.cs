using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
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

namespace ProjectBOX.ItemsWindowForms.DateTimeUC
{
    /// <summary>
    /// Логика взаимодействия для DateTimePickerUCView.xaml
    /// </summary>
    public partial class DateTimePickerUCView : UserControl
    {
        public static readonly DependencyProperty CurrentDateTimeProperty = DependencyProperty.Register("CurrentDateTime", typeof(DateTime), typeof(DateTimePickerUCView)); 

        public DateTime CurrentDateTime
        {
            get
            {
                return (DateTime)GetValue(CurrentDateTimeProperty);
            }
            set
            {
                SetValue(CurrentDateTimeProperty, value);
            }
        }

        public DateTimePickerUCView()
        {
            InitializeComponent();
            Binding binding = new Binding("CurrentDateTime");
            binding.Source = UserControlDatePicker.DataContext;
            //binding.ElementName = "UserControlDatePicker";
            //binding.Path = new PropertyPath("DataContext.CurrentDateTime");
            UserControlDatePicker.SetBinding(CurrentDateTimeProperty, binding);
        }
    }
}
