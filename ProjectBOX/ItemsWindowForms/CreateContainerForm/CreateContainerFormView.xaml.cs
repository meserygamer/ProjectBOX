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
using System.Windows.Shapes;

namespace ProjectBOX.ItemsWindowForms.CreateContainerForm
{
    /// <summary>
    /// Логика взаимодействия для CreateContainerFormView.xaml
    /// </summary>
    public partial class CreateContainerFormView : Window
    {
        public CreateContainerFormView()
        {
            InitializeComponent();
        }

        public void EndThisDialogOnTrue()
        {
            this.DialogResult = true;
        }
    }
}
