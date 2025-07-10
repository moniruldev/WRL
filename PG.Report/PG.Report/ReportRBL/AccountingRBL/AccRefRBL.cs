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
//using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Report.ReportRBL.AccountingRBL
{
    public class AccRefRBL
    {
        public static List<rcAccRefSummary> GetAccRefSummary(clsPrmLedger prmLedger)
        {
            return GetAccRefSummary(prmLedger, null);
        }

        public static List<rcAccRefSummary> GetAccRefSummary(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcAccRefSummary> cRptList = new List<rcAccRefSummary>();

            rcAccRefSummary cRpt = new rcAccRefSummary();

            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);

            dcGLAccount glAccount = null;
            if (prmLedger.GLAccountID > 0)
            {
                glAccount = GLAccountBL.GetGLAccountByID(prmLedger.CompanyID, prmLedger.GLAccountID);
            }


            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRptHeader.NumberFormat = prmLedger.NumberFormat;

            cRptHeader.GLAccountNameDisplay = "(all)";
            if (glAccount != null)
            {
                cRptHeader.GLAccountID = glAccount.GLAccountID;
                cRptHeader.GLAccountCode = glAccount.GLAccountCode;
                cRptHeader.GLAccountName = glAccount.GLAccountName;
                cRptHeader.GLAccountTypeID = glAccount.GLAccountTypeID;

                cRptHeader.GLGroupID = glAccount.GLGroupID;
                cRptHeader.GLGroupCode = glAccount.GLGroupCode;
                cRptHeader.GLGroupName = glAccount.GLGroupName;

                cRptHeader.GLAccountNameDisplay = glAccount.GLAccountCode + ", " + glAccount.GLAccountName + ", " + glAccount.GLGroupNameShort;

            }


            ///summary
            List<rcGLReportItem> cRptItemList = new List<rcGLReportItem>();

            List<dcAccRef> accRefBalance = AccRefBL.GetAccRefBalance(prmLedger, null, dc);


            foreach (dcAccRef accRef in accRefBalance)
            {
                rcGLReportItem rptItem = new rcGLReportItem();

                FillAccRefItem(rptItem, accRef, prmLedger);
                rptItem.ItemLevel = 0;

                cRptItemList.Add(rptItem);
            }

            foreach (rcGLReportItem itm in cRptItemList)
            {
                ResetItemIndent(itm, prmLedger);
                //ResetItemBalanceDisplay(itm, prmLedger);
                //cRptItemList.Add(itm);
            }

            cRpt.AccRefSummaryHeader.Add(cRptHeader);
            cRpt.AccRefSummaryItems = cRptItemList;

            CalcSum(accRefBalance, cRptHeader);

            cRptList.Add(cRpt);

            return cRptList;

        }


        public static void CalcSum(List<dcAccRef> accRefList, rcGLReportHeader itmTotal)
        {
            //List<dcGLGroup> grpAccRoot = grpListCash.Where(c => c.HasParent == false).ToList();

            decimal openDebitAmt = 0;
            decimal openCreditAmt = 0;

            decimal tranDebitAmt = 0;
            decimal tranCreditAmt = 0;

            decimal closeDebitAmt = 0;
            decimal closeCreditAmt = 0;


            foreach (dcAccRef accRef in accRefList)
            {
                openDebitAmt += accRef.OpenDebitAmt;
                openCreditAmt += accRef.OpenCreditAmt;

                tranDebitAmt += accRef.DebitAmt;
                tranCreditAmt += accRef.CreditAmt;

                closeDebitAmt += accRef.CloseDebitAmt;
                closeCreditAmt += accRef.CloseCreditAmt;
            }


            itmTotal.OpenDebitAmt = openDebitAmt;
            itmTotal.OpenCreditAmt = openCreditAmt;
            itmTotal.OpenAmt = openDebitAmt - openCreditAmt;
            itmTotal.OpenBalanceAmt = Math.Abs(itmTotal.OpenAmt);
            itmTotal.OpenBalanceDrCr = itmTotal.OpenAmt >= 0 ? 0 : 1;
            itmTotal.OpenBalanceDrCrText = AccHelper.GetDrCrBalanceText(itmTotal.OpenAmt);
            itmTotal.OpenBalanceDisplay = Math.Abs(itmTotal.OpenAmt);



            itmTotal.TranDebitAmt = tranDebitAmt;
            itmTotal.TranCreditAmt = tranCreditAmt;
            itmTotal.TranAmt = tranDebitAmt - tranCreditAmt;
            itmTotal.TranBalanceAmt = Math.Abs(itmTotal.TranAmt);
            itmTotal.TranBalanceDrCr = itmTotal.TranAmt >= 0 ? 0 : 1;
            itmTotal.TranBalanceDrCrText = AccHelper.GetDrCrBalanceText(itmTotal.TranAmt);
            itmTotal.TranBalanceDisplay = Math.Abs(itmTotal.TranAmt);


            itmTotal.CloseDebitAmt = closeDebitAmt;
            itmTotal.CloseCreditAmt = closeCreditAmt;
            itmTotal.CloseAmt = closeDebitAmt - closeCreditAmt;
            itmTotal.CloseBalanceAmt = Math.Abs(itmTotal.CloseAmt);
            itmTotal.CloseBalanceDrCr = itmTotal.CloseAmt >= 0 ? 0 : 1;
            itmTotal.CloseBalanceDrCrText = AccHelper.GetDrCrBalanceText(itmTotal.CloseAmt);
            itmTotal.CloseBalanceDisplay = Math.Abs(itmTotal.CloseAmt);



        }


        public static void ResetItemIndent(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {
            //if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            //{
            //    //rptItem.ItemLevel = rptItem.ItemLevel > 1 ? 1 : 0;
            //    rptItem.ItemLevel = 0;
            //}

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

        public static void FillAccRefItem(rcGLReportItem item, dcAccRef accRef, clsPrmLedger prmLedger)
        {

            item.ItemType = (int)GLReportItemTypeEnum.AccRef;

            item.IsBoldName = false;
            item.IsItalicName = false;

            item.IsBoldAmt = false;
            item.IsItalicAmt = false;

            item.ItemID = accRef.AccRefID;
            item.ItemName = accRef.AccRefName;
            item.ItemCode = accRef.AccRefCode;

            item.ItemSLNo = accRef.AccRefSLNo;
            
            
            //item.ItemLevel = acc.GLAccountLevel;
            
            
            item.ItemNameParent = accRef.AccRefCategoryName;
            //item.ItemNameIndent = acc.
            //item.ItemNameFull = grp.AccGLGroupNameHierarchy;

            item.ItemNameIndent = accRef.AccRefName;

            item.ItemNameDispaly = item.ItemName;

            item.NumberFormat = prmLedger.NumberFormat;


            item.OpenDebitAmt = accRef.OpenDebitAmt;
            item.OpenCreditAmt = accRef.OpenCreditAmt;
            item.OpenAmt = accRef.OpenAmt;
            item.OpenDebitBalanceAmt = accRef.OpenDebitBalanceAmt;
            item.OpenCreditBalanceAmt = accRef.OpenCreditBalanceAmt;
            item.OpenBalanceAmt = Math.Abs(item.OpenAmt);
            //TODO Change Done
            //New 
            item.OpenAmtDateRange = accRef.OpenAmtDateRange;
            item.OpenBalanceAmtDateRange =Math.Abs(item.OpenAmtDateRange);
            //

            item.OpenBalanceDrCr = AccHelper.GetDrCrBalanceType(item.OpenAmt);
            item.OpenBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.OpenAmt);

            item.OpenBalanceDisplay = item.OpenAmt < 0 ? -Math.Abs(item.OpenAmt) : Math.Abs(item.OpenAmt);
            item.OpenBalanceText = item.OpenAmt.ToString(prmLedger.NumberFormat);


            item.TranDebitAmt = accRef.DebitAmt;
            item.TranCreditAmt = accRef.CreditAmt;
            item.TranAmt = accRef.TranAmt;
            item.TranDebitBalanceAmt = accRef.TranDebitBalanceAmt;
            item.TranCreditBalanceAmt = accRef.TranCreditBalanceAmt;

            item.TranBalanceAmt = Math.Abs(item.TranAmt);
            item.TranBalanceDrCr = item.TranAmt >= 0 ? 0 : 1;
            item.TranBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.TranAmt);
            item.TranBalanceDisplay = item.TranAmt < 0 ? -Math.Abs(item.TranAmt) : Math.Abs(item.TranAmt);




            item.CloseDebitAmt = accRef.CloseDebitAmt;
            item.CloseCreditAmt = accRef.CloseCreditAmt;
            item.CloseAmt = accRef.CloseAmt;
            item.CloseDebitBalanceAmt = accRef.CloseDebitBalanceAmt;
            item.CloseCreditBalanceAmt = accRef.CloseCreditBalanceAmt;
            item.CloseBalanceAmt = Math.Abs(item.CloseAmt);
            item.CloseBalanceDrCr = AccHelper.GetDrCrBalanceType(item.CloseAmt);
            item.CloseBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.CloseAmt);

            item.CloseBalanceText = item.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);

            item.CloseBalanceDisplay = item.CloseAmt < 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);

            //if (acc.BalanceType == (int)DebitCreditEnum.Debit)
            //{
            //    item.CloseBalanceDisplay = item.CloseAmt < 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            //}
            //else
            //{
            //    item.CloseBalanceDisplay = item.CloseAmt > 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            //}

            switch (prmLedger.ItemAmountTypeDisplay)
            {

                case ItemAmountTypeEnum.OpeningDebit:
                    item.ItemDebitAmt = item.OpenDebitAmt;
                    item.ItemCreditAmt = 0;
                    item.ItemDebitBalanceAmt = item.OpenDebitBalanceAmt;
                    item.ItemCreditBalanceAmt = 0;
                    item.ItemAmt = item.ItemDebitAmt;
                    item.ItemBalanceAmt = item.ItemAmt;
                    item.ItemBalanceDrCr = 0;
                    item.ItemBalanceDrCrText = "Dr";
                    item.ItemAmtDisplay = item.ItemAmt;
                    item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
                    break;

                case ItemAmountTypeEnum.OpeningCredit:
                    item.ItemDebitAmt = 0;
                    item.ItemCreditAmt = item.OpenCreditAmt;
                    item.ItemDebitBalanceAmt = 0;
                    item.ItemCreditBalanceAmt = item.OpenCreditBalanceAmt;
                    item.ItemAmt = item.ItemDebitAmt;
                    item.ItemBalanceAmt = item.ItemAmt;
                    item.ItemBalanceDrCr = 0;
                    item.ItemBalanceDrCrText = "Dr";
                    item.ItemAmtDisplay = item.ItemAmt;
                    item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
                    break;

                case ItemAmountTypeEnum.OpeningBalance:
                    item.ItemDebitAmt = item.OpenDebitAmt;
                    item.ItemCreditAmt = item.OpenDebitAmt;
                    item.ItemAmt = item.OpenAmt;
                    item.ItemDebitBalanceAmt = item.OpenDebitBalanceAmt;
                    item.ItemCreditBalanceAmt = item.OpenDebitBalanceAmt;
                    item.ItemBalanceAmt = item.OpenBalanceAmt;
                    item.ItemBalanceDrCr = item.OpenBalanceDrCr;
                    item.ItemBalanceDrCrText = item.OpenBalanceDrCrText;
                    item.ItemAmtDisplay = item.OpenBalanceDisplay;
                    item.ItemAmtDisplayText = item.OpenBalanceText;
                    break;



                case ItemAmountTypeEnum.TranDebit:
                    item.ItemDebitAmt = item.TranDebitAmt;
                    item.ItemCreditAmt = 0;

                    item.ItemDebitBalanceAmt = item.TranDebitBalanceAmt;
                    item.ItemCreditBalanceAmt = 0;


                    item.ItemAmt = item.ItemDebitAmt;
                    item.ItemBalanceAmt = item.ItemAmt;
                    item.ItemBalanceDrCr = 0;
                    item.ItemBalanceDrCrText = "Dr";
                    item.ItemAmtDisplay = item.ItemAmt;
                    item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
                    break;

                case ItemAmountTypeEnum.TranCredit:
                    item.ItemDebitAmt = 0;
                    item.ItemCreditAmt = item.TranCreditAmt;

                    item.ItemDebitBalanceAmt = 0;
                    item.ItemCreditBalanceAmt = item.TranCreditBalanceAmt;

                    item.ItemAmt = item.TranCreditAmt;
                    item.ItemBalanceAmt = item.ItemAmt;
                    item.ItemBalanceDrCr = 1;
                    item.ItemBalanceDrCrText = "Cr";
                    item.ItemAmtDisplay = item.ItemAmt;
                    item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
                    break;

                case ItemAmountTypeEnum.TranBalance:
                    item.ItemDebitAmt = item.TranDebitAmt;
                    item.ItemCreditAmt = item.TranCreditAmt;
                    item.ItemAmt = item.TranAmt;
                    item.ItemDebitBalanceAmt = item.TranDebitBalanceAmt;
                    item.ItemCreditBalanceAmt = item.TranDebitBalanceAmt;
                    item.ItemBalanceAmt = item.TranBalanceAmt;
                    item.ItemBalanceDrCr = item.OpenBalanceDrCr;
                    item.ItemBalanceDrCrText = item.OpenBalanceDrCrText;
                    item.ItemAmtDisplay = item.TranBalanceDisplay;
                    item.ItemAmtDisplayText = item.OpenBalanceText;
                    break;

                case ItemAmountTypeEnum.ClosingDebit:
                    item.ItemDebitAmt = item.CloseDebitAmt;
                    item.ItemCreditAmt = 0;
                    item.ItemDebitBalanceAmt = item.CloseDebitBalanceAmt;
                    item.ItemCreditBalanceAmt = 0;
                    item.ItemAmt = item.ItemDebitAmt;
                    item.ItemBalanceAmt = item.ItemAmt;
                    item.ItemBalanceDrCr = 0;
                    item.ItemBalanceDrCrText = "Dr";
                    item.ItemAmtDisplay = item.ItemAmt;
                    item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
                    break;

                case ItemAmountTypeEnum.ClosingCredit:
                    item.ItemDebitAmt = 0;
                    item.ItemCreditAmt = item.CloseCreditAmt;
                    item.ItemDebitBalanceAmt = 0;
                    item.ItemCreditBalanceAmt = item.CloseCreditBalanceAmt;
                    item.ItemAmt = item.ItemDebitAmt;
                    item.ItemBalanceAmt = item.ItemAmt;
                    item.ItemBalanceDrCr = 0;
                    item.ItemBalanceDrCrText = "Dr";
                    item.ItemAmtDisplay = item.ItemAmt;
                    item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
                    break;

                case ItemAmountTypeEnum.CheckAny:
                case ItemAmountTypeEnum.ClosingBalance:
                    item.ItemDebitAmt = item.CloseDebitAmt;
                    item.ItemCreditAmt = item.CloseCreditAmt;
                    item.ItemAmt = item.CloseAmt;
                    item.ItemDebitBalanceAmt = item.CloseDebitBalanceAmt;
                    item.ItemCreditBalanceAmt = item.CloseCreditBalanceAmt;
                    item.ItemBalanceAmt = item.CloseBalanceAmt;
                    item.ItemBalanceDrCr = item.CloseBalanceDrCr;
                    item.ItemBalanceDrCrText = item.CloseBalanceDrCrText;
                    item.ItemAmtDisplay = item.CloseBalanceDisplay;
                    item.ItemAmtDisplayText = item.CloseBalanceText;
                    break;
            }


        }

        public static List<rcAccRefDetails> GetAccRefDetails(clsPrmLedger prmLedger)
        {
            return GetAccRefDetails(prmLedger, null);
        }

        public static List<rcAccRefDetails> GetAccRefDetails(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcAccRefDetails> cRptList = new List<rcAccRefDetails>();

            rcAccRefDetails cRpt = new rcAccRefDetails();


            rcLedger cRptLedger = new rcLedger();

            dcAccYear accYear = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID, dc);
            dcAccRef accRef = AccRefBL.GetAccRefByID(prmLedger.CompanyID, prmLedger.AccRefID, dc);
            dcGLAccount glAccount = null;
            if (prmLedger.GLAccountID > 0)
            {
                glAccount = GLAccountBL.GetGLAccountByID(prmLedger.CompanyID, prmLedger.GLAccountID);
            }


            cRptLedger.AccRefID = accRef.AccRefID;
            cRptLedger.AccRefCode = accRef.AccRefCode;
            cRptLedger.AccRefName = accRef.AccRefName;
            cRptLedger.AccRefNameShort = accRef.AccRefNameShort;

            cRptLedger.AccRefCategoryID = accRef.AccRefCategoryID;
            cRptLedger.AccRefCategoryCode = accRef.AccRefCategoryCode;
            cRptLedger.AccRefCategoryName = accRef.AccRefCategoryName;

            cRptLedger.AccRefTypeID = accRef.AccRefTypeID;

            cRptLedger.NumberFormat = prmLedger.NumberFormat;

            cRptLedger.GLAccountNameDisplay = "(all)";
            if (glAccount != null   )
            {
                cRptLedger.GLAccountID = glAccount.GLAccountID;
                cRptLedger.GLAccountCode = glAccount.GLAccountCode;
                cRptLedger.GLAccountName= glAccount.GLAccountName;
                cRptLedger.GLAccountTypeID= glAccount.GLAccountTypeID;

                cRptLedger.GLGroupID= glAccount.GLGroupID;
                cRptLedger.GLGroupCode= glAccount.GLGroupCode;
                cRptLedger.GLGroupName= glAccount.GLGroupName;

                cRptLedger.GLAccountNameDisplay= glAccount.GLAccountCode +    ", " + glAccount.GLAccountName + ", "  + glAccount.GLGroupNameShort;

            }



            List<dcJournalDetRef> transListBfDate = new List<dcJournalDetRef>();
            List<dcJournalDetRef> transListDate = new List<dcJournalDetRef>();
            
            decimal openDebitAmtYear = 0;
            decimal openCreditAmtYear = 0;
            decimal openDebitAmtDateRange = 0;
            decimal openCreditAmtDateRange = 0;

            decimal hisBalAmt = 0;
            decimal prevDrAmt = 0;
            decimal prevCrAmt = 0;

            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALL
                    || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeALLIndvidual
                    || prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.IncludeYear)
            {
                dcGLAccountHistoryRef accHistRef = null;
                accHistRef = GLAccountHistoryRefBL.GetGLAccountHistoryRefSumByAccRefID(
                                                prmLedger.CompanyID, prmLedger.AccYearID
                                                , (int)prmLedger.AccRefType , prmLedger.AccRefCategoryID
                                                , prmLedger.AccRefID ,prmLedger.GLAccountID, dc);

                if (accHistRef != null)
                {
                    openDebitAmtYear = accHistRef.DebitAmtOpen;
                    openCreditAmtYear = accHistRef.CreditAmtOpen;
                    hisBalAmt = accHistRef.DebitAmtOpen - accHistRef.CreditAmtOpen;
                }
            }

            clsPrmLedger prmLdgBfDate = (clsPrmLedger)prmLedger.Clone();
            prmLdgBfDate.IsBeforeDate = true;

            transListDate = JournalDetRefBL.GetJournalDetRefByDate(prmLedger, dc);
            transListBfDate = JournalDetRefBL.GetJournalDetRefSumByDate(prmLdgBfDate, dc);


            if (transListBfDate.Count > 0)
            {
                dcJournalDetRef tranBfDate = transListBfDate.FirstOrDefault(c => c.AccRefID == prmLedger.AccRefID);

                if (tranBfDate != null)
                {
                    prevDrAmt = tranBfDate.DebitAmt;
                    prevCrAmt = tranBfDate.CreditAmt;
                }
            }


            decimal opBalance = hisBalAmt + prevDrAmt - prevCrAmt;
            int DrCrOpen = opBalance >= 0 ? (int)DebitCreditEnum.Debit : (int)DebitCreditEnum.Credit;
            string DrCrString = opBalance >= 0 ? "Dr" : "Cr";
            DrCrString = opBalance == 0 ? "" : DrCrString;


            //New

            openDebitAmtDateRange = prevDrAmt;
            openCreditAmtDateRange = prevCrAmt;

            List<rcLedgerTrans> transList = new List<rcLedgerTrans>();

            int slNo = 0;
            //int detGroupID = 0;

            decimal openBalanceAmtYear = openDebitAmtYear - openCreditAmtYear;
            decimal openBalanceAmtDateRange = openDebitAmtDateRange - openCreditAmtDateRange;

            decimal openDebitAmt = openDebitAmtYear + openDebitAmtDateRange;
            decimal openCreditAmt = openCreditAmtYear + openCreditAmtDateRange;

            decimal openBalanceAmt = openDebitAmt - openCreditAmt;


            ///transactions
            decimal curBalance = 0;
            decimal totDebitAmt = 0;
            decimal totCreditAmt = 0;

            decimal debitCreditBal = 0;

            int curDrCr = 0;
            string curDrCrString = "Dr";

            switch (prmLedger.IncludeOpBalanceType)
            {
                case InculdeOpBalanceEnum.None:
                    curBalance = openBalanceAmt;
                    break;

                case InculdeOpBalanceEnum.IncludeALL:
                    slNo++;
                    rcLedgerTrans openTranAll = new rcLedgerTrans();
                    openTranAll.GLAccountID = prmLedger.GLAccountID;
                    openTranAll.TranDate = prmLedger.FromDate;
                    openTranAll.TranSLNo = slNo;
                    openTranAll.TranDesc = "Openning Balance";

                    openTranAll.DebitAmt = openDebitAmt;
                    openTranAll.CreditAmt = openCreditAmt;

                    debitCreditBal = openDebitAmt - openCreditAmt;
                    openTranAll.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranAll.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranAll.DebitAmtDisplay = openTranAll.DebitBalanceAmt;
                    openTranAll.CreditAmtDisplay = openTranAll.CreditBalanceAmt;


                    openTranAll.BalanceAmt = Math.Abs(openBalanceAmt);
                    openTranAll.BalanceAmtDisplay = Math.Abs(openTranAll.BalanceAmt);
                    openTranAll.DrCrBalance = openBalanceAmt >= 0 ? 0 : 1;
                    if (openBalanceAmt != 0)
                    {
                        openTranAll.DrCrText = openBalanceAmt > 0 ? "Dr" : "Cr";
                    }

                    openTranAll.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranAll);

                    curBalance = openBalanceAmt;

                    break;
                case InculdeOpBalanceEnum.IncludeYear:
                    slNo++;
                    rcLedgerTrans openTranYr = new rcLedgerTrans();
                    openTranYr.GLAccountID = prmLedger.GLAccountID;
                    openTranYr.TranDate = accYear.YearStartDate;
                    openTranYr.TranSLNo = slNo;
                    openTranYr.TranDesc = "Openning Balance";

                    openTranYr.DebitAmt = openDebitAmtYear;
                    openTranYr.CreditAmt = openCreditAmtYear;

                    debitCreditBal = openDebitAmtYear - openDebitAmtYear;
                    openTranYr.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranYr.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranYr.DebitAmtDisplay = openTranYr.DebitBalanceAmt;
                    openTranYr.CreditAmtDisplay = openTranYr.CreditBalanceAmt;


                    openTranYr.BalanceAmt = Math.Abs(openBalanceAmtYear);
                    openTranYr.BalanceAmtDisplay = openTranYr.BalanceAmt;



                    openTranYr.DrCrBalance = openBalanceAmtYear >= 0 ? 0 : 1;
                    if (openBalanceAmtYear != 0)
                    {
                        openTranYr.DrCrText = openBalanceAmtYear > 0 ? "Dr" : "Cr";
                    }
                    openTranYr.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranYr);

                    curBalance = openBalanceAmtYear;
                    break;
                case InculdeOpBalanceEnum.IncludeDateRange:
                    slNo++;
                    rcLedgerTrans openTranDr = new rcLedgerTrans();
                    openTranDr.GLAccountID = prmLedger.GLAccountID;
                    openTranDr.TranDate = prmLedger.FromDate;
                    openTranDr.TranSLNo = slNo;
                    openTranDr.TranDesc = "Openning Balance - Date Range";
                    openTranDr.DebitAmt = openDebitAmtDateRange;
                    openTranDr.CreditAmt = openCreditAmtDateRange;


                    debitCreditBal = openDebitAmtDateRange - openCreditAmtDateRange;
                    openTranDr.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranDr.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranDr.DebitAmtDisplay = openTranDr.DebitBalanceAmt;
                    openTranDr.CreditAmtDisplay = openTranDr.CreditBalanceAmt;


                    openTranDr.BalanceAmt = Math.Abs(openBalanceAmtDateRange);
                    openTranDr.BalanceAmtDisplay = openTranDr.BalanceAmt;


                    openTranDr.DrCrBalance = openBalanceAmtDateRange >= 0 ? 0 : 1;
                    if (openBalanceAmtDateRange != 0)
                    {
                        openTranDr.DrCrText = openBalanceAmtDateRange > 0 ? "Dr" : "Cr";
                    }
                    openTranDr.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranDr);
                    curBalance = openBalanceAmtDateRange;


                    break;
                case InculdeOpBalanceEnum.IncludeALLIndvidual:
                    slNo++;
                    rcLedgerTrans openTranYr1 = new rcLedgerTrans();
                    openTranYr1.GLAccountID = prmLedger.GLAccountID;
                    openTranYr1.TranDate = accYear.YearStartDate;
                    openTranYr1.TranSLNo = slNo;
                    openTranYr1.TranDesc = "Openning Balance - Year";

                    openTranYr1.DebitAmt = openDebitAmtYear;
                    openTranYr1.CreditAmt = openCreditAmtYear;


                    debitCreditBal = openDebitAmtYear - openDebitAmtYear;
                    openTranYr1.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranYr1.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranYr1.DebitAmtDisplay = openTranYr1.DebitBalanceAmt;
                    openTranYr1.CreditAmtDisplay = openTranYr1.CreditBalanceAmt;



                    openTranYr1.BalanceAmt = Math.Abs(openBalanceAmtYear);

                    openTranYr1.BalanceAmtDisplay = openTranYr1.BalanceAmt;

                    openTranYr1.DrCrBalance = openBalanceAmtYear >= 0 ? 0 : 1;
                    if (openBalanceAmtYear != 0)
                    {
                        openTranYr1.DrCrText = openBalanceAmtYear > 0 ? "Dr" : "Cr";
                    }
                    openTranYr1.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranYr1);

                    curBalance = openBalanceAmtYear;


                    slNo++;
                    rcLedgerTrans openTranDr1 = new rcLedgerTrans();
                    openTranDr1.GLAccountID = prmLedger.GLAccountID;
                    openTranDr1.TranDate = prmLedger.FromDate;
                    openTranDr1.TranSLNo = slNo;
                    openTranDr1.TranDesc = "Openning Balance - Date Range";
                    openTranDr1.DebitAmt = openDebitAmtDateRange;
                    openTranDr1.CreditAmt = openCreditAmtDateRange;

                    debitCreditBal = openDebitAmtDateRange - openCreditAmtDateRange;
                    openTranDr1.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                    openTranDr1.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                    openTranDr1.DebitAmtDisplay = openTranDr1.DebitBalanceAmt;
                    openTranDr1.CreditAmtDisplay = openTranDr1.CreditBalanceAmt;



                    openTranDr1.BalanceAmt = Math.Abs(openBalanceAmt);
                    openTranDr1.BalanceAmtDisplay = openTranDr1.BalanceAmt;


                    openTranDr1.DrCrBalance = openBalanceAmt >= 0 ? 0 : 1;
                    if (openBalanceAmt != 0)
                    {
                        openTranDr1.DrCrText = openBalanceAmt > 0 ? "Dr" : "Cr";
                    }
                    openTranDr1.NumberFormat = prmLedger.NumberFormat;
                    transList.Add(openTranDr1);
                    curBalance = openBalanceAmt;

                    break;
            }

            foreach (dcJournalDetRef transDate in transListDate)
            {
                slNo++;
                transDate.JournalDetRefSLNo = slNo;

                //if (acc.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                //{
                //    detGroupID--;
                //    transDate.JournalDetID = detGroupID;
                //}

                rcLedgerTrans cLTran = new rcLedgerTrans();
                Helper.CopyPropertyValueByName(transDate, cLTran);
                cLTran.NumberFormat = prmLedger.NumberFormat;

                //cLTran.GLAccountCode = transDate.GLAccountCode;


                cLTran.DetGroupID = transDate.JournalDetID;
                cLTran.TranSLNo = transDate.JournalDetRefSLNo;
                cLTran.TranDate = transDate.JournalDate;

                if (prmLedger.GLAccountID > 0)
                {
                    cLTran.TranDesc = transDate.JournalDetDesc;
                    cLTran.GLAccountNameDisplay = glAccount.GLAccountCode + ", " + glAccount.GLAccountName + ", " + glAccount.GLGroupNameShort;

                }
                else
                {
                    cLTran.TranDesc = transDate.GLAccountCode + ", " + transDate.GLAccountName + ", " + transDate.GLGroupNameShort;
                    if (transDate.JournalDetDesc != string.Empty)
                    {
                        cLTran.TranDesc += "\n\r" + transDate.JournalDetDesc;
                    }
                }

                cLTran.DebitAmt = transDate.DebitAmt;
                cLTran.CreditAmt = transDate.CreditAmt;

                debitCreditBal = cLTran.DebitAmt - cLTran.CreditAmt;
                cLTran.DebitBalanceAmt = debitCreditBal > 0 ? Math.Abs(debitCreditBal) : 0;
                cLTran.CreditBalanceAmt = debitCreditBal < 0 ? Math.Abs(debitCreditBal) : 0;

                //cLTran.DebitAmtDisplay = cLTran.DebitBalanceAmt;
                //cLTran.CreditAmtDisplay = cLTran.CreditBalanceAmt;

                cLTran.DebitAmtDisplay = cLTran.DebitAmt;
                cLTran.CreditAmtDisplay = cLTran.CreditAmt;


                curBalance += cLTran.DebitAmt - cLTran.CreditAmt;
                curDrCr = curBalance >= 0 ? (int)DebitCreditEnum.Debit : (int)DebitCreditEnum.Credit;
                curDrCrString = curBalance >= 0 ? "Dr" : "Cr";
                curDrCrString = curBalance == 0 ? "" : curDrCrString;

                cLTran.RunningTotalAmt = curBalance;

                cLTran.BalanceAmt = Math.Abs(curBalance);
                cLTran.BalanceAmtDisplay = cLTran.BalanceAmt;

                cLTran.DrCrBalance = curDrCr;
                cLTran.DrCrText = curDrCrString;

                //AddTranItem(cLTran, transDate, cInsList, cRefList, cDetListSub, cDetListContra, prmLedger, transList);


                totDebitAmt += transDate.DebitAmt;
                totCreditAmt += transDate.CreditAmt;

                transList.Add(cLTran);
            }


            //cRpt.GLAccountCode = acc.GLAccountCode;
            //cRpt.GLAccountID = acc.GLAccountID;
            //cRpt.GLAccountName = acc.GLAccountName;
            //cRpt.GLGroupName = group.GLGroupName;

            //cRpt.AccYearName = accYear.AccYearName;

            cRptLedger.TotDebitAmt = totDebitAmt;
            cRptLedger.TotCreditAmt = totCreditAmt;

            cRptLedger.BalanceAmt = Math.Abs(curBalance);
            cRptLedger.DrCrBalance = curDrCr;
            cRptLedger.DrCrText = curDrCrString;

            cRptLedger.LedgerTrans = transList;

            cRptLedger.DateString = prmLedger.FromDate.Value.ToString("dd-MMM-yyyy") + " To " + prmLedger.ToDate.Value.ToString("dd-MMM-yyyy");


            cRpt.AccRefDetailsHeader.Add(cRptLedger);
            cRpt.AccRefDetailsItems = transList;

            cRptList.Add(cRpt);


            return cRptList;

        }




    }
}
