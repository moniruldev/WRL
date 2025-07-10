using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using PG.Core.Utility;

using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;

using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;


namespace PG.Report.ReportRBL.AccountingRBL
{
    public class AccountingRBLHelper
    {
        public static void SetPrmLedgerPropFromParams(clsPrmLedger prmLedger, NameValueCollection pReportParams)
        {
            //int rptOpenType = PG.Core.Utility.Conversion.StringToInt(AppReport.GetValueFromParams("rptopentype", pReportParams));
            //int rptViewType = PG.Core.Utility.Conversion.StringToInt(AppReport.GetValueFromParams("rptviewtype", pReportParams));
            //string exportType = AppReport.GetValueFromParams("exporttype", pReportParams);

            int companyid = Conversion.StringToInt(AppReport.GetValueFromParams("companyid", pReportParams));
            int accYearID = Conversion.StringToInt(AppReport.GetValueFromParams("accyearid", pReportParams));

            int glGroupid = Conversion.StringToInt(AppReport.GetValueFromParams("glgroupid", pReportParams));
            int glAccountid = Conversion.StringToInt(AppReport.GetValueFromParams("glaccid", pReportParams));

            int journalID = Conversion.StringToInt(AppReport.GetValueFromParams("journalid", pReportParams));

            DateTime? fromDate = Conversion.NullDateToNull(AppReport.GetValueFromParams("fromdate", pReportParams));
            DateTime? toDate = Conversion.NullDateToNull(AppReport.GetValueFromParams("todate", pReportParams));

            int orderBy = Conversion.StringToInt(AppReport.GetValueFromParams("orderby", pReportParams));

            prmLedger.CompanyID = companyid;
            prmLedger.AccYearID = accYearID;
            prmLedger.GLGroupID = glGroupid;
            prmLedger.GLAccountID = glAccountid;

            prmLedger.FromDate = fromDate;
            prmLedger.ToDate = toDate;


            prmLedger.JournalID = journalID;

            prmLedger.OrderBy = (AccOrderByEnum)orderBy;
        }
    }
}
