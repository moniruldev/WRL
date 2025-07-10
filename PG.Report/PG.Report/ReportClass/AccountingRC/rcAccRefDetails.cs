using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.AccountingRC
{
    [Serializable]
    public class rcAccRefDetails
    {
        List<rcLedger> m_AccRefDetailsHeader = new List<rcLedger>();
        public List<rcLedger> AccRefDetailsHeader
        {
            get { return m_AccRefDetailsHeader; }
            set { this.m_AccRefDetailsHeader = value; }
        }

        private List<rcLedgerTrans> m_AccRefDetailsItems = new List<rcLedgerTrans>();
        public List<rcLedgerTrans> AccRefDetailsItems
        {
            get { return m_AccRefDetailsItems; }
            set { this.m_AccRefDetailsItems = value; }
        }


        public List<rcAccRefDetails> GetData()
        {
            return new List<rcAccRefDetails>();
        }
    }

}
