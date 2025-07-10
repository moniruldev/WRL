using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
    [Serializable]
    public class rcAssemblyFinishedStock
    {
        public int ITEM_GROUP_ID { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public int ITEM_ID { get; set; }
        public int UOM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }       
        public string UOM_NAME { get; set; }
        public int ITEM_TYPE_ID { get; set; }
        public string ITEM_TYPE_NAME { get; set; }

        public int ITEM_CLASS_ID { get; set; }
        public string ITEM_CLASS_NAME { get; set; }

        public string DEPARTMENT_NAME { get; set; }
        public int ORDER_NO { get; set; }


        //good/finish battery Opening
        public decimal OP_GD_IRR_QTY { get; set; }
        public decimal OP_GD_ITC_QTY { get; set; }
        public decimal OP_GD_BAL { get; set; }


        //Market return battery Opening
        public decimal OP_MR_IRR_QTY { get; set; }
        public decimal OP_MR_ITC_QTY { get; set; }
        public decimal OP_MR_BAL_QTY { get; set; }


        //opening WIP

        public decimal OP_ASSEMBLY_QTY { get; set; }
        public decimal OP_PACKING_QTY { get; set; }
        public decimal OP_PACKING_BAL_QTY { get; set; }
        public decimal OP_GD_WIP { get; set; }
        public decimal OP_MR_WIP { get; set; }
     


        //all receive quantity

        //good battery receive
        public decimal IRR_DEPT_QTY { get; set; }
        public decimal IRR_STORE_QTY { get; set; }
        public decimal IRR_PROD_QTY { get; set; }
        public decimal IRR_BAL_QTY { get; set; }

        //Market Battery return quantity receive
        public decimal IRR_RETURN_BAT_QTY { get; set; }


        //all itc quantity
        public decimal ITC_SOLAR_QTY { get; set; }
        public decimal ITC_STORE_QTY { get; set; }
        public decimal ITC_RETURN_BAT_QTY { get; set; }
        public decimal ITC_BAL_QTY { get; set; }

        public decimal OP_BAL { get; set; }
        public decimal OP_ADJ { get; set; }
        public decimal ADJUST_QTY { get; set; }



        public decimal OP_PACKING_ADJUST_QTY { get; set; }

        public decimal OP_PACKING_RCV_QTY { get; set; }

        public decimal OP_PACKING_ISS_QTY { get; set; }

        public decimal OP_ASSEMBEL_ADJUST_QTY { get; set; }

        public decimal OP_ASSEMBEL_RCV_QTY { get; set; }

        public decimal OP_ASSEMBEL_ISS_QTY { get; set; }

        public decimal OP_ASSEMBEL_BAL_QTY { get; set; }

        public decimal CUR_ASSEM_PROD_QTY { get; set; }

        public decimal CUR_ASSEM_USED_QTY { get; set; }

        public decimal CUR_ASSEM_ADJUST_QTY { get; set; }



        public decimal ASSEM_CLOSING_QTY { get; set; }

        public decimal PACKING_CLOSING_QTY { get; set; }

        public decimal CUR_PACKING_ADJUST_QTY { get; set; }

        public decimal CUR_PACKING_RCV_QTY { get; set; }

        public decimal OP_CUR_TOT_PACKING_QTY { get; set; }

        public decimal OP_CUR_TOT_ASSEMBLY_QTY { get; set; }

        //Plate Report Purpose
         public decimal OP_GD_PLATE_IRR_QTY { get; set; }

        public decimal OP_GD_PLATE_ITC_QTY { get; set; }

         public decimal OP_GD_PLATE_BAL { get; set; }
         public decimal IRR_GD_PLATE_QTY { get; set; }
         public decimal ITC_GD_PLATE_QTY { get; set; }
         public decimal ITC_GD_PLATE_BAL_QTY { get; set; }
         public decimal OP_REC_PLATE_IRR_QTY { get; set; }
         public decimal OP_REC_PLATE_ITC_QTY { get; set; }
         public decimal OP_REC_PLATE_BAL_QTY { get; set; }
        
         public decimal IRR_REC_PLATE_QTY { get; set; }
         public decimal ITC_REC_PLATE_QTY { get; set; }
         public decimal ITC_REC_BAL_QTY { get; set; }
         public decimal OP_REJ_PLATE_IRR_QTY { get; set; }
         public decimal OP_REJ_PLATE_ITC_QTY { get; set; }
         public decimal OP_REJ_PLATE_BAL_QTY { get; set; }
         public decimal IRR_REJ_PLATE_QTY { get; set; }
         public decimal ITC_REJ_PLATE_QTY { get; set; }
         public decimal ITC_REJ_BAL_QTY { get; set; }

         public decimal TOTAL_GD_PLATE_RCV_QTY { get; set; }

         public decimal TOTAL_REC_PLATE_RCV_QTY { get; set; }
         public decimal TOTAL_REJ_PLATE_RCV_QTY { get; set; }
         public decimal TOTAL_PLATE_CONS_AS_PROD_QTY { get; set; }
         public decimal TOTAL_PLATE_CONS_WITH_REJ_QTY { get; set; }
         public decimal TOTAL_WIP_REMAIN_QTY { get; set; }

        //p,serv,rnd,others Battery
         public decimal  OP_P_BAT_IRR_QTY { get; set; }
         public decimal OP_P_BAT_ITC_QTY { get; set; }
         public decimal OP_P_BAT_BAL_QTY { get; set; }
         public decimal CUR_P_BAT_IRR_QTY { get; set; }
         public decimal CUR_P_BAT_ITC_QTY { get; set; }
         public decimal OP_SERVICE_BAT_IRR_QTY { get; set; }
         public decimal OP_SERVICE_BAT_ITC_QTY { get; set; }
         public decimal OP_SERVICE_BAT_BAL_QTY { get; set; }
         public decimal CUR_SERVICE_BAT_IRR_QTY { get; set; }

         public decimal CUR_SERVICE_BAT_ITC_QTY { get; set; }

         public decimal OP_RND_BAT_IRR_QTY { get; set; }
         public decimal OP_RND_BAT_ITC_QTY { get; set; }
         public decimal OP_RND_BAT_BAL_QTY { get; set; }
         public decimal CUR_RND_BAT_IRR_QTY { get; set; }
         public decimal CUR_RND_BAT_ITC_QTY { get; set; }
          public decimal OP_OTHERS_BAT_IRR_QTY { get; set; }
         public decimal OP_OTHERS_BAT_ITC_QTY { get; set; }
         public decimal OP_OTHERS_BAT_BAL_QTY { get; set; }
         public decimal CUR_OTHERS_BAT_IRR_QTY { get; set; }
         public decimal CUR_OTHERS_BAT_ITC_QTY { get; set; }

         public decimal CUR_TOTAL_BAT_PROD_QTY { get; set; }
         public decimal OP_TOTAL_ASS_PROD_BAT_QRT { get; set; }

         public decimal TOTAL_DELIVERY_TO_MRB { get; set; }
         public decimal TOTAL_DELIVERY_FROM_ASSEMBLY { get; set; }
         public decimal OP_CUR_TOT_ASSEMBLY_DEPT_QTY { get; set; }

         public decimal MR_BATTERY_CLOSING_QTY { get; set; }
         public decimal SERVICE_BATTERY_CLOSING_QTY { get; set; }
         public decimal RND_BATTERY_CLOSING_QTY { get; set; }
         public decimal OTHERS_BATTERY_CLOSING_QTY { get; set; }
         public decimal P_BATTERY_CLOSING_QTY { get; set; }

         public decimal TOTAL_ASSEMBLY_CLOSING_QTY { get; set; }

         public decimal TOTAL_ASSEMBLY_OPENING_QTY { get; set; }





         public decimal OP_GD_PLATE_ADJ_QTY { get; set; }

         public decimal OP_REC_PLATE_ADJ_QTY { get; set; }

         public decimal OP_REJ_PLATE_ADJ_QTY { get; set; }

         public decimal TOTAL_REC_REMAIN_QTY { get; set; }
        public decimal TOTAL_REJ_REMAIN_QTY { get; set; }


        public decimal ITC_REJ_STO_PLATE_QTY { get; set; }
        public decimal OP_REJ_STO_PLATE_QTY { get; set; }
        public int ITEM_ORDER { get; set; }
        public int BATERY_CAT_SL_NO { get; set; }
        public decimal REJ_STOCK_QTY { get; set; }
        public string BATERY_CAT_DESCR { get; set; }

        public int CUR_CON_IRR_QTY { get; set; }
        public int CONVERT_BAT_ISS { get; set; }
        public int CONVERT_BAT_RCV { get; set; }
    }
}
