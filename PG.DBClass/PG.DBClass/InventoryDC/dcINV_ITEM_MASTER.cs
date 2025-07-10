using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "INV_ITEM_MASTER")]
    public partial class dcINV_ITEM_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ITEM_ID = 0;
        private string m_ITEM_CODE = string.Empty;
        private string m_ITEM_NAME = string.Empty;
        private string m_ITEM_NAME_BANGLA = string.Empty;
        private string m_ITEM_DESC = string.Empty;
        private int m_ITEM_GROUP_ID = 0;
        private int m_ITEM_CLASS_ID = 0;
        private int m_UOM_ID = 0;
        private string m_HS_CODE_01 = string.Empty;
        private string m_HS_CODE_02 = string.Empty;
        private string m_HS_CODE_03 = string.Empty;
        private string m_HS_CODE_FULL = string.Empty;
        private string m_ITEM_BARCODE = string.Empty;
        private decimal m_RE_ORDER_LEVEL = 0;
        private decimal m_SAFTY_STOCK_LEVEL = 0;
        private decimal m_LEAD_TIME = 0;
        private decimal m_DEF_SUP_ID = 0;
        private decimal m_OPEN_QTY = 0;
        private decimal m_WEIGHTED_AVERAGE_PRICE = 0;
        private decimal m_LAST_PURCHASE_PRICE = 0;
        private DateTime? m_LAST_PURCHASE_DATE = null;
        private DateTime? m_OPEN_DATE = null;
        private int m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private int m_ITEM_TYPE_ID = 0;
        private bool m_IS_ACTIVE = true;
        private bool m_IS_VISIBLE = true;
        private int m_ITEM_SNS_ID = 0;
        private int m_IS_PRIME = 0;
        private string m_FOR_PRODUCTION = "";
        private string m_IS_OUTSALE_CASH = string.Empty;
        private int m_PERCENTAGE = 0;
        private string m_ALTER_ITEM_NAME = String.Empty;
        private string m_SND_ITEM_CODE = String.Empty;
        private string m_SND_TRANSFER = String.Empty;
        private string m_IS_BATTERY = String.Empty;
        private int m_BRAND_ID = 0;
        private int m_battery_cat_id = 0;
        private int m_ITEM_CATEGORY_ID = 0;
        private int m_ITEM_SIZE_ID = 0;
        private int m_ITEM_COLOR_ID = 0;
        private int m_ITEM_GEN_ID = 0;
        private string m_IS_COMMON_ITEM = string.Empty;
        private string m_IS_QC = string.Empty;
        private string m_IS_BATCH = string.Empty;
        private string m_IS_CONVERTABLE = string.Empty;
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


        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "ITEM_CODE", Storage = "m_ITEM_CODE", DbType = "126")]
        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
                this.NotifyPropertyChanged("ITEM_CODE");
            }
        }

        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "126")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
                this.NotifyPropertyChanged("ITEM_NAME");
            }
        }

        [DBColumn(Name = "ITEM_NAME_BANGLA", Storage = "m_ITEM_NAME_BANGLA", DbType = "126")]
        public string ITEM_NAME_BANGLA
        {
            get { return this.m_ITEM_NAME_BANGLA; }
            set
            {
                this.m_ITEM_NAME_BANGLA = value;
                this.NotifyPropertyChanged("ITEM_NAME_BANGLA");
            }
        }

        [DBColumn(Name = "ITEM_DESC", Storage = "m_ITEM_DESC", DbType = "126")]
        public string ITEM_DESC
        {
            get { return this.m_ITEM_DESC; }
            set
            {
                this.m_ITEM_DESC = value;
                this.NotifyPropertyChanged("ITEM_DESC");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_ID", Storage = "m_ITEM_GROUP_ID", DbType = "107")]
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ID");
            }
        }

        [DBColumn(Name = "ITEM_CLASS_ID", Storage = "m_ITEM_CLASS_ID", DbType = "107")]
        public int ITEM_CLASS_ID
        {
            get { return this.m_ITEM_CLASS_ID; }
            set
            {
                this.m_ITEM_CLASS_ID = value;
                this.NotifyPropertyChanged("ITEM_CLASS_ID");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "HS_CODE_01", Storage = "m_HS_CODE_01", DbType = "126")]
        public string HS_CODE_01
        {
            get { return this.m_HS_CODE_01; }
            set
            {
                this.m_HS_CODE_01 = value;
                this.NotifyPropertyChanged("HS_CODE_01");
            }
        }

        [DBColumn(Name = "HS_CODE_02", Storage = "m_HS_CODE_02", DbType = "126")]
        public string HS_CODE_02
        {
            get { return this.m_HS_CODE_02; }
            set
            {
                this.m_HS_CODE_02 = value;
                this.NotifyPropertyChanged("HS_CODE_02");
            }
        }

        [DBColumn(Name = "HS_CODE_03", Storage = "m_HS_CODE_03", DbType = "126")]
        public string HS_CODE_03
        {
            get { return this.m_HS_CODE_03; }
            set
            {
                this.m_HS_CODE_03 = value;
                this.NotifyPropertyChanged("HS_CODE_03");
            }
        }

        [DBColumn(Name = "HS_CODE_FULL", Storage = "m_HS_CODE_FULL", DbType = "126")]
        public string HS_CODE_FULL
        {
            get { return this.m_HS_CODE_FULL; }
            set
            {
                this.m_HS_CODE_FULL = value;
                this.NotifyPropertyChanged("HS_CODE_FULL");
            }
        }

        [DBColumn(Name = "ITEM_BARCODE", Storage = "m_ITEM_BARCODE", DbType = "126")]
        public string ITEM_BARCODE
        {
            get { return this.m_ITEM_BARCODE; }
            set
            {
                this.m_ITEM_BARCODE = value;
                this.NotifyPropertyChanged("ITEM_BARCODE");
            }
        }

        [DBColumn(Name = "RE_ORDER_LEVEL", Storage = "m_RE_ORDER_LEVEL", DbType = "107")]
        public decimal RE_ORDER_LEVEL
        {
            get { return this.m_RE_ORDER_LEVEL; }
            set
            {
                this.m_RE_ORDER_LEVEL = value;
                this.NotifyPropertyChanged("RE_ORDER_LEVEL");
            }
        }

        [DBColumn(Name = "SAFTY_STOCK_LEVEL", Storage = "m_SAFTY_STOCK_LEVEL", DbType = "107")]
        public decimal SAFTY_STOCK_LEVEL
        {
            get { return this.m_SAFTY_STOCK_LEVEL; }
            set
            {
                this.m_SAFTY_STOCK_LEVEL = value;
                this.NotifyPropertyChanged("SAFTY_STOCK_LEVEL");
            }
        }

        [DBColumn(Name = "LEAD_TIME", Storage = "m_LEAD_TIME", DbType = "107")]
        public decimal LEAD_TIME
        {
            get { return this.m_LEAD_TIME; }
            set
            {
                this.m_LEAD_TIME = value;
                this.NotifyPropertyChanged("LEAD_TIME");
            }
        }

        [DBColumn(Name = "DEF_SUP_ID", Storage = "m_DEF_SUP_ID", DbType = "107")]
        public decimal DEF_SUP_ID
        {
            get { return this.m_DEF_SUP_ID; }
            set
            {
                this.m_DEF_SUP_ID = value;
                this.NotifyPropertyChanged("DEF_SUP_ID");
            }
        }

        [DBColumn(Name = "OPEN_QTY", Storage = "m_OPEN_QTY", DbType = "107")]
        public decimal OPEN_QTY
        {
            get { return this.m_OPEN_QTY; }
            set
            {
                this.m_OPEN_QTY = value;
                this.NotifyPropertyChanged("OPEN_QTY");
            }
        }

        [DBColumn(Name = "WEIGHTED_AVERAGE_PRICE", Storage = "m_WEIGHTED_AVERAGE_PRICE", DbType = "107")]
        public decimal WEIGHTED_AVERAGE_PRICE
        {
            get { return this.m_WEIGHTED_AVERAGE_PRICE; }
            set
            {
                this.m_WEIGHTED_AVERAGE_PRICE = value;
                this.NotifyPropertyChanged("WEIGHTED_AVERAGE_PRICE");
            }
        }

        [DBColumn(Name = "LAST_PURCHASE_PRICE", Storage = "m_LAST_PURCHASE_PRICE", DbType = "107")]
        public decimal LAST_PURCHASE_PRICE
        {
            get { return this.m_LAST_PURCHASE_PRICE; }
            set
            {
                this.m_LAST_PURCHASE_PRICE = value;
                this.NotifyPropertyChanged("LAST_PURCHASE_PRICE");
            }
        }


        [DBColumn(Name = "LAST_PURCHASE_DATE", Storage = "m_LAST_PURCHASE_DATE", DbType = "106")]
        public DateTime? LAST_PURCHASE_DATE
        {
            get { return this.m_LAST_PURCHASE_DATE; }
            set
            {
                this.m_LAST_PURCHASE_DATE = value;
                this.NotifyPropertyChanged("LAST_PURCHASE_DATE");
            }
        }

        [DBColumn(Name = "OPEN_DATE", Storage = "m_OPEN_DATE", DbType = "106")]
        public DateTime? OPEN_DATE
        {
            get { return this.m_OPEN_DATE; }
            set
            {
                this.m_OPEN_DATE = value;
                this.NotifyPropertyChanged("OPEN_DATE");
            }
        }


        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public int ENTRY_BY
        {
            get { return this.m_ENTRY_BY; }
            set
            {
                this.m_ENTRY_BY = value;
                this.NotifyPropertyChanged("ENTRY_BY");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_ID", Storage = "m_ITEM_TYPE_ID", DbType = "107")]
        public int ITEM_TYPE_ID
        {
            get { return this.m_ITEM_TYPE_ID; }
            set
            {
                this.m_ITEM_TYPE_ID = value;
                this.NotifyPropertyChanged("ITEM_TYPE_ID");
            }
        }


        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public bool IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
            }
        }

        [DBColumn(Name = "IS_VISIBLE", Storage = "m_IS_VISIBLE", DbType = "126")]
        public bool IS_VISIBLE
        {
            get { return this.m_IS_VISIBLE; }
            set
            {
                this.m_IS_VISIBLE = value;
                this.NotifyPropertyChanged("IS_VISIBLE");
            }
        }



        [DBColumn(Name = "ITEM_SNS_ID", Storage = "m_ITEM_SNS_ID", DbType = "107")]
        public int ITEM_SNS_ID
        {
            get { return this.m_ITEM_SNS_ID; }
            set
            {
                this.m_ITEM_SNS_ID = value;
                this.NotifyPropertyChanged("ITEM_SNS_ID");
            }
        }

        [DBColumn(Name = "IS_PRIME", Storage = "m_IS_PRIME", DbType = "107")]
        public int IS_PRIME
        {
            get { return this.m_IS_PRIME; }
            set
            {
                this.m_IS_PRIME = value;
                this.NotifyPropertyChanged("IS_PRIME");
            }
        }

        [DBColumn(Name = "FOR_PRODUCTION", Storage = "m_FOR_PRODUCTION", DbType = "107")]
        public string FOR_PRODUCTION
        {
            get { return this.m_FOR_PRODUCTION; }
            set
            {
                this.m_FOR_PRODUCTION = value;
                this.NotifyPropertyChanged("FOR_PRODUCTION");
            }
        }


        [DBColumn(Name = "IS_OUTSALE_CASH", Storage = "m_IS_OUTSALE_CASH", DbType = "107")]
        public string IS_OUTSALE_CASH
        {
            get { return this.m_IS_OUTSALE_CASH; }
            set
            {
                this.m_IS_OUTSALE_CASH = value;
                this.NotifyPropertyChanged("IS_OUTSALE_CASH");
            }
        }


        [DBColumn(Name = "PERCENTAGE", Storage = "m_PERCENTAGE", DbType = "107")]
        public int PERCENTAGE
        {
            get { return this.m_PERCENTAGE; }
            set
            {
                this.m_PERCENTAGE = value;
                this.NotifyPropertyChanged("PERCENTAGE");
            }
        }



        [DBColumn(Name = "ALTER_ITEM_NAME", Storage = "m_ALTER_ITEM_NAME", DbType = "126")]
        public string ALTER_ITEM_NAME
        {
            get { return this.m_ALTER_ITEM_NAME; }
            set
            {
                this.m_ALTER_ITEM_NAME = value;
                this.NotifyPropertyChanged("ALTER_ITEM_NAME");
            }
        }


      [DBColumn(Name = "SND_ITEM_CODE", Storage = "m_SND_ITEM_CODE", DbType = "126")]
        public string SND_ITEM_CODE
        {
            get { return this.m_SND_ITEM_CODE; }
            set
            {
                this.m_SND_ITEM_CODE = value;
                this.NotifyPropertyChanged("SND_ITEM_CODE");
            }
        }



     [DBColumn(Name = "SND_TRANSFER", Storage = "m_SND_TRANSFER", DbType = "126")]
      public string SND_TRANSFER
        {
            get { return this.m_SND_TRANSFER; }
            set
            {
                this.m_SND_TRANSFER = value;
                this.NotifyPropertyChanged("SND_TRANSFER");
            }
        }

     [DBColumn(Name = "IS_BATTERY", Storage = "m_IS_BATTERY", DbType = "126")]
     public string IS_BATTERY
     {
         get { return this.m_IS_BATTERY; }
         set
         {
             this.m_IS_BATTERY = value;
             this.NotifyPropertyChanged("IS_BATTERY");
         }
     }

     [DBColumn(Name = "BRAND_ID", Storage = "m_BRAND_ID", DbType = "107")]
     public int BRAND_ID
     {
         get { return this.m_BRAND_ID; }
         set
         {
             this.m_BRAND_ID = value;
             this.NotifyPropertyChanged("BRAND_ID");
         }
     }

     [DBColumn(Name = "battery_cat_id", Storage = "m_battery_cat_id", DbType = "107")]
     public int battery_cat_id
     {
         get { return this.m_battery_cat_id; }
         set
         {
             this.m_battery_cat_id = value;
             this.NotifyPropertyChanged("battery_cat_id");
         }
     }


     [DBColumn(Name = "ITEM_CATEGORY_ID", Storage = "m_ITEM_CATEGORY_ID", DbType = "107")]
     public int ITEM_CATEGORY_ID
     {
         get { return this.m_ITEM_CATEGORY_ID; }
         set
         {
             this.m_ITEM_CATEGORY_ID = value;
             this.NotifyPropertyChanged("ITEM_CATEGORY_ID");
         }
     }

     [DBColumn(Name = "ITEM_SIZE_ID", Storage = "m_ITEM_SIZE_ID", DbType = "107")]
     public int ITEM_SIZE_ID
     {
         get { return this.m_ITEM_SIZE_ID; }
         set
         {
             this.m_ITEM_SIZE_ID = value;
             this.NotifyPropertyChanged("ITEM_SIZE_ID");
         }
     }

     [DBColumn(Name = "ITEM_COLOR_ID", Storage = "m_ITEM_COLOR_ID", DbType = "107")]
     public int ITEM_COLOR_ID
     {
         get { return this.m_ITEM_COLOR_ID; }
         set
         {
             this.m_ITEM_COLOR_ID = value;
             this.NotifyPropertyChanged("ITEM_COLOR_ID");
         }
     }

     [DBColumn(Name = "ITEM_GEN_ID", Storage = "m_ITEM_GEN_ID", DbType = "107")]
     public int ITEM_GEN_ID
     {
         get { return this.m_ITEM_GEN_ID; }
         set
         {
             this.m_ITEM_GEN_ID = value;
             this.NotifyPropertyChanged("ITEM_GEN_ID");
         }
     }

     [DBColumn(Name = "IS_COMMON_ITEM", Storage = "m_IS_COMMON_ITEM", DbType = "126")]
     public string IS_COMMON_ITEM
     {
         get { return this.m_IS_COMMON_ITEM; }
         set
         {
             this.m_IS_COMMON_ITEM = value;
             this.NotifyPropertyChanged("IS_COMMON_ITEM");
         }
     }

     [DBColumn(Name = "IS_QC", Storage = "m_IS_QC", DbType = "126")]
     public string IS_QC
     {
         get { return this.m_IS_QC; }
         set
         {
             this.m_IS_QC = value;
             this.NotifyPropertyChanged("IS_QC");
         }
     }

     [DBColumn(Name = "IS_BATCH", Storage = "m_IS_BATCH", DbType = "126")]
     public string IS_BATCH
     {
         get { return this.m_IS_BATCH; }
         set
         {
             this.m_IS_BATCH = value;
             this.NotifyPropertyChanged("IS_BATCH");
         }
     }

     [DBColumn(Name = "IS_CONVERTABLE", Storage = "m_IS_CONVERTABLE", DbType = "126")]
     public string IS_CONVERTABLE
     {
         get { return this.m_IS_CONVERTABLE; }
         set
         {
             this.m_IS_CONVERTABLE = value;
             this.NotifyPropertyChanged("IS_CONVERTABLE");
         }
     }


        #endregion //properties

    }


    public partial class dcINV_ITEM_MASTER
    {
        #region cusotm properties


        private string m_CATEGORY_DESC = string.Empty;
        public string CATEGORY_DESC
        {

            get { return m_CATEGORY_DESC; }
            set { this.m_CATEGORY_DESC = value; }
        }

        private string m_CAT_SUB_DESC = string.Empty;
        public string CAT_SUB_DESC
        {

            get { return m_CAT_SUB_DESC; }
            set { this.m_CAT_SUB_DESC = value; }
        }

        private decimal m_T_STOCK = 0;
        public decimal T_STOCK
        {

            get { return m_T_STOCK; }
            set { this.m_T_STOCK = value; }
        }

        private string m_MSR_NAME = string.Empty;
        public string MSR_NAME
        {

            get { return m_MSR_NAME; }
            set { this.m_MSR_NAME = value; }
        }



        private string m_userName = string.Empty;
        public string userName
        {

            get { return m_userName; }
            set { this.m_userName = value; }
        }


        #endregion

        public int ITEM_SLNO { get; set; }
        private string m_ITEM_GROUP_NAME = string.Empty;
        public string ITEM_GROUP_NAME
        {

            get { return m_ITEM_GROUP_NAME; }
            set { this.m_ITEM_GROUP_NAME = value; }
        }

        private string m_ITEM_GROUP_CODE = string.Empty;
        public string ITEM_GROUP_CODE
        {

            get { return m_ITEM_GROUP_CODE; }
            set { this.m_ITEM_GROUP_CODE = value; }
        }
        public string ITEM_GROUP_KEY
        {
            get
            {
                string pKey = string.Empty;
                pKey = "grpid" + m_ITEM_GROUP_ID.ToString();
                return pKey;
            }
        }

        private string m_ITEM_CLASS_NAME = string.Empty;
        public string ITEM_CLASS_NAME
        {

            get { return m_ITEM_CLASS_NAME; }
            set { this.m_ITEM_CLASS_NAME = value; }
        }

        private string m_ITEM_CLASS_CODE = string.Empty;
        public string ITEM_CLASS_CODE
        {

            get { return m_ITEM_CLASS_CODE; }
            set { this.m_ITEM_CLASS_CODE = value; }
        }


        private string m_ITEM_TYPE_NAME = string.Empty;
        public string ITEM_TYPE_NAME
        {

            get { return m_ITEM_TYPE_NAME; }
            set { this.m_ITEM_TYPE_NAME = value; }
        }

        private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {

            get { return m_ITEM_TYPE_CODE; }
            set { this.m_ITEM_TYPE_CODE = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {

            get { return m_UOM_NAME; }
            set { this.m_UOM_NAME = value; }
        }

        private string m_UOM_CODE = string.Empty;
        public string UOM_CODE
        {

            get { return m_UOM_CODE; }
            set { this.m_UOM_CODE = value; }
        }


        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {

            get { return m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }
        private bool m_IsAssigned = false;
        public bool IsAssigned
        {

            get { return m_IsAssigned; }
            set { this.m_IsAssigned = value; }
        }

        private string m_ITEM_SNS_NAME = string.Empty;
        public string ITEM_SNS_NAME
        {

            get { return m_ITEM_SNS_NAME; }
            set { this.m_ITEM_SNS_NAME = value; }
        }

        private DateTime? m_INDT_DATE = null;
        public DateTime? INDT_DATE
        {

            get { return m_INDT_DATE; }
            set { this.m_INDT_DATE = value; }
        }


        private string m_INDT_NO = null;
        public string INDT_NO
        {

            get { return m_INDT_NO; }
            set { this.m_INDT_NO = value; }
        }

        private string m_FROM_DEPT_NAME = null;
        public string FROM_DEPT_NAME
        {

            get { return m_FROM_DEPT_NAME; }
            set { this.m_FROM_DEPT_NAME = value; }
        }

        private decimal m_INDT_QTY = 0;
        public decimal INDT_QTY
        {

            get { return m_INDT_QTY; }
            set { this.m_INDT_QTY = value; }
        }

        private Int64 m_INDT_DET_ID = 0;
        public Int64 INDT_DET_ID
        {

            get { return m_INDT_DET_ID; }
            set { this.m_INDT_DET_ID = value; }
        }


        private decimal m_ALREADRY_PURCHASE_QTY = 0;
        public decimal ALREADRY_PURCHASE_QTY
        {

            get { return m_ALREADRY_PURCHASE_QTY; }
            set { this.m_ALREADRY_PURCHASE_QTY = value; }
        }


        public decimal BALANCE_QTY
        {
            get { return this.INDT_QTY - this.ALREADRY_PURCHASE_QTY; }
        }


        private decimal m_CURRENT_STOCK = 0;
        public decimal CURRENT_STOCK
        {

            get { return m_CURRENT_STOCK; }
            set { this.m_CURRENT_STOCK = value; }
        }

        private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;
        public decimal ITEM_STANDARD_WEIGHT_KG
        {

            get { return m_ITEM_STANDARD_WEIGHT_KG; }
            set { this.m_ITEM_STANDARD_WEIGHT_KG = value; }
        }
        private decimal m_OPENING_PRICE = 0;
        public decimal OPENING_PRICE
        {
            get { return m_OPENING_PRICE; }
            set { this.m_OPENING_PRICE = value; }
        }

         private decimal m_ASM_REC_CLS_QTY = 0;
        public decimal ASM_REC_CLS_QTY
        {
            get { return m_ASM_REC_CLS_QTY; }
            set { this.m_ASM_REC_CLS_QTY = value; }
        }

         private decimal m_ASM_REJ_CLS_QTY = 0;
        public decimal ASM_REJ_CLS_QTY
        {
            get { return m_ASM_REJ_CLS_QTY; }
            set { this.m_ASM_REJ_CLS_QTY = value; }
        }

        private decimal m_PENDING_REQ_QNTY = 0;
        public decimal PENDING_REQ_QNTY
        {
            get { return m_PENDING_REQ_QNTY; }
            set { this.m_PENDING_REQ_QNTY = value; }
        }

        

        private int m_GLAccountLevel = 0;
        public int GLAccountLevel
        {
            //get { return AppObjectType == null ? m_AppObjectTypeName : this.AppObjectType.AppObjectTypeName; }
            get { return m_GLAccountLevel; }
            set { this.m_GLAccountLevel = value; }

        }

        private decimal m_UNIT_PRICE = 0;
        public decimal UNIT_PRICE
        {
            get { return m_UNIT_PRICE; }
            set { this.m_UNIT_PRICE = value; }

        }

        public int RCV_UOM_ID { get; set; }
        public string RCV_UOM_NAME { get; set; }

         
    }
}
