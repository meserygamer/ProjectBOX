using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBOX.Authorization.RegistrationUC
{
    /// <summary>
    /// Валидатор полей регистрации
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Пользовательские данные для регистрации
        /// </summary>
        private RegistrationData _registrationData;

        /// <summary>
        /// Валидность данных
        /// </summary>
        private bool _valid = true;

        /// <summary>
        /// Конструктор валидатора используя регистрационные данные
        /// </summary>
        /// <param name="registrationData">Регистрационные данные пользователя</param>
        public Validator(RegistrationData registrationData)
        {
            _registrationData = registrationData;
        }

        /// <summary>
        /// Проверка логина на незанятость
        /// </summary>
        /// <returns>Текущий экземпляр класса</returns>
        public Validator CheckOnLoginAvailability()
        {
            using (ProjectBoxDbContext DB = new())
            {
                if(DB.AuthorizationData.Find(_registrationData.Login) is not null) _valid = false;
            }
            return this;
        }

        /// <summary>
        /// Проверка Логина на соответсвие требованиям
        /// </summary>
        /// <returns>Текущий экземпляр класса</returns>
        public Validator CheckLoginOnValid()
        {
            if(!Regex.IsMatch(_registrationData.Login, @"^[a-zA-Z0-9]{6,}$"))
                _valid = false;
            return this;
        }

        /// <summary>
        /// Проверка пароля на соответсвие требованиям
        /// </summary>
        /// <returns>Текущий экземпляр класса</returns>
        public Validator CheckPasswordOnValid()
        {
            if(!Regex.IsMatch(_registrationData.Password, @"^(?=.*[A-Z])(?=.*[0-9])[A-z0-9]{6,}$"))
                _valid = false;
            return this;
        }

        /// <summary>
        /// Проверка соответствия паролей
        /// </summary>
        /// <returns>Текущий экземпляр класса</returns>
        public Validator CheckPasswordsOnEquals()
        {
            if(_registrationData.Password != _registrationData.ConfirmPassword)
                _valid = false;
            return this;
        }

        /// <summary>
        /// Метод валидации
        /// </summary>
        /// <returns>Валидность данных</returns>
        public bool Validate() => _valid;
    }

    /// <summary>
    /// Регистрационные данные
    /// </summary>
    public class RegistrationData
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Конструктор регистрационных данных
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="confirmPassword">Подтверждение пароля</param>
        public RegistrationData(string login, string password, string confirmPassword)
        {
            Login = login ?? "";
            Password = password ?? "";
            ConfirmPassword = confirmPassword ?? "" ;
        }

        /// <summary>
        /// Получение валидатора из регистрационных данных
        /// </summary>
        /// <returns></returns>
        public Validator Validator()
        {
            return new Validator(this);
        }
    }

    /// <summary>
    /// Singleton класс для взаимодействия с БД
    /// </summary>
    public class InteractionsRegistrationUCWithDB
    {
        #region SingletonRealize
        private static InteractionsRegistrationUCWithDB _singleton;

        private InteractionsRegistrationUCWithDB() { }

        public static InteractionsRegistrationUCWithDB GetExamler()
        {
            return _singleton ?? (_singleton = new InteractionsRegistrationUCWithDB());
        }
        #endregion

        /// <summary>
        /// Добавление пользователя в БД
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        public async void AddUserInDataBaseAsync(string Login, string Password)
        {
            await Task.Run(() => 
            {
                using (ProjectBoxDbContext DB = new())
                {
                    using (SHA256 hash = SHA256.Create())
                    {
                        Password = Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(Password)));
                    }
                    DB.UserData.Add(new UserDatum {UserName = Login, AuthorizationDatum = new AuthorizationDatum {Login = Login, SecurePasssword = Password}});
                    DB.SaveChanges();
                }
            });
        }
    }
}
