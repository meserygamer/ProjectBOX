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
        /// <summary>
        /// Обработка переключения на регистрацию из авторизации
        /// </summary>
        #region Public RelayCommand RegistrationSwap
        private RelayCommand _registrationSwap;

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
        #endregion

        /// <summary>
        /// Обработка переключения на авторизацию из регистрации
        /// </summary>
        #region Public RelayCommand LoginSwap
        private RelayCommand _loginSwap;

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
        #endregion

        /// <summary>
        /// Текущее состояние окна входа
        /// </summary>
        #region Public AuthorizationScreens CurrentScreen
        private AuthorizationScreens _currentScreen;

        public AuthorizationScreens CurrentScreen
        {
            get { return _currentScreen; }
            set
            {
                _currentScreen = value;
                OnPropertyChanged("CurrentScreen");
            }
        }
        #endregion

        #region INotifyPropertyChanged Realize
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }

    /// <summary>
    /// Режимы окна входа в систему
    /// </summary>
    public enum AuthorizationScreens
    {
        Login,
        Registration
    }
}
