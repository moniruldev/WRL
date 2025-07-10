using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
     [Serializable]
    public class rcFGF
    {
         public string FC_DESC { get; set; }
         public int FOR_MONTH { get; set; }
         public int FOR_YEAR { get; set; }
         public int BATERY_CAT_ID { get; set; }
         public string BATERY_CAT_DESCR { get; set; }
        public int ITEM_ID { get; set; }
        public string FC_NO { get; set; }

        public decimal ITEM_FC_QTY { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM_CODE { get; set; }


        public string REMARKS { get; set; }
       
        public DateTime? ENTRY_DATE { get; set; }
        public string FORCASTE_BY { get; set; }
         
       
        public int WK1_ITEM_QTY { get; set; }
        public int WK2_ITEM_QTY { get; set; }
        public int WK3_ITEM_QTY { get; set; }
        public int WK4_ITEM_QTY { get; set; }
        public int TOTAL_MONTH_QTY { get; set; }


        public int WK1_DELIVERED { get; set; }
        public int WK2_DELIVERED { get; set; }
        public int WK3_DELIVERED { get; set; }
        public int WK4_DELIVERED { get; set; }
        public int TOTAL_MONTH_DELIVERED { get; set; }
         
         
    }
}
