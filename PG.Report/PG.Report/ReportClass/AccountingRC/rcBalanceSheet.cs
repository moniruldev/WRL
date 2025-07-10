using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcReceiptPayment
    {
        List<rcGLReportHeader> m_ReceiptPaymentHeader = new List<rcGLReportHeader>();
        public List<rcGLReportHeader> ReceiptPaymentHeader
        {
            get { return m_ReceiptPaymentHeader; }
            set { this.m_ReceiptPaymentHeader = value; }
        }

        private List<rcGLReportItem> m_ReceiptPaymentItems = new List<rcGLReportItem>();
        public List<rcGLReportItem> ReceiptPaymentItems
        {
            get { return m_ReceiptPaymentItems; }
            set { this.m_ReceiptPaymentItems = value; }
        }


        public List<rcReceiptPayment> GetData()
        {
            return new List<rcReceiptPayment>();
        }
    }

}
