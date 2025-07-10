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
    public class LedgerRGN
    {
        public static AppReport GenerateLedger(NameValueCollection pReportParams)
        {
            return GenerateLedger(pReportParams, HttpContext.Current);
        }
        public static AppReport GenerateLedger(NameValueCollection pReportParams, HttpContext context)
        {
            string rptKey = string.Empty;

            ReportOptions rptOptions = new ReportOptions();

            AppReport.SetReportOptionsFromParams(rptOptions, pReportParams);

            clsPrmLedger prmLdg = new clsPrmLedger();
            AccountingRBLHelper.SetPrmLedgerPropFromParams(prmLdg, pReportParams);

            return GenerateLedger(prmLdg, rptOptions, null);
        }

        public static AppReport GenerateLedger(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateLedger(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateLedger(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_Ledger;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Ledger";

            //rpt.AddParameter("vCompanyName", "Interwave Solutions");
           
            SetParameter(prmLedger, rpt, dc);

            if (rptOptions.ReportOpenType == ReportEnums.ReportOpenTypeEnum.Export)
            {
                if (rptOptions.ReportExportType == ReportEnums.ReportExportTypeEnum.Excel)
                {
                    prmLedger.IsIndentName = true;
                    prmLedger.IsExcelExport = true;
                }
            }

            //rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptLedger.rdlc";

            List<rcLedger> rList = null;
            //if (prmLedger.IncludeReference || prmLedger.IncludeCostCenter || prmLedger.IncludeInstrument 
            //    || prmLedger.IncludeTranCode   || prmLedger.IncludeContraEntry || prmLedger.IncludeSubAccountForControl )
            //{
            //    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptLedgerFull.rdlc";
            //    rList = LedgerRBL.GetLedgerFull(prmLedger, dc);
            //}

            if (prmLedger.IncludeReference || prmLedger.IncludeCostCenter || prmLedger.IncludeInstrument
            || prmLedger.IncludeTranCode || prmLedger.IncludeContraEntry || prmLedger.IncludeSubAccountForControl)
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptLedgerFull.rdlc";
                rList = LedgerRBL.GetLedgerFull(prmLedger, dc);
            }

            else
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptLedger.rdlc";
                rList = LedgerRBL.GetLedger(prmLedger, dc);
            }

            rpt.DataSources.Add(new AppReport.DataSource("Ledger", rList));
            rpt.DataSources.Add(new AppReport.DataSource("LedgerTrans", rList[0].LedgerTrans));

            
            return rpt;
        }



        public static AppReport GenerateLedgerSummary(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateLedgerSummary(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateLedgerSummary(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_LedgerSummary;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Ledger Summary";

            //rpt.AddParameter("vCompanyName", "Interwave Solutions");

            SetParameter(prmLedger, rpt, dc);

            if (rptOptions.ReportOpenType == ReportEnums.ReportOpenTypeEnum.Export)
            {
                if (rptOptions.ReportExportType == ReportEnums.ReportExportTypeEnum.Excel)
                {
                    prmLedger.IsIndentName = true;
                    prmLedger.IsExcelExport = true;
                }
            }


            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptLedgerSummary.rdlc";


            List<rcLedgerSummary> rList = LedgerRBL.GetLedgerSummary(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("GLReportItem", rList[0].LedgerSummaryItems));
            rpt.DataSources.Add(new AppReport.DataSource("GLReportHeader", rList[0].LedgerSummaryHeader));


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


            //rpt.AddParameter("prmShowPercentage", prmLedger.ShowPercentage ? "true" : "false");

        }

    }
}
