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
    public class AccRefRGN
    {
        public static AppReport GenerateAccRefSummary(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateAccRefSummary(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateAccRefSummary(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            

            //rpt.ReportID = ReportEnums.ReportIDEnum.GL_CostCenterSummary;
            //rpt.ReportOptions.ReportTitle = "Cash/Bank Summary";

            //rpt.AddParameter("vCompanyName", "Interwave Solutions");

            rpt.ReportOptions = rptOptions;

            switch (prmLedger.AccRefType)
            {
                case AccRefTypeEnum.CostCenter:
                    rpt.ReportID = ReportEnums.ReportIDEnum.GL_CostCenterSummary;
                    rpt.ReportOptions.ReportTitle = "Cost Center Summary";
                    rpt.AddParameter("prmNameHeader", "Cost Center");
                    break;
                case AccRefTypeEnum.Reference:
                    rpt.ReportID = ReportEnums.ReportIDEnum.GL_ReferenceSummary;
                    rpt.ReportOptions.ReportTitle = "Reference Summary";
                    rpt.AddParameter("prmNameHeader", "Reference");
                    break;
                case AccRefTypeEnum.TranCode:
                    rpt.ReportID = ReportEnums.ReportIDEnum.GL_TranCodeSummary;
                    rpt.ReportOptions.ReportTitle = "Tran Code Summary";
                    rpt.AddParameter("prmNameHeader", "Tran Code");
                    break;
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






            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptAccRefSummary.rdlc";


            List<rcAccRefSummary> rList = AccRefRBL.GetAccRefSummary(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("GLReportItem", rList[0].AccRefSummaryItems));
            rpt.DataSources.Add(new AppReport.DataSource("GLReportHeader", rList[0].AccRefSummaryHeader));


            return rpt;
        }


        public static AppReport GenerateAccRefDetails(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateAccRefDetails(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateAccRefDetails(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();


            //rpt.ReportID = ReportEnums.ReportIDEnum.GL_CostCenterSummary;
            //rpt.ReportOptions.ReportTitle = "Cash/Bank Summary";

            //rpt.AddParameter("vCompanyName", "Interwave Solutions");

            rpt.ReportOptions = rptOptions;

            switch (prmLedger.AccRefType)
            {
                case AccRefTypeEnum.CostCenter:
                    rpt.ReportID = ReportEnums.ReportIDEnum.GL_CostCenterDetails;
                    rpt.ReportOptions.ReportTitle = "Cost Center Details";
                    rpt.AddParameter("prmAccRefTypeName", "Cost Center");
                    break;
                case AccRefTypeEnum.Reference:
                    rpt.ReportID = ReportEnums.ReportIDEnum.GL_ReferenceDetails;
                    rpt.ReportOptions.ReportTitle = "Reference Details";
                    rpt.AddParameter("prmAccRefTypeName", "Reference");
                    break;
                case AccRefTypeEnum.TranCode:
                    rpt.ReportID = ReportEnums.ReportIDEnum.GL_TranCodeDetails;
                    rpt.ReportOptions.ReportTitle = "Tran Code Details";
                    rpt.AddParameter("prmAccRefTypeName", "Tran Code");
                    break;
            }

            if (prmLedger.GLAccountID > 0)
            {
                rpt.AddParameter("prmNarrationHeadName", "Narration");
            }
            else
            {
                rpt.AddParameter("prmNarrationHeadName", "Ledger/Narration");
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

            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptAccRefDetails.rdlc";

            List<rcAccRefDetails> rList = AccRefRBL.GetAccRefDetails(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("LedgerTrans", rList[0].AccRefDetailsItems));
            rpt.DataSources.Add(new AppReport.DataSource("Ledger", rList[0].AccRefDetailsHeader));

           

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

            string reportName = string.Empty;



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

            if (rpt.ReportOptions.ReportTitle != string.Empty)
            {
                rpt.AddParameter("prmReportTitle", rpt.ReportOptions.ReportTitle);
            }


            //rpt.AddParameter("prmShowPercentage", prmLedger.ShowPercentage ? "true" : "false");

        }

    }
}
