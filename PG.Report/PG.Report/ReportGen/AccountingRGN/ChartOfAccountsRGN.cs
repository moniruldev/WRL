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
    public class ChartOfAccountsRGN
    {
        public static AppReport GenerateChartOfAccounts(NameValueCollection pReportParams)
        {
            return GenerateChartOfAccounts(pReportParams, HttpContext.Current);
        }
        public static AppReport GenerateChartOfAccounts(NameValueCollection pReportParams, HttpContext context)
        {
            string rptKey = string.Empty;

            ReportOptions rptOptions = new ReportOptions();

            AppReport.SetReportOptionsFromParams(rptOptions, pReportParams);

            clsPrmLedger prmLdg = new clsPrmLedger();
            AccountingRBLHelper.SetPrmLedgerPropFromParams(prmLdg, pReportParams);

            return GenerateChartOfAccounts(prmLdg, rptOptions, null);
        }

        public static AppReport GenerateChartOfAccounts(clsPrmLedger prmLedger, ReportOptions rptOptions)
        {
            return GenerateChartOfAccounts(prmLedger, rptOptions, null);
        }
        public static AppReport GenerateChartOfAccounts(clsPrmLedger prmLedger, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.GL_ChartOfAccounts;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Chart Of Accounts";
            SetParameter(prmLedger, rpt, dc);
           
            rpt.AddParameter("prmShowParentGroup", prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers ? "1" : "0");
            //rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptGLAccountList.rdlc";
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.AccountingDef.rptChartofAccounts.rdlc";
            List<rcGLReportItem> rList = GLAccountListRBL.GetGLAccountList(prmLedger,dc);
            rpt.DataSources.Add(new AppReport.DataSource("GLReportItem", rList));

            return rpt;
        }

        public static void SetParameter(clsPrmLedger prmLedger, AppReport rpt)
        {
            SetParameter(prmLedger, rpt);
        }

        public static void SetParameter(clsPrmLedger prmLedger, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string dateString = string.Empty;
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


            criteriaString = dateString + (postString == "" ? "" : ", ") + postString;

            rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

            rpt.AddParameter("prmCriteriaString", criteriaString);


            //rpt.AddParameter("prmShowPercentage", prmLedger.ShowPercentage ? "true" : "false");

        }

    }
}
