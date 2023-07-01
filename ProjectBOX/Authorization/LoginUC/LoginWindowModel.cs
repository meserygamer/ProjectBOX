using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.Authorization.LoginUC
{
    /// <summary>
    /// Класс авторизации
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// Переданные авторизационные данные
        /// </summary>
        private AuthorizationData _authData;

        /// <summary>
        /// Конструктор присваивающий переданные авторизационные данные к себе
        /// </summary>
        /// <param name="authorizationData">Авторизационные данные</param>
        public Authorization(AuthorizationData authorizationData)
        {
            _authData = authorizationData;
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <returns></returns>
        public int? CheckUser()
        {
            using (ProjectBoxDbContext DB = new())
            {
                if (DB.AuthorizationData.Find(_authData.Login) is not null
                && (DB.AuthorizationData.Find(_authData.Login)).SecurePasssword == _authData.Password)
                {
                    return DB.AuthorizationData.Find(_authData.Login).UserId;
                }
                else return null;
            }
        }
    }

    /// <summary>
    /// Класс данных авторизации
    /// </summary>
    public class AuthorizationData
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Пароль, сюда должен передаваться пароль предварительно зашифрованный SHA256
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Конструктор для авторизационных данных
        /// </summary>
        /// <param name="Login">Логин пользователя для поиска</param>
        /// <param name="Password">Пароль зашифрованный SHA256</param>
        public AuthorizationData(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        /// <summary>
        /// Метод получения из авторизационных данных экземпляра класса авторизация
        /// </summary>
        /// <returns>Класс авторизации с данными внутри</returns>
        public Authorization Authorization() => new Authorization(this);
    }
}
