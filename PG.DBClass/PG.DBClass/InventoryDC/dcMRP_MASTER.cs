using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "MRP_MASTER")]
    public partial class dcMRP_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_MRP_ID = 0;
        private string m_MRP_CODE = string.Empty;
        private DateTime? m_MRP_DATE = null;

        private DateTime? m_PO_DATE_CAL = null;
        private decimal m_COMPANY_ID = 0;
        private string m_MRP_NOTE = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private string m_IS_APPROVED = string.Empty;
        private decimal m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;

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


        [DBColumn(Name = "MRP_ID", Storage = "m_MRP_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int MRP_ID
        {
            get { return this.m_MRP_ID; }
            set
            {
                this.m_MRP_ID = value;
                this.NotifyPropertyChanged("MRP_ID");
            }
        }

        [DBColumn(Name = "MRP_CODE", Storage = "m_MRP_CODE", DbType = "126")]
        public string MRP_CODE
        {
            get { return this.m_MRP_CODE; }
            set
            {
                this.m_MRP_CODE = value;
                this.NotifyPropertyChanged("MRP_CODE");
            }
        }

        [DBColumn(Name = "MRP_DATE", Storage = "m_MRP_DATE", DbType = "106")]
        public DateTime? MRP_DATE
        {
            get { return this.m_MRP_DATE; }
            set
            {
                this.m_MRP_DATE = value;
                this.NotifyPropertyChanged("MRP_DATE");
            }
        }

        [DBColumn(Name = "PO_DATE_CAL", Storage = "m_PO_DATE_CAL", DbType = "106")]
        public DateTime? PO_DATE_CAL
        {
            get { return this.m_PO_DATE_CAL; }
            set
            {
                this.m_PO_DATE_CAL = value;
                this.NotifyPropertyChanged("PO_DATE_CAL");
            }
        }

        [DBColumn(Name = "COMPANY_ID", Storage = "m_COMPANY_ID", DbType = "107")]
        public decimal COMPANY_ID
        {
            get { return this.m_COMPANY_ID; }
            set
            {
                this.m_COMPANY_ID = value;
                this.NotifyPropertyChanged("COMPANY_ID");
            }
        }

        [DBColumn(Name = "MRP_NOTE", Storage = "m_MRP_NOTE", DbType = "126")]
        public string MRP_NOTE
        {
            get { return this.m_MRP_NOTE; }
            set
            {
                this.m_MRP_NOTE = value;
                this.NotifyPropertyChanged("MRP_NOTE");
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

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "107")]
        public decimal ENTRY_BY
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

        [DBColumn(Name = "APPROVED_BY", Storage = "m_APPROVED_BY", DbType = "107")]
        public decimal APPROVED_BY
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

        #endregion //properties
    }


    public partial class dcMRP_MASTER
    {

        private List<dcMRP_DETAILS> m_MRPDetList = null;
        public List<dcMRP_DETAILS> MRPDetList
        {
            get { return m_MRPDetList; }
            set { m_MRPDetList = value; }
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
    }
}
