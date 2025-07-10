using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class SalesRequisitionDetails
    {
        public string ITEM_CODE { get; set; }
        public string ITEM_DESC { get; set; }
        public string REQ_NO { get; set; }
        public double ISSUE_QNTY { get; set; }
        public double REQ_QNTY { get; set; }
        public double RCV_QNTY { get; set; }
        public string MSR_ABBR { get; set; }
        public string REQ_NOTE { get; set; }
    }
}
