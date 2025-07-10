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
using PG.Core.Web;

namespace PG.Web.Report
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GenerateReport : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int reportID = WebUtility.GetQueryStringInteger("rptid", context);
            string rptKey = string.Empty;

            switch (reportID)
            {
                case 630:  //project Requisition
                    //rptKey = AppReport.GenerateReport_PaymentRequisition(context.Request.QueryString);
                    break;

            }

            StringBuilder sb = new StringBuilder();

            //clear string;
            sb.Length = 0;


            sb.Append("{");
            sb.Append("\"report\":");
            sb.Append("{");
            sb.Append("\"isreport\":" + 1);
            sb.Append(",\"reportkey\":\"" + rptKey + "\"");
            //sb.Append(",\"totalrecord\":" + qTotalRecord.ToString());
           
            sb.Append("}");

            ///
            sb.Append("}");

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sb.ToString());
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
