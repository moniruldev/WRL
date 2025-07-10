using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "OPENING_BALANCE")]
    public partial class dcOPENING_BALANCE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_SLNO = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_OPENING_QTY = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ITEM_OP_RATE = 0;
        private DateTime? m_BAL_AUDIT_DATE = null;
        private string m_ENTRY_BY = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
        private string m_EDIT_ALLOWED = string.Empty;
        private int m_STORE_ID = 0;
        private int m_ITEM_TYPE_ID = 0;
         
        private decimal m_WEIGHTED_AVERAGE_PRICE = 0;
        private decimal m_LAST_PURCHASE_PRICE = 0;
        private DateTime? m_LAST_PURCHASE_DATE = null;

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


        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public decimal ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "OPENING_QTY", Storage = "m_OPENING_QTY", DbType = "107")]
        public decimal OPENING_QTY
        {
            get { return this.m_OPENING_QTY; }
            set
            {
                this.m_OPENING_QTY = value;
                this.NotifyPropertyChanged("OPENING_QTY");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public decimal UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "ITEM_OP_RATE", Storage = "m_ITEM_OP_RATE", DbType = "107")]
        public decimal ITEM_OP_RATE
        {
            get { return this.m_ITEM_OP_RATE; }
            set
            {
                this.m_ITEM_OP_RATE = value;
                this.NotifyPropertyChanged("ITEM_OP_RATE");
            }
        }

        [DBColumn(Name = "BAL_AUDIT_DATE", Storage = "m_BAL_AUDIT_DATE", DbType = "106")]
        public DateTime? BAL_AUDIT_DATE
        {
            get { return this.m_BAL_AUDIT_DATE; }
            set
            {
                this.m_BAL_AUDIT_DATE = value;
                this.NotifyPropertyChanged("BAL_AUDIT_DATE");
            }
        }

        [DBColumn(Name = "ENTRY_BY", Storage = "m_ENTRY_BY", DbType = "126")]
        public string ENTRY_BY
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

        [DBColumn(Name = "EDIT_ALLOWED", Storage = "m_EDIT_ALLOWED", DbType = "126")]
        public string EDIT_ALLOWED
        {
            get { return this.m_EDIT_ALLOWED; }
            set
            {
                this.m_EDIT_ALLOWED = value;
                this.NotifyPropertyChanged("EDIT_ALLOWED");
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

        [DBColumn(Name = "WEIGHTED_AVERAGE_PRICE", Storage = "m_WEIGHTED_AVERAGE_PRICE", DbType = "107")]
        public decimal WEIGHTED_AVERAGE_PRICE
        {
            get { return this.m_WEIGHTED_AVERAGE_PRICE; }
            set
            {
                this.m_WEIGHTED_AVERAGE_PRICE = value;
                this.NotifyPropertyChanged("WEIGHTED_AVERAGE_PRICE");
            }
        }

        [DBColumn(Name = "LAST_PURCHASE_PRICE", Storage = "m_LAST_PURCHASE_PRICE", DbType = "107")]
        public decimal LAST_PURCHASE_PRICE
        {
            get { return this.m_LAST_PURCHASE_PRICE; }
            set
            {
                this.m_LAST_PURCHASE_PRICE = value;
                this.NotifyPropertyChanged("LAST_PURCHASE_PRICE");
            }
        }

        [DBColumn(Name = "LAST_PURCHASE_DATE", Storage = "m_LAST_PURCHASE_DATE", DbType = "106")]
        public DateTime? LAST_PURCHASE_DATE
        {
            get { return this.m_LAST_PURCHASE_DATE; }
            set
            {
                this.m_LAST_PURCHASE_DATE = value;
                this.NotifyPropertyChanged("LAST_PURCHASE_DATE");
            }
        }

        #endregion //properties
    }
    public partial class dcOPENING_BALANCE
    {
        public string ITEM_SHORT_DESC { get; set; }
        public string MSR_NAME { get; set; }
        public decimal CLOSING_QTY { get; set; }

    }
}
