using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CUSTOMER_ADJUSTMENT")]
    public partial class dcCUSTOMER_ADJUSTMENT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ADJUST_ID = 0;
        private string m_ADJUSTMENT_NO = string.Empty;
        private DateTime? m_ADJ_DATE = null;
        private int m_CUST_ID = 0;
        private string m_SALES_MONTH = string.Empty;
        private decimal m_ADJ_AMT = 0;
        private string m_ADJ_REASON = string.Empty;
        private string m_REF_NUM = string.Empty;
        private decimal m_DEPT_ID = 0;
        private string m_LOC_CODE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private string m_UPDATE_DATE = string.Empty;
        private string m_REC_STATUS = string.Empty;
        private string m_AUTHO_STATUS = string.Empty;
        private decimal m_ADJUSTED_AMT = 0;
        private string m_FULL_ADJUSTED = string.Empty;
        private string m_BPOST = string.Empty;
        private int m_MR_ADJUST_TYPE_ID = 0;
        private string m_AUTHO_BY = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_ADJ_RTN_VC_NO = string.Empty;
        private DateTime? m_ADJ_TIME = null;
        private decimal m_ITEM_SEGMENT_ID = 0;
        private decimal m_IS_SPECIAL = 0;

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


        [DBColumn(Name = "ADJUST_ID", Storage = "m_ADJUST_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ADJUST_ID
        {
            get { return this.m_ADJUST_ID; }
            set
            {
                this.m_ADJUST_ID = value;
                this.NotifyPropertyChanged("ADJUST_ID");
            }
        }

        [DBColumn(Name = "ADJUSTMENT_NO", Storage = "m_ADJUSTMENT_NO", DbType = "126")]
        public string ADJUSTMENT_NO
        {
            get { return this.m_ADJUSTMENT_NO; }
            set
            {
                this.m_ADJUSTMENT_NO = value;
                this.NotifyPropertyChanged("ADJUSTMENT_NO");
            }
        }

        [DBColumn(Name = "ADJ_DATE", Storage = "m_ADJ_DATE", DbType = "106")]
        public DateTime? ADJ_DATE
        {
            get { return this.m_ADJ_DATE; }
            set
            {
                this.m_ADJ_DATE = value;
                this.NotifyPropertyChanged("ADJ_DATE");
            }
        }

        [DBColumn(Name = "CUST_ID", Storage = "m_CUST_ID", DbType = "107")]
        public int CUST_ID
        {
            get { return this.m_CUST_ID; }
            set
            {
                this.m_CUST_ID = value;
                this.NotifyPropertyChanged("CUST_ID");
            }
        }

        [DBColumn(Name = "SALES_MONTH", Storage = "m_SALES_MONTH", DbType = "126")]
        public string SALES_MONTH
        {
            get { return this.m_SALES_MONTH; }
            set
            {
                this.m_SALES_MONTH = value;
                this.NotifyPropertyChanged("SALES_MONTH");
            }
        }

        [DBColumn(Name = "ADJ_AMT", Storage = "m_ADJ_AMT", DbType = "107")]
        public decimal ADJ_AMT
        {
            get { return this.m_ADJ_AMT; }
            set
            {
                this.m_ADJ_AMT = value;
                this.NotifyPropertyChanged("ADJ_AMT");
            }
        }

        [DBColumn(Name = "ADJ_REASON", Storage = "m_ADJ_REASON", DbType = "126")]
        public string ADJ_REASON
        {
            get { return this.m_ADJ_REASON; }
            set
            {
                this.m_ADJ_REASON = value;
                this.NotifyPropertyChanged("ADJ_REASON");
            }
        }

        [DBColumn(Name = "REF_NUM", Storage = "m_REF_NUM", DbType = "126")]
        public string REF_NUM
        {
            get { return this.m_REF_NUM; }
            set
            {
                this.m_REF_NUM = value;
                this.NotifyPropertyChanged("REF_NUM");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public decimal DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "LOC_CODE", Storage = "m_LOC_CODE", DbType = "126")]
        public string LOC_CODE
        {
            get { return this.m_LOC_CODE; }
            set
            {
                this.m_LOC_CODE = value;
                this.NotifyPropertyChanged("LOC_CODE");
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

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "126")]
        public string UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "REC_STATUS", Storage = "m_REC_STATUS", DbType = "126")]
        public string REC_STATUS
        {
            get { return this.m_REC_STATUS; }
            set
            {
                this.m_REC_STATUS = value;
                this.NotifyPropertyChanged("REC_STATUS");
            }
        }

        [DBColumn(Name = "AUTHO_STATUS", Storage = "m_AUTHO_STATUS", DbType = "126")]
        public string AUTHO_STATUS
        {
            get { return this.m_AUTHO_STATUS; }
            set
            {
                this.m_AUTHO_STATUS = value;
                this.NotifyPropertyChanged("AUTHO_STATUS");
            }
        }

        [DBColumn(Name = "ADJUSTED_AMT", Storage = "m_ADJUSTED_AMT", DbType = "107")]
        public decimal ADJUSTED_AMT
        {
            get { return this.m_ADJUSTED_AMT; }
            set
            {
                this.m_ADJUSTED_AMT = value;
                this.NotifyPropertyChanged("ADJUSTED_AMT");
            }
        }

        [DBColumn(Name = "FULL_ADJUSTED", Storage = "m_FULL_ADJUSTED", DbType = "126")]
        public string FULL_ADJUSTED
        {
            get { return this.m_FULL_ADJUSTED; }
            set
            {
                this.m_FULL_ADJUSTED = value;
                this.NotifyPropertyChanged("FULL_ADJUSTED");
            }
        }

        [DBColumn(Name = "BPOST", Storage = "m_BPOST", DbType = "126")]
        public string BPOST
        {
            get { return this.m_BPOST; }
            set
            {
                this.m_BPOST = value;
                this.NotifyPropertyChanged("BPOST");
            }
        }

        [DBColumn(Name = "MR_ADJUST_TYPE_ID", Storage = "m_MR_ADJUST_TYPE_ID", DbType = "126")]
        public int MR_ADJUST_TYPE_ID
        {
            get { return this.m_MR_ADJUST_TYPE_ID; }
            set
            {
                this.m_MR_ADJUST_TYPE_ID = value;
                this.NotifyPropertyChanged("MR_ADJUST_TYPE_ID");
            }
        }

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "126")]
        public string AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "106")]
        public DateTime? AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }

        [DBColumn(Name = "ADJ_RTN_VC_NO", Storage = "m_ADJ_RTN_VC_NO", DbType = "126")]
        public string ADJ_RTN_VC_NO
        {
            get { return this.m_ADJ_RTN_VC_NO; }
            set
            {
                this.m_ADJ_RTN_VC_NO = value;
                this.NotifyPropertyChanged("ADJ_RTN_VC_NO");
            }
        }

        [DBColumn(Name = "ADJ_TIME", Storage = "m_ADJ_TIME", DbType = "106")]
        public DateTime? ADJ_TIME
        {
            get { return this.m_ADJ_TIME; }
            set
            {
                this.m_ADJ_TIME = value;
                this.NotifyPropertyChanged("ADJ_TIME");
            }
        }

        [DBColumn(Name = "ITEM_SEGMENT_ID", Storage = "m_ITEM_SEGMENT_ID", DbType = "107")]
        public decimal ITEM_SEGMENT_ID
        {
            get { return this.m_ITEM_SEGMENT_ID; }
            set
            {
                this.m_ITEM_SEGMENT_ID = value;
                this.NotifyPropertyChanged("ITEM_SEGMENT_ID");
            }
        }

        [DBColumn(Name = "IS_SPECIAL", Storage = "m_IS_SPECIAL", DbType = "107")]
        public decimal IS_SPECIAL
        {
            get { return this.m_IS_SPECIAL; }
            set
            {
                this.m_IS_SPECIAL = value;
                this.NotifyPropertyChanged("IS_SPECIAL");
            }
        }

        #endregion //properties
    }

    public partial class dcCUSTOMER_ADJUSTMENT
    {

        public string m_CUST_NAME = string.Empty;

        public string CUST_NAME
        {
            get { return this.m_CUST_NAME; }
            set
            {
                this.m_CUST_NAME = value;
            }
        }

        public string m_CUST_CODE = string.Empty;

        public string CUST_CODE
        {
            get { return this.m_CUST_CODE; }
            set
            {
                this.m_CUST_CODE = value;
            }
        }

        public string m_ADJUST_TYPE_DESC = string.Empty;

        public string ADJUST_TYPE_DESC
        {
            get { return this.m_ADJUST_TYPE_DESC; }
            set
            {
                this.m_ADJUST_TYPE_DESC = value;
            }
        }

    }

    //jhgjgyjgyu
}
