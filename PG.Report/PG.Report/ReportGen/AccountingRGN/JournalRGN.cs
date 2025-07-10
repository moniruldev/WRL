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
    public class JournalRGN
    {
        public static AppReport GenerateJournal(NameValueCollection pReportParams)
        {
            return GenerateJournal(pReportParams, HttpContext.Current);
        }
        public static AppReport GenerateJournal(NameValueCollection pReportParams, HttpContext context)
        {
            string rptKey = string.Empty;

            ReportOptions rptOptions = new ReportOptions();

            AppReport.SetReportOptionsFromParams(rptOptions, pReportParams);

            clsPrmLedger prmLdg = new clsPrmLedger();
            AccountingRBLHelper.SetPrmLedgerPropFromParams(prmLdg, pReportParams);

            return GenerateJournal(prmLdg, rptOptions, null);
        }

        public static AppReport GenerateJournal(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateJournal(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateJournal(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_Journal;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Journal";

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

            //List<rcJournal> rList = JournalRBL.GetJournal(prmLedger, dc);
            List<rcJournal> rList = new List<rcJournal>();
            switch (prmLedger.JournalReportFormat)
            {
                case JournalReportFormatEnum.Default:
                     rList = JournalRBL.GetJournal(prmLedger, dc);
                    
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournal.rdlc";
                    rpt.DataSources.Add(new AppReport.DataSource("Journal", rList));
                    rpt.DataSources.Add(new AppReport.DataSource("JournalTrans", rList[0].JournalTrans));

                    break;
                case JournalReportFormatEnum.SingleVoucher:
                    rList = JournalRBL.GetJournalSingle(prmLedger, dc);
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournalSingle.rdlc";
                    rpt.DataSources.Add(new AppReport.DataSource("JournalTrans", rList[0].JournalTrans));
                    break;

                case JournalReportFormatEnum.DebitCreditJ:

                    //rList = JournalRBL.GetJournal(prmLedger, dc);
                     List<rcJournalTrans> rList1 = null;
                     rList1 = JournalRBL.GetJournalBookFull(prmLedger, dc);
                     rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournalDebitCredit.rdlc";
                     rpt.DataSources.Add(new AppReport.DataSource("Journal", rList));
                     //rpt.DataSources.Add(new AppReport.DataSource("JournalTrans", rList[0].JournalTrans));
                     rpt.DataSources.Add(new AppReport.DataSource("JournalTrans", rList1));
                     
                    break;
            }
          
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


            List<rcJournalTrans> rList = null;
            if (prmLedger.IncludeReference | prmLedger.IncludeCostCenter | prmLedger.IncludeTranCode
                 | prmLedger.IncludeInstrument | prmLedger.ControlAccountSummary )
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournalBookFull.rdlc";
                rList = JournalRBL.GetJournalBookFull(prmLedger, dc);
            }
            else
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptJournalBook.rdlc";
                rList = JournalRBL.GetJournalBook(prmLedger, dc);
            }





            //List<rcJournalTrans> rList = JournalRBL.GetJournalBook(prmLedger, dc);

            rpt.DataSources.Add(new AppReport.DataSource("JournalTrans", rList));


            return rpt;
        }

        public static AppReport GenerateCashJournalBook(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateCashJournalBook(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateCashJournalBook(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_JournalBook;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Cash Journal Book";

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


            List<rcJournalTrans> rList = null;
            if (prmLedger.IncludeReference | prmLedger.IncludeCostCenter | prmLedger.IncludeTranCode
                 | prmLedger.IncludeInstrument | prmLedger.ControlAccountSummary)
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptCashJournalBookFull.rdlc";
                rList = JournalRBL.GetCashJournalBookFull(prmLedger, dc);
            }
            else
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptCashJournalBook.rdlc";
                rList = JournalRBL.GetCashJournalBook(prmLedger, dc);
            }





            //List<rcJournalTrans> rList = JournalRBL.GetJournalBook(prmLedger, dc);

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


        public static AppReport GenerateCashJournalList(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateCashJournalList(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateCashJournalList(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_JournalList;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Cash Journal List";

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


            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptCashJournalList.rdlc";


            List<rcJournal> rList = JournalRBL.GetCashJournalList(prmLedger, dc);

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
