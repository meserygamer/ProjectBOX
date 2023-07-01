using ProjectBOX.Authorization.LoginUC;
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
    public class RegistrationWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Длина пароля для подсказки
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
        /// Значение фокуса пользователя на поле пароля, для подсказки
        /// </summary>
        #region public bool KeyBoardFocusStatusOnPassword
        private bool _keyBoardFocusStatusOnPassword;

        public bool KeyBoardFocusStatusOnPassword
        {
            get => _keyBoardFocusStatusOnPassword;
            set
            {
                _keyBoardFocusStatusOnPassword = value;
                OnPropertyChanged("KeyBoardFocusStatusOnPassword");
            }
        }
        #endregion

        /// <summary>
        /// Длина подтвержденного пароля для подсказки
        /// </summary>
        #region public int? LenghConfirmPassword
        private int? _lenghConfirmPassword;

        public int? LenghConfirmPassword
        {
            get => _lenghConfirmPassword;
            set
            {
                _lenghConfirmPassword = value;
                OnPropertyChanged("LenghConfirmPassword");
            }
        }
        #endregion

        /// <summary>
        /// Значение фокуса пользователя на поле подтвержденного пароля, для подсказки
        /// </summary>
        #region public bool KeyBoardFocusStatusOnConfirmPassword
        private bool _keyBoardFocusStatusOnConfirmPassword;

        public bool KeyBoardFocusStatusOnConfirmPassword
        {
            get => _keyBoardFocusStatusOnConfirmPassword;
            set
            {
                _keyBoardFocusStatusOnConfirmPassword = value;
                OnPropertyChanged("KeyBoardFocusStatusOnConfirmPassword");
            }
        }
        #endregion

        /// <summary>
        /// Хранит введенный пользователем логин
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
        /// Хранит введенный пользователем пароль
        /// </summary>
        #region public string Password
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        /// <summary>
        /// Хранит введенный пользователем подтвержденный пароль
        /// </summary>
        #region public string ConfirmPassword
        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
        #endregion

        /// <summary>
        /// Действия в случае нажатия на кнопку регистрации
        /// </summary>
        #region public RelayCommand RegistrationCommand
        private RelayCommand _registrationCommand;

        public RelayCommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ??
                  (_registrationCommand = new RelayCommand(obj =>
                  {
                      Validator RegistrationValidator = (new RegistrationData(_login, _password, _confirmPassword)).Validator();
                      if(!RegistrationValidator.CheckLoginOnValid().Validate())
                      {
                          MessageBox.Show("Введенный логин не соответсвует требованиям! Логин должен состоять из латинских букв или цифр," +
                              " а также быть не короче 6 символов");
                          return;
                      }
                      if(!RegistrationValidator.CheckOnLoginAvailability().Validate())
                      {
                          MessageBox.Show("Введенный логин занят другим пользователем!");
                          return;
                      }
                      if (!RegistrationValidator.CheckPasswordOnValid().Validate())
                      {
                          MessageBox.Show("Введенный пароль не соответсвует требованиям! Пароль должен состоять из латинских букв и цифр," +
                              " иметь в своем составе минимум 1 цифру и 1 заглавную букву, а также быть не короче 6 символов");
                          return;
                      }
                      if (!RegistrationValidator.CheckPasswordsOnEquals().Validate())
                      {
                          MessageBox.Show("Введенные пароли не идентичны!");
                          return;
                      }
                      InteractionsRegistrationUCWithDB.GetExamler().AddUserInDataBaseAsync(Login,Password);
                      CleanAllFields();
                      MessageBox.Show("Аккаунт был успешно зарегистрирован! Теперь вы можете авторизироваться в системе");
                  }));
            }
        }
        #endregion

        /// <summary>
        /// Метод очистки всех полей
        /// </summary>
        private void CleanAllFields()
        {
            Login = "";
            Password = "";
            ConfirmPassword = "";
        }

        #region INotifyPropertyChanged Realize
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }

    public partial class RegistrationWindowView
    {
        /// <summary>
        /// Для хранения действий по нажатию на гиперссылку
        /// </summary>
        #region public RelayCommand HyperLinkClick DependencyProperty
        public static readonly DependencyProperty HyperLinkClickProperty = DependencyProperty.Register("HyperLinkClick", typeof(RelayCommand), typeof(RegistrationWindowView));

        public RelayCommand HyperLinkClick
        {
            get => (RelayCommand)GetValue(HyperLinkClickProperty);
            set => SetValue(HyperLinkClickProperty, value);
        }
        #endregion
    }
}
