using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.Core.Utility;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using System.Text;

namespace PG.Web.Service.Accounting
{
    /// <summary>
    /// Summary description for UpdateJournal
    /// </summary>
    public class UpdateJournal : IHttpHandler
    {
        private class JSonTask
        {
            public int EditModeInt {get;set;}
            public int JournalID { get; set; }
        }

        private class JSonResult
        {
            public int JournalID { get; set; }
            public int IsError { get; set; }
            public string ErrorText { get; set; }
        }



        public void ProcessRequest(HttpContext context)
        {
            string jsonTaskString = "";
            string jsonJournalString = "";
            string jsonJournalDetListString = "";
            string jsonJournalRefListString = "";
            string jsonJournalInsListString = "";

            if (context.Request.Params["jsonTask"] != null)
            {
                jsonTaskString = context.Request.Params["jsonTask"];
            }
            
            if (context.Request.Params["jsonJournal"] != null)
            {
                jsonJournalString = context.Request.Params["jsonJournal"];
            }
            
            if (context.Request.Params["jsonJournalDetList"] != null)
            {
                jsonJournalDetListString = context.Request.Params["jsonJournalDetList"];
            }

            if (context.Request.Params["jsonJournalRefListString"] != null)
            {
                jsonJournalRefListString = context.Request.Params["jsonJournalRefListString"];
            }

            if (context.Request.Params["jsonJournalInsListString"] != null)
            {
                jsonJournalInsListString = context.Request.Params["jsonJournalInsListString"];
            }

            JSonTask jsonTask = ParseJSonStringTask(jsonTaskString);
            dcJournal jrnl = ParseJSonStringJournal(jsonJournalString);
            List<dcJournalDet> jrnlDetList = ParseJSonStringJournalDetList(jsonJournalDetListString);
            jrnlDetList = JournalDetBL.ValidateJournalDetList(jrnlDetList);
            JournalDetBL.UpdateSLNo(jrnlDetList, false);

            jrnl.JournalDetList = jrnlDetList;


            bool IsAutoPrint = false;
            dcJournalType jrType = JournalTypeBL.GetJournalTypeByID(jrnl.CompanyID, jrnl.JournalTypeID);
            if (jrType != null)
            {
                if (jrType.IsAutoPost)
                {
                    jrnl.IsPosted = true;
                    jrnl.PostedDate = DateTime.Today;
                }
                IsAutoPrint = jrType.IsAutoPrint;
            }

            int newJournalID = 0;
            bool isAdd = false;
            string errorText = string.Empty;

            isAdd = jsonTask.EditModeInt == 2;  //0=none,1=Read, 2=Add,3=Edit,4=Delete,

            //try
            {
                newJournalID = JournalBL.Save(jrnl, isAdd);
            }
            //catch(Exception ex)
            //{
            //    errorText = ex.InnerException.Message;
            //}
            

            JSonResult jsonResult = new JSonResult();
            string jsonStrResult = "";

            if (newJournalID > 0)
            {
                jsonResult.IsError = 0;
                jsonResult.JournalID = newJournalID;
            }
            else
            {
                jsonResult.IsError = 1;
                jsonResult.JournalID = 0;
                jsonResult.ErrorText = "Error on Save(" + (isAdd ? "Add" : "Edit") + ") -" + errorText;
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jsonStrResult = jss.Serialize(jsonResult);

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(jsonStrResult);
            context.ApplicationInstance.CompleteRequest();


            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        private JSonTask ParseJSonStringTask(string jsonTaskString)
        {
            JSonTask jsonTask = new JSonTask();
            
            Dictionary<string, string> dictTaskData = new Dictionary<string, string>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictTaskData = (Dictionary<string, string>)jss.Deserialize(jsonTaskString, typeof(Dictionary<string, string>));


            if (dictTaskData.ContainsKey("EditModeInt"))
            {
                jsonTask.EditModeInt = Convert.ToInt32(dictTaskData["EditModeInt"]);
            }

            if (dictTaskData.ContainsKey("JournalID"))
            {
                jsonTask.JournalID = Convert.ToInt32(dictTaskData["JournalID"]);
            }


            return jsonTask;
        }

        public dcJournal ParseJSonStringJournal(string jsonJournalString)
        {
            dcJournal jrnl = new dcJournal();

            Dictionary<string, string> dictJouranlData = new Dictionary<string, string>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictJouranlData = (Dictionary<string, string>)jss.Deserialize(jsonJournalString, typeof(Dictionary<string, string>));

            if (dictJouranlData.ContainsKey("CompanyID"))
            {
                jrnl.CompanyID = Convert.ToInt32(dictJouranlData["CompanyID"]);
            }
            if (dictJouranlData.ContainsKey("JournalID"))
            {
              jrnl.JournalID = Convert.ToInt32(dictJouranlData["JournalID"]);
            }

            if (dictJouranlData.ContainsKey("JournalNo"))
            {
                jrnl.JournalNo = dictJouranlData["JournalNo"].ToString();
            }

            if (dictJouranlData.ContainsKey("AccYearID"))
            {
                jrnl.AccYearID = Convert.ToInt32(dictJouranlData["AccYearID"]);
            }

            if (dictJouranlData.ContainsKey("JournalDate"))
            {
                jrnl.JournalDate = Conversion.StringToDate(dictJouranlData["JournalDate"].ToString());
            }

            if (dictJouranlData.ContainsKey("JournalTypeID"))
            {
                jrnl.JournalTypeID = Convert.ToInt32(dictJouranlData["JournalTypeID"]);
            }

            if (dictJouranlData.ContainsKey("JournalUpdateNo"))
            {
                jrnl.JournalUpdateNo = Convert.ToInt32(dictJouranlData["JournalUpdateNo"]);
            }

            //jrnl._ChangedList.Remove("JournalID");

            return jrnl;
        }
        public List<dcJournalDet> ParseJSonStringJournalDetList(string jsonJournalDetListString)
        {
            List<dcJournalDet> jrnlDetList = new List<dcJournalDet>();

            List<Dictionary<string, string>> dictJouranlDetListData = new List<Dictionary<string, string>>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictJouranlDetListData = (List<Dictionary<string, string>>)jss.Deserialize(jsonJournalDetListString, typeof(List<Dictionary<string, string>>));

            foreach (Dictionary<string, string> dicObj in dictJouranlDetListData)
            {
                dcJournalDet jrnlDet = new dcJournalDet();

                if (dicObj.ContainsKey("_RecordStateInt"))
                {
                    jrnlDet._RecordStateInt = Convert.ToInt32(dicObj["_RecordStateInt"]);
                }

                if (dicObj.ContainsKey("JournalDetID"))
                {
                    jrnlDet.JournalDetID = Convert.ToInt32(dicObj["JournalDetID"]);
                }

                if (dicObj.ContainsKey("JournalID"))
                {
                    jrnlDet.JournalID = Convert.ToInt32(dicObj["JournalID"]);
                }

                if (dicObj.ContainsKey("GLAccountID"))
                {
                    jrnlDet.GLAccountID = Convert.ToInt32(dicObj["GLAccountID"]);
                }

                if (dicObj.ContainsKey("DebitAmt"))
                {
                    jrnlDet.DebitAmt = Conversion.StringToDecimal(dicObj["DebitAmt"].ToString());
                }

                if (dicObj.ContainsKey("CreditAmt"))
                {
                    jrnlDet.CreditAmt = Conversion.StringToDecimal(dicObj["CreditAmt"].ToString());
                }

                if (dicObj.ContainsKey("JournalDetDesc"))
                {
                    jrnlDet.JournalDetDesc = Convert.ToString(dicObj["JournalDetDesc"]);
                }

                if (dicObj.ContainsKey("TranTypeID"))
                {
                    jrnlDet.TranTypeID = Convert.ToInt32(dicObj["TranTypeID"]);
                }

                jrnlDetList.Add(jrnlDet);
            }



            //foreach()






            //foreach(D)
            //

            //
            //

            //if (dictJouranlDetListData.ContainsKey("JournalID"))
            //{
            //    jrnl.JournalID = Convert.ToInt32(dictJouranlDetListData["JournalID"]);
            //}

            //if (dictJouranlDetListData.ContainsKey("JournalNo"))
            //{
            //    jrnl.JournalNo = dictJouranlDetListData["JournalNo"].ToString();
            //}

            //if (dictJouranlDetListData.ContainsKey("JournalDate"))
            //{
            //    jrnl.JournalDate = Conversion.StringToDate(dictJouranlDetListData["JournalDate"].ToString());
            //}

            //jrnl._ChangedList.Remove("JournalID");




            return jrnlDetList;
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