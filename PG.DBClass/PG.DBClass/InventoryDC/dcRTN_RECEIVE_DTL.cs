using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "RTN_RECEIVE_DTL")]
    public partial class dcRTN_RECEIVE_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_RCV_DET_ID = 0;
        private int m_RECEIVE_ID = 0;
        private int m_RCV_DET_SLNO = 0;
        private int m_ITEM_ID = 0;
        private decimal m_RECEIVE_QNTY = 0;
        private int m_UOM_ID = 0;
        private string m_RCV_DET_NOTE = string.Empty;
        private decimal m_UNIT_RATE = 0;
        private int m_ITEM_TYPE_ID = 0;
        private int m_RTN_DET_ID = 0;
        private decimal m_TOTAL_AMOUNT = 0;
        private decimal m_RTN_QTY = 0;

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


        [DBColumn(Name = "RCV_DET_ID", Storage = "m_RCV_DET_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int RCV_DET_ID
        {
            get { return this.m_RCV_DET_ID; }
            set
            {
                this.m_RCV_DET_ID = value;
                this.NotifyPropertyChanged("RCV_DET_ID");
            }
        }

        [DBColumn(Name = "RECEIVE_ID", Storage = "m_RECEIVE_ID", DbType = "107")]
        public int RECEIVE_ID
        {
            get { return this.m_RECEIVE_ID; }
            set
            {
                this.m_RECEIVE_ID = value;
                this.NotifyPropertyChanged("RECEIVE_ID");
            }
        }

        [DBColumn(Name = "RCV_DET_SLNO", Storage = "m_RCV_DET_SLNO", DbType = "107")]
        public int RCV_DET_SLNO
        {
            get { return this.m_RCV_DET_SLNO; }
            set
            {
                this.m_RCV_DET_SLNO = value;
                this.NotifyPropertyChanged("RCV_DET_SLNO");
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

        [DBColumn(Name = "RECEIVE_QNTY", Storage = "m_RECEIVE_QNTY", DbType = "107")]
        public decimal RECEIVE_QNTY
        {
            get { return this.m_RECEIVE_QNTY; }
            set
            {
                this.m_RECEIVE_QNTY = value;
                this.NotifyPropertyChanged("RECEIVE_QNTY");
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

        [DBColumn(Name = "RCV_DET_NOTE", Storage = "m_RCV_DET_NOTE", DbType = "126")]
        public string RCV_DET_NOTE
        {
            get { return this.m_RCV_DET_NOTE; }
            set
            {
                this.m_RCV_DET_NOTE = value;
                this.NotifyPropertyChanged("RCV_DET_NOTE");
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

        [DBColumn(Name = "RTN_DET_ID", Storage = "m_RTN_DET_ID", DbType = "107")]
        public int RTN_DET_ID
        {
            get { return this.m_RTN_DET_ID; }
            set
            {
                this.m_RTN_DET_ID = value;
                this.NotifyPropertyChanged("RTN_DET_ID");
            }
        }

        [DBColumn(Name = "TOTAL_AMOUNT", Storage = "m_TOTAL_AMOUNT", DbType = "107")]
        public decimal TOTAL_AMOUNT
        {
            get { return this.m_TOTAL_AMOUNT; }
            set
            {
                this.m_TOTAL_AMOUNT = value;
                this.NotifyPropertyChanged("TOTAL_AMOUNT");
            }
        }

        [DBColumn(Name = "RTN_QTY", Storage = "m_RTN_QTY", DbType = "107")]
        public decimal RTN_QTY
        {
            get { return this.m_RTN_QTY; }
            set
            {
                this.m_RTN_QTY = value;
                this.NotifyPropertyChanged("RTN_QTY");
            }
        }

        #endregion //properties
    }

    public partial class dcRTN_RECEIVE_DTL
    {
        public string ITEM_GROUP_NAME { get; set; }
        public int ITEM_GROUP_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string ITEM_TYPE_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set
            {
                this.m_CLOSING_QTY = value;
            }
        }
        public decimal QC_RTN_QTY { get; set; }
        public decimal PURCHASE_RATE { get; set; }
        public decimal PURCHASE_TOTAL_PRICE { get; set; }
        public decimal PURCHASE_QTY { get; set; }
        public int PURCHASE_DET_ID { get; set; }
        public int PURCHASE_UOM_ID { get; set; }
        
        
    }
}
