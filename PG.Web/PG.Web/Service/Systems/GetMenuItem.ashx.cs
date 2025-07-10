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
using PG.BLLibrary.SystemsBL;

namespace PG.Web.Service.Systems
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetMenuItem : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //System.Threading.Thread.Sleep(2000);
            string pID = string.Empty;
            if (context.Request.QueryString["id"] != null)
            {
                pID = context.Request.QueryString["id"].Trim();
            }
            int id = 0;
            int.TryParse(pID, out id);


            StringBuilder sb = new StringBuilder();
            int totRec = 0;

            dcAppMenu cMenu = null;
            
            if (id > 0)
            {
              //cMenu = BLLibrary.SystemBL.AppMenuBL.GetAppMenuByID(id);
                cMenu = PG.Web.Systems.AppMenu.GetAppMenu(id);
            }
            if (cMenu == null)
            {
                cMenu = new dcAppMenu();
            }
            else
            {
                totRec = 1;
            }

            
            ////
            sb.Append("{\"menu\":[");
            sb.Append("{");
            sb.Append("\"count\":\"" + totRec.ToString() + "\",");
            sb.Append("\"id\":\"" + cMenu.AppMenuID.ToString() + "\",");
            sb.Append("\"name\":\"" + cMenu.AppMenuName.ToString() + "\",");
            sb.Append("\"label\":\"" + cMenu.AppHeaderText + "\",");
            sb.Append("\"type\":\"" + cMenu.AppMenuType.ToString() + "\",");
            
            sb.Append("\"url\":\"" + cMenu.AppMenuURL + "\",");
           
      
            //sb.Append("\"tabaction\":\"" + cMenu.TabAction.ToString() + "\",");
            sb.Append("\"tabaction\":" + cMenu.TabAction.ToString() + ",");
            sb.Append("\"selectaction\":\"" + cMenu.SelectAction.ToString() + "\",");
            sb.Append("\"reload\":\"" + "0" + "\",");
            sb.Append("\"lastcol\":\"" + "last" + "\"");
            sb.Append("}");
            sb.Append("]");
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
