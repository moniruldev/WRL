using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
   public class dcMaterialReceiveItemDetailsReport
    {
        public string ITEM_NAME{ get; set; }
        public string UOM_NAME { get; set; }
        public decimal MRR_QTY { get; set; }
        public string SUP_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_CLASS_NAME { get; set; }
        public string ITEM_TYPE_NAME { get; set; }
        public string CHALLAN_NO { get; set; }
        public string NOTE { get; set; }

        public decimal UNIT_PRICE { get; set; }

        public decimal TOTAL_COST { get; set; }
        public decimal QC_PASS_QTY { get; set; }
        public string alter_item_name { get; set; }
       
    }
}
