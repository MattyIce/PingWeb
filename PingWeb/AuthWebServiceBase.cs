using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingWeb.Core;
using PingWeb.Core.Entities;

namespace PingWeb
{
    public abstract class AuthWebServiceBase : WebServiceBase
    {
        protected IAuthToken Token { get; set; }

        public IAuthService AuthService { get; set; }

        public AuthWebServiceBase(IAuthService authService)
        {
            AuthService = authService;
        }

        protected override bool SetupContext()
        {
            if (!base.SetupContext())
                return false;

            this.Token = new AuthToken(Params["token"]);

            if (!this.Token.IsValid)
            {
                ReturnError("Invalid or expired access token.", 2);
                return false;
            }

            return true;
        }
    }
}
