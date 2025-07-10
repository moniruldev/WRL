using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.ProductionRC
{
    [Serializable]
    public class rcProduction
    {

        #region private members

        private int m_PROD_DTL_ID = 0;
        private int m_PROD_MST_ID = 0;
        private int m_ITEM_GROUP_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_WEIGHT = 0;
        private int m_WEIGHT_UOM_ID = 0;
        private int m_BOM_ID = 0;
        private int m_MACHINE_ID = 0;
        private int m_PANEL_UOM_ID = 0;
        private decimal m_ITEM_PANEL_QTY = 0;
        private int m_SLNO = 0;
        private string m_REMARKS = "";
        private int m_PANEL_PC = 0;
        private string m_GRID_BATCH = "";

        // Formation Required
        private int m_AMPERE = 0;
        private int m_CYCLETIME = 0;
        private decimal m_SULFURIC_GRAVITY = 0;
        private int m_TEMPARATURE = 0;
        private int m_UNFORMED_QTY = 0;
        private int m_FORMED_QTY = 0;
        private string m_PASTING_BATCH = "";
        private string m_FORMATION_STARTTIME = "";
        private DateTime? m_FORMATION_OFFDATE = null;
        private string m_FORMATION_OFFTIME = "";
        private int m_REJECT_QTY = 0;
        // Formation End
        private string m_OPERATOR_ID = "";
        private int m_USED_BAR_PC = 0;
        private int m_BAR_TYPE = 0;
        private decimal m_USED_QTY_KG = 0;
        private decimal m_BAR_WEIGHT = 0;

        //--Packing---//
        private string m_IS_PACKING = String.Empty;
        private string m_PACK_FINISHED_BATCH = String.Empty;
        private decimal m_UF_REUSE_QTY = 0;

        //Rejection
        private string m_REJECT_ITEM_TYPE = String.Empty;
        private string m_REJECTION_REASON = String.Empty;
        private string m_REJECTION_DET_REMARKS = String.Empty;
        
        #endregion  //private members

        public int PROD_DTL_ID
        {
            get { return this.m_PROD_DTL_ID; }
            set
            {
                this.m_PROD_DTL_ID = value;

            }
        }


        public int PROD_MST_ID
        {
            get { return this.m_PROD_MST_ID; }
            set
            {
                this.m_PROD_MST_ID = value;

            }
        }


        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;

            }
        }


        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;

            }
        }


        public decimal ITEM_WEIGHT
        {
            get { return this.m_ITEM_WEIGHT; }
            set
            {
                this.m_ITEM_WEIGHT = value;

            }
        }

        public int WEIGHT_UOM_ID
        {
            get { return this.m_WEIGHT_UOM_ID; }
            set
            {
                this.m_WEIGHT_UOM_ID = value;

            }
        }




        public int BOM_ID
        {
            get { return this.m_BOM_ID; }
            set
            {
                this.m_BOM_ID = value;

            }
        }



        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;

            }
        }


        public int MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;

            }
        }


        public int PANEL_UOM_ID
        {
            get { return this.m_PANEL_UOM_ID; }
            set
            {
                this.m_PANEL_UOM_ID = value;

            }
        }


        public decimal ITEM_PANEL_QTY
        {
            get { return this.m_ITEM_PANEL_QTY; }
            set
            {
                this.m_ITEM_PANEL_QTY = value;

            }
        }

        public decimal UF_REUSE_QTY
        {
            get { return this.m_UF_REUSE_QTY; }
            set
            {
                this.m_UF_REUSE_QTY = value;

            }
        }

        
        public int SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;

            }
        }




        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;

            }
        }


        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;

            }
        }




        public int PANEL_PC
        {
            get { return this.m_PANEL_PC; }
            set
            {
                this.m_PANEL_PC = value;

            }
        }


        public string OPERATOR_ID
        {
            get { return this.m_OPERATOR_ID; }
            set
            {
                this.m_OPERATOR_ID = value;

            }
        }


        public int USED_BAR_PC
        {
            get { return this.m_USED_BAR_PC; }
            set
            {
                this.m_USED_BAR_PC = value;

            }
        }


        public int BAR_TYPE
        {
            get { return this.m_BAR_TYPE; }
            set
            {
                this.m_BAR_TYPE = value;

            }
        }


        public decimal USED_QTY_KG
        {
            get { return this.m_USED_QTY_KG; }
            set
            {
                this.m_USED_QTY_KG = value;

            }
        }




        public decimal BAR_WEIGHT
        {
            get { return this.m_BAR_WEIGHT; }
            set
            {
                this.m_BAR_WEIGHT = value;

            }
        }



        public string GRID_BATCH
        {
            get { return this.m_GRID_BATCH; }
            set
            {
                this.m_GRID_BATCH = value;

            }
        }


        // Formation Required Start //

        public int AMPERE
        {
            get { return this.m_AMPERE; }
            set
            {
                this.m_AMPERE = value;

            }
        }


        public int CYCLETIME
        {
            get { return this.m_CYCLETIME; }
            set
            {
                this.m_CYCLETIME = value;

            }
        }


        public decimal SULFURIC_GRAVITY
        {
            get { return this.m_SULFURIC_GRAVITY; }
            set
            {
                this.m_SULFURIC_GRAVITY = value;

            }
        }


        public int TEMPARATURE
        {
            get { return this.m_TEMPARATURE; }
            set
            {
                this.m_TEMPARATURE = value;

            }
        }


        public int UNFORMED_QTY
        {
            get { return this.m_UNFORMED_QTY; }
            set
            {
                this.m_UNFORMED_QTY = value;

            }
        }


        public int FORMED_QTY
        {
            get { return this.m_FORMED_QTY; }
            set
            {
                this.m_FORMED_QTY = value;

            }
        }


        public string PASTING_BATCH
        {
            get { return this.m_PASTING_BATCH; }
            set
            {
                this.m_PASTING_BATCH = value;

            }
        }




        public string FORMATION_STARTTIME
        {
            get { return this.m_FORMATION_STARTTIME; }
            set
            {
                this.m_FORMATION_STARTTIME = value;

            }
        }


        public DateTime? FORMATION_OFFDATE
        {
            get { return this.m_FORMATION_OFFDATE; }
            set
            {
                this.m_FORMATION_OFFDATE = value;

            }
        }


        public string FORMATION_OFFTIME
        {
            get { return this.m_FORMATION_OFFTIME; }
            set
            {
                this.m_FORMATION_OFFTIME = value;

            }
        }



        public int REJECT_QTY
        {
            get { return this.m_REJECT_QTY; }
            set
            {
                this.m_REJECT_QTY = value;

            }
        }




        // Formation Required End //



        public string IS_PACKING
        {
            get { return this.m_IS_PACKING; }
            set
            {
                this.m_IS_PACKING = value;

            }
        }




        public string PACK_FINISHED_BATCH
        {
            get { return this.m_PACK_FINISHED_BATCH; }
            set
            {
                this.m_PACK_FINISHED_BATCH = value;

            }
        }

        private string m_ITEM_GROUP_DESC = "";
        private string m_ITEM_NAME = "";
        private string m_UOM_NAME = "";
        private string m_BOM_NAME = "";
        private string m_PANEL_UOM_NAME = "";
        private string m_WEIGHT_UOM_NAME = "";
        private string m_MACHINE_NAME = "";
        private int m_BALPACKINGQTY = 0;
        private int m_USED_ITEM_ID = 0;
        private string m_USED_ITEM_NAME = "";

        public int USED_ITEM_ID
        {
            get { return this.m_USED_ITEM_ID; }
            set
            {
                this.m_USED_ITEM_ID = value;

            }
        }
        public string USED_ITEM_NAME
        {
            get { return this.m_USED_ITEM_NAME; }
            set
            {
                this.m_USED_ITEM_NAME = value;
            }
        }
        public string ITEM_GROUP_DESC
        {
            get { return this.m_ITEM_GROUP_DESC; }
            set
            {
                this.m_ITEM_GROUP_DESC = value;
            }
        }

        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
            }
        }


        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
            }
        }


        public string BOM_NAME
        {
            get { return this.m_BOM_NAME; }
            set
            {
                this.m_BOM_NAME = value;
            }
        }



        public string PANEL_UOM_NAME
        {
            get { return this.m_PANEL_UOM_NAME; }
            set
            {
                this.m_PANEL_UOM_NAME = value;
            }
        }


        public string WEIGHT_UOM_NAME
        {
            get { return this.m_WEIGHT_UOM_NAME; }
            set
            {
                this.m_WEIGHT_UOM_NAME = value;
            }
        }


        public string MACHINE_NAME
        {
            get { return this.m_MACHINE_NAME; }
            set
            {
                this.m_MACHINE_NAME = value;
            }
        }

        private string m_BAR_TYPE_NAME = "";
        public string BAR_TYPE_NAME
        {
            get { return this.m_BAR_TYPE_NAME; }
            set { this.m_BAR_TYPE_NAME = value; }
        }

        private string m_OPERATOR_NAME = "";

        public string OPERATOR_NAME
        {
            get { return this.m_OPERATOR_NAME; }
            set { this.m_OPERATOR_NAME = value; }
        }

        public int BALPACKINGQTY
        {
            get { return this.m_BALPACKINGQTY; }
            set { this.m_BALPACKINGQTY = value; }
        }





        // Production Mst 
        private int m_PROD_ID = 0;
        private string m_PROD_NO = "";

        private string m_SUPERVISOR_NAME = "";
        private string m_SUPERVISOR_ID = "";
        private int m_DEPT_ID = 0;
        private string m_DEPARTMENT_NAME = "";
        private string m_REF_NO_MANUAL = "";
        private int m_FORECUSTMONTH = 0;
        private int m_FORECUSTYEAR = 0;
        private string m_RM_FC_DESC = "";

        private int m_FORECUST_ID = 0;
        private string m_SHIFT_ID = "";
        private string m_SHIFT_NAME = "";
        private DateTime? m_BATCH_STARTTIME = null;
        private DateTime? m_BATCH_ENDTIME = null;
        private string m_STARTTIME = "";
        private string m_ENDTIME = "";
        private int m_PROCESS_CODE = 0;
        private DateTime? m_PRODUCTION_DATE = null;
        private int m_REJECTED_QTY = 0;
        private string m_BATCH_ID = "";
        private string m_PROD_BATCH_NO = "";



        public int PROD_ID
        {
            get { return this.m_PROD_ID; }
            set { this.m_PROD_ID = value; }
        }

        public string PROD_NO
        {
            get { return this.m_PROD_NO; }
            set { this.m_PROD_NO = value; }
        }

        public string SUPERVISOR_NAME
        {
            get { return this.m_SUPERVISOR_NAME; }
            set { this.m_SUPERVISOR_NAME = value; }
        }

        public string SUPERVISOR_ID
        {
            get { return this.m_SUPERVISOR_ID; }
            set { this.m_SUPERVISOR_ID = value; }
        }

        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set { this.m_DEPT_ID = value; }
        }

        public string DEPARTMENT_NAME
        {
            get { return this.m_DEPARTMENT_NAME; }
            set { this.m_DEPARTMENT_NAME = value; }
        }

        public string REF_NO_MANUAL
        {
            get { return this.m_REF_NO_MANUAL; }
            set { this.m_REF_NO_MANUAL = value; }
        }
        public int FORECUSTMONTH
        {
            get { return this.m_FORECUSTMONTH; }
            set { this.m_FORECUSTMONTH = value; }
        }

        public int FORECUSTYEAR
        {
            get { return this.m_FORECUSTYEAR; }
            set { this.m_FORECUSTYEAR = value; }
        }

        public string RM_FC_DESC
        {
            get { return this.m_RM_FC_DESC; }
            set { this.m_RM_FC_DESC = value; }
        }

        public int FORECUST_ID
        {
            get { return this.m_FORECUST_ID; }
            set { this.m_FORECUST_ID = value; }
        }

        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set { this.m_SHIFT_ID = value; }
        }

        public string SHIFT_NAME
        {
            get { return this.m_SHIFT_NAME; }
            set { this.m_SHIFT_NAME = value; }
        }

        public DateTime? BATCH_STARTTIME
        {
            get { return this.m_BATCH_STARTTIME; }
            set { this.m_BATCH_STARTTIME = value; }
        }

        public DateTime? BATCH_ENDTIME
        {
            get { return this.m_BATCH_ENDTIME; }
            set { this.m_BATCH_ENDTIME = value; }
        }

        public string STARTTIME
        {
            get { return this.m_STARTTIME; }
            set { this.m_STARTTIME = value; }
        }

        public string ENDTIME
        {
            get { return this.m_ENDTIME; }
            set { this.m_ENDTIME = value; }
        }


        public int PROCESS_CODE
        {
            get { return this.m_PROCESS_CODE; }
            set { this.m_PROCESS_CODE = value; }
        }


        public DateTime? PRODUCTION_DATE
        {
            get { return this.m_PRODUCTION_DATE; }
            set { this.m_PRODUCTION_DATE = value; }
        }


        public int REJECTED_QTY
        {
            get { return this.m_REJECTED_QTY; }
            set { this.m_REJECTED_QTY = value; }
        }


        public string BATCH_ID
        {
            get { return this.m_BATCH_ID; }
            set { this.m_BATCH_ID = value; }
        }

        public string PROD_BATCH_NO
        {
            get { return this.m_PROD_BATCH_NO; }
            set { this.m_PROD_BATCH_NO = value; }
        }

        private string m_CREATE_BY = "";
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set { this.m_CREATE_BY = value; }
        }

        private DateTime? m_ENTRY_DATE = null;
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set { this.m_ENTRY_DATE = value; }
        }

        private decimal? m_MORNING_SHIFT_QTY = null;
        public decimal? MORNING_SHIFT_QTY
        {
            get { return this.m_MORNING_SHIFT_QTY; }
            set
            {
                this.m_MORNING_SHIFT_QTY = value;

            }
        }

        private decimal? m_EVENING_SHIFT_QTY = null;
        public decimal? EVENING_SHIFT_QTY
        {
            get { return this.m_EVENING_SHIFT_QTY; }
            set
            {
                this.m_EVENING_SHIFT_QTY = value;

            }
        }

        private decimal? m_NIGHT_SHIFT_QTY = null;
        public decimal? NIGHT_SHIFT_QTY
        {
            get { return this.m_NIGHT_SHIFT_QTY; }
            set
            {
                this.m_NIGHT_SHIFT_QTY = value;

            }
        }

        private decimal? m_TOTAL_QTY = null;
        public decimal? TOTAL_QTY
        {
            get { return this.m_TOTAL_QTY; }
            set
            {
                this.m_TOTAL_QTY = value;

            }
        }


        private decimal? m_CLOSING_QTY = null;
        public decimal? CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set
            {
                this.m_CLOSING_QTY = value;

            }
        }



        private int? m_SHIFT_MSTID = null;
        public int? SHIFT_MSTID
        {
            get { return this.m_SHIFT_MSTID; }
            set
            {
                this.m_SHIFT_MSTID = value;

            }
        }

        private decimal? m_ITEM_STANDARD_WEIGHT_KG = null;
        public decimal? ITEM_STANDARD_WEIGHT_KG
        {
            get { return this.m_ITEM_STANDARD_WEIGHT_KG; }
            set
            {
                this.m_ITEM_STANDARD_WEIGHT_KG = value;

            }
        }


        private int? m_PANEL_QTY = null;
        public int? PANEL_QTY
        {
            get { return this.m_PANEL_QTY; }
            set
            {
                this.m_PANEL_QTY = value;

            }
        }


        public string SHIFT { get; set; }
        public string AUTH_STATUS { get; set; }
        //public DateTime? PRODUCTION_DATE { get; set; }
        //public string PROD_BATCH_NO { get; set; }
        //public decimal ITEM_QTY { get; set; }
        public string PROCESSTYPE { get; set; }
        public string FULL_NAME { get; set; }
        //public int SHIFT_MSTID { get; set; }

        public decimal PRODUCTION_CAPACITY { get; set; }
        public int DATEDIFF { get; set; }


        public string REJECT_ITEM_TYPE
        {
            get { return this.m_REJECT_ITEM_TYPE; }
            set { this.m_REJECT_ITEM_TYPE = value; }
        }

        public string REJECTION_REASON
        {
            get { return this.m_REJECTION_REASON; }
            set { this.m_REJECTION_REASON = value; }
        }

        public string REJECTION_DET_REMARKS
        {
            get { return this.m_REJECTION_DET_REMARKS; }
            set { this.m_REJECTION_DET_REMARKS= value; }
        }


        public decimal RECOVERY_QTY { set; get; }
        public string MRB_PLATE_NAME { set; get; }
        public decimal MRB_PLATE_QTY { set; get; }
        public decimal MRB_PLATE_WEIGHT { set; get; }
        public decimal SCRAP_BATTERY_WEIGHT { set; get; }

        public string pMonthYear { set; get; }
        public string pMonth { set; get; }

        public int CUR_CON_IRR_QTY { get; set; }
        public int CONVERT_BAT_ISS { get; set; }
        public int CONVERT_BAT_RCV { get; set; }
        public int STLM_ID { get; set; }
        public string STLM_NAME { get; set; }
    }
}
