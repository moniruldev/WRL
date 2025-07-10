using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
    [Serializable]
    public class rcItemTransfermation
    {
        public string STOCK_TRANSFER_NO { get; set; }
        public int DEPT_ID { get; set; }
        public DateTime STOCK_TRANSFER_DATE { get; set; }
        public string TRANSFERM_ITEM { get; set; }
        public int STOCK_TRANSFER_ITEM_ID { get; set; }
        public string STOCK_TRANSFER_ITEM_NAME { get; set; }
        public decimal TRANSFER_QTY { get; set; }
        public string TRANSFER_REASON { get; set; }
        public string REMARKS { get; set; }
        public string UOM_NAME { get; set; }
        public int UOM_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
            public DateTime RECIPE_DATE { get; set; }
             public int ITEM_GROUP_ID { get; set; }
             public string ITEM_GROUP_NAME { get; set; }
             public int ITEM_ID { get; set; }
             public string ITEM_NAME { get; set; }
             public int ITEM_TYPE_ID { get; set; } 
             public string ITEM_TYPE_NAME { get; set; }
             public int ITEM_CLASS_ID { get; set; }
             public string ITEM_CLASS_NAME { get; set; }
             public decimal OP_IRR_QTY { get; set; }
             public decimal OP_ITC_QTY { get; set; }
             public decimal OP_BAL { get; set; }
             public decimal IRR_PROD_QTY { get; set; }
             public string ITEM_CODE { get; set; }
             public int ITEM_ORDER { get; set; }
        public decimal IRR_RECP_QTY { get; set; }
        public decimal BAL_QTY { get; set; }
        public decimal OP_SCRAP_QTY { get; set; }
        public decimal CLOSING_WITHOUT_SCRAP { get; set; }
        public decimal OP_BAL_WITHOUT_SCRAP { get; set; }
        public decimal IRR_SCRAP_QTY { get; set; }
        public decimal OP_ADJUST_QTY { get; set; }

        public decimal IRR_TRANS_QTY { get; set; }
        public decimal ITC_TRANS_QTY { get; set; }
        public decimal ITC_SCRAP_QTY { get; set; }

        public int ITEM_STK_DET_ID { get; set; }
        public DateTime? TRANS_DATE { get; set; }
        public string TRANS_REF_NO { get; set; }
        public string TRANS_REMARKS { get; set; }

        public decimal RUNNING_QTY { get; set; }

        public decimal RCV_QTY { get; set; }
        public decimal ISS_QTY { get; set; }

        public decimal OPENING_QTY { get; set; }
        public decimal CLOSING_QTY { get; set; }
        public int INV_TRANS_TYPE_ID { get; set; }
        public decimal CUR_ADJUST_QTY { get; set; }
        public decimal PURCHASE_QTY { get; set; }
        public decimal MRR_QTY { get; set; }
        public int SL_NO { get; set; } 
    }
}
