using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_MST_ASM")]
    public partial class dcPROD_REJECTION_MST_ASM : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_ASM_REJ_ID = 0;
        private string m_PROD_ASM_REJ_NO = string.Empty;
        private DateTime? m_PROD_ASM_REJ_DATE = null;
        private decimal m_DEPT_ID = 0;
        private string m_REJECTION_ASM_REASON = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private DateTime? m_PRODUCTION_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private DateTime? m_AUTHO_DATE = null;
        private string m_REC_STATUS = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private decimal m_UOM = 0;
        private string m_IS_RECOVERTOREJECT = string.Empty;
        private string m_REJECT_ITEM_TYPE = string.Empty;
        private string m_SHIFT_ID = string.Empty;

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


        [DBColumn(Name = "PROD_ASM_REJ_ID", Storage = "m_PROD_ASM_REJ_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_ASM_REJ_ID
        {
            get { return this.m_PROD_ASM_REJ_ID; }
            set
            {
                this.m_PROD_ASM_REJ_ID = value;
                this.NotifyPropertyChanged("PROD_ASM_REJ_ID");
            }
        }

        [DBColumn(Name = "PROD_ASM_REJ_NO", Storage = "m_PROD_ASM_REJ_NO", DbType = "126")]
        public string PROD_ASM_REJ_NO
        {
            get { return this.m_PROD_ASM_REJ_NO; }
            set
            {
                this.m_PROD_ASM_REJ_NO = value;
                this.NotifyPropertyChanged("PROD_ASM_REJ_NO");
            }
        }

        [DBColumn(Name = "PROD_ASM_REJ_DATE", Storage = "m_PROD_ASM_REJ_DATE", DbType = "106")]
        public DateTime? PROD_ASM_REJ_DATE
        {
            get { return this.m_PROD_ASM_REJ_DATE; }
            set
            {
                this.m_PROD_ASM_REJ_DATE = value;
                this.NotifyPropertyChanged("PROD_ASM_REJ_DATE");
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

        [DBColumn(Name = "REJECTION_ASM_REASON", Storage = "m_REJECTION_ASM_REASON", DbType = "126")]
        public string REJECTION_ASM_REASON
        {
            get { return this.m_REJECTION_ASM_REASON; }
            set
            {
                this.m_REJECTION_ASM_REASON = value;
                this.NotifyPropertyChanged("REJECTION_ASM_REASON");
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

        [DBColumn(Name = "PRODUCTION_DATE", Storage = "m_PRODUCTION_DATE", DbType = "106")]
        public DateTime? PRODUCTION_DATE
        {
            get { return this.m_PRODUCTION_DATE; }
            set
            {
                this.m_PRODUCTION_DATE = value;
                this.NotifyPropertyChanged("PRODUCTION_DATE");
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

        [DBColumn(Name = "UOM", Storage = "m_UOM", DbType = "107")]
        public decimal UOM
        {
            get { return this.m_UOM; }
            set
            {
                this.m_UOM = value;
                this.NotifyPropertyChanged("UOM");
            }
        }
        [DBColumn(Name = "IS_RECOVERTOREJECT", Storage = "m_IS_RECOVERTOREJECT", DbType = "126")]
        public string IS_RECOVERTOREJECT
        {
            get { return this.m_IS_RECOVERTOREJECT; }
            set
            {
                this.m_IS_RECOVERTOREJECT = value;
                this.NotifyPropertyChanged("IS_RECOVERTOREJECT");
            }
        }


     [DBColumn(Name = "REJECT_ITEM_TYPE", Storage = "m_REJECT_ITEM_TYPE", DbType = "126")]
        public string REJECT_ITEM_TYPE
        {
            get { return this.m_REJECT_ITEM_TYPE; }
            set
            {
                this.m_REJECT_ITEM_TYPE = value;
                this.NotifyPropertyChanged("REJECT_ITEM_TYPE");
            }
        }

     [DBColumn(Name = "SHIFT_ID", Storage = "m_SHIFT_ID", DbType = "126")]
     public string SHIFT_ID
     {
         get { return this.m_SHIFT_ID; }
         set
         {
             this.m_SHIFT_ID = value;
             this.NotifyPropertyChanged("SHIFT_ID");
         }
     }
        #endregion //properties
    }

    public partial class dcPROD_REJECTION_MST_ASM
    {

        private List<dcPROD_REJECTION_DTL_ASM> m_RejectionDetList = null;
        public List<dcPROD_REJECTION_DTL_ASM> RejectionDetList
        {
            get { return m_RejectionDetList; }
            set { m_RejectionDetList = value; }
        }


        private string m_CUST_NAME = string.Empty;
        public string CUST_NAME
        {
            get { return m_CUST_NAME; }
            set { m_CUST_NAME = value; }
        }


        private string m_CUST_ADDRESS = string.Empty;
        public string CUST_ADDRESS
        {
            get { return m_CUST_ADDRESS; }
            set { m_CUST_ADDRESS = value; }
        }

        private string m_COMPANY_NAME = string.Empty;
        public string COMPANY_NAME
        {
            get { return m_COMPANY_NAME; }
            set { m_COMPANY_NAME = value; }
        }

        private string m_ITEMTYPEQTY = string.Empty;
        public string ITEMTYPEQTY
        {
            get { return m_ITEMTYPEQTY; }
            set { m_ITEMTYPEQTY = value; }
        }

        private int m_SLNO = 0;
        public int SLNO
        {
            get { return m_SLNO; }
            set { m_SLNO = value; }
        }

        public decimal ITEM_QNTY { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string CUST_PHONE { get; set; }

        public string IS_ROTARY { get; set; }
    }
}
