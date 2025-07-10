using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.Web.SessionState;

using PG.Core.Web;

namespace PG.Web.Report
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetExcelData : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string exKey =  WebUtility.GetQueryString("ek",context);

            string strExcel = string.Empty;

            if (context.Session[exKey] != null)
            {
                strExcel = context.Session[exKey].ToString();
                context.Session[exKey] = null;
            }

            string attachment = "attachment; filename=employee_data.xls";
            context.Response.ClearContent();
            //context.Response.Buffer = true;
            context.Response.AddHeader("content-disposition", attachment);
            context.Response.ContentType = "application/vnd.ms-excel";
            //context.Response.ContentType = "application/ms-excel";
            //context.Response.ContentType = "text/csv";

            context.Response.Write(strExcel.ToString());
            context.ApplicationInstance.CompleteRequest();

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
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
