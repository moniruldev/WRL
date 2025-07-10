using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class GetGLAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        { 

            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            int isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            int accID = WebUtility.GetQueryStringInteger("id", context);

            string searchTerm = WebUtility.GetQueryString("searchTerm", context);
            string accCode = WebUtility.GetQueryString("code", context);

            int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);
            
            string accName = WebUtility.GetQueryString("name", context);
            int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);


            int glGroupID = WebUtility.GetQueryStringInteger("glgroupid", context);
            int accTypeID = WebUtility.GetQueryStringInteger("acctypeid", context);

            int accTypeFilter = WebUtility.GetQueryStringInteger("acctypefilter", context);

            int companyID = WebUtility.GetQueryStringInteger("companyid", context);

            List<int> glGroupClassInclude = WebUtility.GetQueryStringIntList("glgroupclassinclude", context);
            List<int> glGroupClassExclude = WebUtility.GetQueryStringIntList("glgroupclassexclude", context);
            List<int> glClassList = WebUtility.GetQueryStringIntList("glclass", context);


            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc

            int glLocationID = WebUtility.GetQueryStringInteger("gllocationid", context);


            int includeHeirerchy = WebUtility.GetQueryStringInteger("includehr", context);

            if (isTerm == 1)
            {
                accCode = searchTerm;
            }

            if (isCodeName == 1)
            {
                accName = accCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();
            sbStatment.Append(GLAccountBL.GetGLAccountListString());
     
            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblGLAccount.CompanyID", companyID));


            if (accID > 0)
            {
                filterList.Add(new DBFilter("tblGLAccount.GLAccountID", accID));

                //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
                //cmd.Parameters.AddWithValue("@accID", accID);
            }


            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (accCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
                if (codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
                }
                filterListCN.Add(new DBFilter("tblGLAccount.GLAccountCode", accCode, DBFilterDataTypeEnum.String, compTypeAccCode));
                isCodeNameFilter = true;
            }

            if (accName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
                if (nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(nameCompType);
                }
                if (isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblGLAccount.GLAccountName", accName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblGLAccount.GLAccountName", accName, DBFilterDataTypeEnum.String, compTypeAccName));
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

            //if (accCode != string.Empty)
            //{
            //    DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
            //    if (codeCompType > 0)
            //    {
            //        compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
            //    }
            //    filterList.Add(new DBFilter("tblGLAccount.GLAccountCode", accCode, DBFilterDataTypeEnum.String, compTypeAccCode));

            //    //sb.Append(" AND tblGLAccount.GLAccountCode=@accCode ");
            //    //cmd.Parameters.AddWithValue("@accCode", accCode);
            //}

            //if (accName != string.Empty)
            //{
            //    DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
            //    if (nameCompType > 0)
            //    {
            //        compTypeAccName = DBFilterManager.GetCompareTypeFormInt(nameCompType);
            //    }
            //    if (isCodeName == 1)
            //    {
            //        filterList.Add(new DBFilter("tblGLAccount.GLAccountName", accName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
            //    }
            //    else
            //    {
            //        filterList.Add(new DBFilter("tblGLAccount.GLAccountName", accName, DBFilterDataTypeEnum.String, compTypeAccName));
            //    }
            //}


            if (glGroupID > 0)
            {
                filterList.Add(new DBFilter("tblGLAccount.GLGroupID", glGroupID));
            }


            if (glGroupClassInclude.Count > 0)
            {
                if (glGroupClassInclude[0] != 0)
                {
                    filterList.Add(new DBFilter("tblGLGroup.GLGroupClassID", glGroupClassInclude, DBFilterDataTypeEnum.Integer, DBFilterCompareTypeEnum.IN));
                }
            }

            if (glGroupClassExclude.Count > 0)
            {
                if (glGroupClassExclude[0] != 0)
                {
                    DBFilter flt = new DBFilter("tblGLGroup.GLGroupClassID", glGroupClassExclude, DBFilterDataTypeEnum.Integer, DBFilterCompareTypeEnum.IN);
                    flt.NegateExpression = true;
                    filterList.Add(flt );
                }
            }



            //GLAccountTypeFilterEnum typeFilterType = 

            GLAccountTypeFilterEnum glAccFilter = (GLAccountTypeFilterEnum)accTypeFilter;
            if (glAccFilter != GLAccountTypeFilterEnum.NoFilter)
            {
                List<int> values = AccHelper.GetGLAccountTypeIDFilterList(glAccFilter);
                filterList.Add(new DBFilter("tblGLAccount.GLAccountTypeID", values, DBFilterDataTypeEnum.Integer, DBFilterCompareTypeEnum.IN));
            }
            else
            {
                if (accTypeID > 0)
                {
                    filterList.Add(new DBFilter("tblGLAccount.GLAccountTypeID", accTypeID));
                }
            }

            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }
            //Add Location Filter by Moni
            if (glLocationID > 0)
            {
                filterList.Add(new DBFilter("tblLocationGLAccount.LocationID", glLocationID));
            }

            dbq.OrderBy = "tblGLAccount.GLAccountCode,tblGLAccount.GLAccountName";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;
            
            //dbq.DBCommand = cmd;

            List<dcGLAccount> listData = GLAccountBL.GetGLAccountList(dbq, null);
            int totRec = listData.Count;
            string comma = string.Empty;
            //bool isEmptyRecord = false;

            //includeEmpty = 0;


            //if (listData.Count == 0 && includeEmpty == 1)
            //{
            //    dcGLAccount gAcc = new dcGLAccount();
            //    gAcc.GLAccountName = "No record found!";
            //    listData.Add(gAcc);
            //    isEmptyRecord = true;
            //}



            //System.Web.Script.Serialization.JavaScriptSerializer serl = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string d = serl.Serialize(listData);

            var jsonList = from c in listData
                           select new
                           {
                               glaccid = c.GLAccountID,
                               glacccode = c.GLAccountCode,
                               glaccname = c.GLAccountName,
                               companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               glgroupid = c.GLGroupID,
                               glgroupcode = c.GLGroupCode,
                               glgroupname = c.GLGroupName,
                               glgroupnameshort = c.GLGroupNameShort,
                               glclassid = c.GLClassID,
                               glacctypeid = c.GLAccountTypeID,
                               glacctypename = c.GLAccountTypeName,
                               glgroupclassid = c.GLGroupClassID,
                               glgroupclassname = c.GLGroupClassName,
                               glgroupkey = c.GLGroupKey,
                               isinstrument = c.IsInstrument ? 1 : 0,
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
