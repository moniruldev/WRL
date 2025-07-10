using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "DC_DETAILS")]
    public partial class dcDC_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_DC_DET_ID = 0;
        private int m_DC_ID = 0;
        private decimal m_DC_DET_SLNO = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_UOM_ID = 0;
        private decimal m_ITEM_RATE = 0;
        private string m_DC_DET_REMARKS = string.Empty;
        private decimal m_TOTAL_COST = 0;

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


        [DBColumn(Name = "DC_DET_ID", Storage = "m_DC_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public decimal DC_DET_ID
        {
            get { return this.m_DC_DET_ID; }
            set
            {
                this.m_DC_DET_ID = value;
                this.NotifyPropertyChanged("DC_DET_ID");
            }
        }

        [DBColumn(Name = "DC_ID", Storage = "m_DC_ID", DbType = "107")]
        public int DC_ID
        {
            get { return this.m_DC_ID; }
            set
            {
                this.m_DC_ID = value;
                this.NotifyPropertyChanged("DC_ID");
            }
        }

        [DBColumn(Name = "DC_DET_SLNO", Storage = "m_DC_DET_SLNO", DbType = "107")]
        public decimal DC_DET_SLNO
        {
            get { return this.m_DC_DET_SLNO; }
            set
            {
                this.m_DC_DET_SLNO = value;
                this.NotifyPropertyChanged("DC_DET_SLNO");
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

        [DBColumn(Name = "ITEM_QTY", Storage = "m_ITEM_QTY", DbType = "107")]
        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;
                this.NotifyPropertyChanged("ITEM_QTY");
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

        [DBColumn(Name = "ITEM_RATE", Storage = "m_ITEM_RATE", DbType = "107")]
        public decimal ITEM_RATE
        {
            get { return this.m_ITEM_RATE; }
            set
            {
                this.m_ITEM_RATE = value;
                this.NotifyPropertyChanged("ITEM_RATE");
            }
        }

        [DBColumn(Name = "DC_DET_REMARKS", Storage = "m_DC_DET_REMARKS", DbType = "126")]
        public string DC_DET_REMARKS
        {
            get { return this.m_DC_DET_REMARKS; }
            set
            {
                this.m_DC_DET_REMARKS = value;
                this.NotifyPropertyChanged("DC_DET_REMARKS");
            }
        }

        [DBColumn(Name = "TOTAL_COST", Storage = "m_TOTAL_COST", DbType = "107")]
        public decimal TOTAL_COST
        {
            get { return this.m_TOTAL_COST; }
            set
            {
                this.m_TOTAL_COST = value;
                this.NotifyPropertyChanged("TOTAL_COST");
            }
        }

        #endregion //properties
    }

    public partial class dcDC_DETAILS
    {
        private int m_ITEM_GROUP_ID = 0;
        public int ITEM_GROUP_ID
        {
            get { return m_ITEM_GROUP_ID; }
            set { m_ITEM_GROUP_ID = value; }
        }

        private string m_ITEM_GROUP_DESC = string.Empty;
        public string ITEM_GROUP_DESC
        {
            get { return m_ITEM_GROUP_DESC; }
            set { m_ITEM_GROUP_DESC = value; }
        }

        private int m_SLNo = 0;
        public int SLNo
        {
            get { return m_SLNo; }
            set { m_SLNo = value; }
        }

        private string m_ITEM_NAME = string.Empty;
        public string ITEM_NAME
        {
            get { return m_ITEM_NAME; }
            set { m_ITEM_NAME = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {
            get { return m_UOM_NAME; }
            set { m_UOM_NAME = value; }
        }

        private string m_UOM_CODE = string.Empty;
        public string UOM_CODE
        {
            get { return m_UOM_CODE; }
            set { m_UOM_CODE = value; }
        }

        private int m_total_amount = 0;
        public int total_amount
        {
            get { return m_total_amount; }
            set { m_total_amount = value; }
        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {

            get { return m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }

        private int m_ISS_RCV_DET_SLNO = 0;
        public int ISS_RCV_DET_SLNO
        {
            get { return m_ISS_RCV_DET_SLNO; }
            set { m_ISS_RCV_DET_SLNO = value; }
        }

        private int m_RCV_QNTY = 0;
        public int RCV_QNTY
        {
            get { return m_RCV_QNTY; }
            set { m_RCV_QNTY = value; }
        }

        private int m_INVOICE_ID = 0;
        public int INVOICE_ID
        {
            get { return m_INVOICE_ID; }
            set { m_INVOICE_ID = value; }
        }

        private decimal m_REMAIN_QTY = 0;
        public decimal REMAIN_QTY
        {
            get { return m_REMAIN_QTY; }
            set { m_REMAIN_QTY = value; }
        }

        
        
        private decimal m_DC_QTY = 0;
        public decimal DC_QTY
        {
            get { return m_DC_QTY; }
            set { m_DC_QTY = value; }
        }

        private decimal m_INV_QTY = 0;
        public decimal INV_QTY
        {
            get { return m_INV_QTY; }
            set { m_INV_QTY = value; }
        }

        private decimal m_ALREADY_ISSUED_QTY = 0;
        public decimal ALREADY_ISSUED_QTY
        {
            get { return m_ALREADY_ISSUED_QTY; }
            set { m_ALREADY_ISSUED_QTY = value; }
        }

        private string m_INV_DET_REMARKS = string.Empty;
        public string INV_DET_REMARKS
        {
            get { return m_INV_DET_REMARKS; }
            set { m_INV_DET_REMARKS = value; }
        }

        private decimal m_RTN_QNTY = 0;
         public decimal RTN_QNTY
        {
            get { return m_RTN_QNTY; }
            set { m_RTN_QNTY = value; }
        }

         private string m_RTN_DET_NOTE = string.Empty;
         public string RTN_DET_NOTE
        {
            get { return m_RTN_DET_NOTE; }
            set { m_RTN_DET_NOTE = value; }
        }


         private decimal m_ALREADY_RNT_QNTY = 0;
         public decimal ALREADY_RNT_QNTY
        {
            get { return m_ALREADY_RNT_QNTY; }
            set { m_ALREADY_RNT_QNTY = value; }
        }

         public decimal WEIGHTED_AVERAGE_PRICE { get; set; } 
        
        
        
    }
}
