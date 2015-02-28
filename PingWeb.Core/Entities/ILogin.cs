using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWeb.Core.Entities
{
    public interface ILogin
    {
        long UserId { get; set; }
        LoginType Type { get; set; }
        string Name { get; set; }
        string Password { get; set; }
    }
}
