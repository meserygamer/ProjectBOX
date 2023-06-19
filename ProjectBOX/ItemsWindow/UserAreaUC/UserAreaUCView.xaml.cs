using ProjectBOX.Authorization.LoginUC;
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

namespace ProjectBOX.ItemsWindow.UserAreaUC
{
    /// <summary>
    /// Логика взаимодействия для UserAreaUCView.xaml
    /// </summary>
    public partial class UserAreaUCView : UserControl
    {
        public static readonly DependencyProperty NickNameProperty = DependencyProperty.Register("NickName", typeof(string), typeof(UserAreaUCView));
        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register("Email", typeof(string), typeof(UserAreaUCView));
        public static readonly DependencyProperty ClickOnButtonProperty = DependencyProperty.Register("ClickOnButton", typeof(RelayCommand), typeof(UserAreaUCView));

        public string NickName
        {
            get => (string)GetValue(NickNameProperty);
            set => SetValue(NickNameProperty, value);
        }

        public string Email
        {
            get => (string)GetValue(EmailProperty);
            set => SetValue(EmailProperty, value);
        }

        public RelayCommand ClickOnButton
        {
            get => (RelayCommand)GetValue(ClickOnButtonProperty);
            set => SetValue(ClickOnButtonProperty, value);
        }

        public UserAreaUCView()
        {
            InitializeComponent();
        }
    }
}
