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
using PG.Core.Extentions;

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
    public class GetAccRef : IHttpHandler
    {
        public class AccRefParams
        {
            public int isTerm { get; set; }
            public int isCodeName { get; set; }
            public int isPaging { get; set; }
            public int rows { get; set; }
            public int page { get; set; }
            public int accRefID { get; set; }
            public string searchTerm { get; set; }
            public string accRefCode { get; set; }
            public int codeCompType { get; set; }
            public string accRefName { get; set; }
            public int nameCompType { get; set; }
            public int parentID { get; set; }
            public int accRefTypeID { get; set; }
            public int accRefCategoryID { get; set; }
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
            public int gllocationID { get; set; }


        }


        public void ProcessRequest(HttpContext context)
        {

            AccRefParams qryParams = new AccRefParams();

            qryParams.isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            qryParams.isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);


            qryParams.isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            qryParams.rows = WebUtility.GetQueryStringInteger("rows", context);
            qryParams.page = WebUtility.GetQueryStringInteger("page", context);

            qryParams.accRefID = WebUtility.GetQueryStringInteger("id", context);

            qryParams.searchTerm = WebUtility.GetQueryString("searchTerm", context);

            qryParams.accRefCode = WebUtility.GetQueryString("code", context);
            qryParams.codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            qryParams.accRefName = WebUtility.GetQueryString("name", context);
            qryParams.nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);


            qryParams.parentID = WebUtility.GetQueryStringInteger("parentid", -1, context);
            qryParams.accRefTypeID = WebUtility.GetQueryStringInteger("typeid", context);
            qryParams.accRefCategoryID = WebUtility.GetQueryStringInteger("categoryid", context);

            qryParams.needCategoryID = WebUtility.GetQueryStringInteger("needcategoryid", context);

            qryParams.refTypeFilterType = WebUtility.GetQueryStringInteger("typefiltertype", context);

            qryParams.companyID = WebUtility.GetQueryStringInteger("companyid", context);

            qryParams.sortBy = WebUtility.GetQueryString("sidx", context); //colname
            qryParams.sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc


            qryParams.includeHeirerchy = WebUtility.GetQueryStringInteger("includehr", context);
            qryParams.glAccountID = WebUtility.GetQueryStringInteger("glaccountid", context);

            string strTask = WebUtility.GetQueryString("task", context);
            strTask = strTask.Trim() == string.Empty ? "accref" : strTask;
            qryParams.gllocationID = WebUtility.GetQueryStringInteger("gllocationID", context);

            //accref = default task : getlist, getbycode/id
            //glaccref = get glreflist by glaccountid

            //dbq.DBCommand = cmd;

            //List<dcAccRef> listData = new List<dcAccRef>();
            StringBuilder sb = new StringBuilder();

            switch(strTask.ToLower())
            {
                case "accref":
                    sb.Append(GetAccRefDataJSonString(qryParams));
                    break;
                default:
                    sb.Append(GetAccRefDataJSonString(qryParams));
                    break;
            }

           


            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sb.ToString());
            context.ApplicationInstance.CompleteRequest();

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }


        public string GetAccRefDataJSonString(AccRefParams qryParams)
        {

            if (qryParams.isTerm == 1)
            {
                qryParams.accRefCode = qryParams.searchTerm;
            }

            if (qryParams.isCodeName == 1)
            {
                qryParams.accRefName = qryParams.accRefCode;
            }


            StringBuilder sbStatment = new StringBuilder();
            sbStatment.Append(AccRefBL.GetAccRefListString());

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblAccRef.CompanyID", qryParams.companyID));

            if (qryParams.accRefID > 0)
            {
                filterList.Add(new DBFilter("tblAccRef.AccRefID", qryParams.accRefID));

                //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
                //cmd.Parameters.AddWithValue("@accID", accID);
            }


            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (qryParams.accRefCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
                if (qryParams.codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(qryParams.codeCompType);
                }
                filterListCN.Add(new DBFilter("tblAccRef.AccRefCode", qryParams.accRefCode, DBFilterDataTypeEnum.String, compTypeAccCode));
                isCodeNameFilter = true;
            }

            if (qryParams.accRefName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
                if (qryParams.nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(qryParams.nameCompType);
                }
                if (qryParams.isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblAccRef.AccRefName", qryParams.accRefName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblAccRef.AccRefName", qryParams.accRefName, DBFilterDataTypeEnum.String, compTypeAccName));
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
                filterList.Add(new DBFilter("tblAccRef.AccRefIDParent", qryParams.parentID));
            }

            if (qryParams.accRefTypeID > 0)
            {
                filterList.Add(new DBFilter("tblAccRefCategory.AccRefTypeID", qryParams.accRefTypeID));
            }

            if (qryParams.accRefCategoryID > 0)
            {
                filterList.Add(new DBFilter("tblAccRef.AccRefCategoryID", qryParams.accRefCategoryID));
            }

            if (qryParams.isPaging == 1 && qryParams.rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = qryParams.page;
                dbq.RowCount = qryParams.rows;
            }
            if (qryParams.gllocationID > 0)
            {
                filterList.Add(new DBFilter("tblLocationAccRef.LocationID", qryParams.gllocationID));
            }
            dbq.OrderBy = "tblAccRef.AccRefCode,tblAccRef.AccRefName";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            //dbq.DBCommand = cmd;

            List<dcAccRef> listData = new List<dcAccRef>();

            if (qryParams.needCategoryID == 0 | (qryParams.needCategoryID == 1 && qryParams.accRefCategoryID > 0))
            {
                listData = AccRefBL.GetAccRefList(dbq, null);
                qryParams.result_IsPaging = dbq.IsPaging;
                qryParams.result_PageNo = dbq.PageNo;
                qryParams.result_TotalPage = dbq.TotalPage;
                qryParams.result_TotalRecord = dbq.TotalRecord;
                qryParams.result_RecordCount = listData.Count;
            }

            int totRec = listData.Count;
            string comma = string.Empty;


            var jsonList = from c in listData
                           select new
                           {
                               id = c.AccRefID,
                               code = c.AccRefCode,
                               name = c.AccRefName,
                               typeid = c.AccRefTypeID,
                               typename = c.AccRefTypeName,
                               categoryid = c.AccRefCategoryID,
                               categorycode = c.AccRefCategoryCode,
                               categoryname = c.AccRefCategoryName,
                               companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               parenid = c.AccRefIDParent,
                               parentname = c.AccRefNameParent,
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

            //if (dbq.IsPaging)
            //{
            //    pageNo = dbq.PageNo.ToString();
            //    totalPage = dbq.TotalPage.ToString();
            //    //actualrecords = dbq.TotalRecord.ToString();
            //    //records = isEmptyRecord ? "1" : actualrecords;
            //}

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
