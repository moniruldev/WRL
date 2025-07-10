using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]

    public class IPSInvoiceItem
    {
        public string ITEM_SHORT_DESC { get; set; }
        public string BRAND_NAME { get; set; }
        public string ITEM_CAPACITY { get; set; }
        public string GC_NO { get; set; }
        public string SERIAL_NO { get; set; }
        public decimal? ITEM_QNTY { get; set; }
        public string MSR_UNIT_DESC { get; set; }
        public string IPS_INV_NO { get; set; }
        public string IPS_PKG_ID { get; set; }
        public decimal? ITEM_QTY { get; set; }

    }
}
