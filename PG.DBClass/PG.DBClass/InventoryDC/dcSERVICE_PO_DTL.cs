using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "SERVICE_PO_DTL")]
    public partial class dcSERVICE_PO_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PURCHASE_DET_ID = 0;
        private int m_SLNO = 0;
        private int m_PURCHASE_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_PURCHASE_QTY = 0;
        private int m_UOM_ID = 0;
        private decimal m_UNIT_RATE = 0;
        private string m_NOTE = string.Empty;
        private decimal m_DISCOUNTS_AMT = 0;
        private int m_SUP_ID = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_SUP_DC_NO = string.Empty;
        private int m_INDT_DET_ID = 0;
        private int m_INVOICE_DET_ID = 0;
        private decimal m_UNIT_PRICE = 0;
        private int m_ITEM_TYPE_ID = 0;
        private string m_ITEM_NAME = string.Empty;

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


        [DBColumn(Name = "PURCHASE_DET_ID", Storage = "m_PURCHASE_DET_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PURCHASE_DET_ID
        {
            get { return this.m_PURCHASE_DET_ID; }
            set
            {
                this.m_PURCHASE_DET_ID = value;
                this.NotifyPropertyChanged("PURCHASE_DET_ID");
            }
        }

        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107")]
        public int SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }

        [DBColumn(Name = "PURCHASE_ID", Storage = "m_PURCHASE_ID", DbType = "107")]
        public int PURCHASE_ID
        {
            get { return this.m_PURCHASE_ID; }
            set
            {
                this.m_PURCHASE_ID = value;
                this.NotifyPropertyChanged("PURCHASE_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_QTY", Storage = "m_PURCHASE_QTY", DbType = "107")]
        public decimal PURCHASE_QTY
        {
            get { return this.m_PURCHASE_QTY; }
            set
            {
                this.m_PURCHASE_QTY = value;
                this.NotifyPropertyChanged("PURCHASE_QTY");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
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

        [DBColumn(Name = "NOTE", Storage = "m_NOTE", DbType = "126")]
        public string NOTE
        {
            get { return this.m_NOTE; }
            set
            {
                this.m_NOTE = value;
                this.NotifyPropertyChanged("NOTE");
            }
        }

        [DBColumn(Name = "DISCOUNTS_AMT", Storage = "m_DISCOUNTS_AMT", DbType = "107")]
        public decimal DISCOUNTS_AMT
        {
            get { return this.m_DISCOUNTS_AMT; }
            set
            {
                this.m_DISCOUNTS_AMT = value;
                this.NotifyPropertyChanged("DISCOUNTS_AMT");
            }
        }

        [DBColumn(Name = "SUP_ID", Storage = "m_SUP_ID", DbType = "107")]
        public int SUP_ID
        {
            get { return this.m_SUP_ID; }
            set
            {
                this.m_SUP_ID = value;
                this.NotifyPropertyChanged("SUP_ID");
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

        [DBColumn(Name = "SUP_DC_NO", Storage = "m_SUP_DC_NO", DbType = "126")]
        public string SUP_DC_NO
        {
            get { return this.m_SUP_DC_NO; }
            set
            {
                this.m_SUP_DC_NO = value;
                this.NotifyPropertyChanged("SUP_DC_NO");
            }
        }

        [DBColumn(Name = "INDT_DET_ID", Storage = "m_INDT_DET_ID", DbType = "107")]
        public int INDT_DET_ID
        {
            get { return this.m_INDT_DET_ID; }
            set
            {
                this.m_INDT_DET_ID = value;
                this.NotifyPropertyChanged("INDT_DET_ID");
            }
        }

        [DBColumn(Name = "INVOICE_DET_ID", Storage = "m_INVOICE_DET_ID", DbType = "107")]
        public int INVOICE_DET_ID
        {
            get { return this.m_INVOICE_DET_ID; }
            set
            {
                this.m_INVOICE_DET_ID = value;
                this.NotifyPropertyChanged("INVOICE_DET_ID");
            }
        }

        [DBColumn(Name = "UNIT_PRICE", Storage = "m_UNIT_PRICE", DbType = "107")]
        public decimal UNIT_PRICE
        {
            get { return this.m_UNIT_PRICE; }
            set
            {
                this.m_UNIT_PRICE = value;
                this.NotifyPropertyChanged("UNIT_PRICE");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_ID", Storage = "m_ITEM_TYPE_ID", DbType = "107")]
        public int ITEM_TYPE_ID
        {
            get { return this.m_ITEM_TYPE_ID; }
            set
            {
                this.m_ITEM_TYPE_ID = value;
                this.NotifyPropertyChanged("ITEM_TYPE_ID");
            }
        }

        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "126")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
                this.NotifyPropertyChanged("ITEM_NAME");
            }
        }

        #endregion //properties
    }

     public partial class dcSERVICE_PO_DTL
     {
         public string UOM_NAME { get; set; }
         public decimal m_INDT_QTY = 0;
         public decimal INDT_QTY
         {
             get { return m_INDT_QTY; }
             set { m_INDT_QTY = value; }
         }

         public decimal m_ALREADRY_PURCHASE_QTY = 0;
         public decimal ALREADRY_PURCHASE_QTY
         {
             get { return m_ALREADRY_PURCHASE_QTY; }
             set { m_ALREADRY_PURCHASE_QTY = value; }
         }

         public decimal m_BALANCE_QTY = 0;
         public decimal BALANCE_QTY
         {
             get { return m_INDT_QTY - m_ALREADRY_PURCHASE_QTY; }
         }

         private string m_INDT_REMARKS = string.Empty;
         public string INDT_REMARKS
         {
             get { return m_INDT_REMARKS; }
             set { m_INDT_REMARKS = value; }
         }

         private string m_INDT_PRIORITY = string.Empty;
         public string INDT_PRIORITY
         {
             get { return m_INDT_PRIORITY; }
             set { m_INDT_PRIORITY = value; }
         }

         public string DEPARTMENT_NAME { get; set; }
         public string INDT_NO { get; set; }
         public DateTime? INDT_DATE { get; set; }
         public bool IsPurComplete { get; set; }
     }
}
