using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "IMP_PURCHASE_MASTER")]
    public partial class dcIMP_PURCHASE_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_IMP_PURCHASE_ID = 0;
        private string m_IMP_PURCHASE_NO = string.Empty;
        private DateTime? m_IMP_PURCHASE_DATE = null;
        private DateTime? m_IMP_PURCHASE_TIME = null;
        private Int64 m_LC_ID = 0;
        private int? m_SUP_ID = 0;
        private string m_IMP_INVOICE_NO = string.Empty;
        private DateTime? m_IMP_INVOICE_DATE = null;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_REMARKS = string.Empty;
        private string m_IS_CLOSED ="N";
        private int m_COMPANY_ID = 0;

     

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


        [DBColumn(Name = "IMP_PURCHASE_ID", Storage = "m_IMP_PURCHASE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 IMP_PURCHASE_ID
        {
            get { return this.m_IMP_PURCHASE_ID; }
            set
            {
                this.m_IMP_PURCHASE_ID = value;
                this.NotifyPropertyChanged("IMP_PURCHASE_ID");
            }
        }




        [DBColumn(Name = "IMP_PURCHASE_NO", Storage = "m_IMP_PURCHASE_NO", DbType = "126")]
        public string IMP_PURCHASE_NO
        {
            get { return this.m_IMP_PURCHASE_NO; }
            set
            {
                this.m_IMP_PURCHASE_NO = value;
                this.NotifyPropertyChanged("IMP_PURCHASE_NO");
            }
        }

        [DBColumn(Name = "IMP_PURCHASE_DATE", Storage = "m_IMP_PURCHASE_DATE", DbType = "106")]
        public DateTime? IMP_PURCHASE_DATE
        {
            get { return this.m_IMP_PURCHASE_DATE; }
            set
            {
                this.m_IMP_PURCHASE_DATE = value;
                this.NotifyPropertyChanged("IMP_PURCHASE_DATE");
            }
        }

        [DBColumn(Name = "IMP_PURCHASE_TIME", Storage = "m_IMP_PURCHASE_TIME", DbType = "106")]
        public DateTime? IMP_PURCHASE_TIME
        {
            get { return this.m_IMP_PURCHASE_TIME; }
            set
            {
                this.m_IMP_PURCHASE_TIME = value;
                this.NotifyPropertyChanged("IMP_PURCHASE_TIME");
            }
        }

        [DBColumn(Name = "LC_ID", Storage = "m_LC_ID", DbType = "107")]
        public Int64 LC_ID
        {
            get { return this.m_LC_ID; }
            set
            {
                this.m_LC_ID = value;
                this.NotifyPropertyChanged("LC_ID");
            }
        }
        
        

        [DBColumn(Name = "IMP_INVOICE_NO", Storage = "m_IMP_INVOICE_NO", DbType = "126")]
        public string IMP_INVOICE_NO
        {
            get { return this.m_IMP_INVOICE_NO; }
            set
            {
                this.m_IMP_INVOICE_NO = value;
                this.NotifyPropertyChanged("IMP_INVOICE_NO");
            }
        }

        [DBColumn(Name = "IMP_INVOICE_DATE", Storage = "m_IMP_INVOICE_DATE", DbType = "106")]
        public DateTime? IMP_INVOICE_DATE
        {
            get { return this.m_IMP_INVOICE_DATE; }
            set
            {
                this.m_IMP_INVOICE_DATE = value;
                this.NotifyPropertyChanged("IMP_INVOICE_DATE");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
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


        [DBColumn(Name = "IS_CLOSED", Storage = "m_IS_CLOSED", DbType = "126")]
        public string IS_CLOSED
        {
            get { return this.m_IS_CLOSED; }
            set
            {
                this.m_IS_CLOSED = value;
                this.NotifyPropertyChanged("IS_CLOSED");
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

   
        #endregion //properties





        
    }

    public partial class dcIMP_PURCHASE_MASTER
    {
        private bool m_IsMRRComplete=false;
        public bool IsMRRComplete
        {
            get{ 
              return  m_IsMRRComplete;
            }
            set{
                this.m_IsMRRComplete=value;
            }
        }

        private bool m_IsAdjustAble = false;
        public bool IsAdjustAble
        {
            get
            {
                return m_IsAdjustAble;
            }
            set
            {
                this.m_IsAdjustAble = value;
            }
        }

        private string m_CREATE_BY_NAME = string.Empty;
        public string CREATE_BY_NAME
        {
            get { return m_CREATE_BY_NAME; }
            set { this.m_CREATE_BY_NAME = value; }
        }

        private string m_SUP_NAME = string.Empty;
        public string SUP_NAME
        {
            get { return m_SUP_NAME; }
            set { this.m_SUP_NAME = value; }
        }

        private string m_SUP_CODE = string.Empty;
        public string SUP_CODE
        {
            get { return m_SUP_CODE; }
            set { this.m_SUP_CODE = value; }
        }

        private string m_LC_NO = string.Empty;
        private DateTime? m_LC_DATE=null;

        public string LC_NO
        {
            get { return m_LC_NO; }
            set { this.m_LC_NO = value; }
        }
        public DateTime? LC_DATE
        {
            get { return m_LC_DATE; }
            set { this.m_LC_DATE = value; }
        }

        private bool m_IS_COSTING_FROM_LC = false;
        public bool IS_COSTING_FROM_LC
        {
            get
            {
                return m_IS_COSTING_FROM_LC;
            }
            set
            {
                this.m_IS_COSTING_FROM_LC = value;
            }
        }

        private bool m_IS_COSTING_COMPLETE = false;
        public bool IS_COSTING_COMPLETE
        {
            get { return m_IS_COSTING_COMPLETE; }
            set { m_IS_COSTING_COMPLETE = value; }
        }

        public int ITEM_COUNT { get; set; }
        public decimal IMPO_TOT_VALUE_USD { get; set; }

    }
}
