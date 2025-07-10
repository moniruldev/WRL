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
using PG.Core.Utility;

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
    public class GetJournalList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        { 
            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            //int includeEmpty = WebUtility.GetQueryStringInteger("includeempty", context);
            int isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            int journalID = WebUtility.GetQueryStringInteger("id", context);

            string searchTerm = WebUtility.GetQueryString("searchTerm", context);
            string journalNo = WebUtility.GetQueryString("code", context);

            int jNoCompType = WebUtility.GetQueryStringInteger("jnocomptype", context);
            
            int postOption = WebUtility.GetQueryStringInteger("postoption", context);
            int journalTypeID = WebUtility.GetQueryStringInteger("journaltypeid", context);
          
            int companyID = WebUtility.GetQueryStringInteger("companyid", context);
            int yearID = WebUtility.GetQueryStringInteger("yearid", context);
            int locationID = WebUtility.GetQueryStringInteger("locationid", context);

            DateTime? fromDate = WebUtility.GetQueryStringDate("fromdate", context);
            DateTime? toDate = WebUtility.GetQueryStringDate("todate", context);

            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc

            if (isTerm == 1)
            {
                journalNo = searchTerm;
            }

            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();
            sbStatment.Append(JournalBL.GetJournalListString());
     
            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblJournal.CompanyID", companyID));

            if (journalID > 0)
            {
                filterList.Add(new DBFilter("tblJournal.JournalID", journalID));
                //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
                //cmd.Parameters.AddWithValue("@accID", accID);
            }

            if(locationID>0)
            {
                filterList.Add(new DBFilter("tblJournal.LocationID", locationID));
            }

            if (yearID > 0)
            {
                filterList.Add(new DBFilter("tblJournal.AccYearID", yearID));

                //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
                //cmd.Parameters.AddWithValue("@accID", accID);
            }

            List<DBFilter> filterListCN = new List<DBFilter>();
            if (journalNo != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeJNo = DBFilterCompareTypeEnum.EqualTo;
                if (jNoCompType > 0)
                {
                    compTypeJNo = DBFilterManager.GetCompareTypeFormInt(jNoCompType);
                }
                filterListCN.Add(new DBFilter("tblJournal.JournalNo", journalNo, DBFilterDataTypeEnum.String, compTypeJNo));
            }

         


            if (journalTypeID > 0)
            {
                filterList.Add(new DBFilter("tblJournal.JournalTypeID", journalTypeID));
            }

            if (postOption > 0)
            {
                if (postOption == 1)
                {
                    filterList.Add(new DBFilter("tblJournal.IsPosted", true));
                }
                if (postOption == 2)
                {
                    filterList.Add(new DBFilter("tblJournal.IsPosted", false));
                }
            }


            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            dbq.OrderBy = "tblJournal.JournalNo";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;
            
            //dbq.DBCommand = cmd;

            List<dcJournal> listData = JournalBL.GetJournalList(dbq, null);
            int totRec = listData.Count;
            string comma = string.Empty;
            //bool isEmptyRecord = false;

            //if (listData.Count == 0 && includeEmpty == 1)
            //{
            //    dcJournal gJrnl = new dcJournal();
            //    listData.Add(gJrnl);
            //    isEmptyRecord = true;
            //}



            //System.Web.Script.Serialization.JavaScriptSerializer serl = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string d = serl.Serialize(listData);

            var jsonList = from c in listData
                           select new
                           {
                               journalid = c.JournalID,
                               journalno = c.JournalNo,
                               journaldate =   c.JournalID > 0 ?  Conversion.NullDateToEmpty(c.JournalDate, "dd-MMM-yyyy") : "",
                               companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               journaltypeid = c.JournalTypeID,
                               journaltypename = c.JournalTypeName,
                               journallocation=c.LocationName,
                               journalamt = c.JournalID > 0 ?  c.JournalAmt.ToString("#,##0.00") : "",
                               journaldesc = c.JournalDesc,
                               isposted = c.IsPosted ? 1 : 0,
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
