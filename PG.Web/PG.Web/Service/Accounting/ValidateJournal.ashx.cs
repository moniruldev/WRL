using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.Core.Utility;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using System.Text;
using PG.Core.DBValidation;
using PG.DBClass.AccountingDC.AccEnums;
using PG.Core;

namespace PG.Web.Service.Accounting
{
    /// <summary>
    /// Summary description for UpdateJournal
    /// </summary>
    public class ValidateJournal : IHttpHandler
    {
        private class JSonTask
        { 
            public string Task { get; set; }
            public int EditModeInt {get;set;}
            public int DrCr { get; set; }
            public int JournalID { get; set; }
            public int JournalDetID { get; set; }
            public int JournalDetID_Link { get; set; }
            public int AccRefTypeID { get; set; }
            public int AccRefCategoryID { get; set; }

            public bool IsFromWebUI { get; set; }


            public bool UseDetIDLink { get; set; }

            public bool ValidateALL { get; set; }

        }

        private class JSonResult
        {
            public string Task { get; set; }
            public int EditModeInt { get; set; }
            public int JournalID { get; set; }
            public int IsError { get; set; }
            public string ErrorText { get; set; }
            public int JouranlDetID { get; set; }
            public int JouranlDetLineNo { get; set; }
            public int DrCr { get; set; }

            public string ShowText01 { get; set; }
            public string ShowText02 { get; set; }

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

            if (context.Request.Params["jsonJournalAccRefList"] != null)
            {
                jsonJournalRefListString = context.Request.Params["jsonJournalAccRefList"];
            }

            if (context.Request.Params["jsonJournalInsList"] != null)
            {
                jsonJournalInsListString = context.Request.Params["jsonJournalInsList"];
            }

            JSonTask jsonTask = ParseJSonStringTask(jsonTaskString);
            dcJournal jrnl = ParseJSonStringJournal(jsonJournalString);
            jrnl.JournalDetList = ParseJSonStringJournalDetList(jsonJournalDetListString);
            jrnl.JournalDetRefList  = ParseJSonStringJournalDetRefList(jsonJournalRefListString);
            jrnl.JournalDetInsList = ParseJSonStringJournalDetInsList(jsonJournalInsListString);

            JSonResult jsonResult = new JSonResult();
            jsonResult.Task = jsonTask.Task;

            JournalValidationTask vTask = new JournalValidationTask();
            vTask.UseDetIDLink = jsonTask.UseDetIDLink;
            vTask.FromWebUI = jsonTask.IsFromWebUI;
            vTask.EditMode = (FormDataMode)jsonTask.EditModeInt;
            
            
            JournalValidationResult vResult = new JournalValidationResult();
            

            switch (jsonTask.Task.Trim().ToLower())
            {

                case "journal":
                case "journalfull":
                    vTask.DBValidationType = JournalValidtationTypeSet.Journal;
                    vTask.ValidateALL = jsonTask.ValidateALL;
                    vTask.Journal = jrnl;
                    vResult =  JournalValidationBL.ValidateJournalFull(vTask);
                    if (vResult.IsError)
                    {
                        jsonResult.IsError = 1;
                        jsonResult.ErrorText = vResult.ErrorString;
                    }
                    else
                    {
                        jsonResult.IsError = 0;
                        jsonResult.ErrorText = string.Empty;
                    }

                    break;

                case "journaldetreftype":
                    //ValidationResult vResult = new ValidationResult();

                    vTask.DBValidationType = JournalValidtationTypeSet.JournalDetRef;
                    vTask.ValidateALL = false;

                    vTask.AccRefType = (AccRefTypeEnum)jsonTask.AccRefTypeID;

                    if (jsonTask.UseDetIDLink)
                    {
                        jrnl.JournalDetList = jrnl.JournalDetList.Where(c => c.JournalDetID_Link == jsonTask.JournalDetID_Link).ToList();
                        jrnl.JournalDetRefList = jrnl.JournalDetRefList.Where(c => c.JournalDetID_Link == jsonTask.JournalDetID_Link).ToList();
                    }
                    else
                    {
                        jrnl.JournalDetList = jrnl.JournalDetList.Where(c => c.JournalDetID == jsonTask.JournalDetID).ToList();
                        jrnl.JournalDetRefList = jrnl.JournalDetRefList.Where(c => c.JournalDetID == jsonTask.JournalDetID).ToList();
                    }
                    
                    vTask.Journal = jrnl;
                    vResult =  JournalValidationBL.ValidateJournalDetRef(vTask);
                    if (vResult.IsError)
                    {
                        jsonResult.IsError = 1;
                        jsonResult.ErrorText = vResult.ErrorString;
                    }
                    else
                    {
                        jsonResult.IsError = 0;
                        jsonResult.ErrorText = string.Empty;
                    }
                    break;
            }

            string jsonStrResult = "";
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
            if (string.IsNullOrEmpty(jsonTaskString))
            {
                return null;
            }
            Dictionary<string, string> dictTaskData = new Dictionary<string, string>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictTaskData = (Dictionary<string, string>)jss.Deserialize(jsonTaskString, typeof(Dictionary<string, string>));

            if (dictTaskData == null)
            {
                return null;
            }

            JSonTask jsonTask = new JSonTask();

            if (dictTaskData.ContainsKey("Task"))
            {
                jsonTask.Task = Convert.ToString(dictTaskData["Task"]).ToUpper();
            }

            if (dictTaskData.ContainsKey("EditModeInt"))
            {
                jsonTask.EditModeInt = Convert.ToInt32(dictTaskData["EditModeInt"]);
            }

            if (dictTaskData.ContainsKey("DrCr"))
            {
                jsonTask.EditModeInt = Convert.ToInt32(dictTaskData["DrCr"]);
            }

            if (dictTaskData.ContainsKey("JournalID"))
            {
                jsonTask.JournalID = Convert.ToInt32(dictTaskData["JournalID"]);
            }

            if (dictTaskData.ContainsKey("JournalDetID"))
            {
                jsonTask.JournalDetID = Convert.ToInt32(dictTaskData["JournalDetID"]);
            }

            if (dictTaskData.ContainsKey("JournalDetID_Link"))
            {
                jsonTask.JournalDetID_Link = Convert.ToInt32(dictTaskData["JournalDetID_Link"]);
            }

            if (dictTaskData.ContainsKey("AccRefTypeID"))
            {
                jsonTask.AccRefTypeID = Convert.ToInt32(dictTaskData["AccRefTypeID"]);
            }

            if (dictTaskData.ContainsKey("AccRefCategoryID"))
            {
                jsonTask.AccRefCategoryID = Convert.ToInt32(dictTaskData["AccRefCategoryID"]);
            }

            if (dictTaskData.ContainsKey("IsFromWebUI"))
            {
                jsonTask.IsFromWebUI =   Convert.ToInt32(dictTaskData["IsFromWebUI"]) == 1 ? true : false;
            }


            if (dictTaskData.ContainsKey("UseDetIDLink"))
            {
                jsonTask.UseDetIDLink = Convert.ToInt32(dictTaskData["UseDetIDLink"]) == 1 ? true : false;
            }

            if (dictTaskData.ContainsKey("ValidateALL"))
            {
                jsonTask.ValidateALL = Convert.ToInt32(dictTaskData["ValidateALL"]) == 1 ? true : false;
            }


            

            
            return jsonTask;
        }

        public dcJournal ParseJSonStringJournal(string jsonJournalString)
        {
            if (string.IsNullOrEmpty(jsonJournalString))
            {
                return null;
            }

            Dictionary<string, string> dictJouranlData = new Dictionary<string, string>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictJouranlData = (Dictionary<string, string>)jss.Deserialize(jsonJournalString, typeof(Dictionary<string, string>));

            if(dictJouranlData == null)
            {
                return null;
            }
            
            dcJournal jrnl = new dcJournal();

            if (dictJouranlData != null)
            {
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
            }
            //jrnl._ChangedList.Remove("JournalID");

            return jrnl;
        }
        public List<dcJournalDet> ParseJSonStringJournalDetList(string jsonJournalDetListString)
        {
            List<dcJournalDet> jrnlDetList = new List<dcJournalDet>();

            if (string.IsNullOrEmpty(jsonJournalDetListString))
            {
                return jrnlDetList;
            }
            List<Dictionary<string, string>> dictJouranlDetListData = new List<Dictionary<string, string>>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictJouranlDetListData = (List<Dictionary<string, string>>)jss.Deserialize(jsonJournalDetListString, typeof(List<Dictionary<string, string>>));

            if (dictJouranlDetListData==null)
            {
                return jrnlDetList;
            }


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

                if (dicObj.ContainsKey("DrCr"))
                {
                    jrnlDet.DrCr = Convert.ToInt32(dicObj["DrCr"]);
                }

                if (dicObj.ContainsKey("JournalDetSLNo"))
                {
                    jrnlDet.JournalDetSLNo = Convert.ToInt32(dicObj["JournalDetSLNo"]);
                }


                if (dicObj.ContainsKey("JournalDetID_Link"))
                {
                    jrnlDet.JournalDetID_Link = Convert.ToInt32(dicObj["JournalDetID_Link"]);
                }

                //if (dicObj.ContainsKey("TranTypeID"))
                //{
                //    jrnlDet.TranTypeID = Convert.ToInt32(dicObj["TranTypeID"]);
                //}

                jrnlDetList.Add(jrnlDet);
            }

            return jrnlDetList;
        }

        public List<dcJournalDetRef> ParseJSonStringJournalDetRefList(string jsonJournalRefListString)
        {
            List<dcJournalDetRef> jrnlDetRefList = new List<dcJournalDetRef>();

            if (string.IsNullOrEmpty(jsonJournalRefListString))
            {
                return jrnlDetRefList;
            }
            List<Dictionary<string, string>> dictJouranlDetRefListData = new List<Dictionary<string, string>>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictJouranlDetRefListData = (List<Dictionary<string, string>>)jss.Deserialize(jsonJournalRefListString, typeof(List<Dictionary<string, string>>));

            if (dictJouranlDetRefListData == null)
            {
                return jrnlDetRefList;
            }

            foreach (Dictionary<string, string> dicObj in dictJouranlDetRefListData)
            {
                dcJournalDetRef jrnlDetRef = new dcJournalDetRef();

                if (dicObj.ContainsKey("_recordstateint"))
                {
                    jrnlDetRef._RecordStateInt = Convert.ToInt32(dicObj["_recordstateint"]);
                }

                if (dicObj.ContainsKey("journaldetrefid"))
                {
                    jrnlDetRef.JournalDetRefID = Convert.ToInt32(dicObj["journaldetrefid"]);
                }

                if (dicObj.ContainsKey("journaldetid"))
                {
                    jrnlDetRef.JournalDetID = Convert.ToInt32(dicObj["journaldetid"]);
                }

                if (dicObj.ContainsKey("typeid"))
                {
                    jrnlDetRef.AccRefTypeID = Convert.ToInt32(dicObj["typeid"]);
                }

                if (dicObj.ContainsKey("id"))
                {
                    jrnlDetRef.AccRefID = Convert.ToInt32(dicObj["id"]);
                }

                if (dicObj.ContainsKey("code"))
                {
                    jrnlDetRef.AccRefCode = Convert.ToString(dicObj["code"]);
                }

                if (dicObj.ContainsKey("categoryid"))
                {
                    jrnlDetRef.AccRefCategoryID = Convert.ToInt32(dicObj["categoryid"]);
                }

                if (dicObj.ContainsKey("categorycode"))
                {
                    jrnlDetRef.AccRefCategoryCode = Convert.ToString(dicObj["categorycode"]);
                }


                if (dicObj.ContainsKey("drcr"))
                {
                    jrnlDetRef.DrCr = Convert.ToInt32(dicObj["drcr"]);
                }

                if (dicObj.ContainsKey("amt"))
                {
                    decimal amt = Conversion.StringToDecimal(dicObj["amt"].ToString());

                    if (jrnlDetRef.DrCr == 0)
                    {
                        jrnlDetRef.DebitAmt = amt;
                    }
                    else
                    {
                        jrnlDetRef.CreditAmt = amt;
                    }
                }

                if (dicObj.ContainsKey("slno"))
                {
                    jrnlDetRef.JournalDetRefSLNo = Convert.ToInt32(dicObj["slno"]);
                }

                if (dicObj.ContainsKey("linkid"))
                {
                    jrnlDetRef.JournalDetID_Link = Convert.ToInt32(dicObj["linkid"]);
                }

                jrnlDetRefList.Add(jrnlDetRef);
            }

            return jrnlDetRefList;
        }


        public List<dcJournalDetIns> ParseJSonStringJournalDetInsList(string jsonJournalInsListString)
        {
            List<dcJournalDetIns> jrnlDetInsList = new List<dcJournalDetIns>();

            if (string.IsNullOrEmpty(jsonJournalInsListString))
            {
                return jrnlDetInsList;
            }
            List<Dictionary<string, string>> dictJouranlDetInsListData = new List<Dictionary<string, string>>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dictJouranlDetInsListData = (List<Dictionary<string, string>>)jss.Deserialize(jsonJournalInsListString, typeof(List<Dictionary<string, string>>));

            if (dictJouranlDetInsListData == null)
            {
                return jrnlDetInsList;
            }

            foreach (Dictionary<string, string> dicObj in dictJouranlDetInsListData)
            {
                dcJournalDetIns jrnlDetIns = new dcJournalDetIns();

                if (dicObj.ContainsKey("_recordstateint"))
                {
                    jrnlDetIns._RecordStateInt = Convert.ToInt32(dicObj["_recordstateint"]);
                }

                if (dicObj.ContainsKey("journaldetinsid"))
                {
                    jrnlDetIns.JournalDetInsID = Convert.ToInt32(dicObj["journaldetinsid"]);
                }

                if (dicObj.ContainsKey("journaldetid"))
                {
                    jrnlDetIns.JournalDetID = Convert.ToInt32(dicObj["journaldetid"]);
                }

                if (dicObj.ContainsKey("insid"))
                {
                    jrnlDetIns.InstrumentID = Convert.ToInt32(dicObj["insid"]);
                }


                if (dicObj.ContainsKey("insno"))
                {
                    jrnlDetIns.InstrumentNo = Convert.ToString(dicObj["insno"]);
                }

                if (dicObj.ContainsKey("insdate"))
                {
                    //jrnlDetIns.InstrumentNo = Convert.ToString(dicObj["insdate"]);

                    jrnlDetIns.InstrumentDate = Conversion.StringToDateORNull(dicObj["insdate"]);

                }

                if (dicObj.ContainsKey("drcr"))
                {
                    jrnlDetIns.DrCr = Convert.ToInt32(dicObj["drcr"]);
                }

                jrnlDetIns.InstrumentLinkTypeID = (int)InstrumentLinkTypeEnum.Actual;

                decimal insAmt = 0;
                if (dicObj.ContainsKey("amt"))
                {
                    insAmt = Conversion.StringToDecimal(dicObj["amt"].ToString());
                    jrnlDetIns.TranAmt = insAmt;
                }

                if (jrnlDetIns.DrCr == (int)DebitCreditEnum.Debit)
                {
                    jrnlDetIns.DebitAmt = insAmt;
                    jrnlDetIns.CreditAmt = 0;
                }

                if (jrnlDetIns.DrCr == (int)DebitCreditEnum.Credit)
                {
                    jrnlDetIns.DebitAmt = 0;
                    jrnlDetIns.CreditAmt = insAmt;
                }


                if (dicObj.ContainsKey("slno"))
                {
                    jrnlDetIns.JournalDetInsSLNo = Convert.ToInt32(dicObj["slno"]);
                }

                if (dicObj.ContainsKey("linkid"))
                {
                    jrnlDetIns.JournalDetID_Link = Convert.ToInt32(dicObj["linkid"]);
                }

                jrnlDetInsList.Add(jrnlDetIns);
            }

            return jrnlDetInsList;
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