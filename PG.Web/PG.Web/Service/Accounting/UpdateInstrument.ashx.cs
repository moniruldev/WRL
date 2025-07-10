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
    public class UpdateInstrument : IHttpHandler
    {
        private class jsonInstrument
        {

            public int companyid { get; set; }
            public int instypeid { get; set; }
            public int insmodeid { get; set; }

            public int insid { get; set; }
            public string insno { get; set; }
            public string insdate { get; set; }
            public decimal insamt { get; set; }

            public string issuename { get; set; }
            public string bankname { get; set; }
            public string branchname { get; set; }
            public string bankbranchname { get; set; }

            public string remarks { get; set; }

            public int insstatusid { get; set; }
            public string insstatusname { get; set; }

            public string insstatusdate { get; set; }

            public int _recordstateint { get; set; }
        }


        public void ProcessRequest(HttpContext context)
        {
            string postData = "";
            if (context.Request.RequestType.ToUpper() == "POST")
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(context.Request.InputStream, context.Request.ContentEncoding);
                postData = HttpUtility.UrlDecode(sr.ReadToEnd());
            }
            else
            {
                postData = HttpUtility.UrlDecode(context.Request.QueryString.ToString());
            }
            NameValueCollection dList = PG.Web.Helper.ConvertJSonTextToList(postData);

            List<dcInstrument> insList = new List<dcInstrument>();
            string jsonInsData = dList["inslist"];

            JavaScriptSerializer jssIns = new JavaScriptSerializer();
            jsonInsData = HttpUtility.HtmlDecode(jsonInsData);

            List<jsonInstrument> jsonInsList = jssIns.Deserialize<List<jsonInstrument>>(jsonInsData);

            foreach (jsonInstrument jObj in jsonInsList)
            {
                dcInstrument ins = new dcInstrument();
                ins._RecordStateInt = jObj._recordstateint;

                ins.CompanyID = jObj.companyid;
                ins.InstrumentModeID = jObj.insmodeid;
                ins.InstrumentTypeID = jObj.instypeid;

                ins.InstrumentID = jObj.insid;
                ins.InstrumentNo = jObj.insno.Trim();
                ins.InstrumentDate = Conversion.StringToDateORNull(jObj.insdate);

                ins.IssueName = jObj.issuename;
                ins.BankName = jObj.bankname;
                ins.BranchName = jObj.branchname;

                ins.InstrumentAmt = jObj.insamt;

                ins.InstrumentStatusID = jObj.insstatusid;
                ins.InstrumentStatusDate = Conversion.StringToDateORNull(jObj.insstatusdate);

                ins.Remarks = jObj.remarks;
                insList.Add(ins);
            }

            int insID = 0;
            string errMsg = string.Empty;
            string isSuccess = "0";
            bool isValid = false;

            int insTypeID = 0;
            int insModeID = 0;
            string insNo = string.Empty;


            dcInstrument insUpd = insList.FirstOrDefault();
            if (insUpd != null)
            {
                isValid = true;
                if (insUpd.InstrumentNo == string.Empty)
                {
                    errMsg = "Please enter Instrument No!";
                    isValid = false;
                }

                if (insUpd.InstrumentModeID == 0)
                {
                    errMsg = "Please specify instrument mode!";
                    isValid = false;
                }

                if (insUpd.InstrumentTypeID == 0)
                {
                    errMsg = "Please specify instrument type!";
                    isValid = false;
                }


                if (insUpd.InstrumentStatusID == (int)InstrumentStatusEnum.Cleared)
                {
                    if (!insUpd.InstrumentStatusDate.HasValue)
                    {
                        errMsg = "Please enter date for cleared instrument!";
                        isValid = false;
                    }
                }


                if (insUpd.InstrumentID == 0)
                {
                        if (InstrumentBL.IsInstrumentNoExists(insUpd.CompanyID, insUpd.InstrumentNo, insUpd.InstrumentModeID, insUpd.InstrumentTypeID))
                        {
                            errMsg = "Instrument No aleardy exists Please Enter Another!";
                            isValid = false;
                        }
                }


                if (isValid)
                {
                    bool isAdd = insUpd.InstrumentID == 0;
                    insID = InstrumentBL.Save(insUpd,isAdd);
                    if (insID > 0)
                    {
                        isSuccess = "1";
                        insTypeID = insUpd.InstrumentTypeID;
                        insModeID = insUpd.InstrumentModeID;
                        insNo = insUpd.InstrumentNo;
                    }
                }
            }
            else
            {
                isSuccess = "0";
                errMsg = "No Instrument Information!";
            }


            StringBuilder sb = new StringBuilder();
            
            sb.Append("{");
            sb.Append("\"success\":" + isSuccess);
            sb.Append(",\"total\":" + 1);
            sb.Append(",\"msg\":\"" + errMsg + "\"");
            sb.Append(",\"insid\":" + insID.ToString());
            sb.Append(",\"insmodeid\":" + insModeID.ToString());
            sb.Append(",\"instypeid\":" + insTypeID.ToString());
            sb.Append(",\"insno\":\"" + insNo + "\"");
            //sb.Append(",\"rows\":" + jsonData);
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
