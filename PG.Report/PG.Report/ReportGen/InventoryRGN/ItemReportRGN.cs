using PG.BLLibrary.InventoryBL;
using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using PG.Report.ReportClass.InventoryRC;
using PG.Report.ReportEnums;
using PG.Report.ReportRBL.InventoryRBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportGen.InventoryRGN
{
   public class ItemReportRGN
    {

        // Battery Invoice
        public static AppReport GenerateItemReportPreview(clsPrmInventory prm,ReportParameterClass rptClass, ReportOptions rptOptions)
        {
            return GenerateItemReportPreview(rptClass, rptOptions, null, prm);
        }
        public static AppReport GenerateItemReportPreview(ReportParameterClass rptClass, ReportOptions rptOptions, DBContext dc, clsPrmInventory prm)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.ItemReport;
            rpt.ReportOptions = rptOptions;
            //rpt.ReportOptions.ReportTitle = "Location Details";
            SetParameter(rptClass, rpt, dc);
            // rpt.AddParameter("prmShowParentGroup", prmSND.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers ? "1" : "0");
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.InventoryDef.rptItemReport.rdlc";
            List<dcINV_ITEM_MASTER> rList = INV_ITEM_MASTERBL.GetItemList1(prm, dc);
            List<rcItemGroupReport> itemList = rList.Select(x => new rcItemGroupReport() { ITEM_CODE = x.ITEM_CODE, ITEM_GROUP_NAME = x.ITEM_GROUP_NAME, ITEM_TYPE_NAME = x.ITEM_TYPE_NAME, ITEM_CLASS_NAME = x.ITEM_CLASS_NAME, ITEM_NAME = x.ITEM_NAME, UOM_NAME = x.UOM_NAME, ITEM_DESCRIPTION = x.ITEM_DESC, ITEM_SNS_NAME = x.ITEM_SNS_NAME }).ToList();

            if (itemList.Any())
            {
                rpt.AddParameter("prmItemGroupName", itemList.FirstOrDefault().ITEM_GROUP_NAME);
            }          
            rpt.DataSources.Add(new AppReport.DataSource("itemReportDS", itemList));
            return rpt;
        }

       
        public static void SetParameter(ReportParameterClass rptClass, AppReport rpt)
        {
            SetParameter(rptClass, rpt);
        }

        public static void SetParameter(ReportParameterClass rptClass, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string dateString = string.Empty;
            string postString = string.Empty;
            criteriaString = dateString + (postString == "" ? "" : ", ") + postString;
            rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);
            rpt.AddParameter("prmUserName", rpt.ReportOptions.UserName);
            rpt.AddParameter("prmCriteriaString", criteriaString);
        }


        public static AppReport GenerateChartOfItem(clsPrmInventory prm, ReportParameterClass rptClass, ReportOptions rptOptions)
        {
            return GenerateChartOfItem(prm,rptClass, rptOptions, null);
        }
        public static AppReport GenerateChartOfItem(clsPrmInventory prm, ReportParameterClass rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_ChartOfAccounts;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Chart Of Item";
            SetParameter(rptClass, rpt, dc);

            //rpt.AddParameter("prmShowParentGroup", prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers ? "1" : "0");
            //rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptGLAccountList.rdlc";
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptChartofAccounts.rdlc";
            List<rcItemGroupReport> rList = INV_Item_GroupRBL.GetITemHerarchyList(prm, dc);
            rpt.DataSources.Add(new AppReport.DataSource("GLReportItem", rList));

            return rpt;
        }


    }
}
