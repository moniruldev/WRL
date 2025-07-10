using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_REJECTION_MST")]
    public partial class dcPROD_REJECTION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_REJECTION_ID = 0;
        private string m_PROD_REJECTION_NO = string.Empty;
        private DateTime? m_PROD_REJECTION_DATE = null;
        private decimal m_DEPT_ID = 0;
        private string m_REJECT_ITEM_TYPE = String.Empty;
        private string m_REJECTION_REASON = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_UPDATE_BY = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        //private DateTime? m_PRODUCTION_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private  DateTime? m_AUTHO_DATE =null;
        private string m_REC_STATUS = string.Empty;
        private string m_AUTHO_BY = string.Empty;
        private int m_UOM = 0;
        private string m_IS_RM = string.Empty;
        private string m_SHIFT_ID = string.Empty;
        private int m_STLM_ID = 0;
        private string m_APPROVAL_STATUS = string.Empty;
        private int m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;
        private string m_IS_TRANSFERED = string.Empty;
        




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


        [DBColumn(Name = "PROD_REJECTION_ID", Storage = "m_PROD_REJECTION_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_REJECTION_ID
        {
            get { return this.m_PROD_REJECTION_ID; }
            set
            {
                this.m_PROD_REJECTION_ID = value;
                this.NotifyPropertyChanged("PROD_REJECTION_ID");
            }
        }

        [DBColumn(Name = "PROD_REJECTION_NO", Storage = "m_PROD_REJECTION_NO", DbType = "126")]
        public string PROD_REJECTION_NO
        {
            get { return this.m_PROD_REJECTION_NO; }
            set
            {
                this.m_PROD_REJECTION_NO = value;
                this.NotifyPropertyChanged("PROD_REJECTION_NO");
            }
        }

        [DBColumn(Name = "PROD_REJECTION_DATE", Storage = "m_PROD_REJECTION_DATE", DbType = "106")]
        public DateTime? PROD_REJECTION_DATE
        {
            get { return this.m_PROD_REJECTION_DATE; }
            set
            {
                this.m_PROD_REJECTION_DATE = value;
                this.NotifyPropertyChanged("PROD_REJECTION_DATE");
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

        [DBColumn(Name = "REJECT_ITEM_TYPE", Storage = "m_REJECT_ITEM_TYPE", DbType = "107")]
        public string REJECT_ITEM_TYPE
        {
            get { return this.m_REJECT_ITEM_TYPE; }
            set
            {
                this.m_REJECT_ITEM_TYPE = value;
                this.NotifyPropertyChanged("REJECT_ITEM_TYPE");
            }
        }

        [DBColumn(Name = "REJECTION_REASON", Storage = "m_REJECTION_REASON", DbType = "126")]
        public string REJECTION_REASON
        {
            get { return this.m_REJECTION_REASON; }
            set
            {
                this.m_REJECTION_REASON = value;
                this.NotifyPropertyChanged("REJECTION_REASON");
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

        //[DBColumn(Name = "PRODUCTION_DATE", Storage = "m_PRODUCTION_DATE", DbType = "106")]
        //public DateTime? PRODUCTION_DATE
        //{
        //    get { return this.m_PRODUCTION_DATE; }
        //    set
        //    {
        //        this.m_PRODUCTION_DATE = value;
        //        this.NotifyPropertyChanged("PRODUCTION_DATE");
        //    }
        //}


       
     

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
        public int UOM
        {
            get { return this.m_UOM; }
            set
            {
                this.m_UOM = value;
                this.NotifyPropertyChanged("UOM");
            }
        }

        [DBColumn(Name = "IS_RM", Storage = "m_IS_RM", DbType = "107")]
        public string IS_RM
        {
            get { return this.m_IS_RM; }
            set
            {
                this.m_IS_RM = value;
                this.NotifyPropertyChanged("IS_RM");
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

        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "APPROVAL_STATUS", Storage = "m_APPROVAL_STATUS", DbType = "126")]
        public string APPROVAL_STATUS
        {
            get { return this.m_APPROVAL_STATUS; }
            set
            {
                this.m_APPROVAL_STATUS = value;
                this.NotifyPropertyChanged("APPROVAL_STATUS");
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

        [DBColumn(Name = "APPROVED_DATE", Storage = "m_APPROVED_DATE", DbType = "106")]
        public DateTime? APPROVED_DATE
        {
            get { return this.m_APPROVED_DATE; }
            set
            {
                this.m_APPROVED_DATE = value;
                this.NotifyPropertyChanged("APPROVED_DATE");
            }
        }

        [DBColumn(Name = "IS_TRANSFERED", Storage = "m_IS_TRANSFERED", DbType = "106")]
        public string IS_TRANSFERED
        {
            get { return this.m_IS_TRANSFERED; }
            set
            {
                this.m_IS_TRANSFERED = value;
                this.NotifyPropertyChanged("IS_TRANSFERED");
            }
        }

        #endregion //properties
    }

    public partial class dcPROD_REJECTION_MST
    {

        private List<dcPROD_REJECTION_DTL> m_RejectionDetList = null;
        public List<dcPROD_REJECTION_DTL> RejectionDetList
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
        public string STLM_NAME { get; set; }
        public bool IS_TRANSFER_COMPLETE { get; set;}

        private string m_REJECTION_TYPE = string.Empty;
        public string REJECTION_TYPE
        {
            get { return m_REJECTION_TYPE; }
            set { m_REJECTION_TYPE = value; }
        }

        public string APPROVAL_STATUS_FINAL { get; set; }

    }

}
