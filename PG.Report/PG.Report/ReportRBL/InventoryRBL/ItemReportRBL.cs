using PG.BLLibrary.InventoryBL;
using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using PG.Report.ReportClass.InventoryRC;
using PG.Report.ReportClass.ProductionRC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PG.Report.ReportRBL.InventoryRBL
{
   public  class ItemReportRBL
    {



       public static string GetItem_Master_SQLString()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("Select INV_ITEM_MASTER.* ");
           sb.Append(" , INV_ITEM_GROUP.ITEM_GROUP_CODE, INV_ITEM_GROUP.ITEM_GROUP_NAME  ");
           sb.Append(" , INV_ITEM_CLASS.ITEM_CLASS_CODE, INV_ITEM_CLASS.ITEM_CLASS_NAME  ");
           sb.Append(" , INV_ITEM_TYPE.ITEM_TYPE_CODE, INV_ITEM_TYPE.ITEM_TYPE_NAME  ");
           sb.Append(" , UOM_INFO.UOM_CODE, UOM_INFO.UOM_NAME , SNS.ITEM_SNS_NAME ");
           sb.Append(" ,GET_CLOSING_QTY(INV_ITEM_MASTER.ITEM_ID) CLOSING_QTY ");                  
           sb.Append(" FROM INV_ITEM_MASTER ");
           sb.Append(" INNER JOIN INV_ITEM_GROUP ON INV_ITEM_MASTER.ITEM_GROUP_ID = INV_ITEM_GROUP.ITEM_GROUP_ID ");
           sb.Append(" INNER JOIN INV_ITEM_CLASS ON INV_ITEM_MASTER.ITEM_CLASS_ID = INV_ITEM_CLASS.ITEM_CLASS_ID ");
           sb.Append(" INNER JOIN INV_ITEM_TYPE ON INV_ITEM_MASTER.ITEM_TYPE_ID = INV_ITEM_TYPE.ITEM_TYPE_ID ");
           sb.Append(" INNER JOIN UOM_INFO ON INV_ITEM_MASTER.UOM_ID = UOM_INFO.UOM_ID ");
           sb.Append(" LEFT JOIN  INV_ITEM_SNS_MST SNS ON  INV_ITEM_MASTER.ITEM_SNS_ID=SNS.ITEM_SNS_ID ");
           sb.Append(" Where 1=1 ");
           return sb.ToString();
       }




        public static List<dcINV_ITEM_MASTER> LoadItemByItemGroup(DBContext dc)
        {
            List<dcINV_ITEM_MASTER> grpList1 = INV_ITEM_MASTERBL.GetItemList(dc).ToList();
            return grpList1;
        }
      
     
         #region **********************************Item Stock Ledger *********************************************

        public static AppReport Store_Item_Ledger_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Store_Item_Ledger_Report(rptClass, rptOptions, null);
        }
        public static AppReport Store_Item_Ledger_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Item Ledger Report";
            SetParameterStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemStockLedgerReport.rdlc";
            List<rcItemTransfermation> rList = MaterialStockRBL.Item_Store_Item_Ledger_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock_Ledger", rList));
            return rpt;
        }


         public static void SetParameterStock(clsPrmInventory prmINV, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string Group_Name = string.Empty;
            string deptName = string.Empty;
            string Item_Name = string.Empty;
            string Item_Type = string.Empty;
            string Store = string.Empty;
            string classNmae = string.Empty;
            string Dept_Name = string.Empty;
            string invmonth = string.Empty;
            string platetype = string.Empty;
            string fdate = string.Empty;
            string tdate = string.Empty;
            string deptShrotName = string.Empty;

            criteriaString = "Date From :" + prmINV.fromProdDate + " To " + prmINV.toProdDate;

            string strList = string.Empty;
            invmonth = prmINV.fromProdDate;

            if (prmINV.From_Dept_Id > 0)
            {
                if (!string.IsNullOrEmpty(prmINV.From_Dept_Name))
                {
                    deptName = " Dept : " + prmINV.From_Dept_Name;
                    criteriaString = criteriaString + " " + deptName;
                }
            }


            if (prmINV.itemgroupName != "")
            {
                Group_Name = "Group :" + prmINV.itemgroupName;
            }
            else
            {
                Group_Name = "Group : All";

            }

            if (prmINV.itemName != "")
            {
                Item_Name = "Item :" + prmINV.itemName;
                criteriaString = criteriaString + " " + Item_Name;
            }
            else
            {
                Item_Name = "Item : All";
                criteriaString = criteriaString + " " + Item_Name;
            }

            if (prmINV.itemtypeName != "")
            {
                Item_Type = "Type :" + prmINV.itemtypeName;
            }
            else
            {
                Item_Type = "Type : All";

            }

            if (prmINV.From_Dept_Id > 0)
            {
                Dept_Name = "Dept : " + prmINV.From_Dept_Name;
                //criteriaString = criteriaString + " " + Dept_Name;
            }
            

            fdate = prmINV.fromProdDate;
            tdate = prmINV.toProdDate;

          
            Store = "Store :" + prmINV.storeName;

            classNmae = "Class :" + prmINV.itemclassname;

            //rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            //rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

            rpt.AddParameter("prmCriteriaString", criteriaString);
            //rpt.AddParameter("deptName", deptName);
            //rpt.AddParameter("prmGroup", Group_Name);
            //rpt.AddParameter("prmItemName", Item_Name);
            //rpt.AddParameter("prmItemType", Item_Type);
            //rpt.AddParameter("prmStore", Store);
            //rpt.AddParameter("prmclassNmae", classNmae);
            //rpt.AddParameter("prmDeptNmae", Dept_Name);
            //rpt.AddParameter("prmInvMonth", invmonth);
            //rpt.AddParameter("prmfdate", fdate);
            //rpt.AddParameter("prmtdate", tdate);
            //rpt.AddParameter("prmdeptShrotName", deptShrotName);

        }
        #endregion






         #region **********************************Production Item Ledger *********************************************

         public static AppReport Production_Item_Ledger_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
         {
             return Production_Item_Ledger_Report(rptClass, rptOptions, null);
         }
         public static AppReport Production_Item_Ledger_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
         {
             AppReport rpt = new AppReport();
             rpt.ReportOptions = rptOptions;
             rpt.ReportOptions.ReportTitle = "Item Ledger Report";
             SetParameterProduction(rptClass, rpt, dc);
             rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptProductionItemStockLedgerReport.rdlc";
             List<rcMaterialStock> rList = MaterialStockRBL.Production_Item_Ledger_Report(rptClass, dc);
             rpt.DataSources.Add(new AppReport.DataSource("dsMaterialStock", rList));

             List<rcBomVsProduction> rListItemDtl = MaterialStockRBL.Production_Item_Ledger_ItemDtl(rptClass, dc);
             rpt.DataSources.Add(new AppReport.DataSource("dsItemDtl", rListItemDtl));

             return rpt;
         }


         public static void SetParameterProduction(clsPrmInventory prmINV, AppReport rpt, DBContext dc)
         {
             string criteriaString = string.Empty;
             string Group_Name = string.Empty;
             string deptName = string.Empty;
             string Item_Name = string.Empty;
             string Item_Type = string.Empty;
             string Store = string.Empty;
             string classNmae = string.Empty;
             string Dept_Name = string.Empty;
             string invmonth = string.Empty;
             string platetype = string.Empty;
             string fdate = string.Empty;
             string tdate = string.Empty;
             string deptShrotName = string.Empty;

             criteriaString = "Date From :" + prmINV.fromProdDate + " To " + prmINV.toProdDate;

             string strList = string.Empty;
             invmonth = prmINV.fromProdDate;

             if (prmINV.From_Dept_Id > 0)
             {
                 if (!string.IsNullOrEmpty(prmINV.From_Dept_Name))
                 {
                     deptName = " Dept : " + prmINV.From_Dept_Name;
                     criteriaString = criteriaString + " " + deptName;
                 }
             }


             if (prmINV.itemgroupName != "")
             {
                 Group_Name = "Group :" + prmINV.itemgroupName;
             }
             else
             {
                 Group_Name = "Group : All";

             }

             if (prmINV.itemName != "")
             {
                 Item_Name = "Item :" + prmINV.itemName;
                 criteriaString = criteriaString + " " + Item_Name;
             }
             else
             {
                 Item_Name = "Item : All";
                 criteriaString = criteriaString + " " + Item_Name;
             }

             if (prmINV.itemtypeName != "")
             {
                 Item_Type = "Type :" + prmINV.itemtypeName;
             }
             else
             {
                 Item_Type = "Type : All";

             }

             if (prmINV.From_Dept_Id > 0)
             {
                 Dept_Name = "Dept : " + prmINV.From_Dept_Name;
                 //criteriaString = criteriaString + " " + Dept_Name;
             }


             fdate = prmINV.fromProdDate;
             tdate = prmINV.toProdDate;


             Store = "Store :" + prmINV.storeName;

             classNmae = "Class :" + prmINV.itemclassname;

             //rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
             //rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

             rpt.AddParameter("prmCriteriaString", criteriaString);
             //rpt.AddParameter("deptName", deptName);
             //rpt.AddParameter("prmGroup", Group_Name);
             //rpt.AddParameter("prmItemName", Item_Name);
             //rpt.AddParameter("prmItemType", Item_Type);
             //rpt.AddParameter("prmStore", Store);
             //rpt.AddParameter("prmclassNmae", classNmae);
             //rpt.AddParameter("prmDeptNmae", Dept_Name);
             //rpt.AddParameter("prmInvMonth", invmonth);
             //rpt.AddParameter("prmfdate", fdate);
             //rpt.AddParameter("prmtdate", tdate);
             //rpt.AddParameter("prmdeptShrotName", deptShrotName);

         }
         #endregion
    
    }
}
