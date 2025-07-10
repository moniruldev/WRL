using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [Serializable]
    public class dcMaterialStock
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
        public decimal OP_MRR_QTY { get; set; }
        public decimal OP_IRR_QTY { get; set; }
        public decimal OP_ITC_QTY { get; set; }
        public decimal OP_OUTSALES_QTY { get; set; }
        public decimal OP_ROTARY_QTY { get; set; }
        public decimal OP_LOANRP_QTY { get; set; }

        public decimal OP_BAL { get; set; }
        public decimal OP_ADJ { get; set; }
        public decimal MRR_QTY { get; set; }
        public decimal IRR_QTY { get; set; }
        public decimal ITC_QTY { get; set; }
        public decimal OUTSALES_QTY { get; set; }

        public decimal ROTARY_QTY { get; set; }
        public decimal LOANRP_QTY { get; set; }

        public decimal RTN_QTY { get; set; }
        public decimal BAL_QTY { get; set; }

        public decimal OPPENING_BAL_QTY { get; set; }
        public decimal COLISING_QTY { get; set; }
        public decimal CLOSING_QTY { get; set; }

        public decimal CLOSING_QTY_wt { get; set; }
        public decimal TOT_RECEIVE_QNTY { get; set; }

        public decimal TOT_ISSUE_QNTY { get; set; }
        public decimal CUR_MONTH_FORCAST { get; set; }

        public decimal RE_ORDER_LEVEL { get; set; }
        public decimal ITEM_FC_QTY { get; set; }
        public decimal SAFTY_STOCK_LEVEL { get; set; }

        public decimal ADJUST_QTY { get; set; }

        public decimal OPWITHTOTRCV_QTY { get; set; }
        public decimal CUR_IRR_QTY { get; set; }




        //for calculating department stock

        public decimal IRR_DEPT_QTY { get; set; }
        public decimal IRR_STORE_QTY { get; set; }
        public decimal IRR_PROD_QTY { get; set; }
        public decimal IRR_BAL_QTY { get; set; }

        public decimal USUABLE_IRR_BAL_QTY { get; set; }

        public decimal ITC_DEPT_QTY { get; set; }
        public decimal ITC_ASSEMBLY_QTY { get; set; }
        public decimal ITC_FORMATION_QTY { get; set; }
        public decimal ITC_STORE_QTY { get; set; }
        public decimal ITC_PROD_QTY { get; set; }
        public decimal ITC_BAL_QTY { get; set; }
        public string DEPARTMENT_NAME { get; set; }

        public string ITEM_SNS_NAME { get; set; }

        public decimal ADD_ADJUST_QTY { get; set; }
        public decimal DEDUCT_ADJUST_QTY { get; set; }
        public decimal ITEM_STANDARD_WEIGHT_KG { get; set; }

        public decimal PANNEL_QTY { get; set; }
        public int UOM_ID_P { get; set; }
        public string UOM_NAME_P { get; set; }

        public decimal USUABLE_QTY { get; set; }
        public decimal USUABLE_IRR_PROD_QTY { get; set; }
        public decimal UN_USUABLE_QTY { get; set; }
        public int ORDER_NO { get; set; }

        public decimal REJECT_QTY_RCV { get; set; }

        public decimal REJECT_QTY_ISS { get; set; }

        public decimal RCV_FROM_IB { get; set; }
        public decimal RCV_FROM_PASTING { get; set; }

        public decimal REJECT_QTY { get; set; }
        public decimal UNFORMED_QTY { get; set; }



        public decimal FORMATION_ISS_QTY { get; set; }

        public decimal ASSEMBLY_ISS_QTY { get; set; }

        public decimal OP_RTN_QTY { get; set; }

        public string SHIFT { get; set; }
        public string AUTH_STATUS { get; set; }
        public DateTime? PRODUCTION_DATE { get; set; }
        public string PROD_BATCH_NO { get; set; }
        public decimal ITEM_QTY { get; set; }
        public string PROCESSTYPE { get; set; }
        public decimal PASTE_PC_KG { get; set; }
        public decimal TOTAL_PASTE_PC_KG { get; set; }

        public decimal TOTAL_GRID_STD_WEIGHT_KG { get; set; }
        public decimal ISSUE_FOR_PRODUCTION { get; set; }
        public decimal ISSURE_FOR_DROSS { get; set; }
        public decimal OP_ADJUST_QTY { get; set; }
        public decimal PASTED_PLATE_PC_KG_STD { get; set; }
        public decimal TOTAL_SMALL_PARTS_STD_KG { get; set; }
        public string IS_BY_PRODUCT { get; set; }
        public decimal TOTAL_PASTED_PLATE_PC_KG_STD { get; set; }
        public decimal CUR_ADJUST_QTY { get; set; }
        public decimal CUR_REJECT_QTY { get; set; }


        public decimal RE_ORDER_QTY { get; set; }
        public decimal SAFETY_STOCK_QTY { get; set; }
        public decimal OP_GD_GRID_IRR_QTY { get; set; }
        public decimal OP_GD_GRID_ITC_QTY { get; set; }
        public decimal OP_GD_GRID_BAL { get; set; }
        public decimal IRR_GD_GRID_QTY { get; set; }
        public decimal ITC_GD_GRID_QTY { get; set; }
        public decimal ITC_GD_GRID_BAL_QTY { get; set; }
        public decimal OP_REJ_GRID_IRR_QTY { get; set; }
        public decimal OP_REJ_GRID_ITC_QTY { get; set; }
        public decimal OP_REJ_GRID_BAL_QTY { get; set; }
        public decimal IRR_REJ_GRID_QTY { get; set; }
        public decimal ITC_REJ_GRID_QTY { get; set; }
        public decimal ITC_REJ_GRID_BAL_QTY { get; set; }

        public decimal TOTAL_GOOD_GRID_RCV_QTY { get; set; }

        public decimal TOTAL_REJ_GRID_RCV_QTY { get; set; }

        public decimal TOTAL_REJ_GRID_WET { get; set; }

        public decimal ISSUE_TO_STORE_REJ_GRID { get; set; }




        public decimal OP_GD_GRID_ADJ_QTY { get; set; }

        public decimal CUR_PACKED_QTY { get; set; }

        public decimal CUR_UNLOADING_QTY { get; set; }

        public decimal CUR_CHARGED_QTY { get; set; }

        public decimal CUR_GREEN_BAT_RCV_QTY { get; set; }

        public decimal CUR_DRY_BAT_RCV_QTY { get; set; }

        public decimal CUR_LOADING_QTY { get; set; }

        public decimal CUR_UN_PACKED_QTY { get; set; }

        public decimal ITC_RB_QTY { get; set; }
        public decimal CUR_UN_CHARGED_BAL_QTY { get; set; }

        public int INV_TRANS_TYPE_ID { get; set; }

        public string TRANS_REMARKS { get; set; }

        public string STOCK_DEPARTMENT { get; set; }
        public string FROM_DEPARTMENT { get; set; }

        public string TO_DEPARTMENT { get; set; }
        public string INV_TRANS_TYPE_NAME { get; set; }


        public decimal CHARGE_PAC_QTY { get; set; }
        public decimal CUR_GREEN_BAT_LOADING_QTY { get; set; }

        public decimal CUR_DRY_BAT_LOADING_QTY { get; set; }

        public decimal CUR_UN_CHARGED_GREEN_BAT_QTY { get; set; }

        public decimal CUR_UN_CHARGED_DRY_BAT_QTY { get; set; }

        public decimal OP_UN_PACKED_QTY { get; set; }
        public decimal TOTAL_UN_PACKED_QTY { get; set; }
        public decimal CUR_CHARGED_GREEN_BAT_QTY { get; set; }

        public decimal CUR_CHARGED_DRY_BAT_QTY { get; set; }

        public decimal CUR_REJECT_GREEN_BAT_QTY { get; set; }

        public decimal CUR_REJECT_DRY_BAT_QTY { get; set; }

        public decimal GP_BAT_RCV_QTY { get; set; }

        public decimal DRY_BAT_RCV_QTY { get; set; }
        public decimal OP_GREEN_BAT_LOADING_QTY { get; set; }

        public decimal OP_GREEN_BAT_CHARGED_QTY { get; set; }
        public decimal OP_DRY_BAT_LOADING_QTY { get; set; }

        public decimal OP_DRY_BAT_CHARGED_QTY { get; set; }
        public decimal OP_PACKET_QTY { get; set; }
        public decimal OP_CHARGE_UN_PACKET_QTY { get; set; }

        public decimal OP_CHARGE_QTY { get; set; }  // Loading
        public decimal OP_ON_CHARGING { get; set; }
        public decimal OP_TOTAL_CHARGED_QTY { get; set; }

        public decimal TOTAL_CHARGED_QTY { get; set; }

        public decimal TOTAL_UNCHARGED_GP_QTY { get; set; }
        public decimal TOTAL_UNCHARGED_DRY_QTY { get; set; }

        public decimal TOTAL_UNCHARGED_QTY { get; set; }
        public decimal TOTAL_ON_CHARGING { get; set; }

        public decimal FULL_IRR_QTY { get; set; }
        public decimal FULL_PAC_QTY { get; set; }
        public decimal FULL_LOADING { get; set; }

        public decimal IRR_REJECT { get; set; }
        public decimal IRR_WASTAGE_QTY { get; set; }

        public decimal OP_IRR_REJECT_QTY { get; set; }
        public decimal ITC_OTHERS { get; set; }
        public DateTime? TRANS_DATE { get; set; }

        public decimal IRR_RECP_QTY { get; set; }
        public decimal ITC_TRANS_QTY { get; set; }





public decimal DEPT_ISS_CURRENT_MONTH_QTY { get; set; }
public decimal DEPT_ISS_CURRENT_MONTH_WEIGHT { get; set; }
public decimal DEPT_ISS_TODAY_QTY { get; set; }
public decimal DEPT_ISS_TODATY_WEIGHT { get; set; }
 
public decimal DEPT_RCV_CURRENT_MONTH_QTY { get; set; }
public decimal DEPT_RCV_CURRENT_MONTH_WEIGHT { get; set; }
public decimal DEPT_RCV_TODAY_QTY { get; set; }
public decimal DEPT_RCV_TODATY_WEIGHT { get; set; }



public decimal STR_RCV_CURRENT_MONTH_QTY { get; set; }
public decimal STR_RCV_CURRENT_MONTH_WEIGHT{ get; set; }
public decimal STR_RCV_TODAY_QTY{ get; set; }
public decimal STR_RCV_TODAY_WEIGHT { get; set; }
public decimal STR_ISS_CURRENT_MONTH_QTY { get; set; }
public decimal STR_ISS_CURRENT_MONTH_WEIGHT { get; set; }
public decimal STR_ISS_TODAY_QTY{ get; set; }
public decimal STR_ISS_TODAY_WEIGHT { get; set; }

public decimal PROD_TODAY_QTY { get; set; }
public decimal PROD_TODAY_WEIGHT { get; set; }
public decimal PROD_CURRENT_QTY { get; set; }
public decimal PROD_CURRENT_WEIGHT { get; set; }

public decimal REMAINING_QTY { get; set; }
public decimal SCRAP_MIXTURE_QTY { get; set; }

//public decimal SCRAP_MIXTURE_QTY { get; set; }

public int BAT_ITEM_ID { get; set; }
public string BAT_ITEM_NAME { set; get; }
 public decimal  BOM_WEIGHT { set; get; }
  public decimal  PROD_QTY { set; get; }
  public decimal  TOTAL_WEIGHT{ set; get; }
  public decimal  PROD_USED_QTY  { set; get; }
  public decimal DIFF_WEIGHT { set; get; }
  public string pMonthYear { set; get; }
  public decimal IB_CUTTING { set; get; }
  public string pMonth { set; get; }
    }
}
