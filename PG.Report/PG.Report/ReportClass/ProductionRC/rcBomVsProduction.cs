using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
    public class rcBomVsProduction
    {
        //Finish item property

        public int ITEM_GROUP_ID { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public int UOM_ID { get; set; }
        public string UOM_NAME { get; set; }
        public int ITEM_TYPE_ID { get; set; }
        public string ITEM_TYPE_NAME { get; set; }

        public int ITEM_CLASS_ID { get; set; }
        public string ITEM_CLASS_NAME { get; set; }
        public decimal OP_MRR_QTY { get; set; }
        public decimal OP_IRR_QTY { get; set; }
        public decimal OP_ITC_QTY { get; set; }

        public decimal OP_BAL { get; set; }
        public decimal OP_ADJ { get; set; }
        public decimal MRR_QTY { get; set; }
        public decimal IRR_QTY { get; set; }
        public decimal ITC_QTY { get; set; }

        public decimal RTN_QTY { get; set; }
        public decimal BAL_QTY { get; set; }

        public decimal OPPENING_BAL_QTY { get; set; }
        public decimal COLISING_QTY { get; set; }

        public decimal TOT_RECEIVE_QNTY { get; set; }

        public decimal TOT_ISSUE_QNTY { get; set; }

        public decimal ITEM_FC_QTY { get; set; }


        public decimal ADJUST_QTY { get; set; }

        //for calculating department stock

        public decimal IRR_DEPT_QTY { get; set; }
        public decimal IRR_STORE_QTY { get; set; }
        public decimal IRR_PROD_QTY { get; set; }
        public decimal IRR_BAL_QTY { get; set; }



        public decimal ITC_DEPT_QTY { get; set; }
        public decimal ITC_STORE_QTY { get; set; }
        public decimal ITC_PROD_QTY { get; set; }
        public decimal ITC_BAL_QTY { get; set; }
        public string DEPARTMENT_NAME { get; set; }

        public int UOM_ID_P { get; set; }
        public string UOM_NAME_P { get; set; }


        public int ORDER_NO { get; set; }


        //Raw material Property

        public int RM_ITEM_ID { get; set; }
        public string RM_ITEM_NAME { get; set; }
        public string RM_ITEM_CODE { get; set; }
        public int RM_UOM_ID { get; set; }
        public string RM_UOM_NAME { get; set; }

        public decimal RM_STD_QTY { get; set; }
        public decimal RM_WASTAGE_PERCENT { get; set; }
        public decimal RM_STD_WT { get; set; }

        public decimal RM_ACTUAL_QTY { get; set; }
        public decimal RM_ACTUAL_WASTAGE_PERCENT { get; set; }
        public decimal RM_ACTUAL_WT { get; set; }






        public decimal STD_UNIT_QTY { get; set; }

        public decimal RM_USE_DIFF { get; set; }

        public decimal USE_PER { get; set; }
        public decimal USED_QTY_AS_BOM { get; set; }

        public DateTime? PRODUCTION_DATE { get; set; }
        
    }
}
