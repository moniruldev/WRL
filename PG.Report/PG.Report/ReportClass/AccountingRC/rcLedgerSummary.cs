using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcLedgerSummary
    {
        List<rcGLReportHeader> m_LedgerSummaryHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> LedgerSummaryHeader
        {
            get { return m_LedgerSummaryHeader; }
            set { this.m_LedgerSummaryHeader = value; }
        }

        private List<rcGLReportItem> m_LedgerSummaryItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> LedgerSummaryItems
        {
            get { return m_LedgerSummaryItems; }
            set { this.m_LedgerSummaryItems = value; }
        }


        public List<rcLedgerSummary> GetData()
        {
            return new List<rcLedgerSummary>();
        }
    }

}
