using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PingWeb.Json;

namespace PingWeb
{
    /// <summary>
    /// Summary description for WebServiceBase
    /// </summary>
    public abstract class WebServiceBase : IHttpHandler
    {
        protected HttpContext Context { get; set; }

        protected IDictionary<string, object> Data { get { return Context.Request.RequestContext.RouteData.Values; } }

        protected NameValueCollection Params { get { return Context.Request.Params; } }

        public WebServiceBase()
        {
            
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            this.Context = context;

            try
            {
                // Perform any setup that should be done before calling the target method.
                if (!SetupContext())
                    return;

                MethodInfo method = GetMethod((string)context.Request.RequestContext.RouteData.Values["method"]);

                if (method != null)
                    ReturnObject(method.Invoke(this, null));
                else
                    ReturnError("Invalid method or endpoint.", 1);
            }
            catch (Exception ex)
            {
                ReturnError((ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }

        protected virtual bool SetupContext() { return true; }

        protected virtual MethodInfo GetMethod(string methodName)
        {
            foreach (MethodInfo method in GetType().GetMethods())
            {
                ServiceMethodAttribute attr = method.GetCustomAttribute<ServiceMethodAttribute>();

                if (attr != null && attr.MethodName == methodName)
                    return method;
            }

            return null;
        }

        protected virtual void ReturnError(string message, short errorCode = 0)
        {
            Context.Response.Write(JsonSerializer.SerializeObject(new { error = message, code = errorCode }));
        }

        protected virtual void ReturnObject(object obj)
        {
            if (obj != null)
                Context.Response.Write(JsonSerializer.SerializeObject(obj));
        }

        protected virtual void ReturnCustom(string message)
        {
            Context.Response.Write(message);
        }

        public bool IsReusable { get { return false; } }
    }
}
