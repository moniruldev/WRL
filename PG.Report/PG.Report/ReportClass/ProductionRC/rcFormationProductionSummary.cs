using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
    [Serializable]
    public class rcFormationProductionSummary
    {
       
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





        //Here Formation Opening Part Property list
        //

        //Normal plate receive from IB and Pasting
        public decimal OP_TOTAL_RCV_QTY { get; set; }

        //Opening total Reject Quantity


        public decimal OP_TOTAL_REJECT_QTY { get; set; }


        //Formed ,Unformed plate receive,Issue calculation
        public decimal OP_IRR_PROD_QTY { get; set; }
        public decimal OP_WIP_ADJUST_QTY { get; set; }
        public decimal OP_WIP { get; set; }
        public decimal OP_LOAD_QTY { get; set; }
        public decimal OP_UN_LOAD_QTY { get; set; }


        //Formed Opening

        public decimal OP_F_ADJUST_QTY { get; set; }
        public decimal OP_F_IRR_QTY { get; set; }
        public decimal OP_F_ITC_QTY { get; set; }
        public decimal OP_F_BAL { get; set; }


        //UnFormed Opening
        public decimal OP_UF_ADJUST_QTY { get; set; }
        public decimal OP_UF_IRR_QTY { get; set; }
        public decimal OP_UF_ITC_QTY { get; set; }
        public decimal OP_UF_BAL { get; set; }


        //Transactional property withinn date range
        public decimal RCV_FROM_IB { get; set; }
        public decimal RCV_FROM_PASTING { get; set; }
        public decimal ISS_TO_ASSEMBLY { get; set; }
        public decimal UNFORMED_QTY { get; set; }

        public decimal FORMED_BALANCE_QTY { get; set; }
        public decimal UNFORMED_BALANCE_QTY { get; set; }
        public decimal WIP_QTY { get; set; }
        public decimal FINISH_STOCK { get; set; }
        public decimal TOTAL_STOCK { get; set; }


        public decimal OP_BAL { get; set; }
        public decimal OP_ADJ { get; set; }
        public decimal MRR_QTY { get; set; }
        public decimal IRR_QTY { get; set; }
        public decimal ITC_QTY { get; set; }
     

        public decimal BAL_QTY { get; set; }

        public decimal OPPENING_BAL_QTY { get; set; }
        public decimal COLISING_QTY { get; set; }
        public decimal ADJUST_QTY { get; set; }
        public decimal IRR_DEPT_QTY { get; set; }
        public decimal IRR_STORE_QTY { get; set; }
        public decimal IRR_PROD_QTY { get; set; }
        public decimal IRR_BAL_QTY { get; set; }
        public decimal IRR_OTHER_QTY { get; set; }


        public decimal ITC_DEPT_QTY { get; set; }
        public decimal ITC_STORE_QTY { get; set; }
        public decimal ITC_PROD_QTY { get; set; }
        public decimal ITC_BAL_QTY { get; set; }
        public string DEPARTMENT_NAME { get; set; }    
        public int ORDER_NO { get; set; }
        public decimal REJECT_QTY { get; set; }
        public decimal REJECT_PRT { get; set; }        
     
        public decimal CURR_ISS_PAST { get; set; }
         public decimal CURR_ISS_IB { get; set; }
        public decimal CURR_ISS_RED_OXID { get; set; }
        public decimal CURR_ISS_ST { get; set; }
        public decimal CURR_ISS_OTHERS { get; set; }
        public decimal OP_ADJUST_QTY { get; set; }
        public decimal UF_IRR_QTY { get; set; }

        public decimal UF_ITC_QTY { get; set; }

        public decimal PRODUCTION_CAPACITY { get; set; }
        public int DATEDIFF { get; set; }

        public decimal CUR_F_ADJ_QTY { get; set; }

        public decimal CUR_U_ADJ_QTY { get; set; }

        public decimal CUR_WIP_ADJ_QTY { get; set; }

        public decimal NEW_COLISING_QTY { get; set; }

        public decimal NEW_WIP_QTY { get; set; }
        public decimal GRID_PC_KG_STD { get; set; }
        public decimal PASTE_PC_KG_STD { get; set; }
        public string IS_BY_PRODUCT { get; set; }

        public decimal STOCK_TRANS_ITEM_TO_ITEM { get; set; }
        public decimal STOCK_TRANS_ITEM_TO_ITEM_ISSUE { get; set; }


        public decimal OXIDE_TOTAL_OUTSALE { get; set; }

        public decimal TOTAL_ISSUE { get; set; }

        public decimal TOTAL_RECEIVE { get; set; }
        public int ITEM_ORDER { get; set; }
        public decimal TOTAL_REJECT_WEIGHT { get; set; }
    }
}
