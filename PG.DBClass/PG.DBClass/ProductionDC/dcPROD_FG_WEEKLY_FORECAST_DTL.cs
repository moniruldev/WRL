using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [Serializable]
    [DBTable(Name = "PROD_FG_WEEKLY_FORECAST_DTL")]
    public partial class dcPROD_FG_WEEKLY_FORECAST_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_FC_DET_ID = 0;
        private int m_WK_FC_MST_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_DEALER_RATE = 0;
        private decimal m_TRANSFER_RATE = 0;
        private decimal m_WK1_ITEM_QTY = 0;
        private decimal m_WK2_ITEM_QTY = 0;
        private decimal m_WK3_ITEM_QTY = 0;
        private decimal m_WK4_ITEM_QTY = 0;
        private decimal m_TOTAL_MONTH_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_REMARKS = string.Empty;

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


        [DBColumn(Name = "FC_DET_ID", Storage = "m_FC_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int FC_DET_ID
        {
            get { return this.m_FC_DET_ID; }
            set
            {
                this.m_FC_DET_ID = value;
                this.NotifyPropertyChanged("FC_DET_ID");
            }
        }

        [DBColumn(Name = "WK_FC_MST_ID", Storage = "m_WK_FC_MST_ID", DbType = "107")]
        public int WK_FC_MST_ID
        {
            get { return this.m_WK_FC_MST_ID; }
            set
            {
                this.m_WK_FC_MST_ID = value;
                this.NotifyPropertyChanged("WK_FC_MST_ID");
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

        [DBColumn(Name = "DEALER_RATE", Storage = "m_DEALER_RATE", DbType = "107")]
        public decimal DEALER_RATE
        {
            get { return this.m_DEALER_RATE; }
            set
            {
                this.m_DEALER_RATE = value;
                this.NotifyPropertyChanged("DEALER_RATE");
            }
        }

        [DBColumn(Name = "TRANSFER_RATE", Storage = "m_TRANSFER_RATE", DbType = "107")]
        public decimal TRANSFER_RATE
        {
            get { return this.m_TRANSFER_RATE; }
            set
            {
                this.m_TRANSFER_RATE = value;
                this.NotifyPropertyChanged("TRANSFER_RATE");
            }
        }

        [DBColumn(Name = "WK1_ITEM_QTY", Storage = "m_WK1_ITEM_QTY", DbType = "107")]
        public decimal WK1_ITEM_QTY
        {
            get { return this.m_WK1_ITEM_QTY; }
            set
            {
                this.m_WK1_ITEM_QTY = value;
                this.NotifyPropertyChanged("WK1_ITEM_QTY");
            }
        }

        [DBColumn(Name = "WK2_ITEM_QTY", Storage = "m_WK2_ITEM_QTY", DbType = "107")]
        public decimal WK2_ITEM_QTY
        {
            get { return this.m_WK2_ITEM_QTY; }
            set
            {
                this.m_WK2_ITEM_QTY = value;
                this.NotifyPropertyChanged("WK2_ITEM_QTY");
            }
        }

        [DBColumn(Name = "WK3_ITEM_QTY", Storage = "m_WK3_ITEM_QTY", DbType = "107")]
        public decimal WK3_ITEM_QTY
        {
            get { return this.m_WK3_ITEM_QTY; }
            set
            {
                this.m_WK3_ITEM_QTY = value;
                this.NotifyPropertyChanged("WK3_ITEM_QTY");
            }
        }

        [DBColumn(Name = "WK4_ITEM_QTY", Storage = "m_WK4_ITEM_QTY", DbType = "107")]
        public decimal WK4_ITEM_QTY
        {
            get { return this.m_WK4_ITEM_QTY; }
            set
            {
                this.m_WK4_ITEM_QTY = value;
                this.NotifyPropertyChanged("WK4_ITEM_QTY");
            }
        }

        [DBColumn(Name = "TOTAL_MONTH_QTY", Storage = "m_TOTAL_MONTH_QTY", DbType = "107")]
        public decimal TOTAL_MONTH_QTY
        {
            get { return this.m_TOTAL_MONTH_QTY; }
            set
            {
                this.m_TOTAL_MONTH_QTY = value;
                this.NotifyPropertyChanged("TOTAL_MONTH_QTY");
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

        #endregion //properties
    }

    public partial class dcPROD_FG_WEEKLY_FORECAST_DTL 
            {

            public string FC_NO { get; set; }
            public string FC_DESC { get; set; }

            public string BOM_NO { get; set; }
            public int BOM_ID{ get; set; }
            public int BOM_ITEM_ID    { get; set; }
            public string ITEM_NAME { get; set; }
            public string UOM_NAME { get; set; }
            public int ITEM_FC_QTY { get; set; }

            }
}
