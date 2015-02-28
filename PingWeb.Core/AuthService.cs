using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingWeb.Core.Entities;

namespace PingWeb.Core
{
    public interface IAuthService
    {
        IAuthToken Login(string name, string password = null);
        IAuthToken Register(string name, string password = null);
    }

    public class AuthService : IAuthService
    {
        public LoginType LoginType { get { return LoginType.Email; } }
        public IAuthDataAdapter AuthDataAdapter { get; set; }

        public AuthService(IAuthDataAdapter authDataAdapter)
        {
            this.AuthDataAdapter = authDataAdapter;
        }

        public IAuthToken Login(string name, string password = null)
        {
            ILogin login = AuthDataAdapter.GetLogin(this.LoginType, name);

            if(login == null)
                throw new PingWebException(String.Format("No user found with the name: {0}", name), 103);

            if(!Utilities.PasswordHash.ValidatePassword(password, login.Password))
                throw new PingWebException(String.Format("The supplied password was incorrect for user: {0}", name), 104);

            return new AuthToken(login.UserId);
        }

        public IAuthToken Register(string name, string password = null)
        {
            if (AuthDataAdapter.GetLogin(this.LoginType, name) != null)
                throw new PingWebException(String.Format("An account has already been registered with the name: {0}", name), 101);

            long userId = AuthDataAdapter.CreateUser();

            if (!AuthDataAdapter.CreateLogin(this.LoginType, userId, name, Utilities.PasswordHash.CreateHash(password)))
                throw new PingWebException(String.Format("An unknown error occurred creating a new account.", name), 102);

            return this.Login(name, password);
        }
    }

    /// <summary>
    /// The different types of supported logins.
    /// </summary>
    public enum LoginType : short
    {
        Email = 1,
        Facebook = 2
    }
}
