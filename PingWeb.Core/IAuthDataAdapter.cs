using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingWeb.Core.Entities;

namespace PingWeb.Core
{
    public interface IAuthDataAdapter
    {
        ILogin GetLogin(LoginType type, string name);
        long CreateUser();
        bool CreateLogin(LoginType type, long userId, string name, string password = null);
    }
}
