using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcBalanceSheet
    {
        List<rcGLReportHeader> m_BalanceSheetHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> BalanceSheetHeader
        {
            get { return m_BalanceSheetHeader; }
            set { this.m_BalanceSheetHeader = value; }
        }

        private List<rcGLReportItem> m_BalanceSheetItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> BalanceSheetItems
        {
            get { return m_BalanceSheetItems; }
            set { this.m_BalanceSheetItems = value; }
        }


        public List<rcBalanceSheet> GetData()
        {
            return new List<rcBalanceSheet>();
        }
    }

}
