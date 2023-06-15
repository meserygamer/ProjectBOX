using ProjectBOX.Authorization.RegistrationUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.Authorization
{
    class AuthorizationViewModel : INotifyPropertyChanged
    {
        private RelayCommand _registrationSwap;
        private RelayCommand _loginSwap;
        private AuthorizationScreens _currentScreen;

        public RelayCommand RegistrationSwap
        {
            get
            {
                return _registrationSwap ??
                  (_registrationSwap = new RelayCommand(obj =>
                  {
                      //MessageBox.Show("Ты захотел свапнуть меня (Регистрация)!");
                      CurrentScreen = AuthorizationScreens.Login;
                  }));
            }
        }

        public RelayCommand LoginSwap
        {
            get
            {
                return _loginSwap ??
                  (_loginSwap = new RelayCommand(obj =>
                  {
                      //MessageBox.Show("Ты захотел свапнуть меня (Логин)!");
                      CurrentScreen = AuthorizationScreens.Registration;
                  }));
            }
        }

        public AuthorizationScreens CurrentScreen
        {
            get { return _currentScreen; }
            set
            {
                _currentScreen = value;
                OnPropertyChanged("CurrentScreen");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public enum AuthorizationScreens
    {
        Login,
        Registration
    }

}
