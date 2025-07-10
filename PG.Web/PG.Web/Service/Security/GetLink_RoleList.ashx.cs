using PG.BLLibrary.SecurityBL;
using PG.Core.Web;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace PG.Web.Service.Security
{
    /// <summary>
    /// Summary description for GetLink_RoleList
    /// </summary>
    public class GetLink_RoleList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            //int isCodeName = 1;// WebUtility.GetQueryStringInteger("iscodename", context);
            //string isCompositeItem = WebUtility.GetQueryString("isCompositeItem", context);
            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            //string dealerID = WebUtility.GetQueryString("id", context);

            //int locationID = WebUtility.GetQueryStringInteger("locationid", context);
            //string seid = WebUtility.GetQueryString("seid", context);
            //string searchTerm = WebUtility.GetQueryString("searchTerm", context);
            //string dealerCode = WebUtility.GetQueryString("code", context);

            //int batterytypeid = WebUtility.GetQueryStringInteger("batterytypeid", context);

            //int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            //string dealerName = WebUtility.GetQueryString("name", context);
            //int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);

            //int groupid = WebUtility.GetQueryStringInteger("groupid", context);
            //int classid = WebUtility.GetQueryStringInteger("classid", context);
            //int typeid = WebUtility.GetQueryStringInteger("typeid", context);
            //string companyCode = WebUtility.GetQueryString("companycode", context).ToUpper();
            //companyCode = companyCode == "0" ? string.Empty : companyCode;

            //string branchCode = WebUtility.GetQueryString("branchcode", context).ToUpper();
            //branchCode = branchCode == "0" ? string.Empty : branchCode;


            //string isPanelUOM = WebUtility.GetQueryString("isPanelUOM", context).ToUpper();
            //deptCode = deptCode == "0" ? string.Empty : deptCode;

            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc
      
            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();
            List<dcRole> listData = RoleBL.GetRoleList();
            int totRec = listData.Count;
            string comma = string.Empty;
            //System.Web.Script.Serialization.JavaScriptSerializer serl = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string d = serl.Serialize(listData);

            var jsonList = from c in listData
                           select new
                           {
                               roleid = c.RoleID,
                               rolename = c.RoleName,
                               roledesc = c.RoleDesc,
                               isactive = c.IsActive,
                               isadmin = c.IsAdmin,
                               enable = true
                           };





            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string jsonData = jss.Serialize(jsonList);

            //var myobj = jsSerializer.Deserialize<List<BigCommerceOrderProduct>>(jsonData);

            //JavaScriptSerializer.Deserialize

            StringBuilder sb = new StringBuilder();


            string pageNo = "1";
            string totalPage = "1";
            string records = listData.Count.ToString();

            string errorNo = "0";
            string errorString = string.Empty;

           

            if (totRec == 0)
            {
                errorNo = "1";
                errorString = "No Record Found!";
            }

            sb.Append("{");
            sb.Append("\"page\":" + pageNo);
            sb.Append(",\"totalpage\":" + totalPage);
            sb.Append(",\"records\":" + records);
            sb.Append(",\"errorno\":" + errorNo);
            sb.Append(",\"errorstring\":" + "\"" + errorString + "\"");
            sb.Append(",\"rows\":" + jsonData);
            sb.Append("}");

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sb.ToString());
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