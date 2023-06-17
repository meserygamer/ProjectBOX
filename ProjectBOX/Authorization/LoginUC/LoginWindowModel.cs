using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.Authorization.LoginUC
{
    public class Authorization
    {
        private AuthorizationData _authData;

        public Authorization(AuthorizationData authorizationData)
        {
            _authData = authorizationData;
        }

        public bool CheckUser()
        {
            //Здесь должен быть код проверки из базы
            return true;
        }
    }

    public class AuthorizationData
    {
        private string _login;
        private string _password;

        public AuthorizationData(string Login, string Password)
        {
            _login = Login;
            _password = Password;
        }

        public Authorization Authorization() => new Authorization(this);
    }
}
