using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PG.Core;
using PG.Core.Utility;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

using PG.Report.ReportEnums;
using PG.Report.ReportClass.AccountingRC;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Report.ReportRBL.AccountingRBL
{
    public class GLReportRBL
    {
      
        public static void AddGLReportItems(int pGLGroupID, int curLevel, clsPrmLedger prmLedger,
                                            List<dcGLGroup> pGLGroupList, List<dcGLAccount> pGLAccountList,
                                            List<rcGLReportItem> pGLReportItemList)
        {
            

            List<dcGLGroup> list = new List<dcGLGroup>();
            list.Add(pGLGroupList.Single(c => c.GLGroupID == pGLGroupID));

            AddGLReportItems(list, curLevel, prmLedger, pGLGroupList, pGLAccountList, pGLReportItemList);
        }

        public static int AddGLReportItems(List<dcGLGroup> pQueyGroupList, int curLevel, clsPrmLedger prmLedger,
                                            List<dcGLGroup> pGLGroupList, List<dcGLAccount> pGLAccountList,
                                            List<rcGLReportItem> pGLReportItemList)
        {
            int itemCount = 0;
            if (prmLedger.MaxHierarchyLevel != -1 && curLevel > prmLedger.MaxHierarchyLevel)
            {
                return 0;
            }
            switch (prmLedger.OrderBy)
            {
                case AccOrderByEnum.Code:
                    pQueyGroupList = pQueyGroupList.OrderBy(c => c.GLGroupCode).ToList();
                    break;
                case AccOrderByEnum.Name:
                    pQueyGroupList = pQueyGroupList.OrderBy(c => c.GLGroupName).ToList();
                    break;
                case AccOrderByEnum.SLNo:
                    pQueyGroupList = pQueyGroupList.OrderBy(c => c.GLGroupSLNo).ToList();
                    break;
            }
            int workLevel = curLevel;
            decimal grpAmt = 0;
            int childItemCount = 0;
            bool includeLedgers = false;

            foreach (dcGLGroup grp in pQueyGroupList)
            {
                childItemCount = 0;
                includeLedgers = false;
                workLevel = curLevel;
                grpAmt = GetGroupItemAmount(grp, prmLedger.ItemAmountTypeCheck);


             
                if (!prmLedger.IncludeZeroValue && !grp.ShowAlways && grpAmt == 0)
                {
                    //nothing
                }
                else
                {
                    //add groups
                    rcGLReportItem itemG = new rcGLReportItem();
                    if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
                    {
                        grp.GLGroupLevel = workLevel;
                        FillGroupItem(itemG, grp,  prmLedger);
                        pGLReportItemList.Add(itemG);
                        itemCount++;
                    }

                    int nextLevelNo = workLevel + 1;
                    if (grp.ShowAsLedger == false)
                    {
                            childItemCount = ProcessChildGroup(grp, nextLevelNo, prmLedger, pGLGroupList, pGLAccountList, pGLReportItemList);
                            itemG.ChildItemCount = childItemCount;
                    }
                } //addgroup and childs

                if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
                {
                    includeLedgers = true;
                } //show type == groups
                else
                {
                    if (grp.ShowAsLedger)
                    {
                        includeLedgers = false;
                    }
                    else
                    {
                        if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Groups)
                        {
                            if (childItemCount > 0)
                            {
                                includeLedgers = true;
                            }
                        }
                        else
                        {
                            if (childItemCount > 0)
                            {
                                includeLedgers = true;
                            }
                            else
                            {
                                if (prmLedger.MaxHierarchyLevel == -1 || workLevel < prmLedger.MaxHierarchyLevel)
                                {
                                    includeLedgers = true;
                                }
                            }
                        }
                    }//show as ledger
                    
                } //show type

                if (includeLedgers)
                {
                    //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
                    //                            && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

                    List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == grp.GLGroupID).ToList();

                    decimal accAmt = 0;
                    foreach (dcGLAccount acc in accListA)
                    {
                        accAmt = GetAccountItemAmount(acc, prmLedger.ItemAmountTypeCheck);
                        if (!prmLedger.IncludeZeroValue && accAmt == 0)
                        {
                            //
                        }
                        else
                        {
                            rcGLReportItem itemA = new rcGLReportItem();
                            acc.GLAccountLevel = workLevel + 1;
                            FillAccountItem(itemA, acc, prmLedger);
                            pGLReportItemList.Add(itemA);
                            itemCount++;
                        }
                    }
                } //include ledgers
            } //each grp
            return itemCount;
        }





        public static int ProcessChildGroup(dcGLGroup parentGroup, int pLevelNo, clsPrmLedger prmLedger, 
                                                List<dcGLGroup> pGroupList, List<dcGLAccount> pGLAccountList,
                                                    List<rcGLReportItem> pGLReportItemList)
        {
            int itemCount = 0;
            if (prmLedger.MaxHierarchyLevel != -1 && pLevelNo > prmLedger.MaxHierarchyLevel)
            {
                return 0;
            }
            List<dcGLGroup> childGroupList = pGroupList.Where(c => c.GLGroupIDParent == parentGroup.GLGroupID).ToList();
            parentGroup.ChildGroupCount = childGroupList.Count;
            switch (prmLedger.OrderBy)
            {
                case AccOrderByEnum.Code:
                    childGroupList = childGroupList.OrderBy(c => c.GLGroupCode).ToList();
                    break;
                case AccOrderByEnum.Name:
                    childGroupList = childGroupList.OrderBy(c => c.GLGroupName).ToList();
                    break;
                case AccOrderByEnum.SLNo:
                    childGroupList = childGroupList.OrderBy(c => c.GLGroupSLNo).ToList();
                    break;
            }

            int childItemCount = 0;
            decimal grpAmt = 0;
            bool includeLedgers = false;

            foreach (dcGLGroup childGroup in childGroupList)
            {

                childItemCount = 0;
                includeLedgers = false;
                grpAmt = GetGroupItemAmount(childGroup, prmLedger.ItemAmountTypeCheck);
                if (!prmLedger.IncludeZeroValue && !childGroup.ShowAlways && grpAmt == 0)
                {

                }
                else
                {
                    childGroup.GLGroupLevel = pLevelNo;
                    //addgroup
                    rcGLReportItem itemG = new rcGLReportItem();
                    if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
                    {
                        FillGroupItem(itemG, childGroup, prmLedger);
                        itemG.ItemIDParent = parentGroup.GLGroupID;
                        pGLReportItemList.Add(itemG);
                        itemCount++;
                    }

                    if (childGroup.ShowAsLedger == false)
                    {
                        int nextLevelNo = pLevelNo + 1;
                        childItemCount = ProcessChildGroup(childGroup, nextLevelNo, prmLedger, pGroupList, pGLAccountList, pGLReportItemList);
                        itemG.ChildItemCount = childItemCount;
                    }
                }


                if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
                {
                    includeLedgers = true;
                } //show type == groups
                else
                {
                    if (childGroup.ShowAsLedger)
                    {
                        includeLedgers = false;
                    }
                    else
                    {
                        if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Groups)
                        {
                            if (childItemCount > 0)
                            {
                                includeLedgers = true;
                            }
                        }
                        else
                        {
                            if (childItemCount > 0)
                            {
                                includeLedgers = true;
                            }
                            else
                            {
                                if (prmLedger.MaxHierarchyLevel == -1 || pLevelNo < prmLedger.MaxHierarchyLevel)
                                {
                                    includeLedgers = true;
                                }
                            }
                        }
                    } // show as ledger
                } //show type



                if (includeLedgers)
                {
                    //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
                    //                            && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

                    List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == childGroup.GLGroupID).ToList();

                    decimal accAmt = 0;
                    foreach (dcGLAccount acc in accListA)
                    {
                        accAmt = GetAccountItemAmount(acc, prmLedger.ItemAmountTypeCheck);
                        if (!prmLedger.IncludeZeroValue && accAmt == 0)
                        {
                            //
                        }
                        else
                        {
                            rcGLReportItem itemA = new rcGLReportItem();
                            acc.GLAccountLevel = pLevelNo + 1;
                            FillAccountItem(itemA, acc, prmLedger);
                            itemA.ItemIDParent = parentGroup.GLGroupID;
                            pGLReportItemList.Add(itemA);
                            itemCount++;
                        }
                    }
                } //include ledgers


            } //for loop childGrouplist


            //add ledgerData if any
            //bool includeLedgers = false;
            //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
            //                                    && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

           // List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID).ToList();

            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Groups)
            {
                if (childItemCount > 0)
                {
                    includeLedgers = true;
                }


                //if (accListA.Count == 0)
                //{
                //    ///do nothing
                //    includeLedgers = false;
                //}
                //else
                //{
                //    includeLedgers = true;

                //} // IncludeLastGroupAccounts
            } //show type == groups
            else
            {
                includeLedgers = true;
            } //show type groups




            //if (includeLedgers)
            //{
            //    //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
            //    //                            && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

            //    //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID).ToList();

            //    decimal accAmt = 0;
            //    foreach (dcGLAccount acc in accListA)
            //    {
            //        accAmt = GetAccountItemAmount(acc, pItemAmount);
            //        if (!prmLedger.IncludeZeroValue && accAmt == 0)
            //        {
            //            //
            //        }
            //        else
            //        {
            //            rcGLReportItem itemA = new rcGLReportItem();
            //            acc.GLAccountLevel = pLevelNo;
            //            FillAccountItem(itemA, acc, pItemAmount, prmLedger);
            //            pGLReportItemList.Add(itemA);
            //            itemCount++;
            //        }
            //    }
            //} //include ledgers

            return itemCount;
        }




        public static void FillGroupItem(rcGLReportItem item, dcGLGroup grp, clsPrmLedger prmLedger)
        {

            item.ItemType = (int)GLReportItemTypeEnum.Group;

            item.IsBoldName = true;
            item.IsItalicName = false;

            item.IsBoldAmt = false;
            item.IsItalicAmt = false;

            item.ItemID = grp.GLGroupID;
            item.ItemName = grp.GLGroupName;
            item.ItemNameShort = grp.GLGroupNameShort;
            item.ItemCode = grp.GLGroupCode;
            item.ItemSLNo = grp.GLGroupSLNo;
            item.ItemLevel = grp.GLGroupLevel;
            item.ItemNameParent = grp.GLGroupNameParent;
            item.ItemNameParentEffective = grp.GLGroupNameParentEffective;

            item.ItemNameIndent = grp.GLGroupNameIndent;
            item.ItemNameFull = grp.GLGroupNameHierarchy;
            item.ItemIsCash = grp.IsCash;
            item.ItemIsBank = grp.IsBank;
           
            item.ItemNameDispaly = item.ItemNameIndent;

            item.NumberFormat = prmLedger.NumberFormat;

          

            item.OpenDebitAmt = grp.OpenDebitAmt;
            item.OpenCreditAmt = grp.OpenCreditAmt;
            item.OpenAmt = grp.OpenAmt;
            item.OpenDebitBalanceAmt = grp.OpenDebitBalanceAmt;
            item.OpenCreditBalanceAmt = grp.OpenCreditBalanceAmt;

          

            item.OpenBalanceAmt = Math.Abs(item.OpenAmt);
            item.OpenBalanceDrCr = AccHelper.GetDrCrBalanceType(item.OpenAmt, grp.BalanceType);
            item.OpenBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.OpenAmt, grp.BalanceType);

            item.OpenBalanceDisplay = item.OpenAmt < 0 ? -Math.Abs(item.OpenAmt) : Math.Abs(item.OpenAmt);
            item.OpenBalanceText = item.OpenAmt.ToString(prmLedger.NumberFormat);

            item.TranDebitAmt = grp.DebitAmt;
            item.TranCreditAmt = grp.CreditAmt;
            item.TranAmt = grp.TranAmt;
            item.TranDebitBalanceAmt = grp.TranDebitBalanceAmt;
            item.TranCreditBalanceAmt = grp.TranCreditBalanceAmt;

            item.TranBalanceAmt = Math.Abs(item.TranAmt);
            item.TranBalanceDrCr = item.TranAmt >= 0 ? 0 : 1;
            item.TranBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.TranAmt, grp.BalanceType);
            item.TranBalanceDisplay = item.TranAmt < 0 ? -Math.Abs(item.TranAmt) : Math.Abs(item.TranAmt);


            item.CloseDebitAmt = grp.CloseDebitAmt;
            item.CloseCreditAmt = grp.CloseCreditAmt;
            item.CloseAmt = grp.CloseAmt;
            item.CloseDebitBalanceAmt = grp.CloseDebitBalanceAmt;
            item.CloseCreditBalanceAmt = grp.CloseCreditBalanceAmt;

            item.CloseBalanceAmt = Math.Abs(item.CloseAmt);
            item.CloseBalanceDrCr = AccHelper.GetDrCrBalanceType(item.CloseAmt, grp.BalanceType);
            item.CloseBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.CloseAmt, grp.BalanceType);

            item.CloseBalanceText = item.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);


            if (grp.BalanceType == (int)DebitCreditEnum.Debit)
            {
                item.CloseBalanceDisplay = item.CloseAmt < 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            }
            else
            {
                item.CloseBalanceDisplay = item.CloseAmt > 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            }

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



        public static void FillAccountItem(rcGLReportItem item, dcGLAccount acc, clsPrmLedger prmLedger)
        {
            GLAccountTypeEnum accType = (GLAccountTypeEnum)acc.GLAccountTypeID;

            switch (accType)
            {
                case GLAccountTypeEnum.NormalAccount:
                    item.ItemType = (int)GLReportItemTypeEnum.Ledger;
                    break;
                case GLAccountTypeEnum.ControlAccount:
                    item.ItemType = (int)GLReportItemTypeEnum.LedgerControl;
                    break;
                case GLAccountTypeEnum.SubAccount:
                    item.ItemType = (int)GLReportItemTypeEnum.LedgerSub;
                    break;
            }

            item.IsBoldName = false;
            item.IsItalicName = true;

            item.IsBoldAmt = false;
            item.IsItalicAmt = false;

            item.ItemID = acc.GLAccountID;
            item.ItemName = acc.GLAccountName;
            item.ItemCode = acc.GLAccountCode;
            item.ItemSLNo = acc.GLAccountSLNo;
            item.ItemLevel = acc.GLAccountLevel;
            item.ItemNameParent = acc.GLGroupName;
            //item.ItemNameIndent = acc.
            //item.ItemNameFull = grp.AccGLGroupNameHierarchy;

            item.ItemNameIndent = acc.GLAccountNameIndent;

            item.ItemNameDispaly = item.ItemName;

            item.NumberFormat = prmLedger.NumberFormat;
            item.GroupledgerShowType = prmLedger.GroupLedgerShowType.ToString();

            item.OpenDebitAmt = acc.OpenDebitAmt;
            item.OpenCreditAmt = acc.OpenCreditAmt;
            item.OpenAmt = acc.OpenAmt;
            item.OpenDebitBalanceAmt = acc.OpenDebitBalanceAmt;
            item.OpenCreditBalanceAmt = acc.OpenCreditBalanceAmt;
            item.OpenBalanceAmt = Math.Abs(item.OpenAmt);
            //TODO Change Done
            //New 
            item.OpenAmtDateRange = acc.OpenAmtDateRange;
            item.OpenBalanceAmtDateRange = Math.Abs(item.OpenAmtDateRange);

            item.OpenBalanceDrCrDateRange = AccHelper.GetDrCrBalanceType(item.OpenAmtDateRange, acc.BalanceType);
            item.OpenBalanceDrCrTextDateRange = AccHelper.GetDrCrBalanceText(item.OpenAmtDateRange, acc.BalanceType); 
            //
            item.OpenBalanceDrCr = AccHelper.GetDrCrBalanceType(item.OpenAmt, acc.BalanceType);
            item.OpenBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.OpenAmt, acc.BalanceType); 

            item.OpenBalanceDisplay = item.OpenAmt < 0 ? -Math.Abs(item.OpenAmt) : Math.Abs(item.OpenAmt);
            item.OpenBalanceText = item.OpenAmt.ToString(prmLedger.NumberFormat);


            item.TranDebitAmt = acc.DebitAmt;
            item.TranCreditAmt = acc.CreditAmt;
            item.TranAmt = acc.TranAmt;
            item.TranDebitBalanceAmt = acc.TranDebitBalanceAmt;
            item.TranCreditBalanceAmt = acc.TranCreditBalanceAmt;

            item.TranBalanceAmt = Math.Abs(item.TranAmt);
            item.TranBalanceDrCr = item.TranAmt >= 0 ? 0 : 1;
            item.TranBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.TranAmt, acc.BalanceType);
            item.TranBalanceDisplay = item.TranAmt < 0 ? -Math.Abs(item.TranAmt) : Math.Abs(item.TranAmt);




            item.CloseDebitAmt = acc.CloseDebitAmt;
            item.CloseCreditAmt = acc.CloseCreditAmt;
            item.CloseAmt = acc.CloseAmt;
            item.CloseDebitBalanceAmt = acc.CloseDebitBalanceAmt;
            item.CloseCreditBalanceAmt = acc.CloseCreditBalanceAmt;
            item.CloseBalanceAmt = Math.Abs(item.CloseAmt);
            item.CloseBalanceDrCr = AccHelper.GetDrCrBalanceType(item.CloseAmt, acc.BalanceType);
            item.CloseBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.CloseAmt, acc.BalanceType);

            item.CloseBalanceText = item.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);

            if (acc.BalanceType == (int)DebitCreditEnum.Debit)
            {
                item.CloseBalanceDisplay = item.CloseAmt < 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            }
            else
            {
                item.CloseBalanceDisplay = item.CloseAmt > 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            }

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

        public static decimal GetGroupItemAmount(dcGLGroup grp, ItemAmountTypeEnum pItemAmountType)
        {
            decimal amt = 0;
            switch (pItemAmountType)
            {
                case ItemAmountTypeEnum.OpeningDebit:
                    amt = grp.OpenDebitAmt;
                    break;

                case ItemAmountTypeEnum.OpeningCredit:
                    amt = grp.OpenCreditAmt;
                    break;

                case ItemAmountTypeEnum.OpeningBalance:
                    amt = grp.OpenAmt;
                    break;

                case ItemAmountTypeEnum.TranDebit:
                    amt = grp.DebitAmt;
                    break;

                case ItemAmountTypeEnum.TranCredit:
                    amt = grp.CreditAmt;
                    break;
                case ItemAmountTypeEnum.TranBalance:
                    amt = grp.TranAmt;
                    break;

                case ItemAmountTypeEnum.ClosingDebit:
                    amt = grp.CloseDebitAmt;
                    break;
                case ItemAmountTypeEnum.ClosingCredit:
                    amt = grp.CloseCreditAmt;
                    break;

                case ItemAmountTypeEnum.ClosingBalance:
                    amt = grp.CloseAmt;
                    break;

                //case ItemAmountTypeEnum.CheckAny:
                //    amt = grp.OpenDebitAmt;
                //    break;

            }
            return amt;
        }

        public static decimal GetAccountItemAmount(dcGLAccount acc, ItemAmountTypeEnum pItemAmountType)
        {
            decimal amt = 0;
            switch (pItemAmountType)
            {
                case ItemAmountTypeEnum.OpeningDebit:
                    amt = acc.OpenDebitAmt;
                    break;

                case ItemAmountTypeEnum.OpeningCredit:
                    amt = acc.OpenCreditAmt;
                    break;

                case ItemAmountTypeEnum.OpeningBalance:
                    amt = acc.OpenAmt;
                    break;

                case ItemAmountTypeEnum.TranDebit:
                    amt = acc.DebitAmt;
                    break;

                case ItemAmountTypeEnum.TranCredit:
                    amt = acc.CreditAmt;
                    break;
                case ItemAmountTypeEnum.TranBalance:
                    amt = acc.TranAmt;
                    break;

                case ItemAmountTypeEnum.ClosingDebit:
                    amt = acc.CloseDebitAmt;
                    break;
                case ItemAmountTypeEnum.ClosingCredit:
                    amt = acc.CloseCreditAmt;
                    break;

                case ItemAmountTypeEnum.ClosingBalance:
                    amt = acc.CloseAmt;
                    break;
            }
            return amt;
        }


        
        public static rcGLReportItem CreateBlankItem()
        {
            return CreateItem(GLReportItemTypeEnum.Blank, string.Empty);
        }
        public static rcGLReportItem CreateItem(GLReportItemTypeEnum itemType, string strName)
        {
            rcGLReportItem item = new rcGLReportItem();
            item.ItemType = (int)itemType;
            item.ItemName = strName;
            item.NumberFormat = "#;#;#";
            return item;
        }


        public static void RemoveItemBalance(rcGLReportItem item)
        {
            
            
            item.OpenAmtDateRange = 0;


            item.OpenAmt = 0;
            item.OpenDebitAmt = 0;
            item.OpenCreditAmt = 0;
            item.OpenBalanceAmt = 0;
            item.OpenBalanceDrCr = 0;
            item.OpenBalanceDrCrText = string.Empty;

            item.OpenBalanceText = string.Empty;
            item.OpenBalanceDisplay = 0;

            item.TranDebitAmt = 0;
            item.TranCreditAmt = 0;

            item.CloseAmt = 0;
            item.CloseDebitAmt = 0;
            item.CloseCreditAmt =  0;

            item.CloseBalanceAmt = 0;
            item.CloseBalanceDrCr = 0;
            item.CloseBalanceDrCrText = string.Empty;

            item.CloseBalanceDisplay = 0;
            item.CloseBalanceText = string.Empty;
            item.NumberFormat = "#;#;#";
        }

    }
}
