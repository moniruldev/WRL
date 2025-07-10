using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcCashSummary
    {
        List<rcGLReportHeader> m_CashSummaryHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> CashSummaryHeader
        {
            get { return m_CashSummaryHeader; }
            set { this.m_CashSummaryHeader = value; }
        }

        private List<rcGLReportItem> m_CashSummaryItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> CashSummaryItems
        {
            get { return m_CashSummaryItems; }
            set { this.m_CashSummaryItems = value; }
        }


        public List<rcCashSummary> GetData()
        {
            return new List<rcCashSummary>();
        }
    }

}
