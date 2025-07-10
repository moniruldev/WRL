using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "BOM_MST_T")]
    public partial class dcBOM_MST_T : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_BOM_ID = 0;
        private string m_BATTERY_DESC = string.Empty;
        private string m_CONTAINER_TYPE = string.Empty;
        private string m_BRAND_NAME = string.Empty;
        private decimal m_UNIT_ID = 0;
        private decimal m_UNIT_QTY = 0;
        private string m_NOMINAL_VOTL = string.Empty;
        private string m_ACTUAL_CAPACITY = string.Empty;
        private string m_ELECTROLYTE_SP_GR = string.Empty;
        private string m_HEIGHT_WITH_COVER = string.Empty;
        private string m_LENGTH = string.Empty;
        private string m_WIDTH = string.Empty;
        private string m_TOTAL_HEIGTH = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_EDIT_BY = 0;
        private DateTime? m_EDIT_DATE = null;
        private decimal m_BATTERY_TYPE_ID = 0;
        private int m_BOM_ITEM_ID = 0;
        private string m_BOM_ITEM_DESC = string.Empty;
        private string m_REMARKS = string.Empty;
        private int m_FROM_DEPARTMENT_ID = 0;
        private string m_PACKAGE_NAME = string.Empty;
        private string m_ISPACKAGE = string.Empty;
        private string m_BOM_VER = string.Empty;
        private string m_ISACTIVE = "Y";
        private decimal m_DROSS = 0;
        private decimal m_WASTAGE = 0;
        private string m_BOM_NO = "";

        private string m_AUTH_STATUS = "";
        private int m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;

        private DateTime? m_ACTIVE_FROM_DATE = null;
        private DateTime? m_ACTIVE_TO_DATE = null;
        private int m_STLM_ID = 0;
        


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


        [DBColumn(Name = "BOM_ID", Storage = "m_BOM_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int BOM_ID
        {
            get { return this.m_BOM_ID; }
            set
            {
                this.m_BOM_ID = value;
                this.NotifyPropertyChanged("BOM_ID");
            }
        }

        [DBColumn(Name = "BOM_ITEM_ID", Storage = "m_BOM_ITEM_ID", DbType = "126")]
        public int BOM_ITEM_ID
        {
            get { return this.m_BOM_ITEM_ID; }
            set
            {
                this.m_BOM_ITEM_ID = value;
                this.NotifyPropertyChanged("BOM_ITEM_ID");
            }
        }

        [DBColumn(Name = "BOM_ITEM_DESC", Storage = "m_BOM_ITEM_DESC", DbType = "126")]
        public string BOM_ITEM_DESC
        {
            get { return this.m_BOM_ITEM_DESC; }
            set
            {
                this.m_BOM_ITEM_DESC = value;
                this.NotifyPropertyChanged("BOM_ITEM_DESC");
            }
        }


        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }



        [DBColumn(Name = "FROM_DEPARTMENT_ID", Storage = "m_FROM_DEPARTMENT_ID", DbType = "106")]
        public int FROM_DEPARTMENT_ID
        {
            get { return this.m_FROM_DEPARTMENT_ID; }
            set
            {
                this.m_FROM_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("FROM_DEPARTMENT_ID");
            }
        }


        [DBColumn(Name = "BATTERY_DESC", Storage = "m_BATTERY_DESC", DbType = "126")]
        public string BATTERY_DESC
        {
            get { return this.m_BATTERY_DESC; }
            set
            {
                this.m_BATTERY_DESC = value;
                this.NotifyPropertyChanged("BATTERY_DESC");
            }
        }

        [DBColumn(Name = "CONTAINER_TYPE", Storage = "m_CONTAINER_TYPE", DbType = "126")]
        public string CONTAINER_TYPE
        {
            get { return this.m_CONTAINER_TYPE; }
            set
            {
                this.m_CONTAINER_TYPE = value;
                this.NotifyPropertyChanged("CONTAINER_TYPE");
            }
        }

        [DBColumn(Name = "BRAND_NAME", Storage = "m_BRAND_NAME", DbType = "126")]
        public string BRAND_NAME
        {
            get { return this.m_BRAND_NAME; }
            set
            {
                this.m_BRAND_NAME = value;
                this.NotifyPropertyChanged("BRAND_NAME");
            }
        }

        [DBColumn(Name = "UNIT_ID", Storage = "m_UNIT_ID", DbType = "107")]
        public decimal UNIT_ID
        {
            get { return this.m_UNIT_ID; }
            set
            {
                this.m_UNIT_ID = value;
                this.NotifyPropertyChanged("UNIT_ID");
            }
        }

        [DBColumn(Name = "UNIT_QTY", Storage = "m_UNIT_QTY", DbType = "107")]
        public decimal UNIT_QTY
        {
            get { return this.m_UNIT_QTY; }
            set
            {
                this.m_UNIT_QTY = value;
                this.NotifyPropertyChanged("UNIT_QTY");
            }
        }

        [DBColumn(Name = "NOMINAL_VOTL", Storage = "m_NOMINAL_VOTL", DbType = "126")]
        public string NOMINAL_VOTL
        {
            get { return this.m_NOMINAL_VOTL; }
            set
            {
                this.m_NOMINAL_VOTL = value;
                this.NotifyPropertyChanged("NOMINAL_VOTL");
            }
        }

        [DBColumn(Name = "ACTUAL_CAPACITY", Storage = "m_ACTUAL_CAPACITY", DbType = "126")]
        public string ACTUAL_CAPACITY
        {
            get { return this.m_ACTUAL_CAPACITY; }
            set
            {
                this.m_ACTUAL_CAPACITY = value;
                this.NotifyPropertyChanged("ACTUAL_CAPACITY");
            }
        }

        [DBColumn(Name = "ELECTROLYTE_SP_GR", Storage = "m_ELECTROLYTE_SP_GR", DbType = "126")]
        public string ELECTROLYTE_SP_GR
        {
            get { return this.m_ELECTROLYTE_SP_GR; }
            set
            {
                this.m_ELECTROLYTE_SP_GR = value;
                this.NotifyPropertyChanged("ELECTROLYTE_SP_GR");
            }
        }

        [DBColumn(Name = "HEIGHT_WITH_COVER", Storage = "m_HEIGHT_WITH_COVER", DbType = "126")]
        public string HEIGHT_WITH_COVER
        {
            get { return this.m_HEIGHT_WITH_COVER; }
            set
            {
                this.m_HEIGHT_WITH_COVER = value;
                this.NotifyPropertyChanged("HEIGHT_WITH_COVER");
            }
        }

        [DBColumn(Name = "LENGTH", Storage = "m_LENGTH", DbType = "126")]
        public string LENGTH
        {
            get { return this.m_LENGTH; }
            set
            {
                this.m_LENGTH = value;
                this.NotifyPropertyChanged("LENGTH");
            }
        }

        [DBColumn(Name = "WIDTH", Storage = "m_WIDTH", DbType = "126")]
        public string WIDTH
        {
            get { return this.m_WIDTH; }
            set
            {
                this.m_WIDTH = value;
                this.NotifyPropertyChanged("WIDTH");
            }
        }

        [DBColumn(Name = "TOTAL_HEIGTH", Storage = "m_TOTAL_HEIGTH", DbType = "126")]
        public string TOTAL_HEIGTH
        {
            get { return this.m_TOTAL_HEIGTH; }
            set
            {
                this.m_TOTAL_HEIGTH = value;
                this.NotifyPropertyChanged("TOTAL_HEIGTH");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public decimal CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "107")]
        public decimal EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        [DBColumn(Name = "BATTERY_TYPE_ID", Storage = "m_BATTERY_TYPE_ID", DbType = "107")]
        public decimal BATTERY_TYPE_ID
        {
            get { return this.m_BATTERY_TYPE_ID; }
            set
            {
                this.m_BATTERY_TYPE_ID = value;
                this.NotifyPropertyChanged("BATTERY_TYPE_ID");
            }
        }


        [DBColumn(Name = "PACKAGE_NAME", Storage = "m_PACKAGE_NAME", DbType = "107")]
        public string PACKAGE_NAME
        {
            get { return this.m_PACKAGE_NAME; }
            set
            {
                this.m_PACKAGE_NAME = value;
                this.NotifyPropertyChanged("PACKAGE_NAME");
            }
        }

        [DBColumn(Name = "ISPACKAGE", Storage = "m_ISPACKAGE", DbType = "107")]
        public string ISPACKAGE
        {
            get { return this.m_ISPACKAGE; }
            set
            {
                this.m_ISPACKAGE = value;
                this.NotifyPropertyChanged("ISPACKAGE");
            }
        }


        [DBColumn(Name = "BOM_VER", Storage = "m_BOM_VER", DbType = "107")]
        public string BOM_VER
        {
            get { return this.m_BOM_VER; }
            set
            {
                this.m_BOM_VER = value;
                this.NotifyPropertyChanged("BOM_VER");
            }
        }

        [DBColumn(Name = "ISACTIVE", Storage = "m_ISACTIVE", DbType = "107")]
        public string ISACTIVE
        {
            get { return this.m_ISACTIVE; }
            set
            {
                this.m_ISACTIVE = value;
                this.NotifyPropertyChanged("ISACTIVE");
            }
        }



        [DBColumn(Name = "WASTAGE", Storage = "m_WASTAGE", DbType = "107")]
        public decimal WASTAGE
        {
            get { return this.m_WASTAGE; }
            set
            {
                this.m_WASTAGE = value;
                this.NotifyPropertyChanged("WASTAGE");
            }
        }


        [DBColumn(Name = "DROSS", Storage = "m_DROSS", DbType = "107")]
        public decimal DROSS
        {
            get { return this.m_DROSS; }
            set
            {
                this.m_DROSS = value;
                this.NotifyPropertyChanged("DROSS");
            }
        }


        [DBColumn(Name = "BOM_NO", Storage = "m_BOM_NO", DbType = "107")]
        public string BOM_NO
        {
            get { return this.m_BOM_NO; }
            set
            {
                this.m_BOM_NO = value;
                this.NotifyPropertyChanged("BOM_NO");
            }
        }



        [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "107")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }



        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
        public int AUTH_BY
        {
            get { return this.m_AUTH_BY; }
            set
            {
                m_AUTH_BY = value;
                this.NotifyPropertyChanged("AUTH_BY");
            }
        }


        [DBColumn(Name = "AUTH_DATE", Storage = "m_AUTH_DATE", DbType = "106")]
        public DateTime? AUTH_DATE
        {
            get { return this.m_AUTH_DATE; }
            set
            {
                this.m_AUTH_DATE = value;
                this.NotifyPropertyChanged("AUTH_DATE");
            }
        }


        [DBColumn(Name = "ACTIVE_FROM_DATE", Storage = "m_ACTIVE_FROM_DATE", DbType = "106")]
        public DateTime? ACTIVE_FROM_DATE
        {
            get { return this.m_ACTIVE_FROM_DATE; }
            set
            {
                this.m_ACTIVE_FROM_DATE = value;
                this.NotifyPropertyChanged("ACTIVE_FROM_DATE");
            }
        }

        [DBColumn(Name = "ACTIVE_TO_DATE", Storage = "m_ACTIVE_TO_DATE", DbType = "106")]
        public DateTime? ACTIVE_TO_DATE
        {
            get { return this.m_ACTIVE_TO_DATE; }
            set
            {
                this.m_ACTIVE_TO_DATE = value;
                this.NotifyPropertyChanged("ACTIVE_TO_DATE");
            }
        }

         [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "106")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }
        
        #endregion //properties
    }


    public partial class dcBOM_MST_T
    {
        public string m_ITEM_NAME = "";
        private List<dcBOM_DTL_T> m_BOMDetList = null;

        public string m_FULL_NAME = "";
        public List<dcBOM_DTL_T> BOMDetList
        {
            get { return m_BOMDetList; }
            set { m_BOMDetList = value; }
        }


        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set { this.m_ITEM_NAME = value; }
        }

        private int m_ITEM_ID = 0;
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set { this.m_ITEM_ID = value; }
        }


        private int m_UOM_ID = 0;
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set { this.m_UOM_ID = value; }
        }


        private string m_UOM_NAME = "";
        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set { this.m_UOM_NAME = value; }
        }

        private string m_IS_BATCH = "";
        public string IS_BATCH
        {
            get { return this.m_IS_BATCH; }
            set { this.m_IS_BATCH = value; }
        }

        

        private int m_IS_PRIME = 0;

        public int IS_PRIME
        {
            get { return this.m_IS_PRIME; }
            set { this.m_IS_PRIME = value; }
        }
        public string FULL_NAME
        {
            get { return this.m_FULL_NAME; }
            set { this.m_FULL_NAME = value; }
        }

        private int m_BALPACKINGQTY = 0;
        public int BALPACKINGQTY
        {
            get { return this.m_BALPACKINGQTY; }
            set { this.m_BALPACKINGQTY = value; }
        }


        private string m_ITEM_GROUP_NAME = "";
        public string ITEM_GROUP_NAME
        {
            get { return this.m_ITEM_GROUP_NAME; }
            set { this.m_ITEM_GROUP_NAME = value; }
        }

        private int m_ITEM_GROUP_ID = 0;
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set { this.m_ITEM_GROUP_ID = value; }
        }
        

        private string m_ITEM_CLASS_NAME = "";
        public string ITEM_CLASS_NAME
        {
            get { return this.m_ITEM_CLASS_NAME; }
            set { this.m_ITEM_CLASS_NAME = value; }
        }


        private string m_PANEL_UOM_NAME = "";
        public string PANEL_UOM_NAME
        {
            get { return this.m_PANEL_UOM_NAME; }
            set { this.m_PANEL_UOM_NAME = value; }
        }


        private int m_PANEL_UOM_ID = 0;
        public int PANEL_UOM_ID
        {
            get { return this.m_PANEL_UOM_ID; }
            set { this.m_PANEL_UOM_ID = value; }
        }

        private int m_PANEL_PC = 0;
        public int PANEL_PC
        {
            get { return this.m_PANEL_PC; }
            set { this.m_PANEL_PC = value; }
        }



        private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;
        public decimal ITEM_STANDARD_WEIGHT_KG
        {
            get { return this.m_ITEM_STANDARD_WEIGHT_KG; }
            set { this.m_ITEM_STANDARD_WEIGHT_KG = value; }
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

        private decimal m_CURRENT_STOCK = 0;
        public decimal CURRENT_STOCK
        {
            get { return this.m_CURRENT_STOCK; }
            set { this.m_CURRENT_STOCK = value; }
        }




        private decimal m_PRODUCTION_QTY = 0;
        public decimal PRODUCTION_QTY
        {
            get { return this.m_PRODUCTION_QTY; }
            set { this.m_PRODUCTION_QTY = value; }
        }

        private string m_ITEM_CODE = "";
        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set { this.m_ITEM_CODE = value; }
        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }


        private string m_ITEM_TYPE_NAME = String.Empty;
        public string ITEM_TYPE_NAME
        {
            get { return this.m_ITEM_TYPE_NAME; }
            set { this.m_ITEM_TYPE_NAME = value; }
        }

        private int m_PANEL_PC_UOM = 0;
        public int PANEL_PC_UOM
        {
            get { return this.m_PANEL_PC_UOM; }
            set { this.m_PANEL_PC_UOM = value; }
        }



        public decimal? AVAILABLE_PACKING_QUANTITY { get; set; }
        public decimal? AVAILABLE_CHARGING_QUANTITY { get; set; }


        private string m_TO_REJ_ITEM_NAME = String.Empty;
        public string TO_REJ_ITEM_NAME
        {
            get { return this.m_TO_REJ_ITEM_NAME; }
            set { this.m_TO_REJ_ITEM_NAME = value; }
        }

        private int m_TO_REJ_ITEM_ID = 0;
        public int TO_REJ_ITEM_ID
        {
            get { return this.m_TO_REJ_ITEM_ID; }
            set { this.m_TO_REJ_ITEM_ID = value; }
        }

        private decimal m_OPENING_PRICE = 0;
        public decimal OPENING_PRICE
        {
            get { return m_OPENING_PRICE; }
            set { this.m_OPENING_PRICE = value; }
        }

        public decimal WEIGHTED_AVERAGE_PRICE { get; set; }
        public string IS_OUTSALE_CASH { get; set; }
        public string FALSE_LUG_ITEM_ID { get; set; }
        public string FALSE_LUG_NAME { get; set; }

    }
}
