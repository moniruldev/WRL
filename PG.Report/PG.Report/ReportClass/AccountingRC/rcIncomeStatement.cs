using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcIncomeStatement
    {
        List<rcGLReportHeader> m_IncomeStatementHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> IncomeStatementHeader
        {
            get { return m_IncomeStatementHeader; }
            set { this.m_IncomeStatementHeader = value; }
        }

        private List<rcGLReportItem> m_IncomeStatementItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> IncomeStatementItems
        {
            get { return m_IncomeStatementItems; }
            set { this.m_IncomeStatementItems = value; }
        }


        public List<rcIncomeStatement> GetData()
        {
            return new List<rcIncomeStatement>();
        }
    }

}
