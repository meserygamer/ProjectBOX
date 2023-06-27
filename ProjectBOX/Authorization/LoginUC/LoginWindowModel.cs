using ProjectBOX.EntityFrameworkModelFiles;
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
        public event Action<int> LoginWasSuccesfull;
        public event Action LoginWasFailed;

        private AuthorizationData _authData;

        public Authorization(AuthorizationData authorizationData)
        {
            _authData = authorizationData;
        }

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

    public class AuthorizationData
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public AuthorizationData(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        public Authorization Authorization() => new Authorization(this);
    }
}
