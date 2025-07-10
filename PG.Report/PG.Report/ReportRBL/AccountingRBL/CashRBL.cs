using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PG.Core;
using PG.Core.Utility;
using PG.Core.DBBase;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.Report.ReportEnums;
using PG.Report.ReportClass.AccountingRC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Report.ReportRBL.AccountingRBL
{
    public class CashRBL
    {
        public static List<rcCashSummary> GetCashSummary(clsPrmLedger prmLedger)
        {
            return GetCashSummary(prmLedger, null);
        }

        public static List<rcCashSummary> GetCashSummary(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcCashSummary> cRptList = new List<rcCashSummary>();

            rcCashSummary cRpt = new rcCashSummary();

            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);

            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRptHeader.NumberFormat = prmLedger.NumberFormat;


            ///summary
            List<rcGLReportItem> cRptItemList = new List<rcGLReportItem>();

            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(prmLedger.CompanyID, dc);
            List<dcGLGroup> grpListCash = grpList.Where(g => g.IsCash == true || g.IsBank == true).ToList();


            //List<dcGLAccount> accBalanceCash = GLAccountBL.GetGLAccountListbyGroups(prmLedger.CompanyID, grpListCash, dc);
            List<dcGLAccount> accBalanceCash = GLAccountBL.GetAccountBalance(prmLedger, grpListCash, dc);
            grpListCash = GLGroupBL.SetGLGroupListHeirerchy(grpListCash, grpList, prmLedger.IncludeGroupParents);

            List<dcGLGroup> grpBalanceCash = GLGroupBL.GetGroupBalance(prmLedger, grpListCash, accBalanceCash, dc);


            List<rcGLReportItem> cRptListCash = new List<rcGLReportItem>();


            List<dcGLGroup> grpAccRoot = grpListCash.Where(c => c.HasParent == false).ToList();



            GLReportRBL.AddGLReportItems(grpAccRoot, 1, prmLedger,
                                                                grpBalanceCash, accBalanceCash, cRptListCash);


            foreach (rcGLReportItem itm in cRptListCash)
            {
                ResetItemIndent(itm, prmLedger);
                //ResetItemBalanceDisplay(itm, prmLedger);
                cRptItemList.Add(itm);
            }

            cRpt.CashSummaryHeader.Add(cRptHeader);
            cRpt.CashSummaryItems = cRptItemList;

            CalcSum(grpListCash, cRptHeader);

            cRptList.Add(cRpt);

            return cRptList;

        }


        public static void CalcSum(List<dcGLGroup> grpListCash, rcGLReportHeader itmTotal)
        {
            List<dcGLGroup> grpAccRoot = grpListCash.Where(c => c.HasParent == false).ToList();

            decimal openDebitAmt = 0;
            decimal openCreditAmt = 0;

            decimal tranDebitAmt = 0;
            decimal tranCreditAmt = 0;

            decimal closeDebitAmt = 0;
            decimal closeCreditAmt = 0;


            foreach (dcGLGroup grp in grpAccRoot)
            {
                openDebitAmt += grp.OpenDebitAmt;
                openCreditAmt += grp.OpenCreditAmt;

                tranDebitAmt += grp.DebitAmt;
                tranCreditAmt += grp.CreditAmt;

                closeDebitAmt += grp.CloseDebitAmt;
                closeCreditAmt += grp.CloseCreditAmt;
            }


            itmTotal.OpenDebitAmt = openDebitAmt;
            itmTotal.OpenCreditAmt = openCreditAmt;
            itmTotal.OpenAmt = openDebitAmt - openCreditAmt;
            itmTotal.OpenBalanceAmt = Math.Abs(itmTotal.OpenAmt);
            itmTotal.OpenBalanceDrCr = itmTotal.OpenAmt >= 0 ? 0 : 1;
            itmTotal.OpenBalanceDrCrText = itmTotal.OpenAmt >= 0 ? "Dr" : "Cr";
            itmTotal.OpenBalanceDisplay = Math.Abs(itmTotal.OpenAmt);



            itmTotal.TranDebitAmt = tranDebitAmt;
            itmTotal.TranCreditAmt = tranCreditAmt;
            itmTotal.TranAmt = tranDebitAmt - tranCreditAmt;
            itmTotal.TranBalanceAmt = Math.Abs(itmTotal.TranAmt);
            itmTotal.TranBalanceDrCr = itmTotal.TranAmt >= 0 ? 0 : 1;
            itmTotal.TranBalanceDrCrText = itmTotal.TranAmt >= 0 ? "Dr" : "Cr";
            itmTotal.TranBalanceDisplay = Math.Abs(itmTotal.TranAmt);


            itmTotal.CloseDebitAmt = closeDebitAmt;
            itmTotal.CloseCreditAmt = closeCreditAmt;
            itmTotal.CloseAmt = closeDebitAmt - closeCreditAmt;
            itmTotal.CloseBalanceAmt = Math.Abs(itmTotal.CloseAmt);
            itmTotal.CloseBalanceDrCr = itmTotal.CloseAmt >= 0 ? 0 : 1;
            itmTotal.CloseBalanceDrCrText = itmTotal.CloseAmt >= 0 ? "Dr" : "Cr";
            itmTotal.CloseBalanceDisplay = Math.Abs(itmTotal.CloseAmt);



        }


        public static void ResetItemIndent(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {
            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            {
                //rptItem.ItemLevel = rptItem.ItemLevel > 1 ? 1 : 0;
                rptItem.ItemLevel = 0;
            }

            if (prmLedger.IsIndentName)
            {
                if (prmLedger.IsExcelExport)
                {
                    rptItem.ItemNameIndent = string.Concat(ArrayList.Repeat(prmLedger.IndentPaddingCharExcel, rptItem.ItemLevel).ToArray()) + rptItem.ItemName;
                }
                else
                {
                    rptItem.ItemNameIndent = string.Concat(ArrayList.Repeat(prmLedger.IndentPaddingChar, rptItem.ItemLevel).ToArray()) + rptItem.ItemName;
                }
                rptItem.ItemNameDispaly = rptItem.ItemNameIndent;
            }

            else
            {
                rptItem.ItemNameIndent = rptItem.ItemName;
                rptItem.ItemNameDispaly = rptItem.ItemName;
            }
        }





        public static List<rcJournalTrans> GetJournalBook(clsPrmLedger prmLedger)
        {
            return GetJournalBook(prmLedger, null);
        }
        public static List<rcJournalTrans> GetJournalBook(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournalTrans> cRptList = new List<rcJournalTrans>();


            //SqlCommand cmd = new SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder();


            sb.Append("SELECT tblJournalDet.* ");

            sb.Append(" , tblJournal.CompanyID, tblJournal.AccYearID  ");
            sb.Append(" , tblJournal.JournalNo, tblJournal.JournalDate ");
            sb.Append(" , tblJournal.JournalNote, tblJournal.JournalDesc, tblJournal.JournalRefNo ");
            sb.Append(" , tblJournal.IsPosted, tblJournal.JournalAdjustTypeID ");

            sb.Append("  , tblJournal.JournalTypeID , tblJournalType.JournalTypeCode, tblJournalType.JournalTypeName ");
            sb.Append(" , tblGLAccount.GLAccountCode, tblGLAccount.GLAccountName ");
            sb.Append(" , tblGLAccount.BalanceType ");


            sb.Append(" FROM  tblJournalDet ");
            sb.Append(" INNER JOIN  tblJournal ON tblJournalDet.JournalID = tblJournal.JournalID ");
            sb.Append(" INNER JOIN tblGLAccount ON tblJournalDet.GLAccountID = tblGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblJournalType ON tblJournal.JournalTypeID = tblJournalType.JournalTypeID ");

            sb.Append("WHERE (1=1) ");

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }

            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" ORDER BY tblJournal.JournalDate ");
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            cRptList = DBQuery.ExecuteDBQuery<rcJournalTrans>(dbq, dc);


            return cRptList;
        }


        public static List<rcJournal> GetJournalList(clsPrmLedger prmLedger)
        {
            return GetJournalList(prmLedger, null);
        }
        public static List<rcJournal> GetJournalList(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcJournal> cRptList = new List<rcJournal>();


            //SqlCommand cmd = new SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(JournalBL.GetJournalListString());

            if (prmLedger.CompanyID > 0)
            {
                sb.Append(" AND tblJournal.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", prmLedger.CompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", prmLedger.CompanyID);
            }

            if (prmLedger.AccYearID > 0)
            {
                sb.Append(" AND tblJournal.AccYearID=@accYearID ");
                //cmd.Parameters.AddWithValue("@accYearID", prmLedger.AccYearID);
                cmdInfo.DBParametersInfo.Add("@accYearID", prmLedger.AccYearID);
            }

            if (prmLedger.FromDate.HasValue)
            {
                if (prmLedger.ToDate.HasValue)
                {
                    sb.Append(" AND (tblJournal.JournalDate BETWEEN @fromDate AND @toDate) ");
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@toDate", prmLedger.ToDate.Value);
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    //cmd.Parameters.AddWithValue("@toDate", prmLedger.ToDate.Value);
                }
                else
                {
                    sb.Append(" AND (tblJournal.JournalDate = @fromDate) ");
                    //cmd.Parameters.AddWithValue("@fromDate", prmLedger.FromDate.Value);
                    cmdInfo.DBParametersInfo.Add("@fromDate", prmLedger.FromDate.Value);
                }
            }


            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.Posted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", true);
                    break;
                case IncludePostEnum.Unposted:
                    sb.Append(" AND (tblJournal.IsPosted = @isPosted) ");
                    cmdInfo.DBParametersInfo.Add("@isPosted", false);
                    break;
                case IncludePostEnum.All:
                    break;
            }

            if (prmLedger.JournalTypeID > 0)
            {
                sb.Append(" AND tblJournal.JournalTypeID=@journalTypeID ");
                //cmd.Parameters.AddWithValue("@journalTypeID", prmLedger.JournalTypeID);
                cmdInfo.DBParametersInfo.Add("@journalTypeID", prmLedger.JournalTypeID);
            }


            if (prmLedger.JournalAdjustTypeID == 0)
            {
                //do nothing
            }
            else
            {
                if (prmLedger.JournalAdjustTypeID > 0)
                {
                    sb.Append(" AND (tblJournal.JournalAdjustTypeID >= @journalAdjustTypeID) ");
                    //cmd.Parameters.AddWithValue("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                    cmdInfo.DBParametersInfo.Add("@journalAdjustTypeID", prmLedger.JournalAdjustTypeID);
                }
                else
                {

                }
            }

            sb.Append(" ORDER BY tblJournal.JournalDate, tblJournal.JournalNo ");

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            cRptList = DBQuery.ExecuteDBQuery<rcJournal>(dbq, dc);


            return cRptList;
        }
    }
}
