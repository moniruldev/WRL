using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class PurchaseIndentInfo
    {
        public string BRANCH_NAME { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string INDT_NO { get; set; }
        public DateTime? INDT_DATE { get; set; }
        public decimal? INDT_QTY { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string PURCHASE_NO { get; set; }
        public decimal? INDT_QTY_APPROVED { get; set; }
        public DateTime? APPROVED_DATE { get; set; }
        public DateTime? CANCEL_DATE { get; set; }
        public decimal? PURCHASE_QTY { get; set; }
        public DateTime? PURCHASE_DATE { get; set; }
        public string MRR_NO { get; set; }
        public decimal? MRR_QTY { get; set; }
        public DateTime? MRR_DATE { get; set; }
        public string MRR_STATUS { get; set; }
    }
}
