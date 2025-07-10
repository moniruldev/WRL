using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

using PG.Core.DBBase;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;

using PG.Report.ReportClass;
using PG.Report.ReportClass.AccountingRC;
using PG.Report.ReportRBL.AccountingRBL;

using PG.BLLibrary.AccountingBL;


namespace PG.Report.ReportGen.AccountingRGN
{
    public class BalanceSheetRGN
    {
        public static AppReport GenerateBalanceSheet(NameValueCollection pReportParams)
        {
            return GenerateBalanceSheet(pReportParams, HttpContext.Current);
        }
        public static AppReport GenerateBalanceSheet(NameValueCollection pReportParams, HttpContext context)
        {
            string rptKey = string.Empty;

            ReportOptions rptOptions = new ReportOptions();

            AppReport.SetReportOptionsFromParams(rptOptions, pReportParams);

            clsPrmLedger prmLdg = new clsPrmLedger();
            AccountingRBLHelper.SetPrmLedgerPropFromParams(prmLdg, pReportParams);

            return GenerateBalanceSheet(prmLdg, rptOptions, null);
        }

        public static AppReport GenerateBalanceSheet(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateBalanceSheet(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateBalanceSheet(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_BalanceSheet;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Balance Sheet";

            //rpt.AddParameter("vCompanyName", "Interwave Solutions");


            if (prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers)
            {
                prmLedger.MaxHierarchyLevel = -1;
            }


            SetParameter(prmLedger, rpt, dc);

            if (rptOptions.ReportOpenType == ReportEnums.ReportOpenTypeEnum.Export)
            {
                if (rptOptions.ReportExportType == ReportEnums.ReportExportTypeEnum.Excel)
                {
                    prmLedger.IsIndentName = true;
                    prmLedger.IsExcelExport = true;
                }
            }

            if (prmLedger.IncludeOpBalanceType == InculdeOpBalanceEnum.None)
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptBalanceSheet.rdlc";
            }
            else
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptBalanceSheet.rdlc";

            }

            List<rcBalanceSheet> rList = BalanceSheetRBL.GetBalanceSheet(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("GLReportItem", rList[0].BalanceSheetItems));
            rpt.DataSources.Add(new AppReport.DataSource("GLReportHeader", rList[0].BalanceSheetHeader));

            
            return rpt;
        }

        public static void SetParameter(clsPrmLedger prmLedger, AppReport rpt)
        {
            SetParameter(prmLedger, rpt);
        }

        public static void SetParameter(clsPrmLedger prmLedger, AppReport rpt,DBContext dc)
        {
            string criteriaString = string.Empty;
            string dateString  = string.Empty;
            string postString = string.Empty;

            if (prmLedger.FromDate.HasValue)
            {
                dateString = "As at " + prmLedger.FromDate.Value.ToString("dd-MMM-yyyy");
                if (prmLedger.ToDate.HasValue)
                {
                    if (prmLedger.FromDate != prmLedger.ToDate)
                    {
                        dateString = "From " + prmLedger.FromDate.Value.ToString("dd-MMM-yyyy") + " To " + prmLedger.ToDate.Value.ToString("dd-MMM-yyyy");
                    }
                }
            }

            switch (prmLedger.IncludePostType)
            {
                case IncludePostEnum.All:
                    postString = "Unposted entries included";
                    break;
                case IncludePostEnum.Posted:
                    postString = "";
                    break;
                case IncludePostEnum.Unposted:
                    postString = "Only Unposted entries";
                    break;
            }


            criteriaString = dateString + (postString == ""? "" : ", " ) + postString;

            rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy );

            rpt.AddParameter("prmCriteriaString", criteriaString);
            rpt.AddParameter("prmShowPercentage", prmLedger.ShowPercentage ? "true" : "false");

        }

    }
}
