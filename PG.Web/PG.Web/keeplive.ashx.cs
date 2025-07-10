using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG.Web
{
    /// <summary>
    /// Summary description for keeplive
    /// </summary>
    public class keeplive : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("ok");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}