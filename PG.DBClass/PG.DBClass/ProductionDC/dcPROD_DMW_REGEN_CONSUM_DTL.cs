using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_DMW_REGEN_CONSUM_DTL")]
    public partial class dcPROD_DMW_REGEN_CONSUM_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_DMW_REGEN_DTL_ID = 0;
        private int m_PROD_DMW_REGEN_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_CONSUMPTION_QTY = 0;
        private decimal m_UOM_ID = 0;
        private string m_DMW_REGEN_DET_REMARKS = string.Empty;
        private decimal m_DMW_REGEN_DET_SLNO = 0;
        private int m_MACHINE_ID = 0;
        private string m_OPERATOR_ID = "";
        #endregion  //private members

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            _UpdateChangedList(info);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion //public events

        #region properties


        [DBColumn(Name = "PROD_DMW_REGEN_DTL_ID", Storage = "m_PROD_DMW_REGEN_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_DMW_REGEN_DTL_ID
        {
            get { return this.m_PROD_DMW_REGEN_DTL_ID; }
            set
            {
                this.m_PROD_DMW_REGEN_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_DMW_REGEN_DTL_ID");
            }
        }

        [DBColumn(Name = "PROD_DMW_REGEN_ID", Storage = "m_PROD_DMW_REGEN_ID", DbType = "107")]
        public int PROD_DMW_REGEN_ID
        {
            get { return this.m_PROD_DMW_REGEN_ID; }
            set
            {
                this.m_PROD_DMW_REGEN_ID = value;
                this.NotifyPropertyChanged("PROD_DMW_REGEN_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "CONSUMPTION_QTY", Storage = "m_CONSUMPTION_QTY", DbType = "107")]
        public decimal CONSUMPTION_QTY
        {
            get { return this.m_CONSUMPTION_QTY; }
            set
            {
                this.m_CONSUMPTION_QTY = value;
                this.NotifyPropertyChanged("CONSUMPTION_QTY");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public decimal UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "DMW_REGEN_DET_REMARKS", Storage = "m_DMW_REGEN_DET_REMARKS", DbType = "126")]
        public string DMW_REGEN_DET_REMARKS
        {
            get { return this.m_DMW_REGEN_DET_REMARKS; }
            set
            {
                this.m_DMW_REGEN_DET_REMARKS = value;
                this.NotifyPropertyChanged("DMW_REGEN_DET_REMARKS");
            }
        }

        [DBColumn(Name = "DMW_REGEN_DET_SLNO", Storage = "m_DMW_REGEN_DET_SLNO", DbType = "107")]
        public decimal DMW_REGEN_DET_SLNO
        {
            get { return this.m_DMW_REGEN_DET_SLNO; }
            set
            {
                this.m_DMW_REGEN_DET_SLNO = value;
                this.NotifyPropertyChanged("DMW_REGEN_DET_SLNO");
            }
        }

        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
        public int MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "OPERATOR_ID", Storage = "m_OPERATOR_ID", DbType = "107")]
        public string OPERATOR_ID
        {
            get { return this.m_OPERATOR_ID; }
            set
            {
                this.m_OPERATOR_ID = value;
                this.NotifyPropertyChanged("OPERATOR_ID");
            }
        }
        #endregion //properties
    }

    public partial class dcPROD_DMW_REGEN_CONSUM_DTL
    {
        private string m_ITEM_GROUP_DESC = "";
        private string m_ITEM_CODE = "";

        private string m_ITEM_NAME = "";
        private string m_UOM_NAME = "";
        private string m_BOM_NAME = "";
        private string m_PANEL_UOM_NAME = "";
        private string m_WEIGHT_UOM_NAME = "";
        private string m_MACHINE_NAME = "";
        private int m_BALPACKINGQTY = 0;



        private List<dcITEM_STOCK_DETAILS> m_ProductionStockDetailsList = null;
        public List<dcITEM_STOCK_DETAILS> ProductionStockDetailsList
        {
            get { return m_ProductionStockDetailsList; }
            set { m_ProductionStockDetailsList = value; }
        }

        public string ITEM_GROUP_DESC
        {
            get { return this.m_ITEM_GROUP_DESC; }
            set
            {
                this.m_ITEM_GROUP_DESC = value;
            }
        }

        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
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



        private decimal m_PASTE_PC_KG = 0;
        public decimal PASTE_PC_KG
        {
            get { return this.m_PASTE_PC_KG; }
            set { this.m_PASTE_PC_KG = value; }
        }


        private decimal m_PASTE_PANEL_KG = 0;
        public decimal PASTE_PANEL_KG
        {
            get { return this.m_PASTE_PANEL_KG; }
            set { this.m_PASTE_PANEL_KG = value; }
        }


        private int m_USED_GRID_ID = 0;
        public int USED_GRID_ID
        {
            get { return this.m_USED_GRID_ID; }
            set { this.m_USED_GRID_ID = value; }
        }


        private string m_USED_GRID_NAME = "";
        public string USED_GRID_NAME
        {
            get { return this.m_USED_GRID_NAME; }
            set { this.m_USED_GRID_NAME = value; }
        }



       


        private int m_CLOSING_ITEM_ID = 0;
        public int CLOSING_ITEM_ID
        {
            get { return this.m_CLOSING_ITEM_ID; }
            set { this.m_CLOSING_ITEM_ID = value; }
        }

        private int m_CLOSING_UOM_ID = 0;
        public int CLOSING_UOM_ID
        {
            get { return this.m_CLOSING_UOM_ID; }
            set { this.m_CLOSING_UOM_ID = value; }
        }

        private int m_FINISHED_ITEM_ID = 0;
        public int FINISHED_ITEM_ID
        {
            get { return this.m_FINISHED_ITEM_ID; }
            set { this.m_FINISHED_ITEM_ID = value; }
        }

        private string m_CLOSINGITEM_NAME = "";
        public string CLOSINGITEM_NAME
        {
            get { return this.m_CLOSINGITEM_NAME; }
            set { this.m_CLOSINGITEM_NAME = value; }
        }

        private string m_CLOSING_UOM_NAME = "";
        public string CLOSING_UOM_NAME
        {
            get { return this.m_CLOSING_UOM_NAME; }
            set { this.m_CLOSING_UOM_NAME = value; }
        }

        private string m_FINISH_ITEM_NAME = "";
        public string FINISH_ITEM_NAME
        {
            get { return this.m_FINISH_ITEM_NAME; }
            set { this.m_FINISH_ITEM_NAME = value; }
        }

        private decimal m_SYSTEM_OPENING_STOCK = 0;
        public decimal SYSTEM_OPENING_STOCK
        {
            get { return this.m_SYSTEM_OPENING_STOCK; }
            set { this.m_SYSTEM_OPENING_STOCK = value; }
        }

        private decimal m_ISSUE_STOCK = 0;
        public decimal ISSUE_STOCK
        {
            get { return this.m_ISSUE_STOCK; }
            set { this.m_ISSUE_STOCK = value; }
        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }


        private string m_PROD_BATTERY_TYPE_NAME = "";
        public string PROD_BATTERY_TYPE_NAME
        {
            get { return this.m_PROD_BATTERY_TYPE_NAME; }
            set { this.m_PROD_BATTERY_TYPE_NAME = value; }
        }


        private decimal? m_AVAILABLE_PACKING_QUANTITY = 0;
        public decimal? AVAILABLE_PACKING_QUANTITY
        {
            get { return this.m_AVAILABLE_PACKING_QUANTITY; }
            set { this.m_AVAILABLE_PACKING_QUANTITY = value; }
        }

        public decimal? AVAILABLE_CHARGING_QUANTITY { get; set; }
        public string MRB_PLATE_NAME { get; set; }

        public string MRB_PLATE_NAME_N { get; set; }
        public string SUP_NAME { get; set; }
        public int SUP_ID { get; set; }
        public string FALSE_LUG_NAME { get; set; }

    }
}
