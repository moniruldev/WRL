using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "IB_CUTTING_DTL")]
    public partial class dcIB_CUTTING_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members
        private Int64 m_CUTTING_DTL_ID = 0;
        private Int64 m_CUTTING_MST_ID = 0;
        private Int64 m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private int m_UOM_ID = 0;
        private string m_REMARKS = string.Empty;
        private int m_SLNO = 0;
        private decimal m_STOCK_QTY = 0;
        private DateTime? m_CUTTING_DATE = null;
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


        [DBColumn(Name = "CUTTING_DTL_ID", Storage = "m_CUTTING_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 CUTTING_DTL_ID
        {
            get { return this.m_CUTTING_DTL_ID; }
            set
            {
                this.m_CUTTING_DTL_ID = value;
                this.NotifyPropertyChanged("CUTTING_DTL_ID");
            }
        }

        [DBColumn(Name = "CUTTING_MST_ID", Storage = "m_CUTTING_MST_ID", DbType = "107")]
        public Int64 CUTTING_MST_ID
        {
            get { return this.m_CUTTING_MST_ID; }
            set
            {
                this.m_CUTTING_MST_ID = value;
                this.NotifyPropertyChanged("CUTTING_MST_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public Int64 ITEM_ID
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

         [DBColumn(Name = "CUTTING_DATE", Storage = "m_CUTTING_DATE", DbType = "106")]
        public DateTime? CUTTING_DATE
        {
            get { return this.m_CUTTING_DATE; }
            set
            {
                this.m_CUTTING_DATE = value;
                this.NotifyPropertyChanged("CUTTING_DATE");
            }
        }

        #endregion //properties
    }

    public partial class dcIB_CUTTING_DTL
    {
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

    }
}
