using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "SALES_RETURN_MASTER")]
    public partial class dcSALES_RETURN_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RTN_ID = 0;
        private string m_RTN_NO = string.Empty;
        private DateTime? m_RTN_DATE = null;
        private decimal m_DC_ID = 0;
        private decimal m_CUST_ID = 0;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_REMARKS = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;

         private string m_IS_CANCEL="N";    


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


        [DBColumn(Name = "RTN_ID", Storage = "m_RTN_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RTN_ID
        {
            get { return this.m_RTN_ID; }
            set
            {
                this.m_RTN_ID = value;
                this.NotifyPropertyChanged("RTN_ID");
            }
        }

        [DBColumn(Name = "RTN_NO", Storage = "m_RTN_NO", DbType = "126")]
        public string RTN_NO
        {
            get { return this.m_RTN_NO; }
            set
            {
                this.m_RTN_NO = value;
                this.NotifyPropertyChanged("RTN_NO");
            }
        }

        [DBColumn(Name = "RTN_DATE", Storage = "m_RTN_DATE", DbType = "106")]
        public DateTime? RTN_DATE
        {
            get { return this.m_RTN_DATE; }
            set
            {
                this.m_RTN_DATE = value;
                this.NotifyPropertyChanged("RTN_DATE");
            }
        }

        [DBColumn(Name = "DC_ID", Storage = "m_DC_ID", DbType = "107")]
        public decimal DC_ID
        {
            get { return this.m_DC_ID; }
            set
            {
                this.m_DC_ID = value;
                this.NotifyPropertyChanged("DC_ID");
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


        [DBColumn(Name = "IS_CANCEL", Storage = "m_IS_CANCEL", DbType = "126")]
        public string IS_CANCEL
        {
            get { return this.m_IS_CANCEL; }
            set
            {
                this.m_IS_CANCEL = value;
                this.NotifyPropertyChanged("IS_CANCEL");
            }
        }


        #endregion //properties
    }


    public partial class dcSALES_RETURN_MASTER
    {

        private List<dcSALES_RETURN_DETAILS> m_RTNDetList = null;
        public List<dcSALES_RETURN_DETAILS> RTNDetList
        {
            get { return m_RTNDetList; }
            set { m_RTNDetList = value; }
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

        

        private string m_INVOICE_NO = string.Empty;
        public string INVOICE_NO
        {
            get { return m_INVOICE_NO; }
            set { m_INVOICE_NO = value; }
        }

        private DateTime? m_INVOICE_DATE = null;
        public DateTime? INVOICE_DATE
        {
            get { return m_INVOICE_DATE; }
            set { m_INVOICE_DATE = value; }
        }

        private DateTime? m_INVOICE_TIME = null;
        public DateTime? INVOICE_TIME
        {
            get { return m_INVOICE_TIME; }
            set { m_INVOICE_TIME = value; }
        }

        private string m_INVOICE_REMARKS = string.Empty;
        public string INVOICE_REMARKS
        {
            get { return m_INVOICE_REMARKS; }
            set { m_INVOICE_REMARKS = value; }
        }



        public decimal ITEM_QNTY { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string CUST_PHONE { get; set; }
        public string INVOICE_STATUS { get; set; }
        public decimal DO_QTY { get; set; }
        public decimal DC_QTY { get; set; }
        public string UOM_CODE { get; set; }
    }
}
