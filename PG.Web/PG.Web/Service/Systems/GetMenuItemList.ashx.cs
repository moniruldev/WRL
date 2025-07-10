using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using System.Web.Script.Serialization;

using PG.Core.Web;
using PG.BLLibrary.SystemsBL;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.DBClass.SecurityDC;
using PG.DBClass.SystemDC;



namespace PG.Web.Service.Systems
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetMenuItemList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            int isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            //string branchCode = WebUtility.GetQueryString("id", context);

            string searchTerm = WebUtility.GetQueryString("searchTerm", context);

            string menuId = WebUtility.GetQueryString("menuId", context);

            string appId = WebUtility.GetQueryString("appId", context);
            string menuName = WebUtility.GetQueryString("menuName", context);

            int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            
            int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);


            int isdept = WebUtility.GetQueryStringInteger("isdept", context);
            int userid = WebUtility.GetQueryStringInteger("userid", context);
            int isadmin = WebUtility.GetQueryStringInteger("isadmin", context);


            //string companyCode = WebUtility.GetQueryString("companycode", context).ToUpper();
            //companyCode =   companyCode == "0" ? string.Empty : companyCode; 

            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc


            if (isTerm == 1)
            {
                menuId = searchTerm;
            }

            if (isCodeName == 1)
            {
                menuName = menuId;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();


            sbStatment.Append(AppMenuBL.GetAppMenu_SQLString());
           

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();



            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (menuId != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
                if (codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
                }
                filterListCN.Add(new DBFilter("tblAppMenu.APPMENUID", menuId, DBFilterDataTypeEnum.String, compTypeAccCode));
                isCodeNameFilter = true;
            }

            if (menuName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
                if (nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(nameCompType);
                }
                if (isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("UPPER(tblAppMenu.APPMENUTEXT)", menuName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("UPPER(tblAppMenu.APPMENUTEXT)", menuName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName));
                }
                isCodeNameFilter = true;
            }

            if (isCodeNameFilter)
            {
                if (isCodeName == 1)
                {
                    filterList.Add(new DBFilter(filterListCN));
                }
                else
                {
                    filterList.AddRange(filterListCN);
                }
            }


            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            

            
            sbStatment.Append(" AND tblAppMenu.AppID= "+appId);
            



            
            dbq.OrderBy = "tblAppMenu.APPMENUTEXT";

            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            //dbq.DBCommand = cmd;

            List<dcAppMenu> listData =AppMenuBL.GetMenuItemList(dbq, null);
            int totRec = listData.Count;
            string comma = string.Empty;




            //List<dcUserDepartment> listUserDept = UserDepartmentBL.GetUserDepartmentList()



            //System.Web.Script.Serialization.JavaScriptSerializer serl = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string d = serl.Serialize(listData);

            var jsonList = from c in listData
                           select new
                           {
                               menuId = c.AppMenuID,
                               menuName = c.AppMenuText,
                               enable = true
                           };





            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonData = jss.Serialize(jsonList);

            //var myobj = jsSerializer.Deserialize<List<BigCommerceOrderProduct>>(jsonData);

            //JavaScriptSerializer.Deserialize

            StringBuilder sb = new StringBuilder();


            string pageNo = "1";
            string totalPage = "1";
            string records = listData.Count.ToString();

            string errorNo = "0";
            string errorString = string.Empty;

            if (dbq.IsPaging)
            {
                pageNo = dbq.PageNo.ToString();
                totalPage = dbq.TotalPage.ToString();
                //actualrecords = dbq.TotalRecord.ToString();
                //records = isEmptyRecord ? "1" : actualrecords;
            }

            if (totRec == 0)
            {
                errorNo = "1";
                errorString = "No Record Found!";
            }

            //sb.Append("{");
            //sb.Append("\"page\":" + pageNo);
            //sb.Append(",\"total\":" + total);
            //sb.Append(",\"records\":" + records);
            //sb.Append(",\"actualrecords\":" + actualrecords);
            //sb.Append(",\"rows\":" + jsonData);
            //sb.Append("}");

            sb.Append("{");
            sb.Append("\"page\":" + pageNo);
            sb.Append(",\"totalpage\":" + totalPage);
            sb.Append(",\"records\":" + records);
            sb.Append(",\"errorno\":" + errorNo);
            sb.Append(",\"errorstring\":" + "\"" + errorString + "\"");
            sb.Append(",\"rows\":" + jsonData);
            sb.Append("}");



            //sb.Append("{\"rows\":");
            //sb.Append(jsonData);
            //sb.Append("}");


            //////
            //sb.Append("{\"acc\":[");

            //foreach (dcGLAccount glAcc in listData)
            //{
            //    sb.Append(comma);
            //    sb.Append("{");
            //    sb.Append("\"id\":\"" + glAcc.GLAccountID.ToString() + "\",");
            //    sb.Append("\"code\":\"" + glAcc.GLAccountCode + "\",");
            //    sb.Append("\"name\":\"" + glAcc.GLAccountName + "\",");

            //    sb.Append("\"lc\":\"" + "last" + "\"");
            //    sb.Append("}");
            //    comma = ",";
            //}

            //sb.Append("]");
            /////
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
}