using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRR_MASTER")]
    public partial class dcMRR_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_MRR_ID = 0;
        private string m_MRR_NO = string.Empty;
        private DateTime? m_MRR_DATE = null;
        private DateTime? m_MRR_TIME = null;
        private int m_STORE_ID = 0;
        private int m_INV_TRANS_TYPE_ID = 0;
        private int m_TRANS_ID = 0;
        private int? m_SUP_ID = 0;
        private string m_MRR_NOTE = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_IS_APPROVED = string.Empty;
        private int m_APPROVED_BY = 0;
        private DateTime? m_APPROVE_DATE = null;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UNAUTH_PRINT_COUNTER = 0;
        private int m_AUTH_PRINT_COUNTER = 0;
        private int m_COMPANY_ID = 0;
        private string m_QC_STATUS = string.Empty;
        private string m_CHALLAN_NO = string.Empty;
        private int m_DEPARTMENT_ID = 0;
        private string m_IS_QC = string.Empty;
        private int? m_RTN_RCV_ID = 0;
        private string m_IS_REPLACED = string.Empty;
         

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


        [DBColumn(Name = "MRR_ID", Storage = "m_MRR_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 MRR_ID
        {
            get { return this.m_MRR_ID; }
            set
            {
                this.m_MRR_ID = value;
                this.NotifyPropertyChanged("MRR_ID");
            }
        }

        [DBColumn(Name = "MRR_NO", Storage = "m_MRR_NO", DbType = "126")]
        public string MRR_NO
        {
            get { return this.m_MRR_NO; }
            set
            {
                this.m_MRR_NO = value;
                this.NotifyPropertyChanged("MRR_NO");
            }
        }

        [DBColumn(Name = "MRR_DATE", Storage = "m_MRR_DATE", DbType = "106")]
        public DateTime? MRR_DATE
        {
            get { return this.m_MRR_DATE; }
            set
            {
                this.m_MRR_DATE = value;
                this.NotifyPropertyChanged("MRR_DATE");
            }
        }

        [DBColumn(Name = "MRR_TIME", Storage = "m_MRR_TIME", DbType = "106")]
        public DateTime? MRR_TIME
        {
            get { return this.m_MRR_TIME; }
            set
            {
                this.m_MRR_TIME = value;
                this.NotifyPropertyChanged("MRR_TIME");
            }
        }

        [DBColumn(Name = "STORE_ID", Storage = "m_STORE_ID", DbType = "107")]
        public int STORE_ID
        {
            get { return this.m_STORE_ID; }
            set
            {
                this.m_STORE_ID = value;
                this.NotifyPropertyChanged("STORE_ID");
            }
        }

        [DBColumn(Name = "INV_TRANS_TYPE_ID", Storage = "m_INV_TRANS_TYPE_ID", DbType = "107")]
        public int INV_TRANS_TYPE_ID
        {
            get { return this.m_INV_TRANS_TYPE_ID; }
            set
            {
                this.m_INV_TRANS_TYPE_ID = value;
                this.NotifyPropertyChanged("INV_TRANS_TYPE_ID");
            }
        }

        [DBColumn(Name = "TRANS_ID", Storage = "m_TRANS_ID", DbType = "107")]
        public int TRANS_ID
        {
            get { return this.m_TRANS_ID; }
            set
            {
                this.m_TRANS_ID = value;
                this.NotifyPropertyChanged("TRANS_ID");
            }
        }

        [DBColumn(Name = "MRR_NOTE", Storage = "m_MRR_NOTE", DbType = "126")]
        public string MRR_NOTE
        {
            get { return this.m_MRR_NOTE; }
            set
            {
                this.m_MRR_NOTE = value;
                this.NotifyPropertyChanged("MRR_NOTE");
            }
        }

        [DBColumn(Name = "AUTH_STATUS", Storage = "m_AUTH_STATUS", DbType = "126")]
        public string AUTH_STATUS
        {
            get { return this.m_AUTH_STATUS; }
            set
            {
                this.m_AUTH_STATUS = value;
                this.NotifyPropertyChanged("AUTH_STATUS");
            }
        }

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "126")]
        public decimal AUTH_BY
        {
            get { return this.m_AUTH_BY; }
            set
            {
                this.m_AUTH_BY = value;
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

        [DBColumn(Name = "IS_APPROVED", Storage = "m_IS_APPROVED", DbType = "126")]
        public string IS_APPROVED
        {
            get { return this.m_IS_APPROVED; }
            set
            {
                this.m_IS_APPROVED = value;
                this.NotifyPropertyChanged("IS_APPROVED");
            }
        }

        [DBColumn(Name = "APPROVED_BY", Storage = "m_APPROVED_BY", DbType = "126")]
        public int APPROVED_BY
        {
            get { return this.m_APPROVED_BY; }
            set
            {
                this.m_APPROVED_BY = value;
                this.NotifyPropertyChanged("APPROVED_BY");
            }
        }

        [DBColumn(Name = "APPROVE_DATE", Storage = "m_APPROVE_DATE", DbType = "106")]
        public DateTime? APPROVE_DATE
        {
            get { return this.m_APPROVE_DATE; }
            set
            {
                this.m_APPROVE_DATE = value;
                this.NotifyPropertyChanged("APPROVE_DATE");
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


        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int? SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }


        [DBColumn(Name = "UNAUTH_PRINT_COUNTER", Storage = "m_UNAUTH_PRINT_COUNTER", DbType = "107")]
        public int UNAUTH_PRINT_COUNTER
        {
            get { return this.m_UNAUTH_PRINT_COUNTER; }
            set
            {
                this.m_UNAUTH_PRINT_COUNTER = value;
                this.NotifyPropertyChanged("UNAUTH_PRINT_COUNTER");
            }
        }


        [DBColumn(Name = "AUTH_PRINT_COUNTER", Storage = "m_AUTH_PRINT_COUNTER", DbType = "107")]
        public int AUTH_PRINT_COUNTER
        {
            get { return this.m_AUTH_PRINT_COUNTER; }
            set
            {
                this.m_AUTH_PRINT_COUNTER = value;
                this.NotifyPropertyChanged("AUTH_PRINT_COUNTER");
            }
        }

        [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107")]
        public int COMPANY_ID
        {
            get { return this.m_COMPANY_ID; }
            set
            {
                this.m_COMPANY_ID = value;
                this.NotifyPropertyChanged("COMPANY_ID");
            }
        }

        [DBColumn(Name = "QC_STATUS", Storage = "m_QC_STATUS", DbType = "126")]
        public string QC_STATUS
        {
            get { return this.m_QC_STATUS; }
            set
            {
                this.m_QC_STATUS = value;
                this.NotifyPropertyChanged("QC_STATUS");
            }
        }

        [DBColumn(Name = "CHALLAN_NO", Storage = "m_CHALLAN_NO", DbType = "126")]
        public string CHALLAN_NO
        {
            get { return this.m_CHALLAN_NO; }
            set
            {
                this.m_CHALLAN_NO = value;
                this.NotifyPropertyChanged("CHALLAN_NO");
            }
        }

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107")]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
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

        [DBColumn(Name = "RTN_RCV_ID", Storage = "m_RTN_RCV_ID", DbType = "107")]
        public int? RTN_RCV_ID
        {
            get { return this.m_RTN_RCV_ID; }
            set
            {
                this.m_RTN_RCV_ID = value;
                this.NotifyPropertyChanged("RTN_RCV_ID");
            }
        }

        [DBColumn(Name = "IS_REPLACED", Storage = "m_IS_REPLACED", DbType = "126")]
        public string IS_REPLACED
        {
            get { return this.m_IS_REPLACED; }
            set
            {
                this.m_IS_REPLACED = value;
                this.NotifyPropertyChanged("IS_REPLACED");
            }
        }

        #endregion //properties


    }
    public partial class dcMRR_MASTER
    {


        private string m_item_name = string.Empty;
        private string m_uom_name = string.Empty;
        private string m_mrr_qty = string.Empty;
        private string m_PURCHASE_NO = string.Empty;
        private string m_IMP_PURCHASE_NO = string.Empty;
        private bool m_IsMRRComplete = false;
        private bool m_IsQCComplete = false;
        private bool m_IsQCTransferComplete = false;
        private string m_DEPARTMENT_NAME = string.Empty;
        private string m_CREATE_BY_NAME = string.Empty;
        private string m_QC_BY_NAME = string.Empty;
        private DateTime? m_QC_DATE = null;
        private string m_QC_BY_DESIGNATION = string.Empty;
        
        



        public string item_name
        {
            get { return m_item_name; }
            set { this.m_item_name = value; }
        }

        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
        }
        public string mrr_qty
        {
            get { return m_mrr_qty; }
            set { this.m_mrr_qty = value; }
        }
        public string PURCHASE_NO
        {
            get { return m_PURCHASE_NO; }
            set { this.m_PURCHASE_NO = value; }
        }
        public string IMP_PURCHASE_NO
        {
            get { return m_IMP_PURCHASE_NO; }
            set { this.m_IMP_PURCHASE_NO = value; }
        }

        public string m_PURCHASE_TYPE = string.Empty;
        public string PURCHASE_TYPE
        {
            get { return m_PURCHASE_TYPE; }
            set { this.m_PURCHASE_TYPE = value; }
        }

        public string m_PURCHASE_DESCRIPTION = string.Empty;
        public string PURCHASE_DESCRIPTION
        {
            get { return m_PURCHASE_DESCRIPTION; }
            set { this.m_PURCHASE_DESCRIPTION = value; }
        }

        public string m_MRR_ITEM_DETAILS = string.Empty;
        public string MRR_ITEM_DETAILS
        {
            get { return m_MRR_ITEM_DETAILS; }
            set { this.m_MRR_ITEM_DETAILS = value; }
        }

        public string m_SUP_NAME = string.Empty;
        public string SUP_NAME
        {
            get { return m_SUP_NAME; }
            set { this.m_SUP_NAME = value; }
        }
        public DateTime? m_PURCHASE_DATE = null;
        public DateTime? PURCHASE_DATE
        {
            get { return m_PURCHASE_DATE; }
            set { this.m_PURCHASE_DATE = value; }
        }

        public bool IsMRRComplete
        {
            get { return this.m_IsMRRComplete; }
            set { this.m_IsMRRComplete = value; }
        }

        public bool IsQCComplete
        {
            get { return this.m_IsQCComplete; }
            set { this.m_IsQCComplete = value; }
        }

        public bool IsQCTransferComplete
        {
            get { return this.m_IsQCTransferComplete; }
            set { this.m_IsQCTransferComplete = value; }
        }
        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { this.m_DEPARTMENT_NAME = value; }
        }

        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { this.m_CREATE_BY_NAME = value; }
        }

        public string QC_BY_NAME
        {
            get { return m_QC_BY_NAME; }
            set { this.m_QC_BY_NAME = value; }
        }
        public DateTime? QC_DATE
        {
            get { return m_QC_DATE; }
            set { this.m_QC_DATE = value; }
        }
        public string QC_BY_DESIGNATION
        {
            get { return m_QC_BY_DESIGNATION; }
            set { this.m_QC_BY_DESIGNATION = value; }
        }
        public Byte[] SIGN_PHOTO_CREATE_BY { get; set; }
        public Byte[] SIGN_PHOTO_QC_BY { get; set; }


        

    }
}
