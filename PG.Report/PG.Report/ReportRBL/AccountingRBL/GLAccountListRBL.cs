using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PG.Core.DBBase;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.Report.ReportClass.AccountingRC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;
using PG.Report.ReportEnums;
using System.Collections;

namespace PG.Report.ReportRBL.AccountingRBL
{
    public class GLAccountListRBL
    {
        public static List<rcGLReportItem> GetGLAccountList(clsPrmLedger prmLedger)
        {
            return GetGLAccountList(prmLedger, null);
        }

        public static List<rcGLReportItem> GetGLAccountList(clsPrmLedger prmLedger, DBContext dc)
        {

            List<rcGLReportItem> cRptList = new List<rcGLReportItem>();
            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(prmLedger.CompanyID,  dc);
            grpList = GLGroupBL.FormatGLGroup(prmLedger, grpList);
            List<dcGLAccount> accList = GLAccountBL.GetGLAccountList(dc);
            //new
            //List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLedger, dc);
            //List<dcGLGroup> grpBalance = GLGroupBL.GetGroupBalance(prmLedger, grpList, accBalance, dc);

           
            List<dcGLGroup> grpRootList = grpList.Where(c => c.GLGroupIDParent == 0).ToList();

            int assetClassID = (int)GLClassEnum.Assets;
            int liabilityClassID = (int)GLClassEnum.Liabilities;

            int incomeClassID = (int)GLClassEnum.Income;
            int expenseClassID = (int)GLClassEnum.Expense;
            int PLClassID = (int)GLClassEnum.ProfitAndLoss;

            prmLedger.IncludeGLClass = true;
            prmLedger.IncludeRootGLGroup = true;
            prmLedger.MaxHierarchyLevel = -1;
            prmLedger.IncludeZeroValue = true;

            //PG.Report.AccountingRBL.GLReportBL.AddGLReportItems

            List<rcGLReportItem> cRptListItems = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListAst = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListLbl = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListInc = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListExp = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListPL = new List<rcGLReportItem>();


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
            cRptList.Add(itmAssetsHead);
            List<dcGLGroup> grpAssetsRoot = grpList.Where(c => c.GLClassID == assetClassID && c.GLGroupIDParent == 0).ToList();
            //GLReportRBL.AddGLReportItems(grpAssetsRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListAst);

            //GLReportRBL.AddGLReportItems(grpRootList, defLevel, prmLedger, grpList, accList, cRptList);
            GLReportRBL.AddGLReportItems(grpAssetsRoot, defLevel, prmLedger, grpList, accList, cRptList);

            rcGLReportItem itmLiabilityHead = new rcGLReportItem();
            itmLiabilityHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmLiabilityHead.IsBoldName = true;
            itmLiabilityHead.IsBoldAmt = true;
            itmLiabilityHead.ItemName = "Liabilities";
            itmLiabilityHead.ItemNameDispaly = itmLiabilityHead.ItemName;
            itmLiabilityHead.NumberFormat = prmLedger.NumberFormat;
            itmLiabilityHead.IsUnderlinedName = false;

            cRptList.Add(itmLiabilityHead);
            List<dcGLGroup> grpLiabilityRoot = grpList.Where(c => c.GLClassID == liabilityClassID && c.GLGroupIDParent == 0).ToList();
            //GLReportRBL.AddGLReportItems(grpLiabilityRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListLbl);
            GLReportRBL.AddGLReportItems(grpLiabilityRoot, defLevel, prmLedger, grpList, accList, cRptList);

            rcGLReportItem itmIncomeHead = new rcGLReportItem();
            itmIncomeHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmIncomeHead.IsBoldName = true;
            itmIncomeHead.IsBoldAmt = true;
            itmIncomeHead.ItemName = "Income";
            itmIncomeHead.ItemNameDispaly = itmIncomeHead.ItemName;
            itmIncomeHead.NumberFormat = prmLedger.NumberFormat;
            itmIncomeHead.IsUnderlinedName = false;

            cRptList.Add(itmIncomeHead);
            List<dcGLGroup> grpIncomeRoot = grpList.Where(c => c.GLClassID == incomeClassID && c.GLGroupIDParent == 0).ToList();
            //GLReportRBL.AddGLReportItems(grpIncomeRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListInc);
            GLReportRBL.AddGLReportItems(grpIncomeRoot, defLevel, prmLedger, grpList, accList, cRptList);

            rcGLReportItem itmExpenseHead = new rcGLReportItem();
            itmExpenseHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmExpenseHead.IsBoldName = true;
            itmExpenseHead.IsBoldAmt = true;
            itmExpenseHead.ItemName = "Expense";
            itmExpenseHead.ItemNameDispaly = itmExpenseHead.ItemName;
            itmExpenseHead.NumberFormat = prmLedger.NumberFormat;
            itmExpenseHead.IsUnderlinedName = false;

            cRptList.Add(itmExpenseHead);
            List<dcGLGroup> grpExpenseRoot = grpList.Where(c => c.GLClassID == expenseClassID && c.GLGroupIDParent == 0).ToList();
            //GLReportRBL.AddGLReportItems(grpExpenseRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListExp);
            GLReportRBL.AddGLReportItems(grpExpenseRoot, defLevel, prmLedger, grpList, accList, cRptList);


            rcGLReportItem itmPLHead = new rcGLReportItem();
            itmPLHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmPLHead.IsBoldName = true;
            itmPLHead.IsBoldAmt = true;
            itmPLHead.ItemName = "Profit & Loss";
            itmPLHead.ItemNameDispaly = itmPLHead.ItemName;
            itmPLHead.NumberFormat = prmLedger.NumberFormat;
            itmPLHead.IsUnderlinedName = false;

            cRptList.Add(itmPLHead);
            List<dcGLGroup> grpPLRoot = grpList.Where(c => c.GLClassID == PLClassID && c.GLGroupIDParent == 0).ToList();


            

            ////assets
            //if (prmLedger.IncludeGLClass)
            //{
            //    cRptList.Add(itmAssetsHead);
            //}
            //foreach (rcGLReportItem itmRpt in cRptListAst)
            //{
            //    ResetItemIndent(itmRpt, prmLedger);
            //    ResetItemBalanceDisplay(itmRpt, prmLedger);
            //    //if (prmLedger.ShowPercentage)
            //    //{
            //    //    SetPercentage(itmLbl, totLiablity);
            //    //}
            //    cRptList.Add(itmRpt);
            //}

            //if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    cRptList.Add(GLReportRBL.CreateBlankItem());
            //}



            ///liabilie

            //if (prmLedger.IncludeGLClass)
            //{
            //    cRptList.Add(itmLiabilityHead);
            //}
            //foreach (rcGLReportItem itmRpt in cRptListLbl)
            //{
            //    ResetItemIndent(itmRpt, prmLedger);
            //    ResetItemBalanceDisplay(itmRpt, prmLedger);
            //    //if (prmLedger.ShowPercentage)
            //    //{
            //    //    SetPercentage(itmLbl, totLiablity);
            //    //}
            //    cRptList.Add(itmRpt);
            //}

            //if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    cRptList.Add(GLReportRBL.CreateBlankItem());
            //}


            //income
            //if (prmLedger.IncludeGLClass)
            //{
            //    cRptList.Add(itmIncomeHead);
            //}
            //foreach (rcGLReportItem itmRpt in cRptListInc)
            //{
            //    ResetItemIndent(itmRpt, prmLedger);
            //    ResetItemBalanceDisplay(itmRpt, prmLedger);
            //    //if (prmLedger.ShowPercentage)
            //    //{
            //    //    SetPercentage(itmLbl, totLiablity);
            //    //}
            //    cRptList.Add(itmRpt);
            //}

            //if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    cRptList.Add(GLReportRBL.CreateBlankItem());
            //}


            //income
            //if (prmLedger.IncludeGLClass)
            //{
            //    cRptList.Add(itmExpenseHead);
            //}
            //foreach (rcGLReportItem itmRpt in cRptListExp)
            //{
            //    ResetItemIndent(itmRpt, prmLedger);
            //    ResetItemBalanceDisplay(itmRpt, prmLedger);
            //    //if (prmLedger.ShowPercentage)
            //    //{
            //    //    SetPercentage(itmLbl, totLiablity);
            //    //}
            //    cRptList.Add(itmRpt);
            //}

            //if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    cRptList.Add(GLReportRBL.CreateBlankItem());
            //}

            //pl
            //if (prmLedger.IncludeGLClass)
            //{
            //    cRptList.Add(itmPLHead);
            //}
            //foreach (rcGLReportItem itmRpt in cRptListPL)
            //{
            //    ResetItemIndent(itmRpt, prmLedger);
            //    ResetItemBalanceDisplay(itmRpt, prmLedger);
            //    //if (prmLedger.ShowPercentage)
            //    //{
            //    //    SetPercentage(itmLbl, totLiablity);
            //    //}
            //    cRptList.Add(itmRpt);
            //}

            //if (prmLedger.IncludeGLClass && prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            //{
            //    cRptList.Add(GLReportRBL.CreateBlankItem());
            //}

            //prmLedger.IncludeGLClass = true;
            //prmLedger.IncludeRootGLGroup = true;
            //prmLedger.MaxHierarchyLevel = -1;
            //prmLedger.IncludeZeroValue = true;

            prmLedger.InsertBlankBetweenGLClass = prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers;

            //GLReportRBL.AddGLReportItems(grpRootList, defLevel, prmLedger, grpList, accList, cRptList);

            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            {
                foreach (rcGLReportItem itm in cRptList)
                {
                    itm.ItemLevel = 0;
                    itm.ItemNameIndent = itm.ItemName;
                }
                switch (prmLedger.OrderBy)
                {
                    case AccOrderByEnum.Code:
                        cRptList = cRptList.OrderBy(c => c.ItemCode).ToList();
                        break;
                    case AccOrderByEnum.Name:
                        cRptList = cRptList.OrderBy(c => c.ItemNameDispaly).ToList();
                        break;
                    case AccOrderByEnum.SLNo:
                        cRptList = cRptList.OrderBy(c => c.ItemSLNo).ToList();
                        break;
                }
            }
           
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
                    rptItem.ItemLevel = 0;
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

    }
}
