using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using PG.Core.Web;


namespace PG.Web.Service
{
    /// <summary>
    /// Summary description for GetDataInfo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)] 
    public class GetDataInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string funcName = WebUtility.GetQueryString("fn", context).ToLower();

            //string jsonData = string.Empty; 
            string jsonData = "{}";
            switch (funcName)
            {
                //case "nextprjcode":
                //    jsonData = BLLibrary.ServiceBL.ServiceDataBL.GetJSon_NexProjectCode(context);
                //    break;
                //case "isprjcodeexists":
                //    jsonData = BLLibrary.ServiceBL.ServiceDataBL.GetJSon_IsProjectCodeExists(context);
                //    break;
                case "parsedate":
                    jsonData = BLLibrary.ServiceBL.ServiceDataBL.GetJSon_ParseDate(context);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(jsonData);

            context.ApplicationInstance.CompleteRequest();
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