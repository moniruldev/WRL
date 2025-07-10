using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LP_INDT_PURCHASE_DETAILS")]
    public class dcLP_INDT_PURCHASE_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_APRV_NO = string.Empty;
        private DateTime? m_PURCHASE_DATE = null;
        private string m_INDT_NO = string.Empty;
        private decimal m_RCV_SLNO = 0;
        private string m_ITEM_CODE = string.Empty;
        private decimal m_PURCHASE_QNTY = 0;
        private string m_MSR_ID = string.Empty;
        private string m_REQUIRED_DEPT = string.Empty;
        private string m_SR_DEPT = string.Empty;
        private string m_REQ_BRANCH = string.Empty;
        private string m_CAT_ID = string.Empty;
        private string m_CAT_SUB_ID = string.Empty;
        private decimal m_UNIT_RATE = 0;
        private string m_SUP_CODE = string.Empty;
        private string m_TMODE = string.Empty;
        private string m_ORD_NO = string.Empty;
        private string m_STORE_ID = string.Empty;
        private string m_MRN_NO = string.Empty;
        private string m_COMP_ID = string.Empty;
        private string m_ORD_STATUS = string.Empty;
        private string m_SR_BRANCH = string.Empty;
        private string m_RCV_BY = string.Empty;
        private decimal m_APRV_UNIT_RATE = 0;
        private string m_AUDIT_NOTE = string.Empty;
        private DateTime? m_AUDIT_CHK_DATE = null;
        private string m_AUDIT_BY = string.Empty;
        private string m_RATE_EDIT = string.Empty;
        private DateTime? m_RATE_EDIT_DATE = null;
        private string m_RATE_EDIT_BY = string.Empty;
        private int m_DISC_PERCENT = 0;
        private decimal m_DISC_UNIT_RATE = 0;
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


        [DBColumn(Name = "APRV_NO", Storage = "m_APRV_NO", DbType = "126", IsPrimaryKey = true)]
        public string APRV_NO
        {
            get { return this.m_APRV_NO; }
            set
            {
                this.m_APRV_NO = value;
                this.NotifyPropertyChanged("APRV_NO");
            }
        }

        [DBColumn(Name = "PURCHASE_DATE", Storage = "m_PURCHASE_DATE", DbType = "106")]
        public DateTime? PURCHASE_DATE
        {
            get { return this.m_PURCHASE_DATE; }
            set
            {
                this.m_PURCHASE_DATE = value;
                this.NotifyPropertyChanged("PURCHASE_DATE");
            }
        }

        [DBColumn(Name = "INDT_NO", Storage = "m_INDT_NO", DbType = "126", IsPrimaryKey = true)]
        public string INDT_NO
        {
            get { return this.m_INDT_NO; }
            set
            {
                this.m_INDT_NO = value;
                this.NotifyPropertyChanged("INDT_NO");
            }
        }

        [DBColumn(Name = "RCV_SLNO", Storage = "m_RCV_SLNO", DbType = "107")]
        public decimal RCV_SLNO
        {
            get { return this.m_RCV_SLNO; }
            set
            {
                this.m_RCV_SLNO = value;
                this.NotifyPropertyChanged("RCV_SLNO");
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

        [DBColumn(Name = "PURCHASE_QNTY", Storage = "m_PURCHASE_QNTY", DbType = "107")]
        public decimal PURCHASE_QNTY
        {
            get { return this.m_PURCHASE_QNTY; }
            set
            {
                this.m_PURCHASE_QNTY = value;
                this.NotifyPropertyChanged("PURCHASE_QNTY");
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

        [DBColumn(Name = "REQUIRED_DEPT", Storage = "m_REQUIRED_DEPT", DbType = "126", IsPrimaryKey = true)]
        public string REQUIRED_DEPT
        {
            get { return this.m_REQUIRED_DEPT; }
            set
            {
                this.m_REQUIRED_DEPT = value;
                this.NotifyPropertyChanged("REQUIRED_DEPT");
            }
        }

        [DBColumn(Name = "SR_DEPT", Storage = "m_SR_DEPT", DbType = "126", IsPrimaryKey = true)]
        public string SR_DEPT
        {
            get { return this.m_SR_DEPT; }
            set
            {
                this.m_SR_DEPT = value;
                this.NotifyPropertyChanged("SR_DEPT");
            }
        }

        [DBColumn(Name = "REQ_BRANCH", Storage = "m_REQ_BRANCH", DbType = "126", IsPrimaryKey = true)]
        public string REQ_BRANCH
        {
            get { return this.m_REQ_BRANCH; }
            set
            {
                this.m_REQ_BRANCH = value;
                this.NotifyPropertyChanged("REQ_BRANCH");
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

        [DBColumn(Name = "UNIT_RATE", Storage = "m_UNIT_RATE", DbType = "107")]
        public decimal UNIT_RATE
        {
            get { return this.m_UNIT_RATE; }
            set
            {
                this.m_UNIT_RATE = value;
                this.NotifyPropertyChanged("UNIT_RATE");
            }
        }

        [DBColumn(Name = "SUP_CODE", Storage = "m_SUP_CODE", DbType = "126")]
        public string SUP_CODE
        {
            get { return this.m_SUP_CODE; }
            set
            {
                this.m_SUP_CODE = value;
                this.NotifyPropertyChanged("SUP_CODE");
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

        [DBColumn(Name = "ORD_STATUS", Storage = "m_ORD_STATUS", DbType = "126")]
        public string ORD_STATUS
        {
            get { return this.m_ORD_STATUS; }
            set
            {
                this.m_ORD_STATUS = value;
                this.NotifyPropertyChanged("ORD_STATUS");
            }
        }

        [DBColumn(Name = "SR_BRANCH", Storage = "m_SR_BRANCH", DbType = "126")]
        public string SR_BRANCH
        {
            get { return this.m_SR_BRANCH; }
            set
            {
                this.m_SR_BRANCH = value;
                this.NotifyPropertyChanged("SR_BRANCH");
            }
        }

        [DBColumn(Name = "RCV_BY", Storage = "m_RCV_BY", DbType = "126")]
        public string RCV_BY
        {
            get { return this.m_RCV_BY; }
            set
            {
                this.m_RCV_BY = value;
                this.NotifyPropertyChanged("RCV_BY");
            }
        }

        [DBColumn(Name = "APRV_UNIT_RATE", Storage = "m_APRV_UNIT_RATE", DbType = "107")]
        public decimal APRV_UNIT_RATE
        {
            get { return this.m_APRV_UNIT_RATE; }
            set
            {
                this.m_APRV_UNIT_RATE = value;
                this.NotifyPropertyChanged("APRV_UNIT_RATE");
            }
        }

        [DBColumn(Name = "AUDIT_NOTE", Storage = "m_AUDIT_NOTE", DbType = "126")]
        public string AUDIT_NOTE
        {
            get { return this.m_AUDIT_NOTE; }
            set
            {
                this.m_AUDIT_NOTE = value;
                this.NotifyPropertyChanged("AUDIT_NOTE");
            }
        }

        [DBColumn(Name = "AUDIT_CHK_DATE", Storage = "m_AUDIT_CHK_DATE", DbType = "106")]
        public DateTime? AUDIT_CHK_DATE
        {
            get { return this.m_AUDIT_CHK_DATE; }
            set
            {
                this.m_AUDIT_CHK_DATE = value;
                this.NotifyPropertyChanged("AUDIT_CHK_DATE");
            }
        }

        [DBColumn(Name = "AUDIT_BY", Storage = "m_AUDIT_BY", DbType = "126")]
        public string AUDIT_BY
        {
            get { return this.m_AUDIT_BY; }
            set
            {
                this.m_AUDIT_BY = value;
                this.NotifyPropertyChanged("AUDIT_BY");
            }
        }

        [DBColumn(Name = "RATE_EDIT", Storage = "m_RATE_EDIT", DbType = "126")]
        public string RATE_EDIT
        {
            get { return this.m_RATE_EDIT; }
            set
            {
                this.m_RATE_EDIT = value;
                this.NotifyPropertyChanged("RATE_EDIT");
            }
        }

        [DBColumn(Name = "RATE_EDIT_DATE", Storage = "m_RATE_EDIT_DATE", DbType = "106")]
        public DateTime? RATE_EDIT_DATE
        {
            get { return this.m_RATE_EDIT_DATE; }
            set
            {
                this.m_RATE_EDIT_DATE = value;
                this.NotifyPropertyChanged("RATE_EDIT_DATE");
            }
        }

        [DBColumn(Name = "RATE_EDIT_BY", Storage = "m_RATE_EDIT_BY", DbType = "126")]
        public string RATE_EDIT_BY
        {
            get { return this.m_RATE_EDIT_BY; }
            set
            {
                this.m_RATE_EDIT_BY = value;
                this.NotifyPropertyChanged("RATE_EDIT_BY");
            }
        }

        [DBColumn(Name = "DISC_PERCENT", Storage = "m_DISC_PERCENT", DbType = "112")]
        public int DISC_PERCENT
        {
            get { return this.m_DISC_PERCENT; }
            set
            {
                this.m_DISC_PERCENT = value;
                this.NotifyPropertyChanged("DISC_PERCENT");
            }
        }

        [DBColumn(Name = "DISC_UNIT_RATE", Storage = "m_DISC_UNIT_RATE", DbType = "107")]
        public decimal DISC_UNIT_RATE
        {
            get { return this.m_DISC_UNIT_RATE; }
            set
            {
                this.m_DISC_UNIT_RATE = value;
                this.NotifyPropertyChanged("DISC_UNIT_RATE");
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
}
