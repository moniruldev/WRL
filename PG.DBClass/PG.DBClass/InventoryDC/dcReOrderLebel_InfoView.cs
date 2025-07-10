using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcReOrderLebel_InfoView
    {
        public string ITEM_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_CLASS_NAME { get; set; }
        public Nullable<double> CLS_QNTY { get; set; }
        public Nullable<double> RE_ORDER_LEVEL { get; set; }
        public Nullable<double> SAFTY_STOCK_LEVEL { get; set; }
        public Nullable<double> LEAD_TIME { get; set; }
       
    }
}
