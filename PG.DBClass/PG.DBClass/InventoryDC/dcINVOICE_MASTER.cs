using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "INVOICE_MASTER")]
    public partial class dcINVOICE_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_INVOICE_ID = 0;
        private string m_INVOICE_NO = string.Empty;
        private DateTime? m_INVOICE_DATE = null;
        private DateTime? m_INVOICE_TIME = null;
        private decimal m_COMPANY_ID = 0;
        private decimal m_CUST_ID = 0;
        private string m_AUTH_STATUS = string.Empty;
        private decimal m_AUTH_BY = 0;
        private DateTime? m_AUTH_DATE = null;
        private string m_INVOICE_REMARKS = string.Empty;
        private decimal m_CREATE_BY =0;
        private DateTime? m_CREATE_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_IS_APPROVED = string.Empty;
        private decimal m_APPROVED_BY = 0;
        private DateTime? m_APPROVED_DATE = null;
        private string m_DC_NO = string.Empty;
        private string m_GP_NO = string.Empty;
        private DateTime? m_DC_DATE = null;
        private decimal m_DC_BY = 0;
        private decimal m_DC_PRINT_QTY = 0;
        private decimal m_GP_PRINT_QTY = 0;
        private int m_GP_TYPE_ID = 0;
        private DateTime? m_GP_DATE = null;
        private string m_TRANSPORT_DETAILS = string.Empty;
        private int m_BTY_TYPE_ID = 0;
        private string m_IS_REPAIR = string.Empty;
        

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


        [DBColumn(Name = "INVOICE_ID", Storage = "m_INVOICE_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int INVOICE_ID
        {
            get { return this.m_INVOICE_ID; }
            set
            {
                this.m_INVOICE_ID = value;
                this.NotifyPropertyChanged("INVOICE_ID");
            }
        }

        [DBColumn(Name = "INVOICE_NO", Storage = "m_INVOICE_NO", DbType = "126")]
        public string INVOICE_NO
        {
            get { return this.m_INVOICE_NO; }
            set
            {
                this.m_INVOICE_NO = value;
                this.NotifyPropertyChanged("INVOICE_NO");
            }
        }

        [DBColumn(Name = "INVOICE_DATE", Storage = "m_INVOICE_DATE", DbType = "106")]
        public DateTime? INVOICE_DATE
        {
            get { return this.m_INVOICE_DATE; }
            set
            {
                this.m_INVOICE_DATE = value;
                this.NotifyPropertyChanged("INVOICE_DATE");
            }
        }

        [DBColumn(Name = "INVOICE_TIME", Storage = "m_INVOICE_TIME", DbType = "106")]
        public DateTime? INVOICE_TIME
        {
            get { return this.m_INVOICE_TIME; }
            set
            {
                this.m_INVOICE_TIME = value;
                this.NotifyPropertyChanged("INVOICE_TIME");
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

        [DBColumn(Name = "INVOICE_REMARKS", Storage = "m_INVOICE_REMARKS", DbType = "126")]
        public string INVOICE_REMARKS
        {
            get { return this.m_INVOICE_REMARKS; }
            set
            {
                this.m_INVOICE_REMARKS = value;
                this.NotifyPropertyChanged("INVOICE_REMARKS");
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

        [DBColumn(Name = "DC_BY", Storage = "m_DC_BY", DbType = "107")]
        public decimal DC_BY
        {
            get { return this.m_DC_BY; }
            set
            {
                this.m_DC_BY = value;
                this.NotifyPropertyChanged("DC_BY");
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

//
        [DBColumn(Name = "GP_TYPE_ID", Storage = "m_GP_TYPE_ID", DbType = "107")]
        public int GP_TYPE_ID
        {
            get { return this.m_GP_TYPE_ID; }
            set
            {
                this.m_GP_TYPE_ID = value;
                this.NotifyPropertyChanged("GP_TYPE_ID");
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


        [DBColumn(Name = "BTY_TYPE_ID", Storage = "m_BTY_TYPE_ID", DbType = "107")]
        public int BTY_TYPE_ID
        {
            get { return this.m_BTY_TYPE_ID; }
            set
            {
                this.m_BTY_TYPE_ID = value;
                this.NotifyPropertyChanged("BTY_TYPE_ID");
            }

        }

        [DBColumn(Name = "IS_REPAIR", Storage = "m_IS_REPAIR", DbType = "126")]
        public string IS_REPAIR
        {
            get { return this.m_IS_REPAIR; }
            set
            {
                this.m_IS_REPAIR = value;
                this.NotifyPropertyChanged("IS_REPAIR");
            }

        }

        #endregion //properties
    }

    public partial class dcINVOICE_MASTER
    {

        private List<dcINVOICE_DETAILS> m_InvoiceDetList = null;
        public List<dcINVOICE_DETAILS> InvoiceDetList
        {
            get { return m_InvoiceDetList; }
            set { m_InvoiceDetList = value; }
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

        private string m_ITEMTYPEQTY =string.Empty;
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
