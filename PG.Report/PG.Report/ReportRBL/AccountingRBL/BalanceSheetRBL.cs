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
    public class BalanceSheetRBL
    {

        public static List<rcGLReportItem> GetBalanceSheetItems(clsPrmLedger prmLedger)
        {
            return GetBalanceSheetItems(prmLedger, null);
        }

        public static List<rcGLReportItem> GetBalanceSheetItems(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcGLReportItem> cRptList = new List<rcGLReportItem>();

            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(prmLedger.CompanyID, dc);
            grpList = GLGroupBL.FormatGLGroup(prmLedger, grpList);


            List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLedger, dc);
            List<dcGLGroup> grpBalance = GLGroupBL.GetGroupBalance(prmLedger, grpList, accBalance, dc);


            List<rcGLReportItem> cRptListAst = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListLbl = new List<rcGLReportItem>();


            int defLevel = 0;
            if (prmLedger.IncludeGLClass)
            {
                defLevel = 1;
                if (prmLedger.MaxHierarchyLevel != -1)
                {
                    prmLedger.MaxHierarchyLevel += 1;
                }
            }


            int assetClassID = (int)GLClassEnum.Assets;
            int liabilityClassID = (int)GLClassEnum.Liabilities;

            int incomeClassID = (int)GLClassEnum.Income;
            int expenseClassID = (int)GLClassEnum.Expense;
            int PLClassID = (int)GLClassEnum.ProfitAndLoss;

            decimal totOpenDebit = grpBalance.Where(c => c.GLGroupIDParent == 0).Sum(s=>s.OpenDebitAmt);
            decimal totOpenCredit = grpBalance.Where(c => c.GLGroupIDParent == 0).Sum(s=>s.OpenCreditAmt);


            decimal totAsset = grpBalance.Where(c => c.GLClassID == assetClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);
            decimal totLiablity = grpBalance.Where(c => c.GLClassID == liabilityClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);

            decimal totIncome = grpBalance.Where(c => c.GLClassID == incomeClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);
            decimal totExpense = grpBalance.Where(c => c.GLClassID == expenseClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);


            decimal totPLAmtOpen = grpBalance.Where(c => c.GLClassID == PLClassID && c.GLGroupIDParent == 0).Sum(s => s.OpenAmt);
            decimal totPLAmtTran = grpBalance.Where(c => c.GLClassID == PLClassID && c.GLGroupIDParent == 0).Sum(s => s.TranAmt);

            //decimal totPLAmt = grpBalance.Where(c => c.GLClassID == PLClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);

            decimal totPLAmtCur = totIncome + totExpense;

            //decimal totIncExpAmt = totIncome + totExpense;

            decimal totPLClosing = totPLAmtOpen + totPLAmtTran + totPLAmtCur;


            decimal diffAmt = totOpenDebit - totOpenCredit;

            //open diff item
            rcGLReportItem itmOpenDiff = new rcGLReportItem();
            itmOpenDiff.ItemType = (int)GLReportItemTypeEnum.System;
            itmOpenDiff.ItemLevel = defLevel;
            itmOpenDiff.IsBoldName = false;
            itmOpenDiff.IsBoldAmt = true;
            itmOpenDiff.IsItalicName = true;
            //itmOpenDiff.IsItalicAmt = true;
            itmOpenDiff.ItemName = "Difference in Openning Balances";
            itmOpenDiff.ItemNameDispaly = itmOpenDiff.ItemName;

            if (diffAmt > 0)
            {
                itmOpenDiff.OpenAmt = -diffAmt;
                itmOpenDiff.CloseAmt = -diffAmt; 
            }
            else
            {
                itmOpenDiff.OpenAmt = Math.Abs(diffAmt);
                itmOpenDiff.CloseAmt = Math.Abs(diffAmt);
            }

            itmOpenDiff.OpenDebitAmt = Math.Abs(itmOpenDiff.OpenAmt >= 0 ? itmOpenDiff.OpenAmt : 0);
            itmOpenDiff.OpenCreditAmt = Math.Abs(itmOpenDiff.OpenAmt < 0 ? itmOpenDiff.OpenAmt : 0);
            itmOpenDiff.OpenBalanceAmt = Math.Abs(itmOpenDiff.OpenAmt);
            itmOpenDiff.OpenBalanceDisplay = itmOpenDiff.OpenBalanceAmt;
            itmOpenDiff.OpenBalanceText = itmOpenDiff.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);


            itmOpenDiff.CloseDebitAmt = Math.Abs(itmOpenDiff.CloseAmt >= 0 ? itmOpenDiff.CloseAmt : 0);
            itmOpenDiff.CloseCreditAmt = Math.Abs(itmOpenDiff.CloseAmt < 0 ? itmOpenDiff.CloseAmt : 0);
            itmOpenDiff.CloseBalanceAmt = Math.Abs(itmOpenDiff.CloseAmt);
            itmOpenDiff.CloseBalanceDisplay = itmOpenDiff.CloseBalanceAmt;
            itmOpenDiff.CloseBalanceText = itmOpenDiff.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);

            itmOpenDiff.NumberFormat = prmLedger.NumberFormat;


            //PL Class
            rcGLReportItem itmPL = new rcGLReportItem();
            itmPL.ItemType = (int)GLReportItemTypeEnum.System;
            itmPL.ItemLevel = defLevel;
            itmPL.IsBoldName = true;
            itmPL.IsBoldAmt = true;
            itmPL.IsItalicName = false;
            itmPL.IsItalicAmt = true;
            if (prmLedger.IsNonProfit)
            {
               itmPL.ItemName = "Income & Expenditure Statement";
            }
            else
            {
                itmPL.ItemName = "Profit & Loss Account";
            }
            
            //itmPL.ItemNameDispaly = itmPL.ItemName;
            //itmPL.CloseAmt = totProfitAmt;
            //itmPL.CloseBalanceAmt = Math.Abs(totProfitAmt);
            //itmPL.CloseBalanceDisplay = itmPL.CloseBalanceAmt;
            itmPL.NumberFormat = prmLedger.NumberFormat;


            //plitem Open
            rcGLReportItem itmPLOpen = new rcGLReportItem();
            itmPLOpen.ItemType = (int)GLReportItemTypeEnum.PLItem;
            itmPLOpen.ItemLevel = defLevel + 1;

            itmPLOpen.IsBoldName = false;
            itmPLOpen.IsBoldAmt = false;
            itmPLOpen.IsItalicName = false;
            itmPLOpen.IsItalicAmt = false;
            //itmPLOpen.ItemName = "Openning Balance";
            itmPLOpen.ItemName = totPLAmtOpen <= 0 ? "Openning Balance : Profit" : "Openning Balance : Loss";

            itmPLOpen.ItemNameDispaly = itmPLOpen.ItemName;
            itmPLOpen.CloseAmt = totPLAmtOpen;
            itmPLOpen.CloseDebitAmt = Math.Abs(itmPLOpen.CloseAmt >= 0 ? itmPLOpen.CloseAmt : 0);
            itmPLOpen.CloseCreditAmt = Math.Abs(itmPLOpen.CloseAmt < 0 ? itmPLOpen.CloseAmt : 0);
            itmPLOpen.CloseBalanceAmt = Math.Abs(totPLAmtOpen);
            
            
            //itmPLOpen.CloseBalanceDisplay = itmPLOpen.CloseBalanceAmt;
            if (totPLClosing <=0)
            {
                //itmPLOpen.CloseBalanceDisplay = totPLAmtOpen <= 0 ? Math.Abs(totPLAmtOpen) : -Math.Abs(totPLAmtOpen);
                if (totPLAmtOpen != 0)
                {
                    itmPLOpen.CloseBalanceDisplaySub1 = totPLAmtOpen <= 0 ? Math.Abs(totPLAmtOpen) : -Math.Abs(totPLAmtOpen);
                }
                itmPLOpen.CloseBalanceDisplay = 0;
            }
            else
            {
                //itmPLOpen.CloseBalanceDisplay = totPLAmtOpen <= 0 ? -Math.Abs(totPLAmtOpen) : Math.Abs(totPLAmtOpen);
                if (totPLAmtOpen != 0)
                {
                    itmPLOpen.CloseBalanceDisplaySub1 = totPLAmtOpen <= 0 ? -Math.Abs(totPLAmtOpen) : Math.Abs(totPLAmtOpen);
                }
                itmPLOpen.CloseBalanceDisplay = 0;
            }

            itmPLOpen.CloseBalanceText = itmPLOpen.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);

            itmPLOpen.NumberFormat = prmLedger.NumberFormat;


            //plitem cur
            rcGLReportItem itmPLCur = new rcGLReportItem();
            itmPLCur.ItemType = (int)GLReportItemTypeEnum.PLItem;
            itmPLCur.ItemLevel = defLevel + 1;

            itmPLCur.IsBoldName = false;
            itmPLCur.IsBoldAmt = false;
            itmPLCur.IsItalicName = false;
            itmPLCur.IsItalicAmt = false;
            
            itmPLCur.ItemName = totPLAmtCur <= 0 ? "Current Period : Profit" : "Current Period : Loss";
            itmPLCur.ItemName = totPLAmtCur == 0 ? "Current Period" : itmPLCur.ItemName;

            itmPLCur.ItemNameDispaly = itmPLCur.ItemName;
            itmPLCur.CloseAmt = totPLAmtCur;
            itmPLCur.CloseDebitAmt = Math.Abs(itmPLCur.CloseAmt >= 0 ? itmPLCur.CloseAmt : 0);
            itmPLCur.CloseCreditAmt = Math.Abs(itmPLCur.CloseAmt < 0 ? itmPLCur.CloseAmt : 0);
            itmPLCur.CloseBalanceAmt = Math.Abs(totPLAmtCur);
            
            itmPLCur.CloseBalanceDisplay = itmPLCur.CloseBalanceAmt;
            if (totPLClosing <= 0)
            {
                //itmPLCur.CloseBalanceDisplay = totPLAmtCur <= 0 ? Math.Abs(totPLAmtCur) : -Math.Abs(totPLAmtCur);

                if (totPLAmtCur != 0)
                {
                    itmPLCur.CloseBalanceDisplaySub1 = totPLAmtCur <= 0 ? Math.Abs(totPLAmtCur) : -Math.Abs(totPLAmtCur);
                }
                itmPLCur.CloseBalanceDisplay = 0;
            }
            else
            {
                //itmPLCur.CloseBalanceDisplay = totPLAmtCur <= 0 ? -Math.Abs(totPLAmtCur) : Math.Abs(totPLAmtCur);
                if (totPLAmtCur != 0)
                {
                    itmPLCur.CloseBalanceDisplaySub1 = totPLAmtCur <= 0 ? -Math.Abs(totPLAmtCur) : Math.Abs(totPLAmtCur);
                }
                itmPLCur.CloseBalanceDisplay = 0;
            }


            itmPLCur.CloseBalanceText = itmPLCur.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmPLCur.NumberFormat = prmLedger.NumberFormat;


            //pl adjust
            rcGLReportItem itmPLAdj = new rcGLReportItem();
            itmPLAdj.ItemType = (int)GLReportItemTypeEnum.PLItem;
            itmPLAdj.ItemLevel = defLevel + 1;

            itmPLAdj.IsBoldName = false;
            itmPLAdj.IsBoldAmt = false;
            itmPLAdj.IsItalicName = false;
            itmPLAdj.IsItalicAmt = false;
            //itmPLAdj.ItemName = "Less: Transferred";

            itmPLAdj.ItemName = totPLAmtTran <= 0 ? "Transferred: Profit" : "Transferred : Loss";

            itmPLAdj.ItemNameDispaly = itmPLAdj.ItemName;
            itmPLAdj.CloseAmt = totPLAmtTran;
            itmPLAdj.CloseDebitAmt = Math.Abs(itmPLAdj.CloseAmt >= 0 ? itmPLAdj.CloseAmt : 0);
            itmPLAdj.CloseCreditAmt = Math.Abs(itmPLAdj.CloseAmt < 0 ? itmPLAdj.CloseAmt : 0);
            itmPLAdj.CloseBalanceAmt = Math.Abs(totPLAmtTran);
            
            //itmPLAdj.CloseBalanceDisplay = itmPLAdj.CloseBalanceAmt;
            if (totPLClosing <= 0)
            {
                //itmPLAdj.CloseBalanceDisplay = totPLAmtTran <= 0 ? Math.Abs(totPLAmtTran) : -Math.Abs(totPLAmtTran);
                if (totPLAmtTran != 0)
                {
                    itmPLAdj.CloseBalanceDisplaySub1 = totPLAmtTran <= 0 ? Math.Abs(totPLAmtTran) : -Math.Abs(totPLAmtTran);
                }
                itmPLAdj.CloseBalanceDisplay =0;
            }
            else
            {
                //itmPLAdj.CloseBalanceDisplay = totPLAmtTran <= 0 ? -Math.Abs(totPLAmtTran) : Math.Abs(totPLAmtTran);
                if (totPLAmtTran != 0)
                {
                    itmPLAdj.CloseBalanceDisplaySub1 = totPLAmtTran <= 0 ? -Math.Abs(totPLAmtTran) : Math.Abs(totPLAmtTran);
                }
                itmPLAdj.CloseBalanceDisplay = 0;
            }


            itmPLAdj.CloseBalanceText = itmPLAdj.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmPLAdj.NumberFormat = prmLedger.NumberFormat;



            //pl closing
            rcGLReportItem itmPLCls = new rcGLReportItem();
            itmPLCls.ItemType = (int)GLReportItemTypeEnum.PLClosing;
            itmPLCls.ItemLevel = defLevel + 1;

            itmPLCls.IsBoldName = true;
            itmPLCls.IsBoldAmt = true;
            itmPLCls.IsItalicName = false;
            itmPLCls.IsItalicAmt = false;

            if (prmLedger.IsNonProfit)
            {
                itmPLCls.ItemName = totPLClosing <= 0 ? "Excess Of Income over Expediture" : "Excess Of Expediture over Income";
            }
            else
            {
                itmPLCls.ItemName = totPLClosing <= 0 ? "Net Profit" : "Net Loss";
            }

            itmPLCls.ItemNameDispaly = itmPLCls.ItemName;
            itmPLCls.CloseAmt = totPLClosing;
            itmPLCls.CloseDebitAmt = Math.Abs(itmPLCls.CloseAmt >= 0 ? itmPLCls.CloseAmt : 0);
            itmPLCls.CloseCreditAmt = Math.Abs(itmPLCls.CloseAmt < 0 ? itmPLCls.CloseAmt : 0);
            itmPLCls.CloseBalanceAmt = Math.Abs(totPLClosing);
            itmPLCls.CloseBalanceDisplay = itmPLCls.CloseBalanceAmt;
            itmPLCls.CloseBalanceText = itmPLCls.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmPLCls.NumberFormat = prmLedger.NumberFormat;

            if (totPLAmtOpen != 0 | totPLAmtTran != 0 | totPLAmtCur != 0)
            {
                itmPLCls.BorderTopAmtSub1 = "Solid";
                itmPLCls.BorderWidthTopAmtSub1 = "1pt";
            }


            ///Assets

            rcGLReportItem itmAssetsHead = new rcGLReportItem();
            itmAssetsHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmAssetsHead.IsBoldName = true;
            itmAssetsHead.IsBoldAmt = true;
            itmAssetsHead.ItemName = "Assets";
            itmAssetsHead.ItemNameDispaly = itmAssetsHead.ItemName;
            itmAssetsHead.NumberFormat = prmLedger.NumberFormat;
            itmAssetsHead.IsUnderlinedName = true;

            cRptListAst.Add(itmAssetsHead);


            //List<dcAccGLGroup> grpIncome = accGrpBalance.Where(c => c.AccGLClassID == incomeGrp).ToList();
            List<dcGLGroup> grpAssetsRoot = grpList.Where(c => c.GLClassID == assetClassID && c.GLGroupIDParent == 0).ToList();
            itmAssetsHead.ChildItemCount = GLReportRBL.AddGLReportItems(grpAssetsRoot, defLevel, prmLedger,
                                                                            grpBalance, accBalance, cRptListAst);


            if (prmLedger.ShowProfitLossInLiability == false)
            {
                if (totPLClosing > 0)
                {
                    cRptListAst.Add(itmPL);
                    if (totPLAmtOpen != 0)
                    {
                        cRptListAst.Add(itmPLOpen);
                    }

                    if (totPLAmtOpen != 0 | totPLAmtTran != 0)
                    {
                        cRptListAst.Add(itmPLCur);
                    }

                    if (totPLAmtTran != 0)
                    {
                        cRptListAst.Add(itmPLAdj);
                    }
                    cRptListAst.Add(itmPLCls);
                    totAsset += Math.Abs(totPLClosing);
                }
            }




            if (diffAmt < 0)
            {
                if (prmLedger.MaxHierarchyLevel == -1 | prmLedger.MaxHierarchyLevel > 0)
                {
                    cRptListAst.Add(itmOpenDiff);
                }
                totAsset += Math.Abs(diffAmt);
            }



            //total assets
            rcGLReportItem itmTotAssets = new rcGLReportItem();
            itmTotAssets.ItemType = (int)GLReportItemTypeEnum.System;
            itmTotAssets.IsBoldName = true;
            itmTotAssets.IsBoldAmt = true;
            itmTotAssets.ItemName = "Total Assets";
            itmTotAssets.ItemNameDispaly = itmTotAssets.ItemName;
            itmTotAssets.CloseAmt = totAsset;

            itmTotAssets.CloseDebitAmt = Math.Abs(itmTotAssets.CloseAmt >= 0 ? itmTotAssets.CloseAmt : 0);
            itmTotAssets.CloseCreditAmt = Math.Abs(itmTotAssets.CloseAmt < 0 ? itmTotAssets.CloseAmt : 0);

            itmTotAssets.CloseBalanceAmt = Math.Abs(totAsset);

            itmTotAssets.CloseBalanceDisplay = itmTotAssets.CloseBalanceAmt;
            itmTotAssets.CloseBalanceText = itmTotAssets.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);


            itmTotAssets.NumberFormat = prmLedger.NumberFormat;

            itmTotAssets.BorderTopAmt = "Solid";
            itmTotAssets.BorderWidthTopAmt = "1pt";

            itmTotAssets.BorderBottomAmt = "Double";
            itmTotAssets.BorderWidthBottomAmt = "2pt";

            cRptListAst.Add(itmTotAssets);


            ////Liabilities

            rcGLReportItem itmLiabilityHead = new rcGLReportItem();
            itmLiabilityHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmLiabilityHead.IsBoldName = true;
            itmLiabilityHead.IsBoldAmt = true;
            itmLiabilityHead.ItemName = "Liabilities";
            itmLiabilityHead.ItemNameDispaly = itmLiabilityHead.ItemName;
            itmLiabilityHead.NumberFormat = prmLedger.NumberFormat;
            itmLiabilityHead.IsUnderlinedName = true;
            cRptListLbl.Add(itmLiabilityHead);

            List<dcGLGroup> grpLiabilityRoot = grpList.Where(c => c.GLClassID == liabilityClassID && c.GLGroupIDParent == 0).ToList();
            itmLiabilityHead.ChildItemCount = GLReportRBL.AddGLReportItems(grpLiabilityRoot, defLevel, prmLedger
                                                                                , grpBalance, accBalance, cRptListLbl);


            if (prmLedger.ShowProfitLossInLiability == false)
            {
                if (totPLClosing <= 0)
                {
                    cRptListLbl.Add(itmPL);
                    if (totPLAmtOpen != 0)
                    {
                        cRptListLbl.Add(itmPLOpen);
                    }

                    if (totPLAmtOpen != 0 | totPLAmtTran != 0)
                    {
                        cRptListLbl.Add(itmPLCur);
                    }

                    if (totPLAmtTran != 0)
                    {
                        cRptListLbl.Add(itmPLAdj);
                    }
                    cRptListLbl.Add(itmPLCls);

                    totLiablity -= Math.Abs(totPLClosing);
                }
            }
            else
            {


                cRptListLbl.Add(itmPL);
                if (totPLAmtOpen != 0)
                {
                    itmPLOpen.CloseBalanceDisplaySub1 = totPLAmtOpen <= 0 ? Math.Abs(totPLAmtOpen) : -Math.Abs(totPLAmtOpen);
                    cRptListLbl.Add(itmPLOpen);
                }

                if (totPLAmtOpen != 0 | totPLAmtTran != 0)
                {
                    if (itmPLCur.CloseAmt != 0)
                    {
                        itmPLCur.CloseBalanceDisplaySub1 = itmPLCur.CloseAmt <= 0 ? Math.Abs(itmPLCur.CloseAmt) : -Math.Abs(itmPLCur.CloseAmt);
                    }
                    cRptListLbl.Add(itmPLCur);
                }

                if (totPLAmtTran != 0)
                {
                    itmPLAdj.CloseBalanceDisplaySub1 = totPLAmtTran <= 0 ? Math.Abs(totPLAmtTran) : -Math.Abs(totPLAmtTran);
                    cRptListLbl.Add(itmPLAdj);
                }


                if (itmPLCls.CloseAmt != 0)
                {
                    itmPLCls.CloseBalanceDisplay = itmPLCls.CloseAmt <= 0 ? Math.Abs(itmPLCls.CloseAmt) : -Math.Abs(itmPLCls.CloseAmt);
                }
                cRptListLbl.Add(itmPLCls);


                if (totPLClosing <= 0)
                {
                    totLiablity -= Math.Abs(totPLClosing);
                }
                else
                {
                    totLiablity += Math.Abs(totPLClosing);
                }
            }




            if (diffAmt > 0)
            {
                if (prmLedger.MaxHierarchyLevel == -1 | prmLedger.MaxHierarchyLevel > 0)
                {
                    cRptListLbl.Add(itmOpenDiff);
                }


                totLiablity -= Math.Abs(diffAmt);
            }



            //total liablitiy
            rcGLReportItem itemTotLiability = new rcGLReportItem();
            itemTotLiability.ItemType = (int)GLReportItemTypeEnum.System;
            itemTotLiability.IsBoldName = true;
            itemTotLiability.IsBoldAmt = true;
            itemTotLiability.ItemName = "Total Liabilities";
            itemTotLiability.ItemNameDispaly = itemTotLiability.ItemName;
            itemTotLiability.CloseAmt = totLiablity;

            itemTotLiability.CloseDebitAmt = Math.Abs(itemTotLiability.CloseAmt >= 0 ? itemTotLiability.CloseAmt : 0);
            itemTotLiability.CloseCreditAmt = Math.Abs(itemTotLiability.CloseAmt < 0 ? itemTotLiability.CloseAmt : 0);

            itemTotLiability.CloseBalanceAmt = Math.Abs(totLiablity);
            itemTotLiability.CloseBalanceDisplay = itemTotLiability.CloseBalanceAmt;
            itemTotLiability.CloseBalanceText = itmLiabilityHead.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itemTotLiability.NumberFormat = prmLedger.NumberFormat;

            itemTotLiability.BorderTopAmt = "Solid";
            itemTotLiability.BorderWidthTopAmt = "1pt";

            itemTotLiability.BorderBottomAmt = "Double";
            itemTotLiability.BorderWidthBottomAmt = "2pt";
            cRptListLbl.Add(itemTotLiability);


            //Combine Assest And Liabilites
            if (prmLedger.ShowLiabilitiesFirst)
            {
                foreach (rcGLReportItem itmLbl in cRptListLbl)
                {
                    ResetItemIndent(itmLbl, prmLedger);
                    ResetItemBalanceDisplay(itmLbl, prmLedger);
                    if (prmLedger.ShowPercentage)
                    {
                        SetPercentage(itmLbl, totLiablity);
                    }
                    cRptList.Add(itmLbl);
                }
                cRptList.Add(GLReportRBL.CreateBlankItem());
                foreach (rcGLReportItem itmAssets in cRptListAst)
                {
                    ResetItemIndent(itmAssets, prmLedger);
                    ResetItemBalanceDisplay(itmAssets, prmLedger);
                    if (prmLedger.ShowPercentage)
                    {
                        SetPercentage(itmAssets, totAsset);
                    }
                    cRptList.Add(itmAssets);
                }
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }
            else
            {
                foreach (rcGLReportItem itmAssets in cRptListAst)
                {
                    ResetItemIndent(itmAssets, prmLedger);
                    ResetItemBalanceDisplay(itmAssets, prmLedger);
                    if (prmLedger.ShowPercentage)
                    {
                        SetPercentage(itmAssets, totAsset);
                    }
                    cRptList.Add(itmAssets);
                }
                cRptList.Add(GLReportRBL.CreateBlankItem());
                foreach (rcGLReportItem itmLbl in cRptListLbl)
                {
                    ResetItemIndent(itmLbl, prmLedger);
                    ResetItemBalanceDisplay(itmLbl, prmLedger);
                    if (prmLedger.ShowPercentage)
                    {
                        SetPercentage(itmLbl, totLiablity);
                    }
                    cRptList.Add(itmLbl);
                }
                cRptList.Add(GLReportRBL.CreateBlankItem());
            }

            return cRptList;

        }

        public static void SetPercentage(rcGLReportItem rptItem, decimal totAmt)
        {
            if (rptItem.ItemType == (int)GLReportItemTypeEnum.PLItem)
            {
                return;
            }


            if (totAmt == 0)
            {
                return;
            }

            if (rptItem.CloseBalanceDisplay != 0)
            {
                rptItem.CloseBalancePercent = Math.Abs(rptItem.CloseBalanceDisplay) / Math.Abs(totAmt);
            }


            if (rptItem.CloseBalanceDisplaySub1  != 0)
            {
                rptItem.CloseBalancePercentSub1 = Math.Abs(rptItem.CloseBalanceDisplaySub1) / Math.Abs(totAmt);
            }
           
        }

        public static void ResetItemIndent(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {
            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            {
                rptItem.ItemLevel = rptItem.ItemLevel > 1 ? 1 : 0;
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

                        if (rptItem.ItemLevel == 1)
                        {
                            rptItem.IsBoldAmt = true;
                        }

                        if (rptItem.ItemLevel == 2)
                        {
                            rptItem.IsBoldAmt = false;
                        }

                        if (rptItem.ItemLevel > 2)
                        {
                            rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
                            rptItem.CloseBalanceDisplay = 0;

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



        public static List<rcBalanceSheet> GetBalanceSheet(clsPrmLedger prmLedger)
        {
            return GetBalanceSheet(prmLedger, null);
        }

        public static List<rcBalanceSheet> GetBalanceSheet(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcBalanceSheet> cRptList = new List<rcBalanceSheet>();

            rcBalanceSheet cRpt = new rcBalanceSheet();

            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);
             
            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRpt.BalanceSheetHeader.Add(cRptHeader);

            

            cRpt.BalanceSheetItems = GetBalanceSheetItems(prmLedger, dc);

            cRptList.Add(cRpt);

            return cRptList;

        }
    }
}
