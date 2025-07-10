using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "CUSTOMER_OPENNING_BALANCE")]
    public partial class dcCUSTOMER_OPENNING_BALANCE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_COB_ID = 0;
        private decimal m_CUST_ID = 0;
        private DateTime? m_COB_DATE = null;
        private decimal m_COB_AMOUNT = 0;
        private decimal m_COB_APPROVED_BY = 0;
        private DateTime? m_COB_APPROVED_DATE = null;
        private decimal m_COB_AUDIT_BY = 0;
        private DateTime? m_COB_AUDIT_DATE = null;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_COB_REMARKS = string.Empty;

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


        [DBColumn(Name = "COB_ID", Storage = "m_COB_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int COB_ID
        {
            get { return this.m_COB_ID; }
            set
            {
                this.m_COB_ID = value;
                this.NotifyPropertyChanged("COB_ID");
            }
        }

        [DBColumn(Name = "CUST_ID", Storage = "m_CUST_ID", DbType = "107")]
        public decimal CUST_ID
        {
            get { return this.m_CUST_ID; }
            set
            {
                this.m_CUST_ID = value;
                this.NotifyPropertyChanged("CUST_ID");
            }
        }

        [DBColumn(Name = "COB_DATE", Storage = "m_COB_DATE", DbType = "106")]
        public DateTime? COB_DATE
        {
            get { return this.m_COB_DATE; }
            set
            {
                this.m_COB_DATE = value;
                this.NotifyPropertyChanged("COB_DATE");
            }
        }

        [DBColumn(Name = "COB_AMOUNT", Storage = "m_COB_AMOUNT", DbType = "107")]
        public decimal COB_AMOUNT
        {
            get { return this.m_COB_AMOUNT; }
            set
            {
                this.m_COB_AMOUNT = value;
                this.NotifyPropertyChanged("COB_AMOUNT");
            }
        }

        [DBColumn(Name = "COB_APPROVED_BY", Storage = "m_COB_APPROVED_BY", DbType = "107")]
        public decimal COB_APPROVED_BY
        {
            get { return this.m_COB_APPROVED_BY; }
            set
            {
                this.m_COB_APPROVED_BY = value;
                this.NotifyPropertyChanged("COB_APPROVED_BY");
            }
        }

        [DBColumn(Name = "COB_APPROVED_DATE", Storage = "m_COB_APPROVED_DATE", DbType = "106")]
        public DateTime? COB_APPROVED_DATE
        {
            get { return this.m_COB_APPROVED_DATE; }
            set
            {
                this.m_COB_APPROVED_DATE = value;
                this.NotifyPropertyChanged("COB_APPROVED_DATE");
            }
        }

        [DBColumn(Name = "COB_AUDIT_BY", Storage = "m_COB_AUDIT_BY", DbType = "107")]
        public decimal COB_AUDIT_BY
        {
            get { return this.m_COB_AUDIT_BY; }
            set
            {
                this.m_COB_AUDIT_BY = value;
                this.NotifyPropertyChanged("COB_AUDIT_BY");
            }
        }

        [DBColumn(Name = "COB_AUDIT_DATE", Storage = "m_COB_AUDIT_DATE", DbType = "106")]
        public DateTime? COB_AUDIT_DATE
        {
            get { return this.m_COB_AUDIT_DATE; }
            set
            {
                this.m_COB_AUDIT_DATE = value;
                this.NotifyPropertyChanged("COB_AUDIT_DATE");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public decimal UPDATE_BY
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

        [DBColumn(Name = "AUTH_BY", Storage = "m_AUTH_BY", DbType = "107")]
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

        [DBColumn(Name = "COB_REMARKS", Storage = "m_COB_REMARKS", DbType = "126")]
        public string COB_REMARKS
        {
            get { return this.m_COB_REMARKS; }
            set
            {
                this.m_COB_REMARKS = value;
                this.NotifyPropertyChanged("COB_REMARKS");
            }
        }

        #endregion //properties
    }


    public partial class dcCUSTOMER_OPENNING_BALANCE
    {

        //private List<dcINVOICE_DETAILS> m_InvoiceDetList = null;
        //public List<dcINVOICE_DETAILS> InvoiceDetList
        //{
        //    get { return m_InvoiceDetList; }
        //    set { m_InvoiceDetList = value; }
        //}


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
         public string APPROVED_BY_NAME { get; set; }
         public string AUDIT_BY_NAME { get; set; }
            

        public string IS_ROTARY { get; set; }
    }

}
