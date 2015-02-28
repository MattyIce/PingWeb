using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingWeb.Core.Utilities;

namespace PingWeb.Core.Entities
{
    public interface IAuthToken
    {
        string Token { get; }
        long UserId { get; }
        bool IsValid { get; }
        DateTime Expiration { get; }
    }

    public class AuthToken : IAuthToken
    {
        public string Token { get; protected set; }
        public long UserId { get; protected set; }
        public bool IsValid { get; protected set; }
        public DateTime Issued { get; protected set; }
        public DateTime Expiration { get; protected set; }

        public AuthToken(string token)
        {
            this.Token = token;

            if (String.IsNullOrWhiteSpace(token))
                return;

            string[] parts = token.Split(new char[] { '.' });

            if (parts.Length < 3)
                return;

            long userId, ticks;

            if (!Int64.TryParse(parts[0], out userId) || userId <= 0)
                return;

            if (!Int64.TryParse(parts[1], out ticks) || ticks <= 0)
                return;

            this.UserId = userId;
            this.Issued = new DateTime(ticks);
            this.IsValid = Encryption.GetMD5Hash(String.Format("{0}.{1}.{2}", userId, ticks, ConfigurationManager.AppSettings["SecretKey"])) == parts[2];
        }

        internal AuthToken(long userId)
        {
            long ticks = DateTime.Now.Ticks;
            string hash = Encryption.GetMD5Hash(String.Format("{0}.{1}.{2}", userId, ticks, ConfigurationManager.AppSettings["SecretKey"]));
            this.Token = String.Format("{0}.{1}.{2}", userId, ticks, hash);
            this.UserId = userId;
            this.IsValid = true;
            this.Issued = new DateTime(ticks);
        }
    }
}
