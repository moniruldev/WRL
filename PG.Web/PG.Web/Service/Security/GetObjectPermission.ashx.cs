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

namespace PG.Web.Service.Security
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetObjectPermission : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //System.Threading.Thread.Sleep(2000);

            int appID = PG.Core.Web.WebUtility.GetQueryStringInteger("appid", context);
            int userID = PG.Core.Web.WebUtility.GetQueryStringInteger("userid", context);
            int appObjectID = PG.Core.Web.WebUtility.GetQueryStringInteger("objectid", context);
            int seekPermission = PG.Core.Web.WebUtility.GetQueryStringInteger("seekperm", context);
            bool isPermited = AppSecurity.CheckObjectPermissionByUserID(appID, userID, appObjectID, seekPermission);

            string permString = string.Empty;
            string permMsg = string.Empty;
            string permName = string.Empty;


            permName = ((PermissionEnum)seekPermission).ToString();
            permMsg = "Permssion Data not found.";

            if (isPermited)
            {
                permString = "1";
                permMsg = string.Format("You have {0} permission for the object.", permName);
            }
            else
            {
                permString = "0";
                permMsg = string.Format("You don't have {0} permission for the object.", permName);
            }


            StringBuilder sb = new StringBuilder();

            ////
            sb.Append("{\"info\":");
            sb.Append("{");
            sb.Append("\"objectid\":" + appObjectID.ToString() + ",");
            sb.Append("\"permname\":\"" + permName + "\",");
            sb.Append("\"permgranted\":" + permString + ",");
            sb.Append("\"permmsg\":\"" + permMsg + "\",");
           
            sb.Append("\"lastcol\":\"" + "last" + "\"");
            sb.Append("}");
            ///
            sb.Append("}");


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
