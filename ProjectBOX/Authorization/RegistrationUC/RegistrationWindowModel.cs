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
    public class Validator
    {
        private RegistrationData _registrationData;
        private bool _valid = true;

        public Validator(RegistrationData registrationData)
        {
            _registrationData = registrationData;
        }

        public Validator CheckOnLoginAvailability()
        {
            using (ProjectBoxDbContext DB = new())
            {
                if(DB.AuthorizationData.Find(_registrationData.Login) is not null) _valid = false;
            }
            return this;
        }

        public Validator CheckLoginOnValid()
        {
            if(!Regex.IsMatch(_registrationData.Login, @"^[a-zA-Z0-9]{6,}$"))
                _valid = false;
            return this;
        }

        public Validator CheckPasswordOnValid()
        {
            if(!Regex.IsMatch(_registrationData.Password, @"^(?=.*[A-Z])(?=.*[0-9])[A-z0-9]{6,}$"))
                _valid = false;
            return this;
        }

        public Validator CheckPasswordsOnEquals()
        {
            if(_registrationData.Password != _registrationData.ConfirmPassword)
                _valid = false;
            return this;
        }

        public bool Validate() => _valid;
    }

    public class RegistrationData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegistrationData(string login, string password, string confirmPassword)
        {
            Login = login ?? "";
            Password = password ?? "";
            ConfirmPassword = confirmPassword ?? "" ;
        }

        public Validator Validator()
        {
            return new Validator(this);
        }
    }

    public class InteractionsRegistrationUCWithDB
    {
        private static InteractionsRegistrationUCWithDB _singleton;

        public InteractionsRegistrationUCWithDB() { }

        public static InteractionsRegistrationUCWithDB GetExamler()
        {
            return _singleton ?? (_singleton = new InteractionsRegistrationUCWithDB());
        }

        public async void AddUserInDataBase(string Login, string Password)
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
