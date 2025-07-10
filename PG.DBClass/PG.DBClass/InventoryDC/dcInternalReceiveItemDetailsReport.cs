using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcInternalReceiveItemDetailsReport
    {
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public decimal RCV_QNTY { get; set; }
    }
}
