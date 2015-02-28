using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWeb.Core
{
    public class PingWebException : Exception
    {
        public short ErrorCode { get; set; }

        public PingWebException(string message, short errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
