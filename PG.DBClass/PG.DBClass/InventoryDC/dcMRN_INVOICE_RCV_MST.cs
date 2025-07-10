using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRN_INVOICE_RCV_MST")]
    public partial class dcMRN_INVOICE_RCV_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_INV_NO = string.Empty;
        private DateTime? m_INV_RCV_DT = null;
        private string m_SUP_CODE = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_STORE_ID = string.Empty;
        private string m_BRANCH_CODE = string.Empty;
        private string m_DEPARTMENT_CODE = string.Empty;
        private string m_RCV_DR_COA_CODE = string.Empty;
        private string m_RC_CODE = string.Empty;
        private string m_ORD_NO = string.Empty;
        private string m_USER_ID = string.Empty;
        private DateTime? m_ENTER_DATE = null;
        private string m_RCV_CR_COA_CODE = string.Empty;
        private string m_RCV_VC_NO = string.Empty;
        private string m_MRN_NO = string.Empty;
        private string m_DEL_MODE = string.Empty;
        private string m_DEL_LOC = string.Empty;
        private string m_CHALL_NO = string.Empty;
        private DateTime? m_CHALL_DATE = null;
        private decimal m_LOT_RATE = 0;
        private string m_IT_REMARKS = string.Empty;

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


        [DBColumn(Name = "INV_NO", Storage = "m_INV_NO", DbType = "126", IsPrimaryKey = true)]
        public string INV_NO
        {
            get { return this.m_INV_NO; }
            set
            {
                this.m_INV_NO = value;
                this.NotifyPropertyChanged("INV_NO");
            }
        }

        [DBColumn(Name = "INV_RCV_DT", Storage = "m_INV_RCV_DT", DbType = "106")]
        public DateTime? INV_RCV_DT
        {
            get { return this.m_INV_RCV_DT; }
            set
            {
                this.m_INV_RCV_DT = value;
                this.NotifyPropertyChanged("INV_RCV_DT");
            }
        }

        [DBColumn(Name = "SUP_CODE", Storage = "m_SUP_CODE", DbType = "126", IsPrimaryKey = true)]
        public string SUP_CODE
        {
            get { return this.m_SUP_CODE; }
            set
            {
                this.m_SUP_CODE = value;
                this.NotifyPropertyChanged("SUP_CODE");
            }
        }

        [DBColumn(Name = "CAT_ID", Storage = "m_CAT_ID", DbType = "126")]
        public string CAT_ID
        {
            get { return this.m_CAT_ID; }
            set
            {
                this.m_CAT_ID = value;
                this.NotifyPropertyChanged("CAT_ID");
            }
        }

        [DBColumn(Name = "CAT_SUB_ID", Storage = "m_CAT_SUB_ID", DbType = "126")]
        public string CAT_SUB_ID
        {
            get { return this.m_CAT_SUB_ID; }
            set
            {
                this.m_CAT_SUB_ID = value;
                this.NotifyPropertyChanged("CAT_SUB_ID");
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

        [DBColumn(Name = "BRANCH_CODE", Storage = "m_BRANCH_CODE", DbType = "126")]
        public string BRANCH_CODE
        {
            get { return this.m_BRANCH_CODE; }
            set
            {
                this.m_BRANCH_CODE = value;
                this.NotifyPropertyChanged("BRANCH_CODE");
            }
        }

        [DBColumn(Name = "DEPARTMENT_CODE", Storage = "m_DEPARTMENT_CODE", DbType = "126")]
        public string DEPARTMENT_CODE
        {
            get { return this.m_DEPARTMENT_CODE; }
            set
            {
                this.m_DEPARTMENT_CODE = value;
                this.NotifyPropertyChanged("DEPARTMENT_CODE");
            }
        }

        [DBColumn(Name = "RCV_DR_COA_CODE", Storage = "m_RCV_DR_COA_CODE", DbType = "126")]
        public string RCV_DR_COA_CODE
        {
            get { return this.m_RCV_DR_COA_CODE; }
            set
            {
                this.m_RCV_DR_COA_CODE = value;
                this.NotifyPropertyChanged("RCV_DR_COA_CODE");
            }
        }

        [DBColumn(Name = "RC_CODE", Storage = "m_RC_CODE", DbType = "126")]
        public string RC_CODE
        {
            get { return this.m_RC_CODE; }
            set
            {
                this.m_RC_CODE = value;
                this.NotifyPropertyChanged("RC_CODE");
            }
        }

        [DBColumn(Name = "ORD_NO", Storage = "m_ORD_NO", DbType = "126")]
        public string ORD_NO
        {
            get { return this.m_ORD_NO; }
            set
            {
                this.m_ORD_NO = value;
                this.NotifyPropertyChanged("ORD_NO");
            }
        }

        [DBColumn(Name = "USER_ID", Storage = "m_USER_ID", DbType = "126")]
        public string USER_ID
        {
            get { return this.m_USER_ID; }
            set
            {
                this.m_USER_ID = value;
                this.NotifyPropertyChanged("USER_ID");
            }
        }

        [DBColumn(Name = "ENTER_DATE", Storage = "m_ENTER_DATE", DbType = "106")]
        public DateTime? ENTER_DATE
        {
            get { return this.m_ENTER_DATE; }
            set
            {
                this.m_ENTER_DATE = value;
                this.NotifyPropertyChanged("ENTER_DATE");
            }
        }

        [DBColumn(Name = "RCV_CR_COA_CODE", Storage = "m_RCV_CR_COA_CODE", DbType = "126")]
        public string RCV_CR_COA_CODE
        {
            get { return this.m_RCV_CR_COA_CODE; }
            set
            {
                this.m_RCV_CR_COA_CODE = value;
                this.NotifyPropertyChanged("RCV_CR_COA_CODE");
            }
        }

        [DBColumn(Name = "RCV_VC_NO", Storage = "m_RCV_VC_NO", DbType = "126")]
        public string RCV_VC_NO
        {
            get { return this.m_RCV_VC_NO; }
            set
            {
                this.m_RCV_VC_NO = value;
                this.NotifyPropertyChanged("RCV_VC_NO");
            }
        }

        [DBColumn(Name = "MRN_NO", Storage = "m_MRN_NO", DbType = "126")]
        public string MRN_NO
        {
            get { return this.m_MRN_NO; }
            set
            {
                this.m_MRN_NO = value;
                this.NotifyPropertyChanged("MRN_NO");
            }
        }

        [DBColumn(Name = "DEL_MODE", Storage = "m_DEL_MODE", DbType = "126")]
        public string DEL_MODE
        {
            get { return this.m_DEL_MODE; }
            set
            {
                this.m_DEL_MODE = value;
                this.NotifyPropertyChanged("DEL_MODE");
            }
        }

        [DBColumn(Name = "DEL_LOC", Storage = "m_DEL_LOC", DbType = "126")]
        public string DEL_LOC
        {
            get { return this.m_DEL_LOC; }
            set
            {
                this.m_DEL_LOC = value;
                this.NotifyPropertyChanged("DEL_LOC");
            }
        }

        [DBColumn(Name = "CHALL_NO", Storage = "m_CHALL_NO", DbType = "126")]
        public string CHALL_NO
        {
            get { return this.m_CHALL_NO; }
            set
            {
                this.m_CHALL_NO = value;
                this.NotifyPropertyChanged("CHALL_NO");
            }
        }

        [DBColumn(Name = "CHALL_DATE", Storage = "m_CHALL_DATE", DbType = "106")]
        public DateTime? CHALL_DATE
        {
            get { return this.m_CHALL_DATE; }
            set
            {
                this.m_CHALL_DATE = value;
                this.NotifyPropertyChanged("CHALL_DATE");
            }
        }

        [DBColumn(Name = "LOT_RATE", Storage = "m_LOT_RATE", DbType = "107")]
        public decimal LOT_RATE
        {
            get { return this.m_LOT_RATE; }
            set
            {
                this.m_LOT_RATE = value;
                this.NotifyPropertyChanged("LOT_RATE");
            }
        }

        [DBColumn(Name = "IT_REMARKS", Storage = "m_IT_REMARKS", DbType = "126")]
        public string IT_REMARKS
        {
            get { return this.m_IT_REMARKS; }
            set
            {
                this.m_IT_REMARKS = value;
                this.NotifyPropertyChanged("IT_REMARKS");
            }
        }

        #endregion //properties
    }
    public partial class dcMRN_INVOICE_RCV_MST
    {
        private string m_APRV_NO = string.Empty;
        private DateTime? m_PURCHASE_DATE = null;
        private string m_INDT_NO = null;
        private DateTime? m_INDT_DATE = null;
        private string m_SUP_NAME = string.Empty;
        private string m_SUP_ADDRESS = string.Empty;

        public string APRV_NO
        {
            get { return m_APRV_NO; }
            set { this.m_APRV_NO = value; }
        }

        public DateTime? PURCHASE_DATE
        {
            get { return m_PURCHASE_DATE; }
            set { this.m_PURCHASE_DATE = value; }
        }
        public string INDT_NO
        {
            get { return m_INDT_NO; }
            set { this.m_INDT_NO = value; }
        }

        public DateTime? INDT_DATE
        {
            get { return m_INDT_DATE; }
            set { this.m_INDT_DATE = value; }
        }

        public string SUP_NAME
        {
            get { return m_SUP_NAME; }
            set { this.m_SUP_NAME = value; }
        }

        public string SUP_ADDRESS
        {
            get { return m_SUP_ADDRESS; }
            set { this.m_SUP_ADDRESS = value; }
        }


    }

}
