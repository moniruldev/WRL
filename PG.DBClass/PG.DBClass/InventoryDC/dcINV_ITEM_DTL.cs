using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INV_ITEM_DTL")]
    public class dcINV_ITEM_DTL : DBBaseClass, INotifyPropertyChanged 
    {

        #region private members
        private string m_ITEM_ID = string.Empty;
        private string m_ITEM_CODE = string.Empty;
        private string m_ITEM_DESC = string.Empty;
        private string m_ITEM_SHORT_DESC = string.Empty;
        private string m_MSR_ID = string.Empty;
        private string m_COUNTRY_CODE = string.Empty;
        private string m_MODEL_ID = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_COA_CODE = string.Empty;
        private string m_STORE_ID = string.Empty;
        private string m_ITEM_TYPE = string.Empty;
        private string m_OLD_ITEM_CODE = string.Empty;
        private string m_RAW_GRADE = string.Empty;
        private string m_HS1_CODE = string.Empty;
        private string m_HS2_CODE = string.Empty;
        private string m_HS3_CODE = string.Empty;
        private string m_M_TYPE = string.Empty;
        private string m_RAW_SPEC = string.Empty;
        private string m_RAW_VERSION = string.Empty;
        private string m_TMODE = string.Empty;
        private int? m_SHELF_LIFE_MONTH = 0;
        private decimal m_RE_ORDER_LEBEL = 0;
        private DateTime? m_OPEN_DATE = null;
        private decimal m_OPEN_QTY = 0;
        private decimal m_OPEN_RATE = 0;
        private string m_ITEM_REMARKS = string.Empty;
        private int m_ITEM_GROUP_ID = 0;

        

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


        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "126", IsPrimaryKey = true)]
        public string ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }


        [DBColumn(Name = "ITEM_CODE", Storage = "m_ITEM_CODE", DbType = "126", IsPrimaryKey = true)]
        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
                this.NotifyPropertyChanged("ITEM_CODE");
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

        [DBColumn(Name = "ITEM_SHORT_DESC", Storage = "m_ITEM_SHORT_DESC", DbType = "126")]
        public string ITEM_SHORT_DESC
        {
            get { return this.m_ITEM_SHORT_DESC; }
            set
            {
                this.m_ITEM_SHORT_DESC = value;
                this.NotifyPropertyChanged("ITEM_SHORT_DESC");
            }
        }

        [DBColumn(Name = "MSR_ID", Storage = "m_MSR_ID", DbType = "126")]
        public string MSR_ID
        {
            get { return this.m_MSR_ID; }
            set
            {
                this.m_MSR_ID = value;
                this.NotifyPropertyChanged("MSR_ID");
            }
        }

        [DBColumn(Name = "COUNTRY_CODE", Storage = "m_COUNTRY_CODE", DbType = "126")]
        public string COUNTRY_CODE
        {
            get { return this.m_COUNTRY_CODE; }
            set
            {
                this.m_COUNTRY_CODE = value;
                this.NotifyPropertyChanged("COUNTRY_CODE");
            }
        }

        [DBColumn(Name = "MODEL_ID", Storage = "m_MODEL_ID", DbType = "126")]
        public string MODEL_ID
        {
            get { return this.m_MODEL_ID; }
            set
            {
                this.m_MODEL_ID = value;
                this.NotifyPropertyChanged("MODEL_ID");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126", IsPrimaryKey = true)]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
            }
        }

        [DBColumn(Name = "CAT_SUB_ID", Storage = "m_CAT_SUB_ID", DbType = "126", IsPrimaryKey = true)]
        public string CAT_SUB_ID
        {
            get { return this.m_CAT_SUB_ID; }
            set
            {
                this.m_CAT_SUB_ID = value;
                this.NotifyPropertyChanged("CAT_SUB_ID");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "126")]
        public string UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "COA_CODE", Storage = "m_COA_CODE", DbType = "126")]
        public string COA_CODE
        {
            get { return this.m_COA_CODE; }
            set
            {
                this.m_COA_CODE = value;
                this.NotifyPropertyChanged("COA_CODE");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "126")]
        public string STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "ITEM_TYPE", Storage = "m_ITEM_TYPE", DbType = "126")]
        public string ITEM_TYPE
        {
            get { return this.m_ITEM_TYPE; }
            set
            {
                this.m_ITEM_TYPE = value;
                this.NotifyPropertyChanged("ITEM_TYPE");
            }
        }

        [DBColumn(Name = "OLD_ITEM_CODE", Storage = "m_OLD_ITEM_CODE", DbType = "126")]
        public string OLD_ITEM_CODE
        {
            get { return this.m_OLD_ITEM_CODE; }
            set
            {
                this.m_OLD_ITEM_CODE = value;
                this.NotifyPropertyChanged("OLD_ITEM_CODE");
            }
        }

        [DBColumn(Name = "RAW_GRADE", Storage = "m_RAW_GRADE", DbType = "126")]
        public string RAW_GRADE
        {
            get { return this.m_RAW_GRADE; }
            set
            {
                this.m_RAW_GRADE = value;
                this.NotifyPropertyChanged("RAW_GRADE");
            }
        }

        [DBColumn(Name = "HS1_CODE", Storage = "m_HS1_CODE", DbType = "126")]
        public string HS1_CODE
        {
            get { return this.m_HS1_CODE; }
            set
            {
                this.m_HS1_CODE = value;
                this.NotifyPropertyChanged("HS1_CODE");
            }
        }

        [DBColumn(Name = "HS2_CODE", Storage = "m_HS2_CODE", DbType = "126")]
        public string HS2_CODE
        {
            get { return this.m_HS2_CODE; }
            set
            {
                this.m_HS2_CODE = value;
                this.NotifyPropertyChanged("HS2_CODE");
            }
        }

        [DBColumn(Name = "HS3_CODE", Storage = "m_HS3_CODE", DbType = "126")]
        public string HS3_CODE
        {
            get { return this.m_HS3_CODE; }
            set
            {
                this.m_HS3_CODE = value;
                this.NotifyPropertyChanged("HS3_CODE");
            }
        }

        [DBColumn(Name = "M_TYPE", Storage = "m_M_TYPE", DbType = "126")]
        public string M_TYPE
        {
            get { return this.m_M_TYPE; }
            set
            {
                this.m_M_TYPE = value;
                this.NotifyPropertyChanged("M_TYPE");
            }
        }

        [DBColumn(Name = "RAW_SPEC", Storage = "m_RAW_SPEC", DbType = "126")]
        public string RAW_SPEC
        {
            get { return this.m_RAW_SPEC; }
            set
            {
                this.m_RAW_SPEC = value;
                this.NotifyPropertyChanged("RAW_SPEC");
            }
        }

        [DBColumn(Name = "RAW_VERSION", Storage = "m_RAW_VERSION", DbType = "126")]
        public string RAW_VERSION
        {
            get { return this.m_RAW_VERSION; }
            set
            {
                this.m_RAW_VERSION = value;
                this.NotifyPropertyChanged("RAW_VERSION");
            }
        }

        [DBColumn(Name = "TMODE", Storage = "m_TMODE", DbType = "126")]
        public string TMODE
        {
            get { return this.m_TMODE; }
            set
            {
                this.m_TMODE = value;
                this.NotifyPropertyChanged("TMODE");
            }
        }

        [DBColumn(Name = "SHELF_LIFE_MONTH", Storage = "m_SHELF_LIFE_MONTH", DbType = "112")]
        public int? SHELF_LIFE_MONTH
        {
            get { return this.m_SHELF_LIFE_MONTH; }
            set
            {
                this.m_SHELF_LIFE_MONTH = value;
                this.NotifyPropertyChanged("SHELF_LIFE_MONTH");
            }
        }

        [DBColumn(Name = "RE_ORDER_LEBEL", Storage = "m_RE_ORDER_LEBEL", DbType = "107")]
        public decimal RE_ORDER_LEBEL
        {
            get { return this.m_RE_ORDER_LEBEL; }
            set
            {
                this.m_RE_ORDER_LEBEL = value;
                this.NotifyPropertyChanged("RE_ORDER_LEBEL");
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

        [DBColumn(Name = "OPEN_RATE", Storage = "m_OPEN_RATE", DbType = "107")]
        public decimal OPEN_RATE
        {
            get { return this.m_OPEN_RATE; }
            set
            {
                this.m_OPEN_RATE = value;
                this.NotifyPropertyChanged("OPEN_RATE");
            }
        }

        [DBColumn(Name = "ITEM_REMARKS", Storage = "m_ITEM_REMARKS", DbType = "126")]
        public string ITEM_REMARKS
        {
            get { return this.m_ITEM_REMARKS; }
            set
            {
                this.m_ITEM_REMARKS = value;
                this.NotifyPropertyChanged("ITEM_REMARKS");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_ID", Storage = "m_ITEM_GROUP_ID", DbType = "112")]
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ID");
            }
        }

        

        #endregion //properties 

    }
}
