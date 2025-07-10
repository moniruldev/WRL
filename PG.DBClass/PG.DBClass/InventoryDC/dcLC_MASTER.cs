using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LC_MASTER")]
    public partial class dcLC_MASTER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_LC_ID = 0;
        private string m_LC_NO = string.Empty;
        private DateTime? m_LC_DATE = null;
        private decimal m_LC_TYPE_ID = 0;
        private decimal m_MOP_ID = 0;
        private string m_LCA_NO = string.Empty;
        private DateTime? m_LCA_DATE = null;
        private decimal m_TRANSPORT_TYPE_ID = 0;
        private string m_PART_SHIP = string.Empty;
        private decimal m_CURRENCY_ID = 0;
        private decimal m_CONV_RATE = 0;
        private decimal m_SUP_ID = 0;
        private decimal m_CNF_ID = 0;
        private decimal m_COUNTRY_ID = 0;
        private decimal m_INS_COM_ID = 0;
        private string m_INS_COVER_NOTE = string.Empty;
        private DateTime? m_INS_COVER_DATE = null;
        private decimal m_BANK_ID = 0;
        private decimal m_BRANCH_ID = 0;
        private decimal m_ENTRY_BY = 0;
        private DateTime? m_ENTRY_DATE = null;
        private decimal m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_IS_CANCEL = string.Empty;
        private string m_CANCEL_REASON = string.Empty;
        private string m_REMARKS = string.Empty;
        private int m_COMPANY_ID = 0;
        private string m_IS_MULTIPLE_PI = string.Empty;

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


        [DBColumn(Name = "LC_ID", Storage = "m_LC_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int LC_ID
        {
            get { return this.m_LC_ID; }
            set
            {
                this.m_LC_ID = value;
                this.NotifyPropertyChanged("LC_ID");
            }
        }

        [DBColumn(Name = "LC_NO", Storage = "m_LC_NO", DbType = "126")]
        public string LC_NO
        {
            get { return this.m_LC_NO; }
            set
            {
                this.m_LC_NO = value;
                this.NotifyPropertyChanged("LC_NO");
            }
        }

        [DBColumn(Name = "LC_DATE", Storage = "m_LC_DATE", DbType = "106")]
        public DateTime? LC_DATE
        {
            get { return this.m_LC_DATE; }
            set
            {
                this.m_LC_DATE = value;
                this.NotifyPropertyChanged("LC_DATE");
            }
        }

        [DBColumn(Name = "LC_TYPE_ID", Storage = "m_LC_TYPE_ID", DbType = "107")]
        public decimal LC_TYPE_ID
        {
            get { return this.m_LC_TYPE_ID; }
            set
            {
                this.m_LC_TYPE_ID = value;
                this.NotifyPropertyChanged("LC_TYPE_ID");
            }
        }

        [DBColumn(Name = "MOP_ID", Storage = "m_MOP_ID", DbType = "107")]
        public decimal MOP_ID
        {
            get { return this.m_MOP_ID; }
            set
            {
                this.m_MOP_ID = value;
                this.NotifyPropertyChanged("MOP_ID");
            }
        }

        [DBColumn(Name = "LCA_NO", Storage = "m_LCA_NO", DbType = "126")]
        public string LCA_NO
        {
            get { return this.m_LCA_NO; }
            set
            {
                this.m_LCA_NO = value;
                this.NotifyPropertyChanged("LCA_NO");
            }
        }

        [DBColumn(Name = "LCA_DATE", Storage = "m_LCA_DATE", DbType = "106")]
        public DateTime? LCA_DATE
        {
            get { return this.m_LCA_DATE; }
            set
            {
                this.m_LCA_DATE = value;
                this.NotifyPropertyChanged("LCA_DATE");
            }
        }

        [DBColumn(Name = "TRANSPORT_TYPE_ID", Storage = "m_TRANSPORT_TYPE_ID", DbType = "107")]
        public decimal TRANSPORT_TYPE_ID
        {
            get { return this.m_TRANSPORT_TYPE_ID; }
            set
            {
                this.m_TRANSPORT_TYPE_ID = value;
                this.NotifyPropertyChanged("TRANSPORT_TYPE_ID");
            }
        }

        [DBColumn(Name = "PART_SHIP", Storage = "m_PART_SHIP", DbType = "126")]
        public string PART_SHIP
        {
            get { return this.m_PART_SHIP; }
            set
            {
                this.m_PART_SHIP = value;
                this.NotifyPropertyChanged("PART_SHIP");
            }
        }

        [DBColumn(Name = "CURRENCY_ID", Storage = "m_CURRENCY_ID", DbType = "107")]
        public decimal CURRENCY_ID
        {
            get { return this.m_CURRENCY_ID; }
            set
            {
                this.m_CURRENCY_ID = value;
                this.NotifyPropertyChanged("CURRENCY_ID");
            }
        }

        [DBColumn(Name = "CONV_RATE", Storage = "m_CONV_RATE", DbType = "107")]
        public decimal CONV_RATE
        {
            get { return this.m_CONV_RATE; }
            set
            {
                this.m_CONV_RATE = value;
                this.NotifyPropertyChanged("CONV_RATE");
            }
        }

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public decimal SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
            }
        }

        [DBColumn(Name = "CNF_ID", Storage = "m_CNF_ID", DbType = "107")]
        public decimal CNF_ID
        {
            get { return this.m_CNF_ID; }
            set
            {
                this.m_CNF_ID = value;
                this.NotifyPropertyChanged("CNF_ID");
            }
        }

        [DBColumn(Name = "COUNTRY_ID", Storage = "m_COUNTRY_ID", DbType = "107")]
        public decimal COUNTRY_ID
        {
            get { return this.m_COUNTRY_ID; }
            set
            {
                this.m_COUNTRY_ID = value;
                this.NotifyPropertyChanged("COUNTRY_ID");
            }
        }

        [DBColumn(Name = "INS_COM_ID", Storage = "m_INS_COM_ID", DbType = "107")]
        public decimal INS_COM_ID
        {
            get { return this.m_INS_COM_ID; }
            set
            {
                this.m_INS_COM_ID = value;
                this.NotifyPropertyChanged("INS_COM_ID");
            }
        }

        [DBColumn(Name = "INS_COVER_NOTE", Storage = "m_INS_COVER_NOTE", DbType = "126")]
        public string INS_COVER_NOTE
        {
            get { return this.m_INS_COVER_NOTE; }
            set
            {
                this.m_INS_COVER_NOTE = value;
                this.NotifyPropertyChanged("INS_COVER_NOTE");
            }
        }

        [DBColumn(Name = "INS_COVER_DATE", Storage = "m_INS_COVER_DATE", DbType = "106")]
        public DateTime? INS_COVER_DATE
        {
            get { return this.m_INS_COVER_DATE; }
            set
            {
                this.m_INS_COVER_DATE = value;
                this.NotifyPropertyChanged("INS_COVER_DATE");
            }
        }

        [DBColumn(Name = "BANK_ID", Storage = "m_BANK_ID", DbType = "107")]
        public decimal BANK_ID
        {
            get { return this.m_BANK_ID; }
            set
            {
                this.m_BANK_ID = value;
                this.NotifyPropertyChanged("BANK_ID");
            }
        }

        [DBColumn(Name = "BRANCH_ID", Storage = "m_BRANCH_ID", DbType = "107")]
        public decimal BRANCH_ID
        {
            get { return this.m_BRANCH_ID; }
            set
            {
                this.m_BRANCH_ID = value;
                this.NotifyPropertyChanged("BRANCH_ID");
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

        [DBColumn(Name = "CANCEL_REASON", Storage = "m_CANCEL_REASON", DbType = "126")]
        public string CANCEL_REASON
        {
            get { return this.m_CANCEL_REASON; }
            set
            {
                this.m_CANCEL_REASON = value;
                this.NotifyPropertyChanged("CANCEL_REASON");
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

        [DBColumn(Name = "IS_MULTIPLE_PI", Storage = "m_IS_MULTIPLE_PI", DbType = "126")]
        public string IS_MULTIPLE_PI
        {
            get { return this.m_IS_MULTIPLE_PI; }
            set
            {
                this.m_IS_MULTIPLE_PI = value;
                this.NotifyPropertyChanged("IS_MULTIPLE_PI");
            }
        }
        #endregion //properties

       
    }


    public partial class dcLC_MASTER
    {

        private List<dcLC_DETAILS> m_LCDetList = null;
        public List<dcLC_DETAILS> LCDetList
        {
            get { return m_LCDetList; }
            set { m_LCDetList = value; }
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

        private string m_bank_name = string.Empty;
        public string bank_name
        {
            get { return m_bank_name; }
            set { m_bank_name = value; }
        }
        private string m_sup_name = string.Empty;
        public string sup_name
        {
            get { return m_sup_name; }
            set { m_sup_name = value; }
        }


        private string m_lc_type_desc = string.Empty;
        public string lc_type_desc
        {
            get { return m_lc_type_desc; }
            set { m_lc_type_desc = value; }
        }

        private string m_MOD_PAY_DESC = string.Empty;
        public string MOD_PAY_DESC
        {
            get { return m_MOD_PAY_DESC; }
            set { m_MOD_PAY_DESC = value; }
        }
        private bool m_IS_PURCHASE_COMPLETE = false;
        public bool IS_PURCHASE_COMPLETE
        {
            get { return m_IS_PURCHASE_COMPLETE; }
            set { m_IS_PURCHASE_COMPLETE = value; }
        }


        private bool m_IS_INVOICE_FOUND = false;
        public bool IS_INVOICE_FOUND
        {
            get { return m_IS_INVOICE_FOUND; }
            set { m_IS_INVOICE_FOUND = value; }
        }

        private bool m_IS_COSTING_COMPLETE = false;
        public bool IS_COSTING_COMPLETE
        {
            get { return m_IS_COSTING_COMPLETE; }
            set { m_IS_COSTING_COMPLETE = value; }
        }

        

        private string m_INS_COM_NAME = string.Empty;
        public string INS_COM_NAME
        {
            get { return m_INS_COM_NAME; }
            set { m_INS_COM_NAME = value; }
        }


        private string m_CNF_NAME = string.Empty;
        public string CNF_NAME
        {
            get { return m_CNF_NAME; }
            set { m_CNF_NAME = value; }
        }


        private string m_MRR_STATUS = string.Empty;
        public string MRR_STATUS
        {
            get { return m_MRR_STATUS; }
            set { m_MRR_STATUS = value; }
        }
        private string m_IMP_PURCHASE_NO = string.Empty;
        public string IMP_PURCHASE_NO
        {
            get { return m_IMP_PURCHASE_NO; }
            set { m_IMP_PURCHASE_NO = value; }
        }
        public DateTime? IMP_PURCHASE_DATE { get; set; }
        public string MRR_NO { get; set; }
        public DateTime? MRR_DATE { get; set; }
        public string TYPE_QTY { get; set; }
        public int ITEM_COUNT { get; set; }
        public decimal LC_TOT_VALUE_USD { get; set; }
        
        
    }
}
