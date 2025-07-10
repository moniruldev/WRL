using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class StorePurchaseIndent
    {
        public string REQ_BRANCH { get; set; }
        public string SR_DEPT { get; set; }
        public string REQUIRED_DEPT { get; set; }
        public double INDT_NO { get; set; }
        public DateTime? INDT_DATE { get; set; }
        public string REQ_NOTE { get; set; }
    }
}
