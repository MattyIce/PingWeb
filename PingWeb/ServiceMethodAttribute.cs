using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWeb
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ServiceMethodAttribute : Attribute
    {
        public string MethodName { get; set; }
    }
}
