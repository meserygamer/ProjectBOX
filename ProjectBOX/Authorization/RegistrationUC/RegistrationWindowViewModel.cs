using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectBOX.Authorization.RegistrationUC
{
    public class RegistrationWindowViewModel
    {
        private int? _lenghPassword;
        private bool _keyBoardFocusStatusOnPassword;
        private int? _lenghConfirmPassword;
        private bool _keyBoardFocusStatusOnConfirmPassword;
        
        public int? LenghPassword
        {
            get => _lenghPassword;
            set
            {
                _lenghPassword = value;
                OnPropertyChanged("LenghPassword");
            }
        }

        public bool KeyBoardFocusStatusOnPassword
        {
            get => _keyBoardFocusStatusOnPassword;
            set
            {
                _keyBoardFocusStatusOnPassword = value;
                OnPropertyChanged("KeyBoardFocusStatusOnPassword");
            }
        }

        public int? LenghConfirmPassword
        {
            get => _lenghConfirmPassword;
            set
            {
                _lenghConfirmPassword = value;
                OnPropertyChanged("LenghConfirmPassword");
            }
        }

        public bool KeyBoardFocusStatusOnConfirmPassword
        {
            get => _keyBoardFocusStatusOnConfirmPassword;
            set
            {
                _keyBoardFocusStatusOnConfirmPassword = value;
                OnPropertyChanged("KeyBoardFocusStatusOnConfirmPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public partial class RegistrationWindowView
    {
        public static readonly DependencyProperty HyperLinkClickProperty = DependencyProperty.Register("HyperLinkClick", typeof(RelayCommand), typeof(RegistrationWindowView));

        public RelayCommand HyperLinkClick
        {
            get => (RelayCommand)GetValue(HyperLinkClickProperty);
            set => SetValue(HyperLinkClickProperty, value);
        }
    }
}
