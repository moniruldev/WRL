using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcAccRefSummary
    {
        List<rcGLReportHeader> m_AccRefSummaryHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> AccRefSummaryHeader
        {
            get { return m_AccRefSummaryHeader; }
            set { this.m_AccRefSummaryHeader = value; }
        }

        private List<rcGLReportItem> m_AccRefSummaryItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> AccRefSummaryItems
        {
            get { return m_AccRefSummaryItems; }
            set { this.m_AccRefSummaryItems = value; }
        }


        public List<rcAccRefSummary> GetData()
        {
            return new List<rcAccRefSummary>();
        }
    }

}
