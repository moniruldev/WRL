using PG.BLLibrary.InventoryBL;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.InventoryDC;
using PG.Report.ReportClass.InventoryRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportRBL.InventoryRBL
{
    public static class INV_Item_GroupRBL
    {
        public static List<rcINV_ITEM_GROUP> ItemGroupList(clsPrmInventory prm, DBContext dc)
        {
            List<dcINV_ITEM_GROUP> itemGroupList = null;
            itemGroupList = INV_ITEM_GROUPBL.GetItemGroupList();
            if(itemGroupList!=null && itemGroupList.Any())
            {
                itemGroupList = INV_ITEM_GROUPBL.FormatItemGroup(prm, itemGroupList);
                return ConvertDBItemGroupToRCItemGroup(itemGroupList, new List<rcINV_ITEM_GROUP>());
            }
            else
            {
                return new List<rcINV_ITEM_GROUP>();
            }
        }

        public static List<rcINV_ITEM_GROUP> ConvertDBItemGroupToRCItemGroup(List<dcINV_ITEM_GROUP> dbItemGroup, List<rcINV_ITEM_GROUP> rcItemGroup)
        {
            foreach (var fromObj in dbItemGroup)
            {
                rcINV_ITEM_GROUP toObj = new rcINV_ITEM_GROUP();
                Helper.CopyPropertyValueByName(fromObj, toObj);
                rcItemGroup.Add(toObj);

            }
            return rcItemGroup;
        }


        public static List<rcItemGroupReport> GetITemHerarchyList(clsPrmInventory prm)
        {
            return GetITemHerarchyList(prm, null);
        }

        public static List<rcItemGroupReport> GetITemHerarchyList(clsPrmInventory prm, DBContext dc)
        {

            List<rcItemGroupReport> cRptList = new List<rcItemGroupReport>();
            List<dcINV_ITEM_GROUP> itemGroupList = null;
            itemGroupList = INV_ITEM_GROUPBL.GetItemGroupList();
            itemGroupList = INV_ITEM_GROUPBL.FormatItemGroup(prm, itemGroupList);
            List<dcINV_ITEM_MASTER> itemList = INV_ITEM_MASTERBL.GetItemList1(prm, dc);
            //new
            //List<dcGLAccount> accBalance = GLAccountBL.GetAccountBalance(prmLedger, dc);
            //List<dcGLGroup> grpBalance = GLGroupBL.GetGroupBalance(prmLedger, grpList, accBalance, dc);


            List<dcINV_ITEM_GROUP> grpRootList = itemGroupList.Where(c => c.ITEM_GROUP_ID_PARENT == 0).ToList();

            //int assetClassID = (int)GLClassEnum.Assets;
            //int liabilityClassID = (int)GLClassEnum.Liabilities;

            //int incomeClassID = (int)GLClassEnum.Income;
            //int expenseClassID = (int)GLClassEnum.Expense;
            //int PLClassID = (int)GLClassEnum.ProfitAndLoss;

            //prmLedger.IncludeGLClass = true;
            //prmLedger.IncludeRootGLGroup = true;
            prm.MaxHierarchyLevel = -1;
            //prmLedger.IncludeZeroValue = true;

            //PG.Report.AccountingRBL.GLReportBL.AddGLReportItems

            //List<rcGLReportItem> cRptListItems = new List<rcGLReportItem>();

            


            int defLevel = 0;
            if (prm.IncludeItemClass)
            {
                defLevel = 1;
                if (prm.MaxHierarchyLevel != -1)
                {
                    prm.MaxHierarchyLevel += 1;
                }
            }

            rcItemGroupReport itmAssetsHead = new rcItemGroupReport();
            //itmAssetsHead.ItemType = (int)GLReportItemTypeEnum.System;
           
            itmAssetsHead.ITEM_NAME = itmAssetsHead.ITEM_NAME;
            itmAssetsHead.ItemNameDispaly = itmAssetsHead.ITEM_NAME;
            //itmAssetsHead.IsUnderlinedName = false;
            cRptList.Add(itmAssetsHead);
            //List<dcINV_ITEM_GROUP> grpAssetsRoot = itemGroupList.Where(c => c.c == assetClassID && c.GLGroupIDParent == 0).ToList();
            //GLReportRBL.AddGLReportItems(grpAssetsRoot, defLevel, prmLedger, grpBalance, accBalance, cRptListAst);

            //GLReportRBL.AddGLReportItems(grpRootList, defLevel, prmLedger, grpList, accList, cRptList);
            AddGLReportItems(itemGroupList, defLevel, prm, itemGroupList, itemList, cRptList);

            



            //prmLedger.InsertBlankBetweenGLClass = prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers;

            //GLReportRBL.AddGLReportItems(grpRootList, defLevel, prmLedger, grpList, accList, cRptList);

            //if (prm.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            //{
            foreach (rcItemGroupReport itm in cRptList)
            {
                itm.ItemLevel = 0;
                itm.ITEM_NAME = itm.ITEM_NAME;
            }
            switch (prm.OrderBy)
            {
                case InventoryOrderByEnum.Code:
                    cRptList = cRptList.OrderBy(c => c.ITEM_CODE).ToList();
                    break;
                case InventoryOrderByEnum.Name:
                    cRptList = cRptList.OrderBy(c => c.ITEM_NAME).ToList();
                    break;
                //case AccOrderByEnum.SLNo:
                //    cRptList = cRptList.OrderBy(c => c.sl).ToList();
                //    break;
            }
            //}

            return cRptList;

        }


        public static int AddGLReportItems(List<dcINV_ITEM_GROUP> pQueyGroupList, int curLevel, clsPrmInventory prmLedger,
                                           List<dcINV_ITEM_GROUP> pGLGroupList, List<dcINV_ITEM_MASTER> pGLAccountList,
                                           List<rcItemGroupReport> pGLReportItemList)
        {
            int itemCount = 0;
            if (prmLedger.MaxHierarchyLevel != -1 && curLevel > prmLedger.MaxHierarchyLevel)
            {
                return 0;
            }
            switch (prmLedger.OrderBy)
            {
                case InventoryOrderByEnum.Code:
                    pQueyGroupList = pQueyGroupList.OrderBy(c => c.ITEM_GROUP_CODE).ToList();
                    break;
                case InventoryOrderByEnum.Name:
                    pQueyGroupList = pQueyGroupList.OrderBy(c => c.ITEM_GROUP_NAME).ToList();
                    break;
                case InventoryOrderByEnum.SLNo:
                    pQueyGroupList = pQueyGroupList.OrderBy(c => c.ITEM_GROUP_SLNO).ToList();
                    break;
            }
            int workLevel = curLevel;
            decimal grpAmt = 0;
            int childItemCount = 0;
            bool includeLedgers = false;

            foreach (dcINV_ITEM_GROUP grp in pQueyGroupList)
            {
                childItemCount = 0;
                includeLedgers = false;
                workLevel = curLevel;
                //grpAmt = GetGroupItemAmount(grp, prmLedger.ItemAmountTypeCheck);



                //if (!prmLedger.IncludeZeroValue && !grp.ShowAlways && grpAmt == 0)
                //{
                //    //nothing
                //}
                //else
                //{
                    //add groups
                    rcItemGroupReport itemG = new rcItemGroupReport();
                    //if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
                    //{
                        grp.ITEM_GROUP_LEVEL_CURRENT = workLevel;
                        FillGroupItem(itemG, grp, prmLedger);
                        pGLReportItemList.Add(itemG);
                        itemCount++;
                    //}

                    int nextLevelNo = workLevel + 1;
                    //if (grp.ShowAsLedger == false)
                    //{
                        childItemCount = ProcessChildGroup(grp, nextLevelNo, prmLedger, pGLGroupList, pGLAccountList, pGLReportItemList);
                        itemG.ChildItemCount = childItemCount;
                    //}
                //} //addgroup and childs

                //if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
                //{
                //    includeLedgers = true;
                //} //show type == groups
                //else
                //{
                //    if (grp.ShowAsLedger)
                //    {
                //        includeLedgers = false;
                //    }
                //    else
                //    {
                //        if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Groups)
                //        {
                //            if (childItemCount > 0)
                //            {
                //                includeLedgers = true;
                //            }
                //        }
                //        else
                //        {
                //            if (childItemCount > 0)
                //            {
                //                includeLedgers = true;
                //            }
                //            else
                //            {
                //                if (prmLedger.MaxHierarchyLevel == -1 || workLevel < prmLedger.MaxHierarchyLevel)
                //                {
                //                    includeLedgers = true;
                //                }
                //            }
                //        }
                //    }//show as ledger

                //} //show type

                if (includeLedgers)
                {
                    //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
                    //                            && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

                    List<dcINV_ITEM_MASTER> accListA = pGLAccountList.Where(c => c.ITEM_GROUP_ID == grp.ITEM_GROUP_ID).ToList();

                    decimal accAmt = 0;
                    foreach (dcINV_ITEM_MASTER acc in accListA)
                    {
                        //accAmt = GetAccountItemAmount(acc, prmLedger.ItemAmountTypeCheck);
                        //if (!prmLedger.IncludeZeroValue && accAmt == 0)
                        //{
                        //    //
                        //}
                        //else
                        //{
                        rcItemGroupReport itemA = new rcItemGroupReport();
                            acc.GLAccountLevel = workLevel + 1;
                            FillAccountItem(itemA, acc, prmLedger);
                            pGLReportItemList.Add(itemA);
                            itemCount++;
                        //}
                    }
                } //include ledgers
            } //each grp
            return itemCount;
        }





        public static int ProcessChildGroup(dcINV_ITEM_GROUP parentGroup, int pLevelNo, clsPrmInventory prmLedger,
                                                List<dcINV_ITEM_GROUP> pGroupList, List<dcINV_ITEM_MASTER> pGLAccountList,
                                                    List<rcItemGroupReport> pGLReportItemList)
        {
            int itemCount = 0;
            if (prmLedger.MaxHierarchyLevel != -1 && pLevelNo > prmLedger.MaxHierarchyLevel)
            {
                return 0;
            }
            List<dcINV_ITEM_GROUP> childGroupList = pGroupList.Where(c => c.ITEM_GROUP_ID_PARENT == parentGroup.ITEM_GROUP_ID).ToList();
            parentGroup.CHILD_GROUP_COUNT = childGroupList.Count;
            switch (prmLedger.OrderBy)
            {
                case InventoryOrderByEnum.Code:
                    childGroupList = childGroupList.OrderBy(c => c.ITEM_GROUP_CODE).ToList();
                    break;
                case InventoryOrderByEnum.Name:
                    childGroupList = childGroupList.OrderBy(c => c.ITEM_GROUP_NAME).ToList();
                    break;
                case InventoryOrderByEnum.SLNo:
                    childGroupList = childGroupList.OrderBy(c => c.ITEM_GROUP_SLNO).ToList();
                    break;
            }

            int childItemCount = 0;
            decimal grpAmt = 0;
            bool includeLedgers = false;

            foreach (dcINV_ITEM_GROUP childGroup in childGroupList)
            {

                childItemCount = 0;
                includeLedgers = false;
                //grpAmt = GetGroupItemAmount(childGroup, prmLedger.ItemAmountTypeCheck);
                //if (!prmLedger.IncludeZeroValue && !childGroup.ShowAlways && grpAmt == 0)
                //{

                //}
                //else
                //{
                    childGroup.ITEM_GROUP_LEVEL = pLevelNo;
                    //addgroup
                    rcItemGroupReport itemG = new rcItemGroupReport();
                    //if (prmLedger.GroupLedgerShowType != GroupsLedgerShowEnum.Ledgers)
                    //{
                        FillGroupItem(itemG, childGroup, prmLedger);
                        itemG.ItemGroupParentID = parentGroup.ITEM_GROUP_ID_PARENT;
                        pGLReportItemList.Add(itemG);
                        itemCount++;
                    //}

                    //if (childGroup.ShowAsLedger == false)
                    //{
                        int nextLevelNo = pLevelNo + 1;
                        childItemCount = ProcessChildGroup(childGroup, nextLevelNo, prmLedger, pGroupList, pGLAccountList, pGLReportItemList);
                        itemG.ChildItemCount = childItemCount;
                    //}
                //}


                //if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
                //{
                //    includeLedgers = true;
                //} //show type == groups
                //else
                //{
                //    if (childGroup.ShowAsLedger)
                //    {
                //        includeLedgers = false;
                //    }
                //    else
                //    {
                //        if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Groups)
                //        {
                //            if (childItemCount > 0)
                //            {
                //                includeLedgers = true;
                //            }
                //        }
                //        else
                //        {
                //            if (childItemCount > 0)
                //            {
                //                includeLedgers = true;
                //            }
                //            else
                //            {
                //                if (prmLedger.MaxHierarchyLevel == -1 || pLevelNo < prmLedger.MaxHierarchyLevel)
                //                {
                //                    includeLedgers = true;
                //                }
                //            }
                //        }
                //    } // show as ledger
                //} //show type



                //if (includeLedgers)
                //{
                    //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
                    //                            && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

                    List<dcINV_ITEM_MASTER> accListA = pGLAccountList.Where(c => c.ITEM_GROUP_ID == childGroup.ITEM_GROUP_ID).ToList();

                    decimal accAmt = 0;
                    foreach (dcINV_ITEM_MASTER acc in accListA)
                    {
                        //accAmt = GetAccountItemAmount(acc, prmLedger.ItemAmountTypeCheck);
                        //if (!prmLedger.IncludeZeroValue && accAmt == 0)
                        //{
                        //    //
                        //}
                        //else
                        //{
                            rcItemGroupReport itemA = new rcItemGroupReport();
                            acc.GLAccountLevel = pLevelNo + 1;
                            FillAccountItem(itemA, acc, prmLedger);
                            itemA.ItemGroupParentID = parentGroup.ITEM_GROUP_ID_PARENT;
                            pGLReportItemList.Add(itemA);
                            itemCount++;
                        //}
                    }
                //} //include ledgers


            } //for loop childGrouplist


            //add ledgerData if any
            //bool includeLedgers = false;
            //List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID
            //                                    && c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();

            // List<dcGLAccount> accListA = pGLAccountList.Where(c => c.GLGroupID == parentGroup.GLGroupID).ToList();

            //if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Groups)
            //{
            //    if (childItemCount > 0)
            //    {
            //        includeLedgers = true;
            //    }


            //    //if (accListA.Count == 0)
            //    //{
            //    //    ///do nothing
            //    //    includeLedgers = false;
            //    //}
            //    //else
            //    //{
            //    //    includeLedgers = true;

            //    //} // IncludeLastGroupAccounts
            //} //show type == groups
            //else
            //{
            //    includeLedgers = true;
            //} //show type groups




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
        public static void FillAccountItem(rcItemGroupReport item, dcINV_ITEM_MASTER acc, clsPrmInventory prmLedger)
        {
            //GLAccountTypeEnum accType = (GLAccountTypeEnum)acc.GLAccountTypeID;

            //switch (accType)
            //{
            //    case GLAccountTypeEnum.NormalAccount:
            //        item.ItemType = (int)GLReportItemTypeEnum.Ledger;
            //        break;
            //    case GLAccountTypeEnum.ControlAccount:
            //        item.ItemType = (int)GLReportItemTypeEnum.LedgerControl;
            //        break;
            //    case GLAccountTypeEnum.SubAccount:
            //        item.ItemType = (int)GLReportItemTypeEnum.LedgerSub;
            //        break;
            //}

            item.IsBoldName = false;
            item.IsItalicName = true;

            item.IsBoldAmt = false;
            item.IsItalicAmt = false;

            item.ItemID = acc.ITEM_ID;
            item.ItemName = acc.ITEM_NAME;
            item.ItemCode = acc.ITEM_CODE;
            item.ItemSLNo = acc.ITEM_SLNO;
            item.ItemLevel = acc.GLAccountLevel;
            item.ItemNameParent = acc.ITEM_GROUP_NAME;
            //item.ItemNameIndent = acc.
            item.ItemNameFull = acc.ITEM_GROUP_NAME;;

            item.ItemNameIndent = acc.ITEM_NAME;

            item.ItemNameDispaly = acc.ITEM_NAME;

            item.NumberFormat = prmLedger.NumberFormat;
            //item.GroupledgerShowType = prmLedger.GroupLedgerShowType.ToString();

            //item.OpenDebitAmt = acc.OpenDebitAmt;
            //item.OpenCreditAmt = acc.OpenCreditAmt;
            //item.OpenAmt = acc.OpenAmt;
            //item.OpenDebitBalanceAmt = acc.OpenDebitBalanceAmt;
            //item.OpenCreditBalanceAmt = acc.OpenCreditBalanceAmt;
            //item.OpenBalanceAmt = Math.Abs(item.OpenAmt);
            //TODO Change Done
            //New 
            //item.OpenAmtDateRange = acc.OpenAmtDateRange;
            //item.OpenBalanceAmtDateRange = Math.Abs(item.OpenAmtDateRange);

            //item.OpenBalanceDrCrDateRange = AccHelper.GetDrCrBalanceType(item.OpenAmtDateRange, acc.BalanceType);
            //item.OpenBalanceDrCrTextDateRange = AccHelper.GetDrCrBalanceText(item.OpenAmtDateRange, acc.BalanceType);
            ////
            //item.OpenBalanceDrCr = AccHelper.GetDrCrBalanceType(item.OpenAmt, acc.BalanceType);
            //item.OpenBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.OpenAmt, acc.BalanceType);

            //item.OpenBalanceDisplay = item.OpenAmt < 0 ? -Math.Abs(item.OpenAmt) : Math.Abs(item.OpenAmt);
            //item.OpenBalanceText = item.OpenAmt.ToString(prmLedger.NumberFormat);


            //item.TranDebitAmt = acc.DebitAmt;
            //item.TranCreditAmt = acc.CreditAmt;
            //item.TranAmt = acc.TranAmt;
            //item.TranDebitBalanceAmt = acc.TranDebitBalanceAmt;
            //item.TranCreditBalanceAmt = acc.TranCreditBalanceAmt;

            //item.TranBalanceAmt = Math.Abs(item.TranAmt);
            //item.TranBalanceDrCr = item.TranAmt >= 0 ? 0 : 1;
            //item.TranBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.TranAmt, acc.BalanceType);
            //item.TranBalanceDisplay = item.TranAmt < 0 ? -Math.Abs(item.TranAmt) : Math.Abs(item.TranAmt);




            //item.CloseDebitAmt = acc.CloseDebitAmt;
            //item.CloseCreditAmt = acc.CloseCreditAmt;
            //item.CloseAmt = acc.CloseAmt;
            //item.CloseDebitBalanceAmt = acc.CloseDebitBalanceAmt;
            //item.CloseCreditBalanceAmt = acc.CloseCreditBalanceAmt;
            //item.CloseBalanceAmt = Math.Abs(item.CloseAmt);
            //item.CloseBalanceDrCr = AccHelper.GetDrCrBalanceType(item.CloseAmt, acc.BalanceType);
            //item.CloseBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.CloseAmt, acc.BalanceType);

            //item.CloseBalanceText = item.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);

            //if (acc.BalanceType == (int)DebitCreditEnum.Debit)
            //{
            //    item.CloseBalanceDisplay = item.CloseAmt < 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            //}
            //else
            //{
            //    item.CloseBalanceDisplay = item.CloseAmt > 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            //}

            //switch (prmLedger.ItemAmountTypeDisplay)
            //{

            //    case ItemAmountTypeEnum.OpeningDebit:
            //        item.ItemDebitAmt = item.OpenDebitAmt;
            //        item.ItemCreditAmt = 0;
            //        item.ItemDebitBalanceAmt = item.OpenDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = 0;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.OpeningCredit:
            //        item.ItemDebitAmt = 0;
            //        item.ItemCreditAmt = item.OpenCreditAmt;
            //        item.ItemDebitBalanceAmt = 0;
            //        item.ItemCreditBalanceAmt = item.OpenCreditBalanceAmt;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.OpeningBalance:
            //        item.ItemDebitAmt = item.OpenDebitAmt;
            //        item.ItemCreditAmt = item.OpenDebitAmt;
            //        item.ItemAmt = item.OpenAmt;
            //        item.ItemDebitBalanceAmt = item.OpenDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = item.OpenDebitBalanceAmt;
            //        item.ItemBalanceAmt = item.OpenBalanceAmt;
            //        item.ItemBalanceDrCr = item.OpenBalanceDrCr;
            //        item.ItemBalanceDrCrText = item.OpenBalanceDrCrText;
            //        item.ItemAmtDisplay = item.OpenBalanceDisplay;
            //        item.ItemAmtDisplayText = item.OpenBalanceText;
            //        break;



            //    case ItemAmountTypeEnum.TranDebit:
            //        item.ItemDebitAmt = item.TranDebitAmt;
            //        item.ItemCreditAmt = 0;

            //        item.ItemDebitBalanceAmt = item.TranDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = 0;


            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.TranCredit:
            //        item.ItemDebitAmt = 0;
            //        item.ItemCreditAmt = item.TranCreditAmt;

            //        item.ItemDebitBalanceAmt = 0;
            //        item.ItemCreditBalanceAmt = item.TranCreditBalanceAmt;

            //        item.ItemAmt = item.TranCreditAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 1;
            //        item.ItemBalanceDrCrText = "Cr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.TranBalance:
            //        item.ItemDebitAmt = item.TranDebitAmt;
            //        item.ItemCreditAmt = item.TranCreditAmt;
            //        item.ItemAmt = item.TranAmt;
            //        item.ItemDebitBalanceAmt = item.TranDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = item.TranDebitBalanceAmt;
            //        item.ItemBalanceAmt = item.TranBalanceAmt;
            //        item.ItemBalanceDrCr = item.OpenBalanceDrCr;
            //        item.ItemBalanceDrCrText = item.OpenBalanceDrCrText;
            //        item.ItemAmtDisplay = item.TranBalanceDisplay;
            //        item.ItemAmtDisplayText = item.OpenBalanceText;
            //        break;

            //    case ItemAmountTypeEnum.ClosingDebit:
            //        item.ItemDebitAmt = item.CloseDebitAmt;
            //        item.ItemCreditAmt = 0;
            //        item.ItemDebitBalanceAmt = item.CloseDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = 0;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.ClosingCredit:
            //        item.ItemDebitAmt = 0;
            //        item.ItemCreditAmt = item.CloseCreditAmt;
            //        item.ItemDebitBalanceAmt = 0;
            //        item.ItemCreditBalanceAmt = item.CloseCreditBalanceAmt;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.CheckAny:
            //    case ItemAmountTypeEnum.ClosingBalance:
            //        item.ItemDebitAmt = item.CloseDebitAmt;
            //        item.ItemCreditAmt = item.CloseCreditAmt;
            //        item.ItemAmt = item.CloseAmt;
            //        item.ItemDebitBalanceAmt = item.CloseDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = item.CloseCreditBalanceAmt;
            //        item.ItemBalanceAmt = item.CloseBalanceAmt;
            //        item.ItemBalanceDrCr = item.CloseBalanceDrCr;
            //        item.ItemBalanceDrCrText = item.CloseBalanceDrCrText;
            //        item.ItemAmtDisplay = item.CloseBalanceDisplay;
            //        item.ItemAmtDisplayText = item.CloseBalanceText;
            //        break;
            //}


        }

        public static void FillGroupItem(rcItemGroupReport item, dcINV_ITEM_GROUP grp, clsPrmInventory prmLedger)
        {

            //item.ItemType = (int)GLReportItemTypeEnum.Group;

            item.IsBoldName = true;
            item.IsItalicName = false;

            item.IsBoldAmt = false;
            item.IsItalicAmt = false;

            item.ItemID = grp.ITEM_GROUP_ID;
            item.ItemName = grp.ITEM_GROUP_NAME;
            //item.ItemNameShort = grp.;
            item.ItemCode = grp.ITEM_GROUP_CODE;
            item.ItemSLNo = grp.ITEM_GROUP_SLNO;
            item.ItemLevel = grp.ITEM_GROUP_LEVEL;
            item.ItemNameParent = grp.ITEM_GROUP_NAME_PARENT;
            //item.ItemNameParentEffective = grp.;
            item.ItemNameIndent = grp.ITEM_GROUP_NAME;
            item.ItemNameFull = grp.ITEM_GROUP_NAME;
            //item.ItemNameIndent = grp.;
            //item.ItemNameFull = grp.;
            //item.ItemIsCash = grp.IsCash;
            //item.ItemIsBank = grp.IsBank;

            item.ItemNameDispaly = item.ItemNameIndent;

            item.NumberFormat = prmLedger.NumberFormat;



            //item.OpenDebitAmt = grp.OpenDebitAmt;
            //item.OpenCreditAmt = grp.OpenCreditAmt;
            //item.OpenAmt = grp.OpenAmt;
            //item.OpenDebitBalanceAmt = grp.OpenDebitBalanceAmt;
            //item.OpenCreditBalanceAmt = grp.OpenCreditBalanceAmt;



            //item.OpenBalanceAmt = Math.Abs(item.OpenAmt);
            //item.OpenBalanceDrCr = AccHelper.GetDrCrBalanceType(item.OpenAmt, grp.BalanceType);
            //item.OpenBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.OpenAmt, grp.BalanceType);

            //item.OpenBalanceDisplay = item.OpenAmt < 0 ? -Math.Abs(item.OpenAmt) : Math.Abs(item.OpenAmt);
            //item.OpenBalanceText = item.OpenAmt.ToString(prmLedger.NumberFormat);

            //item.TranDebitAmt = grp.DebitAmt;
            //item.TranCreditAmt = grp.CreditAmt;
            //item.TranAmt = grp.TranAmt;
            //item.TranDebitBalanceAmt = grp.TranDebitBalanceAmt;
            //item.TranCreditBalanceAmt = grp.TranCreditBalanceAmt;

            //item.TranBalanceAmt = Math.Abs(item.TranAmt);
            //item.TranBalanceDrCr = item.TranAmt >= 0 ? 0 : 1;
            //item.TranBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.TranAmt, grp.BalanceType);
            //item.TranBalanceDisplay = item.TranAmt < 0 ? -Math.Abs(item.TranAmt) : Math.Abs(item.TranAmt);


            //item.CloseDebitAmt = grp.CloseDebitAmt;
            //item.CloseCreditAmt = grp.CloseCreditAmt;
            //item.CloseAmt = grp.CloseAmt;
            //item.CloseDebitBalanceAmt = grp.CloseDebitBalanceAmt;
            //item.CloseCreditBalanceAmt = grp.CloseCreditBalanceAmt;

            //item.CloseBalanceAmt = Math.Abs(item.CloseAmt);
            //item.CloseBalanceDrCr = AccHelper.GetDrCrBalanceType(item.CloseAmt, grp.BalanceType);
            //item.CloseBalanceDrCrText = AccHelper.GetDrCrBalanceText(item.CloseAmt, grp.BalanceType);

            //item.CloseBalanceText = item.CloseBalanceDisplay.ToString(prmLedger.NumberFormat);


            //if (grp.BalanceType == (int)DebitCreditEnum.Debit)
            //{
            //    item.CloseBalanceDisplay = item.CloseAmt < 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            //}
            //else
            //{
            //    item.CloseBalanceDisplay = item.CloseAmt > 0 ? -Math.Abs(item.CloseAmt) : Math.Abs(item.CloseAmt);
            //}

            //switch (prmLedger.ItemAmountTypeDisplay)
            //{

            //    case ItemAmountTypeEnum.OpeningDebit:
            //        item.ItemDebitAmt = item.OpenDebitAmt;
            //        item.ItemCreditAmt = 0;
            //        item.ItemDebitBalanceAmt = item.OpenDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = 0;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.OpeningCredit:
            //        item.ItemDebitAmt = 0;
            //        item.ItemCreditAmt = item.OpenCreditAmt;
            //        item.ItemDebitBalanceAmt = 0;
            //        item.ItemCreditBalanceAmt = item.OpenCreditBalanceAmt;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.OpeningBalance:
            //        item.ItemDebitAmt = item.OpenDebitAmt;
            //        item.ItemCreditAmt = item.OpenDebitAmt;
            //        item.ItemAmt = item.OpenAmt;
            //        item.ItemDebitBalanceAmt = item.OpenDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = item.OpenDebitBalanceAmt;
            //        item.ItemBalanceAmt = item.OpenBalanceAmt;
            //        item.ItemBalanceDrCr = item.OpenBalanceDrCr;
            //        item.ItemBalanceDrCrText = item.OpenBalanceDrCrText;
            //        item.ItemAmtDisplay = item.OpenBalanceDisplay;
            //        item.ItemAmtDisplayText = item.OpenBalanceText;
            //        break;



            //    case ItemAmountTypeEnum.TranDebit:
            //        item.ItemDebitAmt = item.TranDebitAmt;
            //        item.ItemCreditAmt = 0;

            //        item.ItemDebitBalanceAmt = item.TranDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = 0;


            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.TranCredit:
            //        item.ItemDebitAmt = 0;
            //        item.ItemCreditAmt = item.TranCreditAmt;

            //        item.ItemDebitBalanceAmt = 0;
            //        item.ItemCreditBalanceAmt = item.TranCreditBalanceAmt;

            //        item.ItemAmt = item.TranCreditAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 1;
            //        item.ItemBalanceDrCrText = "Cr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.TranBalance:
            //        item.ItemDebitAmt = item.TranDebitAmt;
            //        item.ItemCreditAmt = item.TranCreditAmt;
            //        item.ItemAmt = item.TranAmt;
            //        item.ItemDebitBalanceAmt = item.TranDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = item.TranDebitBalanceAmt;
            //        item.ItemBalanceAmt = item.TranBalanceAmt;
            //        item.ItemBalanceDrCr = item.OpenBalanceDrCr;
            //        item.ItemBalanceDrCrText = item.OpenBalanceDrCrText;
            //        item.ItemAmtDisplay = item.TranBalanceDisplay;
            //        item.ItemAmtDisplayText = item.OpenBalanceText;
            //        break;

            //    case ItemAmountTypeEnum.ClosingDebit:
            //        item.ItemDebitAmt = item.CloseDebitAmt;
            //        item.ItemCreditAmt = 0;
            //        item.ItemDebitBalanceAmt = item.CloseDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = 0;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.ClosingCredit:
            //        item.ItemDebitAmt = 0;
            //        item.ItemCreditAmt = item.CloseCreditAmt;
            //        item.ItemDebitBalanceAmt = 0;
            //        item.ItemCreditBalanceAmt = item.CloseCreditBalanceAmt;
            //        item.ItemAmt = item.ItemDebitAmt;
            //        item.ItemBalanceAmt = item.ItemAmt;
            //        item.ItemBalanceDrCr = 0;
            //        item.ItemBalanceDrCrText = "Dr";
            //        item.ItemAmtDisplay = item.ItemAmt;
            //        item.ItemAmtDisplayText = item.ItemAmt.ToString(prmLedger.NumberFormat);
            //        break;

            //    case ItemAmountTypeEnum.CheckAny:
            //    case ItemAmountTypeEnum.ClosingBalance:
            //        item.ItemDebitAmt = item.CloseDebitAmt;
            //        item.ItemCreditAmt = item.CloseCreditAmt;
            //        item.ItemAmt = item.CloseAmt;
            //        item.ItemDebitBalanceAmt = item.CloseDebitBalanceAmt;
            //        item.ItemCreditBalanceAmt = item.CloseCreditBalanceAmt;
            //        item.ItemBalanceAmt = item.CloseBalanceAmt;
            //        item.ItemBalanceDrCr = item.CloseBalanceDrCr;
            //        item.ItemBalanceDrCrText = item.CloseBalanceDrCrText;
            //        item.ItemAmtDisplay = item.CloseBalanceDisplay;
            //        item.ItemAmtDisplayText = item.CloseBalanceText;
            //        break;
            //}
        }
        //  
    }
}
