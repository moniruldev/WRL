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

namespace PG.Service.Accounting
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetInstrument : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            
            int isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            int insID = WebUtility.GetQueryStringInteger("insid", context);
            string searchTerm = WebUtility.GetQueryString("searchTerm", context);
        
            string insNo = WebUtility.GetQueryString("insno", context);
            int insNoCompType = WebUtility.GetQueryStringInteger("insnocomptype", context);

         
            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc


            int insModeID = WebUtility.GetQueryStringInteger("insmodeid", context);
            int insStatusID = WebUtility.GetQueryStringInteger("insstatusid", context);
            int insTypeID = WebUtility.GetQueryStringInteger("instypeid", context);
            int glAccID = WebUtility.GetQueryStringInteger("glaccid", context);

            int companyID = WebUtility.GetQueryStringInteger("companyid", context);

            if (isTerm == 1)
            {
                insNo = searchTerm;
            }

            if (isCodeName == 1)
            {
                //accRefName = accRefCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sbStatment = new StringBuilder();
            sbStatment.Append(InstrumentBL.GetInsturmentListString());
     
            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblInstrument.CompanyID", companyID));

            if (insID > 0)
            {
                filterList.Add(new DBFilter("tblInstrument.InstrumentID", insID));
                //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
                //cmd.Parameters.AddWithValue("@accID", accID);
            }


            //List<DBFilter> filterListCN = new List<DBFilter>();
            //bool isCodeNameFilter = false;
            if (insNo != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeInsNo = DBFilterCompareTypeEnum.EqualTo;
                if (insNoCompType > 0)
                {
                    compTypeInsNo = DBFilterManager.GetCompareTypeFormInt(insNoCompType);
                }
                //filterListCN.Add(new DBFilter("tblInstrument.InstrumentNo", insNo, DBFilterDataTypeEnum.String, compTypeInsNo));
                filterList.Add(new DBFilter("tblInstrument.InstrumentNo", insNo, DBFilterDataTypeEnum.String, compTypeInsNo));
                //isCodeNameFilter = true;
            }


            if (insModeID > 0)
            {
                filterList.Add(new DBFilter("tblInstrument.InstrumentModeID", insModeID));
            }

            if (insStatusID > 0)
            {
                filterList.Add(new DBFilter("tblInstrument.InstrumentStatusID", insStatusID));
            }


            if (insTypeID > 0)
            {
                filterList.Add(new DBFilter("tblInstrument.InstrumentTypeID", insTypeID));
            }

            if (glAccID > 0)
            {
                filterList.Add(new DBFilter("tblInstrument.GLAccountID", glAccID));
            }


     
            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            dbq.OrderBy = "tblInstrument.InstrumentNo, tblInstrument.InstrumentDate";

            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            List<dcInstrument> listData = InstrumentBL.GetInstrumentList(dbq, null);
            int totRec = listData.Count;
            string comma = string.Empty;
            //bool isEmptyRecord = false;

            //if (listData.Count == 0 && includeEmpty == 1)
            //{
            //    dcInstrument ins = new dcInstrument();
            //    listData.Add(ins);
            //    isEmptyRecord = true;
            //}

            var jsonList = from c in listData
                           select new
                           {
                               insid = c.InstrumentID,
                               insno = c.InstrumentNo,
                               insdate = Conversion.DateTimeNullToEmpty(c.InstrumentDate),
                               issuename = c.IssueName,
                               bankname = c.BankName,
                               branchname = c.BranchName,
                               bankbranchname = c.BankBranchName,

                               insmodeid = c.InstrumentModeID,
                               instypeid = c.InstrumentTypeID,
                               instypename = c.InstrumentTypeName,

                               insamt = c.InstrumentAmt,
                               tranamt = c.InstrumentAmtTran,
                               remainamt = c.InstrumentAmtRemain,



                               insstatusid = c.InstrumentStatusID,
                               insstatusname = c.InstrumentStatusName,
                               insstatusdate = Conversion.DateTimeNullToEmpty(c.InstrumentStatusDate),
                               remarks = c.Remarks,
              
                               //companyname = c.CompanyName,

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
