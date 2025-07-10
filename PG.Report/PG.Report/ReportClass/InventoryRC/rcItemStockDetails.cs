using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.InventoryRC
{
     [Serializable]
    public class rcItemStockDetails
    {
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public decimal OPENING_QTY { get; set; }
        public decimal CLOSING_QTY { get; set; }
        public string ITEM_CODE { get; set; }
        public decimal RCV_QTY { get; set; }
        public decimal ISS_QTY { get; set; }
        public DateTime? TRANS_DATE { get; set; }

        public int ITEM_ID { get; set; }
        public int ITEM_STK_DET_ID { get; set; }
        public int UOM_ID { get; set; }
        public string TRANS_REF_NO { get; set; }
        public string TRANS_REMARKS { get; set; }

        public decimal RUNNING_QTY { get; set; }
    }
}
