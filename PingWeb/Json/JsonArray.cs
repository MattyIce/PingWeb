using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWeb.Json
{
    public class JsonArray : List<object>
    {
        public override string ToString()
        {
            return JsonSerializer.SerializeObject(this);
        }
    }
}
