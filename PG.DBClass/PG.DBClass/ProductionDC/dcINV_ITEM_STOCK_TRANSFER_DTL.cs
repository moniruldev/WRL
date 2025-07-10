using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "INV_ITEM_STOCK_TRANSFER_DTL")]
    public partial class dcINV_ITEM_STOCK_TRANSFER_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int  m_STOCK_TRANSFER_DTL_ID = 0;
        private int m_STOCK_TRANSFER_ID = 0;
        private int m_STOCK_TRANSFER_ITEM_ID = 0;
        private decimal m_TRANSFER_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_REMARKS = string.Empty;
        private int m_STOCK_TRANSFER_SLNO = 0;
        private decimal m_STOCK_QTY = 0;
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


        [DBColumn(Name = "STOCK_TRANSFER_DTL_ID", Storage = "m_STOCK_TRANSFER_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int STOCK_TRANSFER_DTL_ID
        {
            get { return this.m_STOCK_TRANSFER_DTL_ID; }
            set
            {
                this.m_STOCK_TRANSFER_DTL_ID = value;
                this.NotifyPropertyChanged("STOCK_TRANSFER_DTL_ID");
            }
        }

        [DBColumn(Name = "STOCK_TRANSFER_ID", Storage = "m_STOCK_TRANSFER_ID", DbType = "107")]
        public int STOCK_TRANSFER_ID
        {
            get { return this.m_STOCK_TRANSFER_ID; }
            set
            {
                this.m_STOCK_TRANSFER_ID = value;
                this.NotifyPropertyChanged("STOCK_TRANSFER_ID");
            }
        }

        [DBColumn(Name = "STOCK_TRANSFER_ITEM_ID", Storage = "m_STOCK_TRANSFER_ITEM_ID", DbType = "107")]
        public int STOCK_TRANSFER_ITEM_ID
        {
            get { return this.m_STOCK_TRANSFER_ITEM_ID; }
            set
            {
                this.m_STOCK_TRANSFER_ITEM_ID = value;
                this.NotifyPropertyChanged("STOCK_TRANSFER_ITEM_ID");
            }
        }

        [DBColumn(Name = "TRANSFER_QTY", Storage = "m_TRANSFER_QTY", DbType = "107")]
        public decimal TRANSFER_QTY
        {
            get { return this.m_TRANSFER_QTY; }
            set
            {
                this.m_TRANSFER_QTY = value;
                this.NotifyPropertyChanged("TRANSFER_QTY");
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

        [DBColumn(Name = "STOCK_TRANSFER_SLNO", Storage = "m_STOCK_TRANSFER_SLNO", DbType = "107")]
        public int STOCK_TRANSFER_SLNO
        {
            get { return this.m_STOCK_TRANSFER_SLNO; }
            set
            {
                this.m_STOCK_TRANSFER_SLNO = value;
                this.NotifyPropertyChanged("STOCK_TRANSFER_SLNO");
            }
        }

         [DBColumn(Name = "STOCK_QTY", Storage = "m_STOCK_QTY", DbType = "107")]
        public decimal STOCK_QTY
        {
            get { return this.m_STOCK_QTY; }
            set
            {
                this.m_STOCK_QTY = value;
                this.NotifyPropertyChanged("STOCK_QTY");
            }
        }
        

        #endregion //properties
    }

    public partial class dcINV_ITEM_STOCK_TRANSFER_DTL
    {
        // STOCK_TRANSFER_ITEM_NAME

        private string m_STOCK_TRANSFER_ITEM_NAME = string.Empty;
        public string STOCK_TRANSFER_ITEM_NAME
        {
            get { return m_STOCK_TRANSFER_ITEM_NAME; }
            set { m_STOCK_TRANSFER_ITEM_NAME = value; }
        }

        private string m_UOM_NAME = string.Empty;
        public string UOM_NAME
        {
            get { return m_UOM_NAME; }
            set { m_UOM_NAME = value; }
        }



         

        //private List<dcINV_ITEM_STOCK_TRANSFER_DTL> m_stockTransferDetList = null;
        //public List<dcINV_ITEM_STOCK_TRANSFER_DTL> stockTransferDetList
        //{
        //    get { return m_stockTransferDetList; }
        //    set { m_stockTransferDetList = value; }
        //}
    }
    
}
