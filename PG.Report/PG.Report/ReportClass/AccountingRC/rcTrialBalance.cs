using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcTrialBalance
    {

        List<rcGLReportHeader> m_TrialBalanceHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> TrialBalanceHeader
        {
            get { return m_TrialBalanceHeader; }
            set { this.m_TrialBalanceHeader = value; }
        }


        private List<rcGLReportItem> m_TrialBalanceItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> TrialBalanceItems
        {
            get { return m_TrialBalanceItems; }
            set { this.m_TrialBalanceItems = value; }
        }


        public List<rcTrialBalance> GetData()
        {
            return new List<rcTrialBalance>();
        }
    }

}
