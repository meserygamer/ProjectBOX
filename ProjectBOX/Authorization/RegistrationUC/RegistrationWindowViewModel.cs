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
        private int? _lenghPassword;
        private bool _keyBoardFocusStatusOnPassword;
        private int? _lenghConfirmPassword;
        private bool _keyBoardFocusStatusOnConfirmPassword;
        private string _login;
        private string _password;
        private string _confirmPassword;
        private RelayCommand _registrationCommand;
        
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

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

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
                      InteractionsRegistrationUCWithDB.GetExamler().AddUserInDataBase(Login,Password);
                      CleanAllFields();
                      MessageBox.Show("Аккаунт был успешно зарегистрирован! Теперь вы можете авторизироваться в системе");
                  }));
            }
        }

        private void CleanAllFields()
        {
            Login = "";
            Password = "";
            ConfirmPassword = "";
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
