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
    public class IncomeStatementRBL
    {
        public static List<rcGLReportItem> GetIncomeStatementItems(clsPrmLedger prmLedger)
        {
            return GetIncomeStatementItems(prmLedger, null);
        }

        public static List<rcGLReportItem> GetIncomeStatementItems(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcGLReportItem> cRptList = new List<rcGLReportItem>();

            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(prmLedger.CompanyID, dc);
            grpList = GLGroupBL.FormatGLGroup(prmLedger, grpList);

            List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLedger, dc);
            List<dcGLGroup> grpBalance = GLGroupBL.GetGroupBalance(prmLedger, grpList, accBalance, dc);

            List<rcGLReportItem> cRptListIncGross = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListExpGross = new List<rcGLReportItem>();

            List<rcGLReportItem> cRptListIncNonGross = new List<rcGLReportItem>();
            List<rcGLReportItem> cRptListExpNonGross = new List<rcGLReportItem>();

            //int assetClassID = (int)GLClassEnum.Assets;
            //int liabilityClassID = (int)GLClassEnum.Liabilities;

            int incomeClassID = (int)GLClassEnum.Income;
            int expenseClassID = (int)GLClassEnum.Expense;
            //int PLClassID = (int)GLClassEnum.ProfitAndLoss;

            decimal totOpenDebit = grpBalance.Where(c => c.GLClassID == incomeClassID && c.GLGroupIDParent == 0).Sum(s=>s.OpenDebitAmt);
            decimal totOpenCredit = grpBalance.Where(c => c.GLClassID == expenseClassID && c.GLGroupIDParent == 0).Sum(s=>s.OpenCreditAmt);

            decimal totIncomeGross = grpBalance.Where(c => c.GLClassID == incomeClassID 
                                                        && c.GLGroupIDParent == 0
                                                        && c.IsGrossProfit == true  ).Sum(s => s.CloseAmt);
            decimal totExpenseGross = grpBalance.Where(c => c.GLClassID == expenseClassID 
                                                        && c.GLGroupIDParent == 0
                                                        && c.IsGrossProfit == true).Sum(s => s.CloseAmt);


            decimal totIncomeNonGross = grpBalance.Where(c => c.GLClassID == incomeClassID
                                            && c.GLGroupIDParent == 0
                                            && c.IsGrossProfit == false).Sum(s => s.CloseAmt);
            decimal totExpenseNonGross = grpBalance.Where(c => c.GLClassID == expenseClassID
                                                        && c.GLGroupIDParent == 0
                                                        && c.IsGrossProfit == false).Sum(s => s.CloseAmt);

            decimal totIncome = grpBalance.Where(c => c.GLClassID == incomeClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);
            decimal totExpense = grpBalance.Where(c => c.GLClassID == expenseClassID && c.GLGroupIDParent == 0).Sum(s => s.CloseAmt);


            decimal totPLGross = totIncomeGross + totExpenseGross;
            decimal totPLNet = totIncome + totExpense;


            ///Sales/Revenues - Gross
            ///
            rcGLReportItem itmIncomeGrossHead = new rcGLReportItem();
            itmIncomeGrossHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmIncomeGrossHead.IsBoldName = true;
            itmIncomeGrossHead.IsBoldAmt = true;
            itmIncomeGrossHead.ItemName = "Sales/Revenues";
            itmIncomeGrossHead.ItemNameDispaly = itmIncomeGrossHead.ItemName;
            itmIncomeGrossHead.NumberFormat = prmLedger.NumberFormat;
            itmIncomeGrossHead.IsUnderlinedName = true;
            cRptListIncGross.Add(itmIncomeGrossHead);


            //List<dcAccGLGroup> grpIncome = accGrpBalance.Where(c => c.AccGLClassID == incomeGrp).ToList();
            List<dcGLGroup> grpIncGrossRoot = grpList.Where(c => c.GLClassID == incomeClassID 
                                                                 && c.GLGroupIDParent == 0
                                                                 && c.IsGrossProfit == true).ToList();
            int incGrossCount =  GLReportRBL.AddGLReportItems(grpIncGrossRoot, 1, prmLedger,
                                             grpBalance, accBalance, cRptListIncGross);


            //Income Gross Total
            rcGLReportItem itmIncomeGrossTotal = new rcGLReportItem();
            itmIncomeGrossTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmIncomeGrossTotal.ItemLevel = 0;

            itmIncomeGrossTotal.IsBoldName = true;
            itmIncomeGrossTotal.IsBoldAmt = true;
            itmIncomeGrossTotal.IsItalicName = false;
            itmIncomeGrossTotal.IsItalicAmt = false;

            itmIncomeGrossTotal.ItemName = "Total Sales/Revenues";
            itmIncomeGrossTotal.ItemNameDispaly = itmIncomeGrossTotal.ItemName;
            itmIncomeGrossTotal.CloseAmt = totIncomeGross;
            itmIncomeGrossTotal.CloseDebitAmt = Math.Abs(itmIncomeGrossTotal.CloseAmt >= 0 ? itmIncomeGrossTotal.CloseAmt : 0);
            itmIncomeGrossTotal.CloseCreditAmt = Math.Abs(itmIncomeGrossTotal.CloseAmt < 0 ? itmIncomeGrossTotal.CloseAmt : 0);
            itmIncomeGrossTotal.CloseBalanceAmt = Math.Abs(totIncomeGross);
            itmIncomeGrossTotal.CloseBalanceDisplay = itmIncomeGrossTotal.CloseBalanceAmt;
            itmIncomeGrossTotal.CloseBalanceText = itmIncomeGrossTotal.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmIncomeGrossTotal.NumberFormat = prmLedger.NumberFormat;

            itmIncomeGrossTotal.BorderTopAmt = "Solid";
            itmIncomeGrossTotal.BorderWidthTopAmt = "1pt";
            cRptListIncGross.Add(itmIncomeGrossTotal);


            //cRptList.Add(GLReportRBL.CreateBlankItem());



            ///Cost of Sales: Expense Gross head
            ///
            rcGLReportItem itmExpenseGrossHead = new rcGLReportItem();
            itmExpenseGrossHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmExpenseGrossHead.IsBoldName = true;
            itmExpenseGrossHead.IsBoldAmt = true;
            itmExpenseGrossHead.ItemName = "Cost of Sales";
            itmExpenseGrossHead.ItemNameDispaly = itmIncomeGrossHead.ItemName;
            itmExpenseGrossHead.NumberFormat = prmLedger.NumberFormat;
            itmExpenseGrossHead.IsUnderlinedName = true;
            cRptListExpGross.Add(itmExpenseGrossHead);


            //List<dcAccGLGroup> grpIncome = accGrpBalance.Where(c => c.AccGLClassID == incomeGrp).ToList();
            List<dcGLGroup> grpExpGrossRoot = grpList.Where(c => c.GLClassID == expenseClassID
                                                                 && c.GLGroupIDParent == 0
                                                                 && c.IsGrossProfit == true).ToList();
            int expGrossCount = GLReportRBL.AddGLReportItems(grpExpGrossRoot, 1, prmLedger,
                                                                            grpBalance, accBalance, cRptListExpGross);


            //Expense Gross Total
            rcGLReportItem itmExpenseGrossTotal = new rcGLReportItem();
            itmExpenseGrossTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmExpenseGrossTotal.ItemLevel = 0;

            itmExpenseGrossTotal.IsBoldName = true;
            itmExpenseGrossTotal.IsBoldAmt = true;
            itmExpenseGrossTotal.IsItalicName = false;
            itmExpenseGrossTotal.IsItalicAmt = false;

            itmExpenseGrossTotal.ItemName = "Total Cost of Sales";
            itmExpenseGrossTotal.ItemNameDispaly = itmIncomeGrossTotal.ItemName;
            itmExpenseGrossTotal.CloseAmt = totExpenseGross;
            itmExpenseGrossTotal.CloseDebitAmt = Math.Abs(itmExpenseGrossTotal.CloseAmt >= 0 ? itmExpenseGrossTotal.CloseAmt : 0);
            itmExpenseGrossTotal.CloseCreditAmt = Math.Abs(itmExpenseGrossTotal.CloseAmt < 0 ? itmExpenseGrossTotal.CloseAmt : 0);
            itmExpenseGrossTotal.CloseBalanceAmt = Math.Abs(totExpenseGross);
            itmExpenseGrossTotal.CloseBalanceDisplay = itmExpenseGrossTotal.CloseBalanceAmt;
            itmExpenseGrossTotal.CloseBalanceText = itmIncomeGrossTotal.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmExpenseGrossTotal.NumberFormat = prmLedger.NumberFormat;

            itmExpenseGrossTotal.BorderTopAmt = "Solid";
            itmExpenseGrossTotal.BorderWidthTopAmt = "1pt";
            cRptListExpGross.Add(itmExpenseGrossTotal);


            //Gross Profit
            rcGLReportItem itmPLGross = new rcGLReportItem();
            itmPLGross.ItemType = (int)GLReportItemTypeEnum.PLClosing;
            itmPLGross.ItemLevel = 0;

            itmPLGross.IsBoldName = true;
            itmPLGross.IsBoldAmt = true;
            itmPLGross.IsItalicName = false;
            itmPLGross.IsItalicAmt = false;

            itmPLGross.ItemName = totPLGross <= 0 ? "Gross Profit:" : "Gross Loss:";
            itmPLGross.ItemNameDispaly = itmPLGross.ItemName;
            itmPLGross.CloseAmt = totPLGross;
            itmPLGross.CloseDebitAmt = Math.Abs(itmPLGross.CloseAmt >= 0 ? itmPLGross.CloseAmt : 0);
            itmPLGross.CloseCreditAmt = Math.Abs(itmPLGross.CloseAmt < 0 ? itmPLGross.CloseAmt : 0);
            itmPLGross.CloseBalanceAmt = Math.Abs(totPLGross);
            itmPLGross.CloseBalanceDisplay = itmPLGross.CloseBalanceAmt;
            itmPLGross.CloseBalanceText = itmPLGross.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmPLGross.NumberFormat = prmLedger.NumberFormat;

            itmPLGross.BorderTopAmt = "Solid";
            itmPLGross.BorderWidthTopAmt = "1pt";

            //cRptListIncGross.Add(itmPLGross);


            ///Income Non Gross head
            ///
            rcGLReportItem itmIncomeNonGrossHead = new rcGLReportItem();
            itmIncomeNonGrossHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmIncomeNonGrossHead.IsBoldName = true;
            itmIncomeNonGrossHead.IsBoldAmt = true;
            itmIncomeNonGrossHead.ItemName = "Other Incomes";
            itmIncomeNonGrossHead.ItemNameDispaly = itmIncomeNonGrossHead.ItemName;
            itmIncomeNonGrossHead.NumberFormat = prmLedger.NumberFormat;
            itmIncomeNonGrossHead.IsUnderlinedName = true;
            cRptListIncNonGross.Add(itmIncomeNonGrossHead);


            List<dcGLGroup> grpIncNonGrossRoot = grpList.Where(c => c.GLClassID == incomeClassID
                                                                && c.GLGroupIDParent == 0
                                                                && c.IsGrossProfit == false).ToList();
            int incNonGrossCount = GLReportRBL.AddGLReportItems(grpIncNonGrossRoot, 1, prmLedger,
                                                                    grpBalance, accBalance, cRptListIncNonGross);



            //Expense Non Gross Total
            rcGLReportItem itmIncomeNonGrossTotal = new rcGLReportItem();
            itmIncomeNonGrossTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmIncomeNonGrossTotal.ItemLevel = 0;

            itmIncomeNonGrossTotal.IsBoldName = true;
            itmIncomeNonGrossTotal.IsBoldAmt = true;
            itmIncomeNonGrossTotal.IsItalicName = false;
            itmIncomeNonGrossTotal.IsItalicAmt = false;

            itmIncomeNonGrossTotal.ItemName = "Total Other Incomes";
            itmIncomeNonGrossTotal.ItemNameDispaly = itmIncomeGrossTotal.ItemName;
            itmIncomeNonGrossTotal.CloseAmt = totIncomeNonGross;
            itmIncomeNonGrossTotal.CloseDebitAmt = Math.Abs(itmIncomeNonGrossTotal.CloseAmt >= 0 ? itmIncomeNonGrossTotal.CloseAmt : 0);
            itmIncomeNonGrossTotal.CloseCreditAmt = Math.Abs(itmIncomeNonGrossTotal.CloseAmt < 0 ? itmIncomeNonGrossTotal.CloseAmt : 0);
            itmIncomeNonGrossTotal.CloseBalanceAmt = Math.Abs(totIncomeNonGross);
            itmIncomeNonGrossTotal.CloseBalanceDisplay = itmIncomeNonGrossTotal.CloseBalanceAmt;
            itmIncomeNonGrossTotal.CloseBalanceText = itmIncomeNonGrossTotal.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmIncomeNonGrossTotal.NumberFormat = prmLedger.NumberFormat;

            itmIncomeNonGrossTotal.BorderTopAmt = "Solid";
            itmIncomeNonGrossTotal.BorderWidthTopAmt = "1pt";
            cRptListIncNonGross.Add(itmIncomeNonGrossTotal);



            //cRptList.Add(GLReportRBL.CreateBlankItem());


            //List<dcAccGLGroup> grpIncome = accGrpBalance.Where(c => c.AccGLClassID == incomeGrp).ToList();


            ///Expense Non Gross head
            ///
            rcGLReportItem itmExpenseNonGrossHead = new rcGLReportItem();
            itmExpenseNonGrossHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmExpenseNonGrossHead.IsBoldName = true;
            itmExpenseNonGrossHead.IsBoldAmt = true;
            itmExpenseNonGrossHead.ItemName = "Operating Expenses";
            itmExpenseNonGrossHead.ItemNameDispaly = itmExpenseNonGrossHead.ItemName;
            itmExpenseNonGrossHead.NumberFormat = prmLedger.NumberFormat;
            itmExpenseNonGrossHead.IsUnderlinedName = true;
            cRptListExpNonGross.Add(itmExpenseNonGrossHead);

            List<dcGLGroup> grpExpNonGrossRoot = grpList.Where(c => c.GLClassID == expenseClassID
                                                                 && c.GLGroupIDParent == 0
                                                                 && c.IsGrossProfit == false).ToList();
            int expNonGrossCount = GLReportRBL.AddGLReportItems(grpExpNonGrossRoot, 1, prmLedger,
                                                                            grpBalance, accBalance, cRptListExpNonGross);



            //Expense Non Gross Total
            rcGLReportItem itmExpenseNonGrossTotal = new rcGLReportItem();
            itmExpenseNonGrossTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmExpenseNonGrossTotal.ItemLevel = 0;

            itmExpenseNonGrossTotal.IsBoldName = true;
            itmExpenseNonGrossTotal.IsBoldAmt = true;
            itmExpenseNonGrossTotal.IsItalicName = false;
            itmExpenseNonGrossTotal.IsItalicAmt = false;

            itmExpenseNonGrossTotal.ItemName = "Total Operating Expenses";
            itmExpenseNonGrossTotal.ItemNameDispaly = itmIncomeGrossTotal.ItemName;
            itmExpenseNonGrossTotal.CloseAmt = totExpenseNonGross;
            itmExpenseNonGrossTotal.CloseDebitAmt = Math.Abs(itmExpenseNonGrossTotal.CloseAmt >= 0 ? itmExpenseNonGrossTotal.CloseAmt : 0);
            itmExpenseNonGrossTotal.CloseCreditAmt = Math.Abs(itmExpenseNonGrossTotal.CloseAmt < 0 ? itmExpenseNonGrossTotal.CloseAmt : 0);
            itmExpenseNonGrossTotal.CloseBalanceAmt = Math.Abs(totExpenseNonGross);
            itmExpenseNonGrossTotal.CloseBalanceDisplay = itmExpenseNonGrossTotal.CloseBalanceAmt;
            itmExpenseNonGrossTotal.CloseBalanceText = itmExpenseNonGrossTotal.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmExpenseNonGrossTotal.NumberFormat = prmLedger.NumberFormat;

            itmExpenseNonGrossTotal.BorderTopAmt = "Solid";
            itmExpenseNonGrossTotal.BorderWidthTopAmt = "1pt";
            cRptListExpNonGross.Add(itmExpenseNonGrossTotal);



            //pl Net
            rcGLReportItem itmPLNet = new rcGLReportItem();
            itmPLNet.ItemType = (int)GLReportItemTypeEnum.PLClosing;
            itmPLNet.ItemLevel = 0;

            itmPLNet.IsBoldName = true;
            itmPLNet.IsBoldAmt = true;
            itmPLNet.IsItalicName = false;
            itmPLNet.IsItalicAmt = false;

            if (prmLedger.IsNonProfit)
            {
                itmPLNet.ItemName = totPLNet <= 0 ? "Excess Of Income over Expediture:" : "Excess Of Expediture over Income:";
            }
            else
            {
                itmPLNet.ItemName = totPLNet <= 0 ? "Net Profit:" : "Net Loss:";
            }

            itmPLNet.ItemNameDispaly = itmPLNet.ItemName;
            itmPLNet.CloseAmt = totPLNet;
            itmPLNet.CloseDebitAmt = Math.Abs(itmPLNet.CloseAmt >= 0 ? itmPLNet.CloseAmt : 0);
            itmPLNet.CloseCreditAmt = Math.Abs(itmPLNet.CloseAmt < 0 ? itmPLNet.CloseAmt : 0);
            itmPLNet.CloseBalanceAmt = Math.Abs(totPLNet);
            itmPLNet.CloseBalanceDisplay = itmPLNet.CloseBalanceAmt;
            itmPLNet.CloseBalanceText = itmPLNet.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);
            itmPLNet.NumberFormat = prmLedger.NumberFormat;

            itmPLNet.BorderTopAmt = "Solid";
            itmPLNet.BorderWidthTopAmt = "1pt";

            itmPLNet.BorderBottomAmt = "Double";
            itmPLNet.BorderWidthBottomAmt = "2pt";



            //Combine Incomes And Expense

            ///income gross
            foreach (rcGLReportItem itmIncGross in cRptListIncGross)
            {
                ResetItemIndent(itmIncGross, prmLedger);
                ResetItemBalanceDisplay(itmIncGross, prmLedger);
                //SetPercentage(itmLbl, totLiablity);
                cRptList.Add(itmIncGross);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());

            //expense Gross
            foreach (rcGLReportItem itmExpGross in cRptListExpGross)
            {
                ResetItemIndent(itmExpGross, prmLedger);
                ResetItemBalanceDisplay(itmExpGross, prmLedger);
                //SetPercentage(itmLbl, totLiablity);
                cRptList.Add(itmExpGross);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());

            cRptList.Add(itmPLGross);


            cRptList.Add(GLReportRBL.CreateBlankItem());

            //expense non Gross
            foreach (rcGLReportItem itmExpNonGross in cRptListExpNonGross)
            {
                ResetItemIndent(itmExpNonGross, prmLedger);
                ResetItemBalanceDisplay(itmExpNonGross, prmLedger);
                //SetPercentage(itmLbl, totLiablity);
                cRptList.Add(itmExpNonGross);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());


            ///income non gross
            foreach (rcGLReportItem itmIncNonGross in cRptListIncNonGross)
            {
                ResetItemIndent(itmIncNonGross, prmLedger);
                ResetItemBalanceDisplay(itmIncNonGross, prmLedger);
                //SetPercentage(itmLbl, totLiablity);
                cRptList.Add(itmIncNonGross);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());



            cRptList.Add(itmPLNet);

            cRptList.Add(GLReportRBL.CreateBlankItem());

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
                                rptItem.IsBoldAmt = true;
                                rptItem.IsUnderlinedAmt = true;
                            }
                            else
                            {
                                rptItem.IsBoldAmt = true;
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

        public static List<rcIncomeStatement> GetIncomeStatement(clsPrmLedger prmLedger)
        {
            return GetIncomeStatement(prmLedger, null);
        }

        public static List<rcIncomeStatement> GetIncomeStatement(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcIncomeStatement> cRptList = new List<rcIncomeStatement>();

            rcIncomeStatement cRpt = new rcIncomeStatement();
            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);

            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRpt.IncomeStatementHeader.Add(cRptHeader);
            cRpt.IncomeStatementItems = GetIncomeStatementItems(prmLedger, dc);

            cRptList.Add(cRpt);

            return cRptList;

        }
    }
}
