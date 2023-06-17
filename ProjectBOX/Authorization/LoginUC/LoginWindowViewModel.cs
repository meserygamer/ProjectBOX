using ProjectBOX.Authorization.RegistrationUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectBOX.Authorization.LoginUC
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        private int? _lenghPassword;
        private bool _keyBoardFocusStatus;
        private string _securePassword;
        private string _login;
        private RelayCommand _loginCommand;

        public RelayCommand RelayCommand
        {
            get
            {
                return _loginCommand ??
                  (_loginCommand = new RelayCommand(obj =>
                  {
                      
                  }));
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string SecurePassword
        {
            get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        public int? LenghPassword
        {
            get => _lenghPassword;
            set
            {
                _lenghPassword = value;
                OnPropertyChanged("LenghPassword");
            }
        }

        public bool KeyBoardFocusStatus
        {
            get => _keyBoardFocusStatus;
            set
            {
                _keyBoardFocusStatus = value;
                OnPropertyChanged("KeyBoardFocusStatus");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public partial class LoginWindowView
    {
        public static readonly DependencyProperty HyperLinkClickProperty = DependencyProperty.Register("HyperLinkClick", typeof(RelayCommand), typeof(LoginWindowView));

        public RelayCommand HyperLinkClick
        {
            get => (RelayCommand)GetValue(HyperLinkClickProperty);
            set => SetValue(HyperLinkClickProperty, value);
        }
    }

}
