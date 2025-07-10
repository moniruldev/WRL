using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcOpeningBlanceItemInfo
    {
        public string ITEM_NAME { get; set; }
        public string SLNO { get; set; }
        public int? OPENING_QTY { get; set; }
        public string UOM_NAME { get; set; }
        public decimal? ITEM_OP_RATE { get; set; }
        public string EDIT_ALLOWED { get; set; }
        public string STORE_NAME { get; set; }
        public int? STORE_ID { get; set; }
        public DateTime? BAL_AUDIT_DATE { get; set; }
        public int ITEM_ID { get; set; }
        public int UOM_ID { get; set; }

        public string ITEM_CODE { get; set; } 
        
    }
}
