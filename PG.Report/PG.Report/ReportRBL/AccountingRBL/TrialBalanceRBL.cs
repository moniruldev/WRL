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
    public class TrialBalanceRBL
    {

        public static List<rcGLReportItem> GetTrialBalanceItems(clsPrmLedger prmLedger, rcGLReportHeader cRptHeader)
        {
            return GetTrialBalanceItems(prmLedger, null);
        }

        public static List<rcGLReportItem> GetTrialBalanceItems(clsPrmLedger prmLedger, rcGLReportHeader cRptHeader, DBContext dc)
        {

            List<rcGLReportItem> cRptList = new List<rcGLReportItem>();

            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(prmLedger.CompanyID, dc);
            grpList = GLGroupBL.FormatGLGroup(prmLedger, grpList);
            
            List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLedger, dc);
            List<dcGLGroup> grpBalance = GLGroupBL.GetGroupBalance(prmLedger, grpList, accBalance,dc);
            
            List<dcGLGroup> grpRootList = grpList.Where(c => c.GLGroupIDParent == 0).ToList();

            int assetClassID = (int)GLClassEnum.Assets;
            int liabilityClassID = (int)GLClassEnum.Liabilities;

            int incomeClassID = (int)GLClassEnum.Income;
            int expenseClassID = (int)GLClassEnum.Expense;
            int PLClassID = (int)GLClassEnum.ProfitAndLoss;


            decimal totOpenAmtDebit = 0;
            decimal totOpenAmtCredit = 0;

            decimal totOpenAmtDebitBalance = 0;
            decimal totOpenAmtCreditBalance = 0;

            decimal totTranAmtDebit = 0;
            decimal totTranAmtCredit = 0;

            decimal totTranAmtDebitBalance = 0;
            decimal totTranAmtCreditBalance = 0;

            decimal totCloseAmtDebit = 0;
            decimal totCloseAmtCredit = 0;


            decimal totCloseAmtDebitBalance = 0;
            decimal totCloseAmtCreditBalance = 0;
           

            //sum data
            foreach (dcGLGroup grpRoot in grpRootList)
            {
                totOpenAmtDebit += grpRoot.OpenDebitAmt;
                totOpenAmtCredit += grpRoot.OpenCreditAmt;

                totOpenAmtDebitBalance += grpRoot.OpenDebitBalanceAmt;
                totOpenAmtCreditBalance += grpRoot.OpenCreditBalanceAmt;

                totTranAmtDebit += grpRoot.DebitAmt;
                totTranAmtCredit += grpRoot.CreditAmt;

                totTranAmtDebitBalance += grpRoot.TranDebitBalanceAmt;
                totTranAmtCreditBalance += grpRoot.TranCreditBalanceAmt;

                totCloseAmtDebit += grpRoot.CloseDebitAmt;
                totCloseAmtCredit += grpRoot.CloseCreditAmt;

                totCloseAmtDebitBalance += grpRoot.CloseDebitBalanceAmt;
                totCloseAmtCreditBalance += grpRoot.CloseCreditBalanceAmt;


            }

            //totCloseAmtDebitBalance = accBalance.Sum(c => c.CloseDebitBalanceAmt);
            //totCloseAmtCreditBalance = accBalance.Sum(c => c.CloseCreditBalanceAmt);



            decimal totAsset = grpBalance.Where(c => c.GLClassID == assetClassID & c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);
            decimal totLiablity = grpBalance.Where(c => c.GLClassID == liabilityClassID & c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);

            decimal totIncome = grpBalance.Where(c => c.GLClassID == incomeClassID & c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);
            decimal totExpense = grpBalance.Where(c => c.GLClassID == expenseClassID & c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);



            //PG.Report.AccountingRBL.GLReportBL.AddGLReportItems

            List<rcGLReportItem> cRptListItems = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListAst = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListLbl = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListInc = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListExp = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListPL= new List<rcGLReportItem>();



            int defLevel = 0;
            if (prmLedger.IncludeGLClass)
            {
                defLevel = 1;
                if (prmLedger.MaxHierarchyLevel != -1)
                {
                    prmLedger.MaxHierarchyLevel += 1;
                }
            }

            rcGLReportItem itmAssetsHead = new rcGLReportItem();
            itmAssetsHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmAssetsHead.IsBoldName = true;
            itmAssetsHead.IsBoldAmt = true;
            itmAssetsHead.ItemName = "Assets";
            itmAssetsHead.ItemNameDispaly = itmAssetsHead.ItemName;
            itmAssetsHead.NumberFormat = prmLedger.NumberFormat;
            itmAssetsHead.IsUnderlinedName = false;

            List<dcGLGroup> grpAssetsRoot = grpList.Where(c => c.GLClassID == assetClassID && c.GLGroupIDParent == 0).ToList();
            GLReportRBL.AddGLReportItems(grpAssetsRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListAst);

            rcGLReportItem itmLiabilityHead = new rcGLReportItem();
            itmLiabilityHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmLiabilityHead.IsBoldName = true;
            itmLiabilityHead.IsBoldAmt = true;
            itmLiabilityHead.ItemName = "Liabilities";
            itmLiabilityHead.ItemNameDispaly = itmLiabilityHead.ItemName;
            itmLiabilityHead.NumberFormat = prmLedger.NumberFormat;
            itmLiabilityHead.IsUnderlinedName = false;

            List<dcGLGroup> grpLiabilityRoot = grpList.Where(c => c.GLClassID == liabilityClassID && c.GLGroupIDParent == 0).ToList();
            GLReportRBL.AddGLReportItems(grpLiabilityRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListLbl);

            rcGLReportItem itmIncomeHead = new rcGLReportItem();
            itmIncomeHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmIncomeHead.IsBoldName = true;
            itmIncomeHead.IsBoldAmt = true;
            itmIncomeHead.ItemName = "Income";
            itmIncomeHead.ItemNameDispaly = itmIncomeHead.ItemName;
            itmIncomeHead.NumberFormat = prmLedger.NumberFormat;
            itmIncomeHead.IsUnderlinedName = false;

            List<dcGLGroup> grpIncomeRoot = grpList.Where(c => c.GLClassID == incomeClassID && c.GLGroupIDParent == 0).ToList();
            GLReportRBL.AddGLReportItems(grpIncomeRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListInc);

            rcGLReportItem itmExpenseHead = new rcGLReportItem();
            itmExpenseHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmExpenseHead.IsBoldName = true;
            itmExpenseHead.IsBoldAmt = true;
            itmExpenseHead.ItemName = "Expense";
            itmExpenseHead.ItemNameDispaly = itmExpenseHead.ItemName;
            itmExpenseHead.NumberFormat = prmLedger.NumberFormat;
            itmExpenseHead.IsUnderlinedName = false;

            List<dcGLGroup> grpExpenseRoot = grpList.Where(c => c.GLClassID == expenseClassID && c.GLGroupIDParent == 0).ToList();
            GLReportRBL.AddGLReportItems(grpExpenseRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListExp);


            rcGLReportItem itmPLHead = new rcGLReportItem();
            itmPLHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmPLHead.IsBoldName = true;
            itmPLHead.IsBoldAmt = true;
            itmPLHead.ItemName = "Profit & Loss";
            itmPLHead.ItemNameDispaly = itmPLHead.ItemName;
            itmPLHead.NumberFormat = prmLedger.NumberFormat;
            itmPLHead.IsUnderlinedName = false;

            List<dcGLGroup> grpPLRoot = grpList.Where(c => c.GLClassID == PLClassID && c.GLGroupIDParent == 0).ToList();
            GLReportRBL.AddGLReportItems(grpPLRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListPL);



            //assets
            if (prmLedger.IncludeGLClass)
            {
                cRptList.Add(itmAssetsHead);
            }
            foreach (rcGLReportItem itmRpt in cRptListAst)
            {
                ResetItemIndent(itmRpt, prmLedger);
                ResetItemBalanceDisplay(itmRpt, prmLedger);
                //if (prmLedger.ShowPercentage)
                //{
                //    SetPercentage(itmLbl, totLiablity);
                //}
                cRptList.Add(itmRpt);
            }

            if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }



            ///liabilie

            if (prmLedger.IncludeGLClass)
            {
                cRptList.Add(itmLiabilityHead);
            }
            foreach (rcGLReportItem itmRpt in cRptListLbl)
            {
                ResetItemIndent(itmRpt, prmLedger);
                ResetItemBalanceDisplay(itmRpt, prmLedger);
                //if (prmLedger.ShowPercentage)
                //{
                //    SetPercentage(itmLbl, totLiablity);
                //}
                cRptList.Add(itmRpt);
            }

            if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }


            //income
            if (prmLedger.IncludeGLClass)
            {
                cRptList.Add(itmIncomeHead);
            }
            foreach (rcGLReportItem itmRpt in cRptListInc)
            {
                ResetItemIndent(itmRpt, prmLedger);
                ResetItemBalanceDisplay(itmRpt, prmLedger);
                //if (prmLedger.ShowPercentage)
                //{
                //    SetPercentage(itmLbl, totLiablity);
                //}
                cRptList.Add(itmRpt);
            }

            if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }


            //income
            if (prmLedger.IncludeGLClass)
            {
                cRptList.Add(itmExpenseHead);
            }
            foreach (rcGLReportItem itmRpt in cRptListExp)
            {
                ResetItemIndent(itmRpt, prmLedger);
                ResetItemBalanceDisplay(itmRpt, prmLedger);
                //if (prmLedger.ShowPercentage)
                //{
                //    SetPercentage(itmLbl, totLiablity);
                //}
                cRptList.Add(itmRpt);
            }

            if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }

            //pl
            if (prmLedger.IncludeGLClass)
            {
                cRptList.Add(itmPLHead);
            }
            foreach (rcGLReportItem itmRpt in cRptListPL)
            {
                ResetItemIndent(itmRpt, prmLedger);
                ResetItemBalanceDisplay(itmRpt, prmLedger);
                //if (prmLedger.ShowPercentage)
                //{
                //    SetPercentage(itmLbl, totLiablity);
                //}
                cRptList.Add(itmRpt);
            }

            if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }

            ///op bal dif
            //opAmt Diff
            if (totOpenAmtCredit != totOpenAmtDebit)
            {
                decimal diffAmt = totOpenAmtDebit - totOpenAmtCredit;

                rcGLReportItem itmOpDiff = new rcGLReportItem();
                itmOpDiff.ItemType = (int)GLReportItemTypeEnum.System;
                itmOpDiff.ItemName = "Difference in Openning Balances";
                itmOpDiff.ItemNameDispaly = itmOpDiff.ItemName;
                itmOpDiff.NumberFormat = prmLedger.NumberFormat;

                itmOpDiff.IsBoldAmt = true;

                if (diffAmt > 0)
                {
                    itmOpDiff.OpenCreditAmt = Math.Abs(diffAmt);
                    itmOpDiff.OpenDebitAmt = 0;
                    itmOpDiff.OpenAmt = -diffAmt;
                    itmOpDiff.OpenDebitBalanceAmt = 0;
                    itmOpDiff.OpenCreditBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.OpenBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.OpenBalanceDisplay = itmOpDiff.OpenBalanceAmt;
                    itmOpDiff.OpenBalanceDrCr = (int)DebitCreditEnum.Credit;
                    itmOpDiff.OpenBalanceDrCrText = "Cr";

                    itmOpDiff.CloseCreditAmt = Math.Abs(diffAmt);
                    itmOpDiff.CloseDebitAmt = 0;
                    itmOpDiff.CloseAmt = -diffAmt;

                    itmOpDiff.CloseDebitBalanceAmt = 0;
                    itmOpDiff.CloseCreditBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.CloseBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.CloseBalanceDisplay = itmOpDiff.OpenBalanceAmt;
                    itmOpDiff.CloseBalanceDrCr = (int)DebitCreditEnum.Credit;
                    itmOpDiff.CloseBalanceDrCrText = "Cr";

                    totOpenAmtCredit += Math.Abs(diffAmt);
                    totCloseAmtCredit += Math.Abs(diffAmt);

                    totCloseAmtCreditBalance += Math.Abs(diffAmt);
                }
                else
                {
                    itmOpDiff.OpenCreditAmt = 0;
                    itmOpDiff.OpenDebitAmt = Math.Abs(diffAmt); ;
                    itmOpDiff.OpenAmt = Math.Abs(diffAmt);
                    itmOpDiff.OpenDebitBalanceAmt = 0;
                    itmOpDiff.OpenCreditBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.OpenBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.OpenBalanceDisplay = itmOpDiff.OpenBalanceAmt;
                    itmOpDiff.OpenBalanceDrCr = (int)DebitCreditEnum.Debit;
                    itmOpDiff.OpenBalanceDrCrText = "Dr";

                    itmOpDiff.CloseCreditAmt = 0;
                    itmOpDiff.CloseDebitAmt = Math.Abs(diffAmt); ;
                    itmOpDiff.CloseAmt = Math.Abs(diffAmt);

                    itmOpDiff.CloseDebitBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.CloseCreditBalanceAmt = 0;

                    itmOpDiff.CloseBalanceAmt = Math.Abs(diffAmt);
                    itmOpDiff.CloseBalanceDisplay = itmOpDiff.OpenBalanceAmt;
                    itmOpDiff.CloseBalanceDrCr = (int)DebitCreditEnum.Debit;
                    itmOpDiff.CloseBalanceDrCrText = "Dr";

                    totOpenAmtDebit += Math.Abs(diffAmt);
                    totCloseAmtDebit += Math.Abs(diffAmt);

                    totCloseAmtDebitBalance += Math.Abs(diffAmt);

                }
                cRptList.Add(itmOpDiff);
            }

            decimal totOpenAmt = totOpenAmtDebit - totOpenAmtCredit;
            decimal totTranAmt = totTranAmtDebit - totTranAmtCredit;
            decimal totCloseAmt = totCloseAmtDebit - totCloseAmtCredit;


            cRptHeader.OpenDebitAmt = totOpenAmtDebit;
            cRptHeader.OpenCreditAmt = totOpenAmtCredit;
            cRptHeader.OpenAmt = totOpenAmt;
            cRptHeader.OpenDebitBalanceAmt = totOpenAmtDebitBalance;
            cRptHeader.OpenCreditBalanceAmt = totOpenAmtCreditBalance;
            cRptHeader.OpenBalanceAmt = Math.Abs(cRptHeader.OpenAmt);
            cRptHeader.OpenBalanceDrCr = cRptHeader.OpenAmt > 0 ? 0 : 1;
            cRptHeader.OpenBalanceDrCrText = cRptHeader.OpenAmt > 0 ? "Dr" : "Cr";


            cRptHeader.OpenBalanceAmt = Math.Abs(cRptHeader.OpenAmt);

            cRptHeader.TranDebitAmt = totTranAmtDebit;
            cRptHeader.TranCreditAmt = totTranAmtCredit;
            cRptHeader.TranAmt = totTranAmt;
            cRptHeader.TranDebitBalanceAmt = totTranAmtDebitBalance;
            cRptHeader.TranCreditBalanceAmt = totTranAmtCreditBalance;

            cRptHeader.TranBalanceAmt = Math.Abs(cRptHeader.TranAmt);
            cRptHeader.TranBalanceDrCr = cRptHeader.TranAmt > 0 ? 0 : 1;
            cRptHeader.TranBalanceDrCrText = cRptHeader.TranAmt > 0 ? "Dr" : "Cr";

            cRptHeader.CloseDebitAmt = totCloseAmtDebit;
            cRptHeader.CloseCreditAmt = totCloseAmtCredit;
            cRptHeader.CloseAmt = totCloseAmt;
            cRptHeader.CloseDebitBalanceAmt = totCloseAmtDebitBalance;
            cRptHeader.CloseCreditBalanceAmt = totCloseAmtCreditBalance;
            cRptHeader.CloseBalanceAmt = Math.Abs(cRptHeader.CloseAmt);
            cRptHeader.CloseBalanceDrCr = cRptHeader.CloseAmt > 0 ? 0 : 1;
            cRptHeader.CloseBalanceDrCrText = cRptHeader.CloseAmt > 0 ? "Dr" : "Cr";

            return cRptList;
        }


        public static void ResetItemIndent(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {
            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            {
                if (prmLedger.IncludeGLClass)
                {
                    rptItem.ItemLevel = rptItem.ItemLevel > 1 ? 1 : 0;
                }
                else
                {
                    rptItem.ItemLevel =  0;
                }
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


        public static void ResetItemBalanceDisplay(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {


            if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {

                if (rptItem.ItemType != (int)GLReportItemTypeEnum.System
                         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLItem
                         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLClosing)
                {

                    if (rptItem.CloseBalanceDisplay != 0)
                    {
                        if (rptItem.ItemLevel == 0)
                        {
                            rptItem.IsBoldAmt = true;
                        }

                        if (rptItem.ItemLevel == 0)
                        {
                            rptItem.IsBoldAmt = true;
                        }

                        if (rptItem.ItemLevel == 1)
                        {
                            rptItem.IsBoldAmt = false;
                        }

                        if (rptItem.ItemLevel > 1)
                        {
                            //rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
                            //rptItem.CloseBalanceDisplay = 0;

                            if (rptItem.ChildItemCount > 0)
                            {
                                rptItem.IsBoldAmt = false;
                                rptItem.IsUnderlinedAmt = false;
                            }
                            else
                            {
                                rptItem.IsBoldAmt = false;
                                rptItem.IsUnderlinedAmt = false;
                            }

                        }

                        //if (rptItem.ItemLevel > 2)
                        //{
                        //    rptItem.IsBoldAmt = false;
                        //    rptItem.IsUnderlinedAmt = false;
                        //    rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
                        //    rptItem.CloseBalanceDisplay = 0;
                        //}
                    }


                }

            }




            //if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    if (rptItem.CloseBalanceDisplay == 0 | rptItem.ChildItemCount == 0)
            //    {
            //        return;
            //    }


            //    if (rptItem.ItemType != (int)GLReportItemTypeEnum.System
            //         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLItem
            //         & rptItem.ItemType != (int)GLReportItemTypeEnum.PLClosing)
            //    {

            //        if (prmLedger.DisplayBalanceLevel == -1)
            //        {
            //            if (rptItem.ChildItemCount > 0)
            //            {
            //                rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
            //                rptItem.CloseBalanceDisplay = 0;
            //            }
            //        }
            //        else
            //        {
            //            if (prmLedger.DisplayBalanceLevel != rptItem.ItemLevel)
            //            {
            //                rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
            //                rptItem.CloseBalanceDisplay = 0;
            //            }
            //        }

            //    }

            //}

        }


        public static List<rcTrialBalance> GetTrialBalance(clsPrmLedger prmLedger)
        {
            return GetTrialBalance(prmLedger, null);
        }

        public static List<rcTrialBalance> GetTrialBalance(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcTrialBalance> cRptList = new List<rcTrialBalance>();

            rcTrialBalance cRpt = new rcTrialBalance();

            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);

            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRpt.TrialBalanceHeader.Add(cRptHeader);
            cRpt.TrialBalanceItems = GetTrialBalanceItems(prmLedger, cRptHeader, dc);

            cRptHeader.NumberFormat = prmLedger.NumberFormat;

            cRptList.Add(cRpt);

            return cRptList;

        }

    }
}
