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
        /// <summary>
        /// Отслеживает длину пароля введенного пользователем для подсказки
        /// </summary>
        #region public int? LenghPassword
        private int? _lenghPassword;

        public int? LenghPassword
        {
            get => _lenghPassword;
            set
            {
                _lenghPassword = value;
                OnPropertyChanged("LenghPassword");
            }
        }
        #endregion

        /// <summary>
        /// Отслеживает находится ли пользовательский фокус внутри поля пароля, для подсказки
        /// </summary>
        #region public bool KeyBoardFocusStatus
        private bool _keyBoardFocusStatus;

        public bool KeyBoardFocusStatus
        {
            get => _keyBoardFocusStatus;
            set
            {
                _keyBoardFocusStatus = value;
                OnPropertyChanged("KeyBoardFocusStatus");
            }
        }
        #endregion

        /// <summary>
        /// Хранит хэш введенного пароля
        /// </summary>
        #region public string SecurePassword
        private string _securePassword;

        public string SecurePassword
        {
            get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }
        #endregion

        /// <summary>
        /// Хранит введенный логин
        /// </summary>
        #region public string Login
        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }
        #endregion

        /// <summary>
        /// Обработчик нажатия на кнопку "Войти"
        /// </summary>
        #region public RelayCommand LoginCommand
        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                  (_loginCommand = new RelayCommand(obj =>
                  {
                      int? UserID = (new AuthorizationData(_login, _securePassword)).Authorization().CheckUser();
                      if (UserID is not null) LoginWasSuccesfull((int)UserID);
                      else LoginWasFailed();
                  }));
            }
        }
        #endregion

        #region Privates Methods
        /// <summary>
        /// Выводит сообщение для неудачной авторизации
        /// </summary>
        private void LoginWasFailed() => MessageBox.Show("Неправильно введен логин или пароль");

        /// <summary>
        /// Набор действий в случае успешной авторизации, закрытвает окно и открывает окно предметов
        /// </summary>
        /// <param name="IDAuthorizatedUser">Номер авторизированного пользователя</param>
        private void LoginWasSuccesfull(int IDAuthorizatedUser)
        {
            Application.Current.Resources["UserID"] = IDAuthorizatedUser;
            (new ItemsWindow.ItemsWindowView()).Show();
            Application.Current.Windows[0].Close();
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

    public partial class LoginWindowView
    {
        #region public RelayCommand HyperLinkClick DependencyProperty
        public static readonly DependencyProperty HyperLinkClickProperty = DependencyProperty.Register("HyperLinkClick", typeof(RelayCommand), typeof(LoginWindowView));

        public RelayCommand HyperLinkClick
        {
            get => (RelayCommand)GetValue(HyperLinkClickProperty);
            set => SetValue(HyperLinkClickProperty, value);
        }
        #endregion
    }

}
