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
using System.Web.Script.Serialization;

using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.Core.Web;


using PG.BLLibrary.SecurityBL;
using PG.DBClass.SecurityDC;


namespace PG.Service.Security
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GeUser : IHttpHandler
    {
        public class UserParams
        {
            public int isTerm { get; set; }
            public int isNameEmpCode { get; set; }
            public int isPaging { get; set; }
            public int rows { get; set; }
            public int page { get; set; }
            public int userID { get; set; }
            public string searchTerm { get; set; }
            public string userName { get; set; }
            public int nameCompType { get; set; }
            public string empCode { get; set; }
            public int empCodeCompType { get; set; }

            public string fullName { get; set; }
            public int fullNameCompType { get; set; }

            public int companyID { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
            public bool result_IsPaging { get; set; }
            public int result_PageNo { get; set; }
            public int result_TotalPage { get; set; }
            public int result_TotalRecord { get; set; }
            public int result_RecordCount { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            UserParams qryParams = new UserParams();

            qryParams.isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            qryParams.isNameEmpCode = WebUtility.GetQueryStringInteger("isnameempcode", context);


            qryParams.isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            qryParams.rows = WebUtility.GetQueryStringInteger("rows", context);
            qryParams.page = WebUtility.GetQueryStringInteger("page", context);

            qryParams.userID = WebUtility.GetQueryStringInteger("id", context);

            qryParams.searchTerm = WebUtility.GetQueryString("searchTerm", context);

            qryParams.userName = WebUtility.GetQueryString("name", context);
            qryParams.nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);

            qryParams.empCode = WebUtility.GetQueryString("empcode", context);
            qryParams.empCodeCompType = WebUtility.GetQueryStringInteger("empcodecomptype", context);

            qryParams.fullName  = WebUtility.GetQueryString("fullname", context);
            qryParams.fullNameCompType = WebUtility.GetQueryStringInteger("fullnamecomptype", context);

            qryParams.companyID = WebUtility.GetQueryStringInteger("companyid", context);

            qryParams.sortBy = WebUtility.GetQueryString("sidx", context); //colname
            qryParams.sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc



            string strTask = WebUtility.GetQueryString("task", context);
            strTask = strTask.Trim() == string.Empty ? "user" : strTask;

            StringBuilder sb = new StringBuilder();

            //List<dcAccRefCategory> listData = new List<dcAccRefCategory>();

            switch (strTask.ToLower())
            {
                case "user":
                    sb.Append(GetUserJSonString(qryParams));
                    break;
                default:
                    sb.Append(GetUserJSonString(qryParams));
                    break;
            }

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

        public string GetUserJSonString(UserParams qryParams)
        {
            if (qryParams.isTerm == 1)
            {
                qryParams.userName = qryParams.searchTerm;
            }

            if (qryParams.isNameEmpCode == 1)
            {
                qryParams.empCode = qryParams.userName;
                qryParams.fullName = qryParams.userName;
            }


            StringBuilder sbStatment = new StringBuilder();

            sbStatment.Append("SELECT tblUser.* ");
            sbStatment.Append(" FROM tblUser ");
            sbStatment.Append(" WHERE 1=1 ");



            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblUser.CompanyID", qryParams.companyID));

            if (qryParams.userID > 0)
            {
                filterList.Add(new DBFilter("tblUser.UserID", qryParams.userID));
            }

            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isNameEmpCodeFilter = false;
            if (qryParams.userName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeUserName = DBFilterCompareTypeEnum.EqualTo;
                if (qryParams.nameCompType > 0)
                {
                    compTypeUserName = DBFilterManager.GetCompareTypeFormInt(qryParams.nameCompType);
                }
                filterListCN.Add(new DBFilter("tblUser.UserName", qryParams.userName, DBFilterDataTypeEnum.String, compTypeUserName));
                isNameEmpCodeFilter = true;
            }

            if (qryParams.empCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeEmpCode = DBFilterCompareTypeEnum.StartsWith;
                if (qryParams.empCodeCompType > 0)
                {
                    compTypeEmpCode = DBFilterManager.GetCompareTypeFormInt(qryParams.empCodeCompType);
                }
                if (qryParams.isNameEmpCode == 1)
                {
                    filterListCN.Add(new DBFilter("tblUser.EmpCode", qryParams.empCode, DBFilterDataTypeEnum.String, compTypeEmpCode, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblUser.EmpCode", qryParams.empCode, DBFilterDataTypeEnum.String, compTypeEmpCode));
                }
                isNameEmpCodeFilter = true;
            }

            if (qryParams.fullName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeFullName = DBFilterCompareTypeEnum.StartsWith;
                if (qryParams.fullNameCompType > 0)
                {
                    compTypeFullName = DBFilterManager.GetCompareTypeFormInt(qryParams.fullNameCompType);
                }
                if (qryParams.isNameEmpCode == 1)
                {
                    filterListCN.Add(new DBFilter("tblUser.FullName", qryParams.fullName, DBFilterDataTypeEnum.String, compTypeFullName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblUser.FullName", qryParams.fullName, DBFilterDataTypeEnum.String, compTypeFullName));
                }
                isNameEmpCodeFilter = true;
            }


            if (isNameEmpCodeFilter)
            {
                if (qryParams.isNameEmpCode == 1)
                {
                    filterList.Add(new DBFilter(filterListCN));
                }
                else
                {
                    filterList.AddRange(filterListCN);
                }
            }

            //if (accRefCategoryID > 0)
            //{
            //    filterList.Add(new DBFilter("tblAccRefCategory.AccRefCategoryID", accRefCategoryID));
            //}

            if (qryParams.isPaging == 1 && qryParams.rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = qryParams.page;
                dbq.RowCount = qryParams.rows;
            }

            dbq.OrderBy = "tblUser.UserName, tblUser.EmpCode";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            List<dcUser> listData = new List<dcUser>();

            listData = UserBL.GetUserList(dbq, null);

            qryParams.result_IsPaging = dbq.IsPaging;
            qryParams.result_PageNo = dbq.PageNo;
            qryParams.result_TotalPage = dbq.TotalPage;
            qryParams.result_TotalRecord = dbq.TotalRecord;
            qryParams.result_RecordCount = listData.Count;

            int totRec = listData.Count;
            string comma = string.Empty;



            var jsonList = from c in listData
                           select new
                           {
                               id = c.UserID,
                               name = c.UserName,
                               empcode = c.EmpCode,
                               fullname = c.FullName,
                               //companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               //parenid = c.LocationIDParent,
                               //parentname = c.AccRefCategoryNameParent,
                               enable = true
                           };


            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonData = jss.Serialize(jsonList);


            StringBuilder sb = new StringBuilder();

            string pageNo = "1";
            string totalPage = "1";
            string records = listData.Count.ToString();

            string errorNo = "0";
            string errorString = string.Empty;

            if (qryParams.result_IsPaging)
            {
                pageNo = qryParams.result_PageNo.ToString();
                totalPage = qryParams.result_TotalPage.ToString();
                //actualrecords = dbq.TotalRecord.ToString();
                //records = isEmptyRecord ? "1" : actualrecords;
            }

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

            return sb.ToString();
        }

    }
}
