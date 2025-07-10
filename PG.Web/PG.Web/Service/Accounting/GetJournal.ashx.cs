using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PG.Core.Web;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using System.Text;
using System.Web.Script.Serialization;

namespace PG.Web.Service.Accounting
{
    /// <summary>
    /// Summary description for Journal
    /// </summary>
    public class GetJournal : IHttpHandler
    {

        #region private json class 

        private class jsonJournal
        {
            public int JournalID { get; set; }
            public int CompanyID { get; set; }
            public string JournalNo { get; set; }
            public int AccYearID { get; set; }
            public string JournalDate { get; set; }
            public int JournalTypeID { get; set; }
            public string JournalTypeName { get; set; }

            public decimal JournalAmt { get; set; }
            public string JournalDesc { get; set; }
            public int IsPosted { get; set; }
            public int JournalUpdateNo { get; set; }
        }

        private class jsonJournalDet
        {
            public int _RecordStateInt { get; set; }

            public int JournalDetID { get; set; }
            public int JournalID { get; set; }

            public int JournalDetSLNo { get; set; }

            public int DrCr { get; set; }
            public decimal DebitAmt { get; set; }
            public decimal CreditAmt { get; set; }
            public decimal TranAmt { get; set; }
            
            public string JournalDetDesc { get; set; }

            public int GLAccountID { get; set; }
            public string GLAccountCode { get; set; }
            public string GLAccountName { get; set; }
            public int GLAccountIDEdit { get; set; }
            public int GLGroupIDAcc { get; set; }

            public int GLGroupID { get; set; }
            public string GLGroupCode { get; set; }
            public string GLGroupName { get; set; }
            public string GLGroupNameShort { get; set; }
            public int GLGroupIDEdit { get; set; }
            public int GLGroupClassID { get; set; }


            public int TranTypeID { get; set; }
            public string TranTypeCode { get; set; }
            public string TranTypeName { get; set; }
            public int TranTypeCategoryID { get; set; }


            public int IsInstrument { get; set; }
            public int IsCash { get; set; }
           
    
        }


        #endregion

        public void ProcessRequest(HttpContext context)
        {
            int journalID = WebUtility.GetQueryStringInteger("journalid", context);
            int companyID = WebUtility.GetQueryStringInteger("companyid", context);
            int editMode = WebUtility.GetQueryStringInteger("editmode", context);


            string jsonData = GetJournalJsonString(journalID, companyID);

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(jsonData);
            context.ApplicationInstance.CompleteRequest();


            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }


        private string GetJournalJsonString(int pJournalID, int pCompanyID)
        {
            int errorNo = 0;
            bool isError = false;
            string errorString = string.Empty;
            string statuString = string.Empty;

            string jsonStrJournal = string.Empty;
            string jsonStrJournalDetList = string.Empty;

            dcJournal jrnl = JournalBL.GetJournalByID(pCompanyID, pJournalID,0);//TODO Change Add 0
            List<dcJournalDet> jrnlDetList = new List<dcJournalDet>();
            List<dcJournalDetRef> jrnlDetRefList = new List<dcJournalDetRef>();

            
            jsonJournal jsJrnl = null;
            List<jsonJournalDet> jsJrnlDetList = null;



            if (jrnl == null)
            {
                jsonStrJournal = "null";
                jsonStrJournalDetList = "null";
                isError = true;
                errorString = "No Journal Found!";
            }
            else
            {
                jsJrnl = new jsonJournal();
                jsJrnl.CompanyID = jrnl.CompanyID;
                jsJrnl.JournalID = jrnl.JournalID;
                jsJrnl.JournalNo = jrnl.JournalNo;
                jsJrnl.AccYearID = jrnl.AccYearID;
                jsJrnl.JournalDate = jrnl.JournalDate.ToString("dd-MMM-yyyy");
                jsJrnl.JournalTypeID = jrnl.JournalTypeID;
                jsJrnl.JournalTypeName = jrnl.JournalTypeName;
                jsJrnl.JournalUpdateNo = jrnl.JournalUpdateNo;
                jsJrnl.IsPosted = jrnl.IsPosted ? 1 : 0;
                jsJrnl.JournalAmt = jrnl.JournalAmt;

                jrnlDetList = JournalDetBL.GetJournalDetList(jrnl.CompanyID,jrnl.JournalID);
                jrnlDetRefList = JournalDetRefBL.GetJournalDetRefList(jrnl.CompanyID, pJournalID, 0);
                JournalDetBL.SetTranTypeToDetList(jrnlDetList, jrnlDetRefList);


                jsJrnlDetList = new List<jsonJournalDet>();
                foreach (dcJournalDet jrnlDet in jrnlDetList)
                {
                    jsonJournalDet jsJrnlDet = new jsonJournalDet();

                    jsJrnlDet._RecordStateInt = jrnlDet._RecordStateInt;
                    
                    jsJrnlDet.JournalDetID = jrnlDet.JournalDetID;
                    jsJrnlDet.JournalID = jrnlDet.JournalID;
                    jsJrnlDet.JournalDetSLNo = jrnlDet.JournalDetSLNo;

                    jsJrnlDet.GLAccountID = jrnlDet.GLAccountID;
                    jsJrnlDet.GLAccountCode = jrnlDet.GLAccountCode;
                    jsJrnlDet.GLAccountName = jrnlDet.GLAccountName;
                    jsJrnlDet.GLGroupIDAcc = jrnlDet.GLGroupID;
                    jsJrnlDet.GLAccountIDEdit = jrnlDet.GLAccountID;
                    


                    jsJrnlDet.GLGroupID = jrnlDet.GLGroupID;
                    jsJrnlDet.GLGroupCode = jrnlDet.GLGroupCode;
                    jsJrnlDet.GLGroupName = jrnlDet.GLGroupName;
                    jsJrnlDet.GLGroupNameShort = jrnlDet.GLGroupNameShort;
                    jsJrnlDet.GLGroupClassID = jrnlDet.GLGroupClassID;
                    jsJrnlDet.GLGroupIDEdit = jrnlDet.GLGroupID;


                    jsJrnlDet.TranTypeID = jrnlDet.TranTypeID;
                    jsJrnlDet.TranTypeCode = jrnlDet.TranTypeCode;
                    jsJrnlDet.TranTypeName = jrnlDet.TranTypeName;
                    jsJrnlDet.TranTypeCategoryID = jrnlDet.TranTypeCategoryID;
                    
                    jsJrnlDet.IsInstrument = jrnlDet.IsInstrument ? 1 : 0;
                    jsJrnlDet.IsCash = jrnlDet.IsCash ? 1 : 0;
                    


                    jsJrnlDet.DrCr = jrnlDet.DrCr;
                    jsJrnlDet.DebitAmt = jrnlDet.DebitAmt;
                    jsJrnlDet.CreditAmt = jrnlDet.CreditAmt;
                    jsJrnlDet.TranAmt = jrnlDet.TranAmt;

                    jsJrnlDet.JournalDetDesc = jrnlDet.JournalDetDesc;
                    jsJrnlDetList.Add(jsJrnlDet);
                }

            }


            JavaScriptSerializer jss = new JavaScriptSerializer();
            jsonStrJournal = jss.Serialize(jsJrnl);
            jsonStrJournalDetList = jss.Serialize(jsJrnlDetList);

            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            sb.Append("\"iserror\":" + (isError ? "1" : "0"));
            sb.Append(",\"errorno\":" + errorNo.ToString());
            sb.Append(",\"errorstring\":" + "\"" + errorString + "\"");
            sb.Append(",\"statusstring\":" + "\"" + statuString + "\"");
            sb.Append(",\"journal\":" + jsonStrJournal);
            sb.Append(",\"journaldetlist\":" + jsonStrJournalDetList);


            //sb.Append("\"page\":" + pageNo);
            //sb.Append(",\"totalpage\":" + totalPage);
            //sb.Append(",\"records\":" + records);
            
            //sb.Append(",\"journal\":" + jsonData);
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