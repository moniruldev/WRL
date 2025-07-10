using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcGatePassItemDetailsReport
    {
        public string ITEM_NAME { get; set; }

        public string ALTER_ITEM_NAME { get; set; }

        public decimal ITEM_QNTY { get; set; }
        public string UOM_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_CLASS_NAME { get; set; }
        public string ITEM_TYPE_NAME { get; set; }
        public string DC_DET_REMARKS { get; set; }
        public string INV_DET_REMARKS { get; set; }


        public decimal ITEM_RATE { get; set; }
       
    }
}
