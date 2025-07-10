using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.Threading;

using PG.DBClass.SystemDC;
using PG.DBClass.SecurityDC;
using PG.BLLibrary.SystemsBL;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Web.Service.Accounting
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetAccRefSettings : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //System.Threading.Thread.Sleep(2000);

            int companyID = PG.Core.Web.WebUtility.GetQueryStringInteger("companyid", context);
            int accRefTypeID = PG.Core.Web.WebUtility.GetQueryStringInteger("accreftypeid", context);


            dcAccRefSettings refSettings = AccRefSettingsBL.GetAccRefSettingByType(companyID, accRefTypeID);

            StringBuilder sb = new StringBuilder();

            if (refSettings == null)
            {
                sb.Append("{\"accrefsettings\": null");
                sb.Append("}");
            }
            else
            {
                sb.Append("{\"accrefsettings\":");
                sb.Append("{");
                sb.Append("\"companyid\":" + refSettings.CompanyID.ToString() + ",");
                sb.Append("\"accreftypeid\":\"" + refSettings.AccRefTypeID.ToString() + "\",");
                sb.Append("\"multicategory\":" + (refSettings.AllowMultipleCategory ? "1" : "0") + ",");
                sb.Append("\"sumbycategory\":\"" + (refSettings.TotalSumCheckByCtategory ? "1" : "0") + "\",");
                sb.Append("\"nonbindcategory\":\"" + (refSettings.AllowNonBindCategory ? "1" : "0") + "\",");

                sb.Append("\"lastcol\":\"" + "last" + "\"");
                sb.Append("}");
                ///
                sb.Append("}");

            }

            //sb.Append("{");
            //sb.Append("\"date\":");
            //sb.Append("{");
            //sb.Append("\"datestring\":\"" + dateString + "\"");

            ////sb.Append(",\"pageno\":" + qPageNo.ToString());

            //sb.Append("}");

            //sb.Append("}");


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
    //public class SearchHandler : IHttpHandler
    //{
    //    public void ProcessRequest(HttpContext context)
    //{
    //    var term = context.Request.QueryString["term"].ToString();

    //    context.Response.Clear();
    //    context.Response.ContentType = "application/json";

    //    var search = //TODO implement select logic based on the term above

    //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
    //    string json = jsSerializer.Serialize(search);
    //    context.Response.Write(json);
    //    context.Response.End();
    //}

    //    public bool IsReusable
    //    {
    //        get
    //        {
    //            return false;
    //        }
    //    }
    //}

}
