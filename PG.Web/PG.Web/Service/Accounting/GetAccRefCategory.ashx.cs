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

using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Service.Accounting
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetAccRefCategory : IHttpHandler
    {
        public class AccRefCategoryParams
        {
            public int isTerm { get; set; }
            public int isCodeName { get; set; }
            public int isPaging { get; set; }
            public int rows { get; set; }
            public int page { get; set; }
            public int accRefCategoryID { get; set; }
            public string searchTerm { get; set; }
            public string accRefCategoryCode { get; set; }
            public int codeCompType { get; set; }
            public string accRefCategoryName { get; set; }
            public int nameCompType { get; set; }
            public int parentID { get; set; }
            public int accRefTypeID { get; set; }

            public int needCategoryID { get; set; }
            public int refTypeFilterType { get; set; }
            public int companyID { get; set; }

            public int includeHeirerchy { get; set; }

            public string sortBy { get; set; }

            public string sortOrder { get; set; }

            public int glAccountID { get; set; }

            public string glAccountCode { get; set; }

            public bool result_IsPaging { get; set; }
            public int result_PageNo { get; set; }
            public int result_TotalPage { get; set; }
            public int result_TotalRecord { get; set; }
            public int result_RecordCount { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            AccRefCategoryParams qryParams = new AccRefCategoryParams();


            qryParams.isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            qryParams.isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);


            qryParams.isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            qryParams.rows = WebUtility.GetQueryStringInteger("rows", context);
            qryParams.page = WebUtility.GetQueryStringInteger("page", context);

            qryParams.accRefCategoryID = WebUtility.GetQueryStringInteger("id", context);

            qryParams.searchTerm = WebUtility.GetQueryString("searchTerm", context);

            qryParams.accRefCategoryCode = WebUtility.GetQueryString("code", context);
            qryParams.codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            qryParams.accRefCategoryName = WebUtility.GetQueryString("name", context);
            qryParams.nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);


            qryParams.parentID = WebUtility.GetQueryStringInteger("parentid", -1, context);
            qryParams.accRefTypeID = WebUtility.GetQueryStringInteger("typeid", context);

            qryParams.refTypeFilterType = WebUtility.GetQueryStringInteger("typefiltertype", context);

            qryParams.companyID = WebUtility.GetQueryStringInteger("companyid", context);

            qryParams.sortBy = WebUtility.GetQueryString("sidx", context); //colname
            qryParams.sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc

            qryParams.includeHeirerchy = WebUtility.GetQueryStringInteger("includehr", context);

            qryParams.glAccountID = WebUtility.GetQueryStringInteger("glaccountid", context);

            qryParams.glAccountID = WebUtility.GetQueryStringInteger("glaccountid", context);


            string strTask = WebUtility.GetQueryString("task", context);
            strTask = strTask.Trim() == string.Empty ? "accrefcategory" : strTask;

            StringBuilder sb = new StringBuilder();

            //List<dcAccRefCategory> listData = new List<dcAccRefCategory>();

            switch (strTask.ToLower())
            {
                case "accrefcategory":
                    sb.Append(GetAccRefCategoryJSonString(qryParams));
                    break;
                case "glaccrefcategory":
                    sb.Append(GetGLAccRefCategoryByJSonString(qryParams));
                    break;
                default:
                    sb.Append(GetGLAccRefCategoryByJSonString(qryParams));
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

        public string GetAccRefCategoryJSonString(AccRefCategoryParams qryParams)
        {
            if (qryParams.isTerm == 1)
            {
                qryParams.accRefCategoryCode = qryParams.searchTerm;
            }

            if (qryParams.isCodeName == 1)
            {
                qryParams.accRefCategoryName = qryParams.accRefCategoryCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sbStatment = new StringBuilder();
            sbStatment.Append(AccRefCategoryBL.GetAccRefCategoryListString());

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblAccRefCategory.CompanyID", qryParams.companyID));

            if (qryParams.accRefCategoryID > 0)
            {
                filterList.Add(new DBFilter("tblAccRefCategory.AccRefCategoryID", qryParams.accRefCategoryID));
            }

            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (qryParams.accRefCategoryCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
                if (qryParams.codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(qryParams.codeCompType);
                }
                filterListCN.Add(new DBFilter("tblAccRefCategory.AccRefCategoryCode", qryParams.accRefCategoryCode, DBFilterDataTypeEnum.String, compTypeAccCode));
                isCodeNameFilter = true;
            }

            if (qryParams.accRefCategoryName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
                if (qryParams.nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(qryParams.nameCompType);
                }
                if (qryParams.isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblAccRefCategory.AccRefCategoryName", qryParams.accRefCategoryName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblAccRefCategory.AccRefCategoryName", qryParams.accRefCategoryName, DBFilterDataTypeEnum.String, compTypeAccName));
                }
                isCodeNameFilter = true;
            }

            if (isCodeNameFilter)
            {
                if (qryParams.isCodeName == 1)
                {
                    filterList.Add(new DBFilter(filterListCN));
                }
                else
                {
                    filterList.AddRange(filterListCN);
                }
            }
            if (qryParams.parentID != -1)
            {
                filterList.Add(new DBFilter("tblAccRefCategory.AccRefIDParent", qryParams.parentID));
            }

            if (qryParams.accRefTypeID > 0)
            {
                filterList.Add(new DBFilter("tblAccRefCategory.AccRefTypeID", qryParams.accRefTypeID));
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

            dbq.OrderBy = "tblAccRefCategory.AccRefCategoryCode,tblAccRefCategory.AccRefCategoryName";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            List<dcAccRefCategory> listData = new List<dcAccRefCategory>();

            listData = AccRefCategoryBL.GetAccRefCategoryList(dbq, null);

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
                               id = c.AccRefCategoryID,
                               code = c.AccRefCategoryCode,
                               name = c.AccRefCategoryName,
                               typeid = c.AccRefTypeID,
                               typename = c.AccRefTypeName,
                               companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               parenid = c.AccRefCategoryIDParent,
                               parentname = c.AccRefCategoryNameParent,
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


        public string GetGLAccRefCategoryByJSonString(AccRefCategoryParams qryParams)
        {
            if (qryParams.isTerm == 1)
            {
                qryParams.accRefCategoryCode = qryParams.searchTerm;
            }

            if (qryParams.isCodeName == 1)
            {
                qryParams.accRefCategoryName = qryParams.accRefCategoryCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sbStatment = new StringBuilder();
            sbStatment.Append(GLAccountRefCategoryBL.GetAccountRefCategory_SQLString());

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            //filterList.Add(new DBFilter("tblAccRefCategory.CompanyID", qryParams.companyID));

            if (qryParams.accRefTypeID > 0)
            {
                filterList.Add(new DBFilter("tblAccRefCategory.AccRefTypeID", qryParams.accRefTypeID));
            }

            if (qryParams.glAccountID > 0)
            {
                filterList.Add(new DBFilter("tblGLAccountRefCategory.GLAccountID", qryParams.glAccountID));
            }


            if (qryParams.accRefCategoryID > 0)
            {
                filterList.Add(new DBFilter("tblGLAccountRefCategory.AccRefCategoryID", qryParams.accRefCategoryID));
            }

            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (qryParams.accRefCategoryCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
                if (qryParams.codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(qryParams.codeCompType);
                }
                filterListCN.Add(new DBFilter("tblAccRefCategory.AccRefCategoryCode", qryParams.accRefCategoryCode, DBFilterDataTypeEnum.String, compTypeAccCode));
                isCodeNameFilter = true;
            }

            if (qryParams.accRefCategoryName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
                if (qryParams.nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(qryParams.nameCompType);
                }
                if (qryParams.isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblAccRefCategory.AccRefCategoryName", qryParams.accRefCategoryName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblAccRefCategory.AccRefCategoryName", qryParams.accRefCategoryName, DBFilterDataTypeEnum.String, compTypeAccName));
                }
                isCodeNameFilter = true;
            }

            if (isCodeNameFilter)
            {
                if (qryParams.isCodeName == 1)
                {
                    filterList.Add(new DBFilter(filterListCN));
                }
                else
                {
                    filterList.AddRange(filterListCN);
                }
            }

            if (qryParams.parentID != -1)
            {
                filterList.Add(new DBFilter("tblAccRefCategory.AccRefIDParent", qryParams.parentID));
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

            dbq.OrderBy = "tblAccRefCategory.AccRefCategoryCode,tblAccRefCategory.AccRefCategoryName";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            List<dcGLAccountRefCategory> listData = new List<dcGLAccountRefCategory>();

            listData = GLAccountRefCategoryBL.GetAccountRefCategoryList(dbq, null);

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
                               id = c.AccRefCategoryID,
                               code = c.AccRefCategoryCode,
                               name = c.AccRefCategoryName,
                               typeid = c.AccRefTypeID,
                               typename = c.AccRefTypeName,
                               //companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               //parenid = c.AccRefCategoryIDParent,
                               //parentname = c.AccRefCategoryNameParent,
                               isdefault = c.IsDefault ? 1 : 0,
                               ismandatory = c.IsMandatory ? 1 : 0,
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
