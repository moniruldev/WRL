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
    public class ReceiptPaymentRBL
    {

        public static List<rcGLReportItem> GetReceiptPaymentItems(clsPrmLedger prmLedger)
        {
            return GetReceiptPaymentItems(prmLedger, null);
        }

        public static List<rcGLReportItem> GetReceiptPaymentItems(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcGLReportItem> cRptList = new List<rcGLReportItem>();

            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(prmLedger.CompanyID, dc);
            //grpList = GLGroupBL.FormatGLGroup(prmLedger, grpList);

            List<dcGLGroup> grpListCash = grpList.Where(g => g.IsCash == true).ToList();
          
            //List<dcGLAccount> accBalanceCash = GLAccountBL.GetGLAccountListbyGroups(prmLedger.CompanyID, grpListCash, dc);
            List<dcGLAccount> accBalanceCash = GLAccountBL.GetAccountBalance(prmLedger, grpListCash, dc);

            grpListCash = GLGroupBL.SetGLGroupListHeirerchy(grpListCash, grpList, prmLedger.IncludeGroupParents);
            
            //grpListCash = GLGroupBL.UpdateGLGroupLevelCurrent(grpListCash);

            List<dcGLGroup> grpBalanceCash = GLGroupBL.GetGroupBalance(prmLedger, grpListCash, accBalanceCash, dc);

            List<rcGLReportItem> cRptListOpen = new List<rcGLReportItem>();

            rcGLReportItem itmOpenHead = new rcGLReportItem();
            itmOpenHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmOpenHead.IsBoldName = true;
            itmOpenHead.IsBoldAmt = true;
            itmOpenHead.ItemName = "Opening Balance";
            itmOpenHead.ItemNameDispaly = itmOpenHead.ItemName;
            itmOpenHead.NumberFormat = prmLedger.NumberFormat;
            itmOpenHead.IsUnderlinedName = true;

            cRptListOpen.Add(itmOpenHead);

            ///add ope
            ///
            int minLevel = grpListCash.Min(c => c.GLGroupLevel);
            //List<dcGLGroup> grpOpenRoot = grpListCash.Where(c => c.GLGroupLevel == 0).ToList();

            prmLedger.ItemAmountTypeCheck = ItemAmountTypeEnum.OpeningBalance;
            prmLedger.ItemAmountTypeDisplay = ItemAmountTypeEnum.OpeningBalance;
            List<dcGLGroup> grpOpenRoot = grpListCash.Where(c => c.HasParent == false).ToList();
            itmOpenHead.ChildItemCount = GLReportRBL.AddGLReportItems(grpOpenRoot, 1, prmLedger,
                                                                grpBalanceCash, accBalanceCash, cRptListOpen);
            
            ClearNonCashGroupBalance(cRptListOpen, grpListCash);


            rcGLReportItem itmOpenTotal = new rcGLReportItem();
            itmOpenTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmOpenTotal.IsBoldName = true;
            itmOpenTotal.IsBoldAmt = true;
            itmOpenTotal.ItemName = "Total Opening Balance";
            itmOpenTotal.ItemNameDispaly = itmOpenHead.ItemName;
            itmOpenTotal.NumberFormat = prmLedger.NumberFormat;
            itmOpenTotal.IsUnderlinedName = false;
            //itmOpenTotal.


            itmOpenTotal.BorderTopAmt = "Solid";
            itmOpenTotal.BorderWidthTopAmt = "1pt";

            decimal totOpenAmt = grpOpenRoot.Sum(c => c.OpenAmt);
            itmOpenTotal.CloseAmt = totOpenAmt;
            itmOpenTotal.ItemAmtDisplay = totOpenAmt;

            cRptListOpen.Add(itmOpenTotal);



            //cRptList.AddRange(cRptListOpen);
            List<dcGLAccount> accCashContra = GLAccountBL.GetAccountTranCash(prmLedger, CashTranOption.CashTranContra);
            List<dcGLAccount> accCashContraDb = accCashContra.Where(c => c.DebitAmt > 0).ToList();
            List<dcGLAccount> accCashContraCr = accCashContra.Where(c => c.CreditAmt > 0).ToList();


            List<dcGLGroup> grpContraDb = GLGroupBL.GetGLGroupListByAccountList(accCashContraDb, grpList);
            List<dcGLGroup> grpContraCr = GLGroupBL.GetGLGroupListByAccountList(accCashContraCr, grpList);

            grpContraDb = GLGroupBL.SetGLGroupListHeirerchy(grpContraDb, grpList, prmLedger.IncludeGroupParents);
            grpContraCr = GLGroupBL.SetGLGroupListHeirerchy(grpContraCr, grpList, prmLedger.IncludeGroupParents);


            grpContraDb = GLGroupBL.GetGroupBalance(prmLedger, grpContraDb, accCashContraDb, dc);
            grpContraCr = GLGroupBL.GetGroupBalance(prmLedger, grpContraCr, accCashContraCr, dc);

            //Receipt
            List<rcGLReportItem> cRptListReceipt = new List<rcGLReportItem>();

            rcGLReportItem itmReceiptHead = new rcGLReportItem();
            itmReceiptHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmReceiptHead.IsBoldName = true;
            itmReceiptHead.IsBoldAmt = true;
            itmReceiptHead.ItemName = "Receipts";
            itmReceiptHead.ItemNameDispaly = itmReceiptHead.ItemName;
            itmReceiptHead.NumberFormat = prmLedger.NumberFormat;
            itmReceiptHead.IsUnderlinedName = true;

            cRptListReceipt.Add(itmReceiptHead);

            prmLedger.ItemAmountTypeCheck = ItemAmountTypeEnum.TranCredit;
            prmLedger.ItemAmountTypeDisplay = ItemAmountTypeEnum.TranCredit;

            List<dcGLGroup> grpReceiptRoot = grpContraCr.Where(c => c.HasParent == false).ToList();
            itmReceiptHead.ChildItemCount = GLReportRBL.AddGLReportItems(grpReceiptRoot, 1, prmLedger,
                                                                grpContraCr, accCashContraCr, cRptListReceipt);


            rcGLReportItem itmReceiptTotal = new rcGLReportItem();
            itmReceiptTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmReceiptTotal.IsBoldName = true;
            itmReceiptTotal.IsBoldAmt = true;
            itmReceiptTotal.ItemName = "Total Receipts";
            itmReceiptTotal.ItemNameDispaly = itmReceiptTotal.ItemName;
            itmReceiptTotal.NumberFormat = prmLedger.NumberFormat;
            itmReceiptTotal.IsUnderlinedName = false;
            //itmOpenTotal.


            itmReceiptTotal.BorderTopAmt = "Solid";
            itmReceiptTotal.BorderWidthTopAmt = "1pt";

            decimal totReceiptAmt = grpReceiptRoot.Sum(c => c.CreditAmt);
            itmReceiptTotal.CloseAmt = totReceiptAmt;
            itmReceiptTotal.ItemAmtDisplay = totReceiptAmt;

            cRptListReceipt.Add(itmReceiptTotal);



            rcGLReportItem itmOpenReceiptTotal = new rcGLReportItem();
            itmOpenReceiptTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmOpenReceiptTotal.IsBoldName = true;
            itmOpenReceiptTotal.IsBoldAmt = true;
            itmOpenReceiptTotal.ItemName = "Total Opening and Receipts";
            itmOpenReceiptTotal.ItemNameDispaly = itmOpenReceiptTotal.ItemName;
            itmOpenReceiptTotal.NumberFormat = prmLedger.NumberFormat;
            itmOpenReceiptTotal.IsUnderlinedName = false;
            //itmOpenTotal.


            itmOpenReceiptTotal.BorderTopAmt = "Solid";
            itmOpenReceiptTotal.BorderWidthTopAmt = "1pt";

            decimal totOpenReceiptAmt = totOpenAmt + totReceiptAmt;
            itmOpenReceiptTotal.CloseAmt = totOpenReceiptAmt;
            itmOpenReceiptTotal.ItemAmtDisplay = totOpenReceiptAmt;

            //cRptListReceipt.Add(itmReceiptTotal);




            //Payment
            List<rcGLReportItem> cRptListPayment = new List<rcGLReportItem>();

            rcGLReportItem itmPaymentHead = new rcGLReportItem();
            itmPaymentHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmPaymentHead.IsBoldName = true;
            itmPaymentHead.IsBoldAmt = true;
            itmPaymentHead.ItemName = "Payments";
            itmPaymentHead.ItemNameDispaly = itmPaymentHead.ItemName;
            itmPaymentHead.NumberFormat = prmLedger.NumberFormat;
            itmPaymentHead.IsUnderlinedName = true;

            cRptListPayment.Add(itmPaymentHead);

            prmLedger.ItemAmountTypeCheck = ItemAmountTypeEnum.TranDebit;
            prmLedger.ItemAmountTypeDisplay = ItemAmountTypeEnum.TranDebit;
            List<dcGLGroup> grpPaymentRoot = grpContraDb.Where(c => c.HasParent == false).ToList();
            itmPaymentHead.ChildItemCount = GLReportRBL.AddGLReportItems(grpPaymentRoot, 1, prmLedger,
                                                                grpContraDb, accCashContraDb, cRptListPayment);


            rcGLReportItem itmPaymentTotal = new rcGLReportItem();
            itmPaymentTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmPaymentTotal.IsBoldName = true;
            itmPaymentTotal.IsBoldAmt = true;
            itmPaymentTotal.ItemName = "Total Payments";
            itmPaymentTotal.ItemNameDispaly = itmPaymentTotal.ItemName;
            itmPaymentTotal.NumberFormat = prmLedger.NumberFormat;
            itmPaymentTotal.IsUnderlinedName = false;
            //itmOpenTotal.


            itmPaymentTotal.BorderTopAmt = "Solid";
            itmPaymentTotal.BorderWidthTopAmt = "1pt";

            decimal totPaymentAmt = grpPaymentRoot.Sum(c => c.DebitAmt);
            itmPaymentTotal.CloseAmt = totPaymentAmt;
            itmPaymentTotal.ItemAmtDisplay = totPaymentAmt;

            cRptListPayment.Add(itmPaymentTotal);


            ///Close

            List<rcGLReportItem> cRptListClose = new List<rcGLReportItem>();

            rcGLReportItem itmCloseHead = new rcGLReportItem();
            itmCloseHead.ItemType = (int)GLReportItemTypeEnum.System;
            itmCloseHead.IsBoldName = true;
            itmCloseHead.IsBoldAmt = true;
            itmCloseHead.ItemName = "Closing Balance";
            itmCloseHead.ItemNameDispaly = itmOpenHead.ItemName;
            itmCloseHead.NumberFormat = prmLedger.NumberFormat;
            itmCloseHead.IsUnderlinedName = true;

            cRptListClose.Add(itmCloseHead);


            prmLedger.ItemAmountTypeCheck = ItemAmountTypeEnum.ClosingBalance;
            prmLedger.ItemAmountTypeDisplay = ItemAmountTypeEnum.ClosingBalance;
            List<dcGLGroup> grpCloseRoot = grpListCash.Where(c => c.HasParent == false).ToList();

            itmCloseHead.ChildItemCount = GLReportRBL.AddGLReportItems(grpCloseRoot, 1, prmLedger,
                                                                grpBalanceCash, accBalanceCash, cRptListClose);

            ClearNonCashGroupBalance(cRptListClose, grpListCash);

            rcGLReportItem itmCloseTotal = new rcGLReportItem();
            itmCloseTotal.ItemType = (int)GLReportItemTypeEnum.System;
            itmCloseTotal.IsBoldName = true;
            itmCloseTotal.IsBoldAmt = true;
            itmCloseTotal.ItemName = "Total Closing Balance";
            itmCloseTotal.ItemNameDispaly = itmCloseTotal.ItemName;
            itmCloseTotal.NumberFormat = prmLedger.NumberFormat;
            itmCloseTotal.IsUnderlinedName = false;
            //itmOpenTotal.


            itmCloseTotal.BorderTopAmt = "Solid";
            itmCloseTotal.BorderWidthTopAmt = "1pt";

            decimal totCloseAmt = grpOpenRoot.Sum(c => c.CloseAmt);
            itmCloseTotal.CloseAmt = totCloseAmt;
            itmCloseTotal.ItemAmtDisplay = totCloseAmt;

            cRptListClose.Add(itmCloseTotal);

            /////////////////////////////////////////////

            //
            ///ope
            foreach (rcGLReportItem itm in cRptListOpen)
            {
                ResetItemIndent(itm, prmLedger);
                ResetItemBalanceDisplay(itm, prmLedger);
                cRptList.Add(itm);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());


            //Receipt
            foreach (rcGLReportItem itm in cRptListReceipt)
            {
                ResetItemIndent(itm, prmLedger);
                ResetItemBalanceDisplay(itm, prmLedger);
                cRptList.Add(itm);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());


            //ope + Receipt
            cRptList.Add(itmOpenReceiptTotal);
            cRptList.Add(GLReportRBL.CreateBlankItem());



            //Payment
            foreach (rcGLReportItem itm in cRptListPayment)
            {
                ResetItemIndent(itm, prmLedger);
                ResetItemBalanceDisplay(itm, prmLedger);
                cRptList.Add(itm);
            }
            cRptList.Add(GLReportRBL.CreateBlankItem());



            //close
            foreach (rcGLReportItem itm in cRptListClose)
            {
                ResetItemIndent(itm, prmLedger);
                ResetItemBalanceDisplay(itm, prmLedger);
                cRptList.Add(itm);
            }
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

            if (rptItem.ItemAmtDisplay != 0)
            {
                rptItem.ItemAmtPercent = Math.Abs(rptItem.ItemAmtDisplay) / Math.Abs(totAmt);
            }


            if (rptItem.ItemAmtDisplaySub1 != 0)
            {
                rptItem.ItemBalancePercentSub1 = Math.Abs(rptItem.ItemAmtDisplaySub1) / Math.Abs(totAmt);
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


        public static void ClearNonCashGroupBalance(List<rcGLReportItem> rptItems, List<dcGLGroup> grpListCash)
        {
            foreach (rcGLReportItem item in rptItems)
            {
                if (item.ItemType == (int)GLReportItemTypeEnum.Group)
                {
                    dcGLGroup grp = grpListCash.FirstOrDefault(c => c.GLGroupID == item.ItemID);
                    if (grp != null)
                    {
                        if (grp.IsCash == false)
                        {
                            item.ItemDebitAmt = 0;
                            item.ItemCreditAmt = 0;
                            item.ItemAmt = 0;
                            item.ItemBalanceAmt = 0;
                            item.ItemAmtDisplay = 0;

                            //item.ItemDebitAmtSub1
                            item.ItemBalanceAmtSub1 = 0;
                            item.ItemBalanceAmtSub2 = 0;

                            
                            item.ItemAmtDisplaySub1 = 0;
                            item.ItemAmtDisplaySub2 = 0;
                        }
                    } // group not null
                } // group
            } //loop
        }


        //public static void ChangeItemOpenBalanceToClose(List<rcGLReportItem> rptItems)
        //{
        //    foreach (rcGLReportItem item in rptItems)
        //    {
        //        item.CloseDebitAmt = item.OpenDebitAmt; 
        //        item.CloseCreditAmt = item.OpenCreditAmt;                
        //        item.CloseAmt = item.OpenAmt;
        //        item.CloseBalanceAmt = item.OpenBalanceAmt;
        //        item.CloseBalanceDisplay = item.CloseBalanceAmt;
        //        item.CloseBalanceDrCr = item.OpenDrCr;

        //        item.OpenDebitAmt = 0;
        //        item.OpenCreditAmt = 0;
        //        item.OpenAmt = 0;
        //        item.OpenBalanceAmt = 0;
        //        item.OpenDrCr = 0;

        //        item.DebitAmt = 0;
        //        item.CreditAmt = 0;
        //    }
        //}


        public static void ResetItemBalanceDisplay(rcGLReportItem rptItem, clsPrmLedger prmLedger)
        {

            if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
            {
                if (rptItem.ItemAmtDisplay == 0 | rptItem.ChildItemCount == 0)
                {
                    return;
                }


                if (rptItem.ItemType != (int)GLReportItemTypeEnum.System
                     & rptItem.ItemType != (int)GLReportItemTypeEnum.PLItem
                     & rptItem.ItemType != (int)GLReportItemTypeEnum.PLClosing)
                {

                    if (prmLedger.DisplayBalanceLevel == -1)
                    {
                        if (rptItem.ChildItemCount > 0)
                        {
                            rptItem.ItemAmtDisplaySub1 = rptItem.ItemAmtDisplay;
                            rptItem.ItemAmtDisplay = 0;
                        }
                    }
                    else
                    {
                        if (prmLedger.DisplayBalanceLevel != rptItem.ItemLevel)
                        {
                            rptItem.ItemAmtDisplaySub1 = rptItem.ItemAmtDisplay;
                            rptItem.ItemAmtDisplay = 0;
                        }
                    }

                }

                //if (prmLedger.DisplayBalanceLevel == -1)
                //{

                //}
                //else
                //{
                //    if (prmLedger.DisplayBalanceLevel != rptItem.ItemLevel)
                //    {
                //        if (rptItem.ItemType != (int)GLReportItemTypeEnum.System
                //            & rptItem.ItemType != (int)GLReportItemTypeEnum.PLItem
                //            & rptItem.ItemType != (int)GLReportItemTypeEnum.PLClosing)
                //        {
                //            rptItem.CloseBalanceDisplaySub1 = rptItem.CloseBalanceDisplay;
                //            rptItem.CloseBalanceDisplay = 0;

                //        }
                //    }
                //}
            }

        }


        public static List<rcReceiptPayment> GetReceiptPayment(clsPrmLedger prmLedger)
        {
            return GetReceiptPayment(prmLedger, null);
        }

        public static List<rcReceiptPayment> GetReceiptPayment(clsPrmLedger prmLedger, DBContext dc)
        {
            List<rcReceiptPayment> cRptList = new List<rcReceiptPayment>();

            rcReceiptPayment cRpt = new rcReceiptPayment();

            rcGLReportHeader cRptHeader = new rcGLReportHeader();

            dcAccYear year = BLLibrary.AccountingBL.AccYearBL.GetAccYearByID(prmLedger.CompanyID, prmLedger.AccYearID,dc);

            cRptHeader.AccYearID = prmLedger.AccYearID;
            cRptHeader.AccYearName = year.AccYearName;

            cRptHeader.FromDate = prmLedger.FromDate;
            cRptHeader.ToDate = prmLedger.ToDate;

            cRpt.ReceiptPaymentHeader.Add(cRptHeader);
            cRpt.ReceiptPaymentItems = GetReceiptPaymentItems(prmLedger, dc);

            cRptList.Add(cRpt);

            return cRptList;

        }
    }
}
