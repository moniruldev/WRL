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
    public class CashRGN
    {
        public static AppReport GenerateCashSummary(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateCashSummary(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateCashSummary(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_CashSummary;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Cash/Bank Summary";

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


            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptCashSummary.rdlc";


            List<rcCashSummary> rList = CashRBL.GetCashSummary(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("GLReportItem", rList[0].CashSummaryItems));
            rpt.DataSources.Add(new AppReport.DataSource("GLReportHeader", rList[0].CashSummaryHeader));


            return rpt;
        }



        public static AppReport GenerateJournalBook(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateJournalBook(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateJournalBook(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_JournalBook;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Journal Book";

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

            
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournalBook.rdlc";


            List<rcJournalTrans> rList = JournalRBL.GetJournalBook(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("JournalTrans", rList));


            return rpt;
        }


        public static AppReport GenerateJournalList(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateJournalList(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateJournalList(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_JournalList;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Journal List";

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


            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournalList.rdlc";


            List<rcJournal> rList = JournalRBL.GetJournalList(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("Journal", rList));


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
                dateString = "Date: " + prmLedger.FromDate.Value.ToString("dd-MMM-yyyy");
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
                    postString = "Posted & Unposted";
                    break;
                case IncludePostEnum.Posted:
                    postString = "Posted";
                    break;
                case IncludePostEnum.Unposted:
                    postString = "Unposted";
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
