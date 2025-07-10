using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "DC_MASTER")]
    public partial class dcDC_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_DC_ID = 0;
        private string m_DC_NO = string.Empty;
        private int m_INVOICE_ID = 0;
        private DateTime? m_DC_DATE = null;
        private string m_GP_NO = string.Empty;
        private DateTime? m_GP_DATE = null;
        private decimal m_GP_TYPE_ID = 0;
        private string m_TRANSPORT_DETAILS = string.Empty;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_DC_REMARKS = string.Empty;
        private decimal m_DC_PRINT_QTY = 0;
        private decimal m_GP_PRINT_QTY = 0;
        private string m_OS_TYPE = string.Empty;
        private decimal m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_SALES_INVOICE_NO = string.Empty;
        private decimal m_CASH_AMOUNT = 0;
        

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


        [DBColumn(Name = "DC_ID", Storage = "m_DC_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal DC_ID
        {
            get { return this.m_DC_ID; }
            set
            {
                this.m_DC_ID = value;
                this.NotifyPropertyChanged("DC_ID");
            }
        }

        [DBColumn(Name = "DC_NO", Storage = "m_DC_NO", DbType = "126")]
        public string DC_NO
        {
            get { return this.m_DC_NO; }
            set
            {
                this.m_DC_NO = value;
                this.NotifyPropertyChanged("DC_NO");
            }
        }

        [DBColumn(Name = "INVOICE_ID", Storage = "m_INVOICE_ID", DbType = "107")]
        public int INVOICE_ID
        {
            get { return this.m_INVOICE_ID; }
            set
            {
                this.m_INVOICE_ID = value;
                this.NotifyPropertyChanged("INVOICE_ID");
            }
        }

        [DBColumn(Name = "DC_DATE", Storage = "m_DC_DATE", DbType = "106")]
        public DateTime? DC_DATE
        {
            get { return this.m_DC_DATE; }
            set
            {
                this.m_DC_DATE = value;
                this.NotifyPropertyChanged("DC_DATE");
            }
        }

        [DBColumn(Name = "GP_NO", Storage = "m_GP_NO", DbType = "126")]
        public string GP_NO
        {
            get { return this.m_GP_NO; }
            set
            {
                this.m_GP_NO = value;
                this.NotifyPropertyChanged("GP_NO");
            }
        }

        [DBColumn(Name = "GP_DATE", Storage = "m_GP_DATE", DbType = "106")]
        public DateTime? GP_DATE
        {
            get { return this.m_GP_DATE; }
            set
            {
                this.m_GP_DATE = value;
                this.NotifyPropertyChanged("GP_DATE");
            }
        }

        [DBColumn(Name = "GP_TYPE_ID", Storage = "m_GP_TYPE_ID", DbType = "107")]
        public decimal GP_TYPE_ID
        {
            get { return this.m_GP_TYPE_ID; }
            set
            {
                this.m_GP_TYPE_ID = value;
                this.NotifyPropertyChanged("GP_TYPE_ID");
            }
        }

        [DBColumn(Name = "TRANSPORT_DETAILS", Storage = "m_TRANSPORT_DETAILS", DbType = "126")]
        public string TRANSPORT_DETAILS
        {
            get { return this.m_TRANSPORT_DETAILS; }
            set
            {
                this.m_TRANSPORT_DETAILS = value;
                this.NotifyPropertyChanged("TRANSPORT_DETAILS");
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

        [DBColumn(Name = "DC_REMARKS", Storage = "m_DC_REMARKS", DbType = "126")]
        public string DC_REMARKS
        {
            get { return this.m_DC_REMARKS; }
            set
            {
                this.m_DC_REMARKS = value;
                this.NotifyPropertyChanged("DC_REMARKS");
            }
        }

        [DBColumn(Name = "DC_PRINT_QTY", Storage = "m_DC_PRINT_QTY", DbType = "107")]
        public decimal DC_PRINT_QTY
        {
            get { return this.m_DC_PRINT_QTY; }
            set
            {
                this.m_DC_PRINT_QTY = value;
                this.NotifyPropertyChanged("DC_PRINT_QTY");
            }
        }

        [DBColumn(Name = "GP_PRINT_QTY", Storage = "m_GP_PRINT_QTY", DbType = "107")]
        public decimal GP_PRINT_QTY
        {
            get { return this.m_GP_PRINT_QTY; }
            set
            {
                this.m_GP_PRINT_QTY = value;
                this.NotifyPropertyChanged("GP_PRINT_QTY");
            }
        }

        [DBColumn(Name = "OS_TYPE", Storage = "m_OS_TYPE", DbType = "126")]
        public string OS_TYPE
        {
            get { return this.m_OS_TYPE; }
            set
            {
                this.m_OS_TYPE = value;
                this.NotifyPropertyChanged("OS_TYPE");
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

        [DBColumn(Name = "SALES_INVOICE_NO", Storage = "m_SALES_INVOICE_NO", DbType = "126")]
        public string SALES_INVOICE_NO
        {
            get { return this.m_SALES_INVOICE_NO; }
            set
            {
                this.m_SALES_INVOICE_NO = value;
                this.NotifyPropertyChanged("SALES_INVOICE_NO");
            }
        }


        [DBColumn(Name = "CASH_AMOUNT", Storage = "m_CASH_AMOUNT", DbType = "107")]
        public decimal CASH_AMOUNT
        {
            get { return this.m_CASH_AMOUNT; }
            set
            {
                this.m_CASH_AMOUNT = value;
                this.NotifyPropertyChanged("CASH_AMOUNT");
            }
        }



        #endregion //properties
    }


    public partial class dcDC_MASTER
    {

        private List<dcDC_DETAILS> m_DCDetList = null;
        public List<dcDC_DETAILS> DCDetList
        {
            get { return m_DCDetList; }
            set { m_DCDetList = value; }
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

        private int m_CUST_ID = 0;
        public int CUST_ID
        {
            get { return m_CUST_ID; }
            set { m_CUST_ID = value; }
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
        public string ITEM_CODE { get; set; }
    }
}
